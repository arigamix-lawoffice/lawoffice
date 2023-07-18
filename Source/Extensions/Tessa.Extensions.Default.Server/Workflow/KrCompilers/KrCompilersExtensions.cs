#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NLog;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public static class KrCompilersExtensions
    {
        #region Constants And Static Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region ITessaCompilationObject<string, IKrScript> Extensions

        /// <summary>
        /// Создаёт экземпляр объекта <see cref="IKrScript"/> с именем <paramref name="objectName"/>.
        /// </summary>
        /// <param name="tessaCompilationObject"><inheritdoc cref="ITessaCompilationObject{TKey, TInstance}" path="/summary"/></param>
        /// <param name="objectName">Имя получаемого класса.</param>
        /// <param name="validationResult"><inheritdoc cref="IValidationResultBuilder" path="/summary"/></param>
        /// <param name="ignoreNotExistsObject">Значение <see langword="true"/>, если ошибка отсутствия объекта должна быть проигнорирована, иначе - <see langword="false"/>.</param>
        /// <returns>Созданный экземпляр объекта <see cref="IKrScript"/> или значение <see langword="null"/>, если произошла ошибка.</returns>
        public static IKrScript? TryCreateKrScriptInstance(
            this ITessaCompilationObject<string, IKrScript> tessaCompilationObject,
            string objectName,
            IValidationResultBuilder validationResult,
            bool ignoreNotExistsObject)
        {
            ThrowIfNull(tessaCompilationObject);
            ThrowIfNull(objectName);
            ThrowIfNull(validationResult);

            if (!tessaCompilationObject.Result.ValidationResult.IsSuccessful)
            {
                ValidationSequence
                    .Begin(validationResult)
                    .SetObjectName(tessaCompilationObject.Result)
                    .ErrorDetails(
                        "$KrProcess_AssemblyMissed",
                        tessaCompilationObject.Result.RawOutput)
                    .End();

                validationResult.Add(tessaCompilationObject.Result.ValidationResult);

                logger.LogResult(validationResult);

                return null;
            }

            if (tessaCompilationObject.Factory.TryGet(objectName) is { } script)
            {
                return script;
            }

            if (!ignoreNotExistsObject)
            {
                validationResult.AddError(
                nameof(KrCompilersExtensions),
                "$KrProcess_ClassMissed",
                objectName);

                validationResult.Add(tessaCompilationObject.Result.ValidationResult);

                logger.LogResult(validationResult);
            }

            return null;
        }

        #endregion

        #region object model to string

        public static string ToStringSummary(this Stage stage, int spaces = 0)
        {
            ThrowIfNull(stage);

            var sb = new StringBuilder();
            sb.Append(Indent(spaces));
            sb.Append(LocalizationManager.Localize(stage.StageTypeCaption));
            sb.Append(' ');
            sb.Append(stage.Name);
            sb.Append('(');
            if (stage.BasedOnTemplate)
            {
                sb.Append(LocalizationManager.GetString("CardTypes_TypesNames_KrStageTemplate"));
                sb.Append(" \"");
                sb.Append(stage.TemplateName);
                sb.Append("\", ");
            }

            sb.Append(LocalizationManager.GetString("CardTypes_TypesNames_KrStageGroup"));
            sb.Append(" \"");
            sb.Append(stage.StageGroupName);
            sb.Append("\")");
            return sb.ToString();
        }

        public static string ToStringDetailed(this Stage stage, int spaces = 0)
        {
            ThrowIfNull(stage);

            var stringBuilder = new StringBuilder();
            var indent = Indent(spaces);
            var properties = typeof(Stage)
                .GetProperties()
                .Where(p => p.Name is not nameof(Stage.Performers)
                    and not nameof(Stage.Info)
                    and not nameof(Stage.Settings))
                .OrderBy(p => p.Name);
            foreach (var property in properties)
            {
                AppendProperty(property, stage, stringBuilder, indent);
            }

            stringBuilder
                .Append(indent)
                .AppendLine("]");

            return stringBuilder.ToString();
        }

        public static string ToStringSummary(this Performer performer, int spaces = 0)
        {
            ThrowIfNull(performer);

            return $"{Indent(spaces)}{performer.PerformerID} {LocalizationManager.Localize(performer.PerformerName)}";
        }

        public static string ToStringDetailed(this Performer performer, int spaces = 0)
        {
            ThrowIfNull(performer);

            var stringBuilder = new StringBuilder();
            var indent = Indent(spaces);

            var properties = typeof(Performer)
                .GetProperties()
                .OrderBy(p => p.Name);
            foreach (var property in properties)
            {
                AppendProperty(property, performer, stringBuilder, indent);
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Private Methods

        private static string Indent(int spaces) =>
            new string(' ', spaces);

        private static void AppendProperty(
            PropertyInfo property,
            object obj,
            StringBuilder sb,
            string indent)
        {
            string strValue;
            var value = property.GetValue(obj, null);
            if (value is Dictionary<string, object?> valueDict)
            {
                strValue = StorageHelper.Print(valueDict);
            }
            else
            {
                strValue = property.GetValue(obj, null)?.ToString() ?? "null";
                strValue = LocalizationManager.Localize(strValue);
            }

            sb
                .Append(indent)
                .Append(property.Name)
                .Append(" = ")
                .AppendLine(strValue);
        }

        #endregion
    }
}
