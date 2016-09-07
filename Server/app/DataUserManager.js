var sqlite3 = require('sqlite3').verbose()
var userModel = require('./User')

module.exports = DataUserManager

function DataUserManager () {
  console.log('create database')
  var db = new sqlite3.Database('db/user.db')
  this.createDatabase = function () {
    db.run('CREATE TABLE user (id INTEGER PRIMARY KEY AUTOINCREMENT, iduser text,username text ,score INTEGER)')
  }

  this.login = function (idUser, callback, callback2) {
    var user = null
    console.log('login: ' + idUser)
    db.get('select  p1.*, (select  count(*) from user as p2   where   p2.score > p1.score ) as rank from user as p1 where p1.iduser = ?', idUser, function (err, row) {
      console.log('wtf')
      if (err) {
        console.log(' not find user1')
      }else {
        if (typeof row != 'undefined') {
          console.log(' not find user2')
          user = new userModel(row.id, row.iduser , row.username, row.score, row.rank)
          callback(user)
        }else {
          console.log(' not find user')
          callback2()
        // return null
        }
      }
    })
  }

  this.regis = function (idUser, username, callback) {
    console.log(username)
    db.run('INSERT INTO user(iduser,username,score) VALUES (?,?,?)', [idUser, username, 0], function (err) {
      if (err) {
        throw err
      }else {
        var user = null
        console.log('insert success + idUser: ' + idUser)
        db.each('select  p1.*, (select  count(*) from user as p2   where   p2.score > p1.score ) as rank from user as p1 where p1.iduser = ?', [idUser], function (err, row) {
          if (err)
            throw err
          else {
            console.log('select success')
            user = new userModel(row.id, row.iduser , row.username, row.score, row.rank)
            if (user == null) {
            }else {
              console.log('call send info')
              callback(user)
            }
          }
        })
      }
    })
  }
  this.updateUser = function (userid, score) {
    db.run('UPDATE user SET score = ? WHERE idUser = ? ', score, userid)
  }

  this.getLeaderboard = function (callback) {
  
    var listUser = []
    var rank = 0
    db.each('SELECT * FROM user ORDER BY score DESC', function (err, row) {
      var user = new userModel(row.id, row.iduser, row.username, row.score, rank + 1)

      rank = rank + 1
      listUser.push(user)
    //  console.log('push user')
    }, function () {
     
      callback(listUser)
    })
  }

this.getLeaderboard2 = function (callback) {
    
    var listUser = []
    var rank = 0
    db.each('SELECT * FROM user ORDER BY score DESC LIMIT 50', function (err, row) {
      var user = new userModel(row.id, row.iduser, row.username, row.score, rank + 1)

      rank = rank + 1
      listUser.push(user)
    //  console.log('push user')
    }, function () {
      
      callback(listUser)
    })
  }
  this.getRankUser = function (userid) {
    var rank = 0
    db.each('select  p1.*, (select  count(*) from user as p2   where   p2.score > p1.score ) as rank from user as p1 where p1.iduser = ?', userid, function (err, row) {
      rank = row.rank
      return rank
    })
  }

  this.commitDataUser = function (listUser) {
    listUser.forEach(function (item, index) {
      db.each('UPDATE scores SET score = ? WHERE idUser= ? ', item.score, item.userid, function (err, wor) {})
    })
  // stmt.finalize()
  }
  this.close = function () {
    db.close()
  }
}
