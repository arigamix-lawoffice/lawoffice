module.exports = {
  presets: ['@babel/preset-react', ['@babel/preset-env', { modules: false }]],
  plugins: [
    '@babel/plugin-proposal-object-rest-spread',
    '@babel/plugin-proposal-class-properties',
    '@babel/plugin-transform-object-assign',
    '@babel/plugin-syntax-dynamic-import',
    '@babel/plugin-proposal-async-generator-functions'
  ],
  env: {
    test: {
      presets: ['@babel/preset-react', ['@babel/preset-env', { modules: 'cjs' }]],
      plugins: [
        '@babel/plugin-proposal-object-rest-spread',
        '@babel/plugin-proposal-class-properties',
        '@babel/plugin-transform-object-assign',
        '@babel/plugin-syntax-dynamic-import',
        '@babel/plugin-proposal-async-generator-functions'
      ]
    }
  }
};
