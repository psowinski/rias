var path = require("path");
var webpack = require("webpack");
var nodeExternals = require('webpack-node-externals')

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

var babelOptions = {
  presets: ["@babel/env"],
  plugins: ["@babel/plugin-transform-modules-commonjs"]
};

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
  mode: isProduction ? 'production' : 'development',
  devtool: "source-map",
  entry: ['@babel/polyfill', resolve('./index.js')],
  target: 'node',
  externals: [nodeExternals()],
  output: {
    filename: 'bundle.js',
    path: resolve('./dist'),
    library: "lib",
    libraryTarget: "commonjs2"
  },
  resolve: {
    modules: [
      "node_modules", resolve("./node_modules/")
    ]
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        }
      }
    ]
  }
}
