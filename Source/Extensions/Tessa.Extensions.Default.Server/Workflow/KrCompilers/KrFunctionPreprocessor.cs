using System;
using System.Text.RegularExpressions;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public class KrFunctionPreprocessor: IKrPreprocessor
    {
        #region constants

        // Директивы, которые необходимо искать в коде
        private const string ExpressionDirective = "expression";
        private const string ScriptDirective = "script";

        // Для регулярки
        private const string DirectiveGroup = "dir";
        private static readonly string regexStr =
            $@"^\s*#(?<{DirectiveGroup}>{ExpressionDirective}|{ScriptDirective})(;|\b)";

        #endregion

        #region fields

        private bool isStatement = true;

        #endregion

        #region private

        private string OnMatching(Match match)
        {
            if (match.Groups[DirectiveGroup].Value == ExpressionDirective)
            {
                this.isStatement = true;
            }
            else if (match.Groups[DirectiveGroup].Value == ScriptDirective)
            {
                this.isStatement = false;
            }
            return string.Empty;
        }

        private string ReplaceDirectives(string sourceText)
        {
            return Regex.Replace(
                sourceText,
                regexStr,
                this.OnMatching,
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Compiled);
        }

        #endregion

        #region implementation

        /// <summary>
        /// Выполнить предобработку исходного кода метода с возвращаемым значением
        /// Найти #statement/#script и обработать.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Preprocess(string source)
        {
            var newSource = this.ReplaceDirectives(source);
            return this.isStatement 
                ? $"return {Environment.NewLine}{newSource}{Environment.NewLine};" 
                : newSource;
        }

        #endregion
    }
}