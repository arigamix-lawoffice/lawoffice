using System.Text.RegularExpressions;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public class KrProcedurePreprocessor: IKrPreprocessor
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

        #region private
        
        private static string ReplaceDirectives(string sourceText)
        {
            return Regex.Replace(
                sourceText,
                regexStr,
                m => string.Empty,
                RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Compiled);
        }

        #endregion

        #region implementation

        /// <summary>
        /// Выполнить предобработку исходного кода метода без возвращаемого значения
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Preprocess(string source)
        {
            return ReplaceDirectives(source);
        }

        #endregion
    }
}