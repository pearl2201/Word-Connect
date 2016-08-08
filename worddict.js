module.exports = WordDict

function WordDict () {
  var dictWord = []
  var fileDict = fs.createReadStream('dict/dict.txt')
  
}

WordDict.prototype.read = function () {
  console.log('read')
  this.readLines(this.fileDict)
}

WordDict.prototype.readLines = function (input) {
  var remaining = ''

  input.on('data', function (data) {
    remaining += data
    var index = remaining.indexOf('\n')
    while (index > -1) {
      var line = remaining.substring(0, index)
      remaining = remaining.substring(index + 1)

      this.dictWord.push(line)

      index = remaining.indexOf('\n')
    }
  })

  input.on('end', function () {
    if (remaining.length > 0) {
      this.dictWord.push(remaining)
    }
  })
}

WordDict.prototype.getWord = function () {
  var r = Math.floor(Math.random() * (this.dictWord.length - 1))

  return this.dictWord[r]
}
