var fs = require('fs');
path = require('path')
var arr= [];
function readLines(input, func) {
  var remaining = '';

  input.on('data', function(data) {
    remaining += data;
    var index = remaining.indexOf('\n');
    while (index > -1) {
      var line = remaining.substring(0, index);
      remaining = remaining.substring(index + 1);
      func(line);
      index = remaining.indexOf('\n');
    }
  });

  input.on('end', function() {
    if (remaining.length > 0) {
      func(remaining);
    }
    console.log("arr length: " + arr.length);
  for (i =0; i< arr.length; i++)
  {
       console.log("check" + arr[i]);
  } 
  });
    
  
  
}

function func(data) {

  arr.push(data);
  
  
}

var input = fs.createReadStream('dict/dict.txt');
readLines(input, func);
