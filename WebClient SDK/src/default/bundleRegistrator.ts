import { ExtensionContainer } from 'tessa/extensions';
ExtensionContainer.instance.registerBundle({
  name: 'Arigamix.Extensions.Default.js',
  buildTime: process.env.BUILD_TIME!
});
