const path = require('path');
const webpack = require('webpack');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const TessaDllPlugin = require('./tools/tessaDllPlugin');

const outputPath = path.join(__dirname, 'wwwroot/extensions');
const publicPath = 'http://localhost:3000/';

module.exports = function (qwe, argv) {
  const WEBPACK_ENV = process.env.WEBPACK_ENV;
  const mode = WEBPACK_ENV ? WEBPACK_ENV : argv.mode;

  const params = {
    development: {
      devServer: {
        headers: {
          'Access-Control-Allow-Origin': '*'
        },
        port: 3000,
        hot: false,
        liveReload: true,
        client: {
          webSocketURL: 'ws://localhost:3000/ws'
        }
      },
      output: {
        filename: 'extensions.bundle.js',
        publicPath: publicPath,
        clean: true
      }
    },
    production: {
      output: {
        filename: '[name].[chunkhash].js',
        clean: true
      }
    }
  };

  return {
    ...params[mode],
    output: {
      path: outputPath,
      ...params[mode].output
    },
    devtool: 'source-map',
    context: path.join(__dirname),
    entry: {
      default: './src/default/index.ts',
      arigamix: './src/law/index.ts'
    },
    externals: [
      {
        react: 'React',
        'react-dom': 'ReactDom',
        classnames: 'classnames',
        mobx: 'mobx',
        'mobx-utils': 'mobxUtils',
        'mobx-react': 'mobxReact',
        moment: 'Moment',
        'styled-components': 'styledComponents',
        'react-dropzone': 'ReactDropZone',
        d3: 'd3',
        history: 'history',
        clipboard: 'Clipboard'
      },
      TessaDllPlugin.resolve({
        modules: ['tessa', 'ui', 'common', 'components'],
        extensions: ['.ts', '.tsx', '.js', '.jsx']
      })
    ],
    module: {
      rules: [
        {
          test: /.jsx?$/,
          loader: 'babel-loader',
          exclude: /node_modules/
        },
        {
          test: /\.tsx?$/,
          use: [
            {
              loader: 'babel-loader',
              options: {
                cacheCompression: false,
                cacheDirectory: true
              }
            },
            {
              loader: 'ts-loader',
              options: {
                transpileOnly: true
              }
            }
          ],
          exclude: /node_modules/
        },
        {
          test: /\.css$/,
          use: [
            {
              loader: 'style-loader',
              options: {
                insert: '#tessa-css-modules-root'
              }
            },
            {
              loader: 'css-loader',
              options: {
                esModule: false
              }
            }
          ]
        },
        {
          test: /\.scss$/,
          use: [
            {
              loader: 'style-loader',
              options: { insert: '#tessa-css-modules-root' }
            },
            {
              loader: 'css-loader',
              options: {
                esModule: false
              }
            },
            { loader: 'sass-loader' }
          ]
        }
      ]
    },
    plugins: [
      new TessaDllPlugin({
        manifestPath: path.join(__dirname, 'platform-api-manifest.json')
      }),
      new webpack.DefinePlugin({
        'process.env': {
          BUILD_TIME: JSON.stringify(Date.now())
        }
      }),
      new ForkTsCheckerWebpackPlugin({
        async: mode === 'development'
      })
    ],
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.jsx', '.css', '.scss'],
      modules: [path.resolve(__dirname), 'node_modules']
    },
    performance: {
      hints: false
    },
    stats: {
      errorDetails: true
    }
  };
};
