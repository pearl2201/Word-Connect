var User = function(id, username, score){
    this.id = id;
    this.username = username;
    this.score = score;
}

var Subject = function (nameSubject, listWord) {
    this.nameSubject = nameSubject;
    this.listWord = listWord;

    this.getWord = function () {
        r = Math.random(listWord.length)
        console.log(listWord[r])
        return listWord[r];
    }
}
var data4tu = ["abcd", "cdef"];
var list4tu = new Subject('list4tu', data4tu); 
list4tu.getWord()


