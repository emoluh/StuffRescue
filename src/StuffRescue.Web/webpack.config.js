var webpack = require('webpack');
var path = require('path');

var BUILD_DIR = path.resolve(__dirname, 'wwwroot/js/public');
var APP_DIR = path.resolve(__dirname, 'wwwroot/js/Components');

var config = {
  entry: {
    bundle: APP_DIR + '/Nav.jsx',
    vendor: [ 'react', 'react-dom' ]
  },
  entry: APP_DIR + '/Nav.jsx',
  output: {
    path: BUILD_DIR,
    filename: 'bundle.js'
  },
  module : {
    loaders : [
      {
        test : /\.jsx?/,
        include : APP_DIR,
        loader : 'babel-loader'
      }
    ]
  }};

module.exports = config;