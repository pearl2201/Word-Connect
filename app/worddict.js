var fs = require('fs')

module.exports = WordDict

function WordDict () {
  var dictWord = []
  var fileDict = fs.createReadStream('./../dict/dict.txt')

  this.read = function (callback) {
    console.log('read')

    this.readLines(fileDict,callback)
  }

  this.readLines = function (input,callback) {
    var remaining = ''

    input.on('data', function (data) {
      remaining += data
      var index = remaining.indexOf('\n')
      while (index > -1) {
        var line = remaining.substring(0, index)
        remaining = remaining.substring(index + 1)

        dictWord.push(line)

        index = remaining.indexOf('\n')
      }
    })

    input.on('end', function () {
      if (remaining.length > 0) {
        dictWord.push(remaining)
        console.log('finish')
        callback()
      }
    })
  }

  /**
 * choose a random word  from dict
 *
 * @return {string} word
 */
  this.getWord = function () {
    var r = Math.floor(Math.random() * (dictWord.length))

    return dictWord[r]
  }


  this.createQuestion = function(answer)
  {
    
    var a = answer.split(""),
        n = a.length;

    for(var i = n - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        var tmp = a[i];
        a[i] = a[j];
        a[j] = tmp;
    }
    return a.join("");
  }
}
