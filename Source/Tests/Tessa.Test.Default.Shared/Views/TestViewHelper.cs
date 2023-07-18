using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    /// Вспомогательные методы для работы с представлениями используемые в тестах.
    /// </summary>
    public static class TestViewHelper
    {
        #region Public Methods

        /// <summary>
        /// Выполняет чтение моделей представлений из встроенных ресурсов расположенных в указанной сборке по заданному пути.
        /// </summary>
        /// <param name="assembly">Сборка в которой выполняется поиск представлений.</param>
        /// <param name="interpreter">Интерпретатор текста формата обмена.</param>
        /// <param name="indentationStrategy">Стратегия выравнивание текста.</param>
        /// <param name="jsonViewModelImporter">Объект для импорта представлений.</param>
        /// <param name="jsonViewModelAdapter">Адаптер представлений.</param>
        /// <param name="directory">Путь, относительный к <see cref="ResourcesPaths.Views"/>, по которому выполнятся загрузка представлений. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, тогда загрузка будет выполнена из <see cref="ResourcesPaths.Views"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Список моделей представлений.</returns>
        public static async ValueTask<List<TessaViewModel>> ReadViewsAsync(
            Assembly assembly,
            IExchangeFormatInterpreter interpreter,
            IIndentationStrategy indentationStrategy,
            IJsonViewModelImporter jsonViewModelImporter,
            IJsonViewModelConverter jsonViewModelAdapter,
            string directory = default,
            CancellationToken cancellationToken = default)
        {
            const string jsonViewExtension = "jview";
            const string xmlViewExtension = "view";
            const string jsonViewExtensionWithDot = "." + jsonViewExtension;
            const string xmlViewExtensionWithDot = "." + xmlViewExtension;
            const string viewExtensionPattern = xmlViewExtension + "|" + jsonViewExtension;

            Check.ArgumentNotNull(assembly, nameof(assembly));
            Check.ArgumentNotNull(interpreter, nameof(interpreter));
            Check.ArgumentNotNull(indentationStrategy, nameof(indentationStrategy));
            Check.ArgumentNotNull(jsonViewModelImporter, nameof(jsonViewModelImporter));
            Check.ArgumentNotNull(jsonViewModelAdapter, nameof(jsonViewModelAdapter));

            var result = new List<TessaViewModel>();

            var basePath = Path.Join(ResourcesPaths.Resources, ResourcesPaths.Views, directory);
            var filePaths = AssemblyHelper.GetFileNameEnumerableFromEmbeddedResources(
                assembly,
                basePath,
                viewExtensionPattern);

            foreach (var filePath in filePaths)
            {
                await using var stream = assembly.GetResourceStream(filePath.FullName);
                var fileExtension = Path.GetExtension(filePath.Name);

                if (string.Equals(fileExtension, xmlViewExtensionWithDot, StringComparison.OrdinalIgnoreCase))
                {
                    var context = await interpreter.InterpretAsync(stream, indentationStrategy, cancellationToken: cancellationToken);
                    result.AddRange(context.GetViews());
                }
                else if (string.Equals(fileExtension, jsonViewExtensionWithDot, StringComparison.OrdinalIgnoreCase))
                {
                    var jsonViewModel = await jsonViewModelImporter.ImportAsync(stream, cancellationToken);
                    result.Add(jsonViewModelAdapter.ConvertToTessaViewModel(jsonViewModel));
                }
            }

            return result;
        }

        #endregion
    }
}
