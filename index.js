var fs = require('fs')

var server = require('http').createServer()
var io = require('socket.io')(server)
var dictGame = require('./worddict')

var players = []
var dictGame
var currAnswer = ''
var currIdAnswer = 0
var countPlayerReceiveQuestion = 0
var countPlayerAnswerCorrect = 0
var countPlayerAnswerWrong = 0
function Init () {
  gameUpdate()
}

function gameUpdate () {
  var date = new Date()
  var startTime = (date.getSeconds())
  var currTime = 0
  var sendAnswer = false
  currIdAnswer = 0
  sendQuestion()
}

function sendQuestion () {
  currAnswer = dictGame.getWord()
  currIdAnswer = currIdAnswer + 1
  countPlayerReceiveQuestion = 0
  countPlayerAnswerCorrect = 0
  countPlayerAnswerWrong = 0
  io.emit('question', {'question': currAnswer,'idAnswer': currIdAnswer})
  setTimeout(function () { sendAnswer() }, 3000)
}

function sendAnswer () {
  // canculate wrong/corrrect/noattend
  io.emit('answer', {'answer': currAnswer,'idAnswer': currIdAnswer, 'attend': countPlayerReceiveQuestion, 'correct': countPlayerAnswerCorrect, 'wrong': countPlayerAnswerWrong})
  setTimeout(function () { sendQuestion() }, 1000)
}
io.on('connection', function (socket) {
  console.log('has connect')
  var isAuth = false
  var score = 0
  var idUser = 0
  socket.on('login', function () {
    console.log('bee')
  // socket.emit('boop')
  })

  socket.on('regisaccount', function (username, password, useremail) {})

  socket.on('answer', function (answer, id) {
    if (answer == currAnswer && id == currIdAnswer) {
      score++
      // save score
      countPlayerAnswerCorrect += 1
    }else if (answer != currAnswer && id == currIdAnswer) {
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
  // socket.emit('boop')
  })
})

function CheckUserAccount (username, password) {
  if (username == 'username' && password == 'password') {
    return true
  }else {
    return false
  }
}

dictGame.read()
console.log('Server is now running...')
// io.attach(4568)

server.listen(4568, function () {
  gameUpdate()
})
