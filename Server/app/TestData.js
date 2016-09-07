var sqlite3 = require('sqlite3').verbose()
var userModel = require('./User')
var db = new sqlite3.Database('db/user.db')
var idUser = '122373'
var username = 'anh1ngoc2'

function cb(user)
{
  console.log('tada: ' + user.username )
}

function regis(idUser, username, callback) {
  console.log(idUser)
    db.run('INSERT INTO user(iduser,username,score) VALUES (?,?,?)', [idUser, username, 0], function (err) {
      if (err) {
        throw err
      }else {
        
        
        db.each('select  p1.*, (select  count(*) from user as p2   where   p2.score > p1.score ) as rank from user as p1 where p1.iduser = ?', [idUser], function (err, row) {
          if (err)
            throw err
          else {
            
            var user = new userModel(row.id, row.userid , row.username, row.score, row.rank)
            if (user == null) {
              
            }else {
              
              callback(user)
            }
          }
        })
      }
    })
  }

  regis('23231', 'nmnmnm', cb)