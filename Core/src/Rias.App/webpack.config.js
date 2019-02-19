var input = "./Rias.App.fsproj"
var output = "../../dist"

var path = require("path");
var webpack = require("webpack");

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
  entry: resolve(input),
  output: {
    filename: 'bundle.js',
    path: resolve(output),
    library: "lib",
    libraryTarget: "commonjs2"
  },
  resolve: {
    modules: [
      "node_modules", resolve("./node_modules/")
    ]
  },
  devServer: {
    contentBase: resolve('./public'), //http://localhost:8085/
    port: 8085
  },
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: {
            babel: babelOptions,
            define: isProduction ? [] : ["DEBUG"]
          }
        }
      },
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
