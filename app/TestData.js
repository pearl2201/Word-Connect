var sqlite3 = require('sqlite3').verbose()
var userModel = require('./User')
var db = new sqlite3.Database('db/user.db')
var idUser = '123'
var username = 'anhngoc1'
db.run('INSERT INTO user(iduser,username,score) VALUES (?,?,?)', [idUser, username, 30], function (err) {
  if (err)
    throw err
  else {
    var user = null
    db.each('select  p1.*, (select  count(*) from user as p2   where   p2.score > p1.score ) as rank from user as p1 where p1.iduser = ?', idUser, function (err, row) {
      console.log(row)
      user = new userModel(row.id, row.userid , row.username, row.score, row.rank)
      //
      if (user == null) {
        console.log('regis fail')
      }
    })
  }
})
