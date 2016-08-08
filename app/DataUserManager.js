var sqlite3 = require('sqlite3').verbose()
var userModel = require('./User')
/**
var check
db.serialize(function() {

db.run("CREATE TABLE if not exists user_info (info TEXT)")
var stmt = db.prepare("INSERT INTO user_info VALUES (?)")
for (var i = 0; i < 10; i++) {
    stmt.run("Ipsum " + i)
}
stmt.finalize()

})
})

db.close()
**/
module.exports = DataUserManager

function DataUserManager () {
  var db = new sqlite3.Database('./db/word.db')

  this.createDatabase = function () {}

  this.login = function (idUser) {
    var user = new userModel(0, 'pearl', 2, 0)
    return user
  }

  this.regis = function (idUser, username) {
    var user = new userModel(0, 'pearl', 2, 0)
    return user
  }
  this.updateUser = function (userid, score) {}

  this.getLeaderboard = function () {
    var listUser = []
    db.each('SELECT rowid AS id, info FROM user_info', function (err, row) {
      console.log(row.id + ': ' + row.info)
    })
    return listUser
  }

  this.getRankUser = function () {
    var rank = 0
    db.each('SELECT rowid AS id, info FROM user_info', function (err, row) {
      console.log(row.id + ': ' + row.info)
    })
    return rank
  }

  this.commitDataUser = function()
  {

  }
  this.close = function () {
    db.close()
  }
}
