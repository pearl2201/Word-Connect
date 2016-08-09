var server = require('http').createServer()
var io = require('socket.io')(server)
var worddict = require('./worddict')
var DataUserManager = require('./DataUserManager')
var players = []
var dictGame
var currQuestion = ''
var currAnswer = ''
var currIdAnswer = 0
var countPlayerReceiveQuestion = 0
var countPlayerAnswerCorrect = 0
var countPlayerAnswerWrong = 0
var db = new DataUserManager()

process.stdin.resume() // so the program will not close instantly

function exitHandler (options, err) {
  if (err) console.log(err.stack)
  else if (options.exit || option.cleanup) {
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
  // db.createDatabase()
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
  'idAnswer': currIdAnswer.toString(), 'answer': currAnswer})

  setTimeout(function () { sendAnswer() }, 3000)
}

function sendAnswer () {
  // canculate wrong/corrrect/noattend

  var rankJson = createJsonBoardRank(db.getLeaderboard())
  io.emit('answer', {'answer': currAnswer,
    'idAnswer': currIdAnswer.toString(),
    'attend': countPlayerReceiveQuestion,
    'correct': countPlayerAnswerCorrect,
  'wrong': countPlayerAnswerWrong,'leaderboard': rankJson})
  setTimeout(function () { sendQuestion() }, 1000)
}

function createJsonBoardRank (boardUserRank) {
  var strRank = ''
  return strRank
}

io.on('connection', function (socket) {
  var user = null
  socket.on('login', function (idUser) {
    user = db.login(idUser)
    sendInfoUser()
  })

  socket.on('regisaccount', function (userid, username) {
    user = db.regis(userid, username)
    sendInfoUser()
  }

  )

  socket.on('answer', function (answer, id) {
    if (answer == currQuestion && id == currIdAnswer) {
      user.score++
      // save score
      countPlayerAnswerCorrect += 1
    }else if (answer != currQuestion && id == currIdAnswer) {
      user.score -= 10
      if (user.score < 0) {
        user.score = 0
      }
      countPlayerAnswerWrong++
    }
  })

  socket.on('receiveAnswer', function () {
    countPlayerReceiveQuestion++
  })

  socket.on('beep', function () {
    console.log('beep')
  })

  socket.on('leaderboard', function () {
    socket.emit('leaderboard', {'leaderboard': createJsonBoardRank(db.getLeaderboard())})
  })

  function sendInfoUser () {
    if (user != null)
      socket.emit('infouser', {'username': user.username,'score': user.score,'rank': user.rank})
  }
})

var dictGame = new worddict()

server.listen(4568, function () {
  console.log('Server is now running...')
  dictGame.read(gameUpdate)
})
