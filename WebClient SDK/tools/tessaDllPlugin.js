let tessaApiManifestData = {};

class TessaDllPlugin {
  constructor(options) {
    this.options = options;
  }

  apply(compiler) {
    compiler.hooks.beforeCompile.tapAsync('TessaDllPlugin', (context, callback) => {
      compiler.inputFileSystem.readFile(this.options.manifestPath, (err, result) => {
        if (err) return callback(err);
        try {
          tessaApiManifestData = JSON.parse(result.toString('utf-8'));
        } catch (e) {
          return callback(e);
        }
        return callback();
      });
    });
  }
}

TessaDllPlugin.resolve = function (options) {
  const modules = options.modules;
  const extensions = [''];
  for (const ext of options.extensions) {
    extensions.push(ext);
    extensions.push(`/index${ext}`);
  }

  return function ({ request }, callback) {
    if (request && modules.some(x => x === request || request.startsWith(`${x}/`))) {
      for (let i = 0; i < extensions.length; i++) {
        const innerRequest = './' + request;
        const extension = extensions[i];
        const requestPlusExt = innerRequest + extension;
        const resolved = tessaApiManifestData[requestPlusExt];
        if (resolved != undefined) {
          return callback(null, `var tessa.apiLoader(${resolved})`);
        }
      }
    }
    return callback();
  };
};

module.exports = TessaDllPlugin;
