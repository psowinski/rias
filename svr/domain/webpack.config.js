/* eslint-disable */
var path = require('path');
var nodeExternals = require('webpack-node-externals');
var cleanPlugin = require('clean-webpack-plugin');

function resolve(filePath) {
  return path.join(__dirname, filePath);
}

var isProduction = process.argv.indexOf('-p') >= 0;
console.log(
  'Bundling for ' + (isProduction ? 'production' : 'development') + '...'
);

module.exports = env => {
  return {
    mode: isProduction ? 'production' : 'development',
    devtool: 'source-map',
    entry: resolve('./src/index.js'),
    target: 'node',
    externals: [nodeExternals()],
    output: {
      filename: 'bundle.js',
      path: resolve('./dist'),
      library: 'lib',
      libraryTarget: 'commonjs2'
    },
    plugins: [new cleanPlugin()],
    resolve: {
      modules: ['node_modules', resolve('./node_modules/')],
      extensions: ['*', '.mjs', '.js', '.json']
    },
    module: {
      rules: [
        {
          test: /\.m?js$/,
          type: 'javascript/auto',
          exclude: /node_modules/,
          use: {
            loader: 'babel-loader',
            options: { presets: ['@babel/env'] }
          }
        }
      ]
    }
  };
};
