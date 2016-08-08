var server = require('http').createServer()
var io = require('socket.io')(server)
var worddict = require('./worddict')
var dataUserManager = require('./DataUserManager')
var players = []
var dictGame
var currQuestion = ''
var currAnswer = ''
var currIdAnswer = 0
var countPlayerReceiveQuestion = 0
var countPlayerAnswerCorrect = 0
var countPlayerAnswerWrong = 0
var db = new dataUserManager()
var listUser = []
var boardRank = []
process.stdin.resume() // so the program will not close instantly

function exitHandler (options, err) {
  if (err) console.log(err.stack)
  if (options.exit || option.cleanup) {
    db.close()
    process.exit()
  }
}

// do something when app is closing
process.on('exit', exitHandler.bind(null, {cleanup: true}))

// catches ctrl+c event
process.on('SIGINT', exitHandler.bind(null, {exit: true}))

// catches uncaught exceptions
process.on('uncaughtException', exitHandler.bind(null, {exit: true}))

function gameUpdate () {
  currIdAnswer = 0
  listUser = db.getLeaderboard()
  console.log('start send question')
  sendQuestion()
}

function sendQuestion () {
  currAnswer = dictGame.getWord()
  currQuestion = dictGame.createQuestion(currAnswer)
  console.log(currQuestion + ' - ' + currAnswer)
  currIdAnswer = currIdAnswer + 1
  countPlayerReceiveQuestion = 0
  countPlayerAnswerCorrect = 0
  countPlayerAnswerWrong = 0
  io.emit('question', {'question': currQuestion,
  'idAnswer': currIdAnswer})

  setTimeout(function () { sendAnswer() }, 3000)
}

function sendAnswer () {
  // canculate wrong/corrrect/noattend
  updateBoardRank();
  var rankJson = createJsonBoardRank()
  io.emit('answer', {'answer': currAnswer,
    'idAnswer': currIdAnswer.toString(),
    'attend': countPlayerReceiveQuestion,
    'correct': countPlayerAnswerCorrect,
  'wrong': countPlayerAnswerWrong,'leaderboard':rankJson})
  setTimeout(function () { sendQuestion() }, 1000)
}

function createJsonBoardRank()
{
  var strRank = ''
  return strRank
}

function updateBoardRank () {
  boardRank.cleanup()
  boardRank = listUser.slice()
  var tmp = []
  for (i = 0; i < boardRank.length; i++) {
    if (tmp.indexOf(boardRank[i].score) != -1)
      tmp.push(boardRank[i].score)
  }
  var t = null
  for (i = 0; i < tmp.length; i++) {
    for (j = tmp.length - 1; j > i; j++) {
      if (tmp[j] < tmp[j - 1]) {
        t = tmp[j]
        tmp[j] = tmp[j - 1]
        tmp[j - 1] = t
      }
    }
  }

  boardRank.forEach(function (item, index) {
    item.rank = tmp.indexOf(item.score)
  })
}

// sort on values
function srt (desc) {
  return function (a, b) {
    return desc ? ~~(a < b) : ~~(a > b)
  }
}

// sort on key values
function keysrt (key, desc) {
  return function (a, b) {
    return desc ? ~~(a[key] < b[key]) : ~~(a[key] > b[key])
  }
}

io.on('connection', function (socket) {
  console.log('has connect')
  socket.on('login', function () {
    console.log('bee')
  // socket.emit('boop')
  })

  socket.on('regisaccount', function (username, password, useremail) {})

  socket.on('answer', function (answer, id) {
    if (answer == currQuestion && id == currIdAnswer) {
      score++
      // save score
      countPlayerAnswerCorrect += 1
    }else if (answer != currQuestion && id == currIdAnswer) {
      score -= 10
      if (score < 0) {
        score = 0
      }
      countPlayerAnswerWrong++
    }
  })

  socket.on('receiveAnswer', function () {
    countPlayerReceiveQuestion++
  })

  socket.on('beep', function () {
    console.log('beep')
    checkBeep()
  })
})

function checkBeep () {
  console.log('check beep')
}
var dictGame = new worddict()

server.listen(4568, function () {
  console.log('Server is now running...')
  dictGame.read(gameUpdate)
})
