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
var listIdUserActive = []
process.stdin.resume() // so the program will not close instantly

function exitHandler (options, err) {
  if (err) console.log(err.stack)
  else if (options.exit || option.cleanup) {
    process.exit()
  }
}

// do something when app is closing
process.on('exit', exitHandler.bind(null, { cleanup: true }))

// catches ctrl+c event
process.on('SIGINT', exitHandler.bind(null, { exit: true }))

// catches uncaught exceptions
process.on('uncaughtException', exitHandler.bind(null, { exit: true }))

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
  io.emit('question', {
    'question': currQuestion,
    'idAnswer': currIdAnswer.toString(), 'answer': currAnswer
  })

  setTimeout(function () { readLeaderboard() }, 18000)
}

function readLeaderboard () {
  db.getLeaderboard(sendAnswer)
}
function sendAnswer (boardUserRank) {
  // canculate wrong/corrrect/noattend

  var rankJson = createJsonBoardRankOnline(boardUserRank)
  io.emit('answer', {
    'answer': currAnswer,
    'idAnswer': currIdAnswer.toString(),
    'attend': countPlayerReceiveQuestion.toString(),
    'correct': countPlayerAnswerCorrect.toString(),
    'wrong': countPlayerAnswerWrong.toString(), 'leaderboard': rankJson
  })
  setTimeout(function () { sendQuestion() }, 7000)
}

function createJsonBoardRankOnline (boardUserRank) {
  var tmpUserRank = []

  for (i = 0; i < boardUserRank.length; i++) {
    if (listIdUserActive.indexOf(boardUserRank[i].userid) >= 0) {
      tmpUserRank.push({ 'userid': boardUserRank[i].userid,'score': boardUserRank[i].score, 'rank:': boardUserRank[i].rank })
    }
  }

  var strRank = JSON.stringify(tmpUserRank)

  console.log(strRank)

  return strRank
}


function createJsonBoardRank (boardUserRank) {
  var tmpUserRank = []

  for (i = 0; i < boardUserRank.length; i++) {
    
      tmpUserRank.push({ 'userid': boardUserRank[i].userid, 'rank:': boardUserRank[i].rank })
    
  }

  var strRank = JSON.stringify(tmpUserRank)

  console.log(strRank)

  return strRank
}
io.on('connection', function (socket) {
  var user = null

  socket.on('login', function (message) {
    var userid = message['userid']
    console.log(userid)
    db.login(userid, sendInfoUser, sendRequestRegis)
  })

  socket.on('regisaccount', function (message) {
    var userid = message['userid']
    var username = message['username']
    console.log('regis: ' + userid + ' - username' + username)
    db.regis(userid, username , sendInfoUser)
  }

  )

  socket.on('answer', function (message) {
    if (user != null) {
      var receiveAnswer = message['answer']

      if (receiveAnswer == currAnswer && message['id'] == currIdAnswer) {
        user.score++
        // save score
        countPlayerAnswerCorrect += 1
      } else if (message['answer'] != currAnswer && message['id'] == currIdAnswer) {
        user.score -= 10
        if (user.score < 0) {
          user.score = 0
        }

        countPlayerAnswerWrong++
      }
      db.updateUser(user.userid, user.score)
    } else {
      console.log('uswer == null')
    }
  })

  socket.on('receiveAnswer', function () {
    countPlayerReceiveQuestion++
  })

  socket.on('beep', function () {
    console.log('beep')
  })

  socket.on('leaderboard', function () {
    db.getLeaderboard2(sendLeaderboard)
  })

  function sendLeaderboard (boardUserRank) {
    console.log('leaderboard kkk')
    socket.emit('leaderboard', { 'leaderboard': createJsonBoardRank(boardUserRank) })
  }

  function sendRequestRegis () {
    console.log('emit request regis')
    socket.emit('requestregis')
  }

  function sendInfoUser (_user) {
    user = _user
    console.log('user login: ' + user.userid + ' score: ' + user.score + ' rank: ' + user.rank)
    listIdUserActive.push(user.userid)
    socket.emit('infouser', { 'username': user.username, 'score': user.score.toString(), 'rank': user.rank.toString() })
  }

  socket.on('exit', function () {
    var indexIdUser = listIdUserActive.indexOf(user.userid)
    if (indexIdUser >= 0) {
      listIdUserActive = listIdUserActive.splice(indexIdUser + 1, 1)
      console.log('before: ' + listIdUserActive)
    }
  })
})

var dictGame = new worddict()

server.listen(4568, function () {
  console.log('Server is now running...')
  dictGame.read(gameUpdate)
})
