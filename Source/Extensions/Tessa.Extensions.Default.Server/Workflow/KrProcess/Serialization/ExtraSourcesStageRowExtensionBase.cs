using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Базовый абстрактный класс расширения на сериализацию параметров этапов предоставляющий методы для получения и сохранения информации о дополнительных методах этапов.
    /// </summary>
    public abstract class ExtraSourcesStageRowExtensionBase : KrStageRowExtension
    {
        #region Fields

        private readonly IExtraSourceSerializer extraSourceSerializer;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExtraSourcesStageRowExtensionBase"/>.
        /// </summary>
        /// <param name="extraSourceSerializer">Сериализатор объектов содержащих информацию о дополнительных методах.</param>
        public ExtraSourcesStageRowExtensionBase(
            IExtraSourceSerializer extraSourceSerializer)
        {
            this.extraSourceSerializer = extraSourceSerializer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает сериализатор объектов содержащих информацию о дополнительных методах.
        /// </summary>
        protected IExtraSourceSerializer ExtraSourceSerializer { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Переносит информацию о сценарии этапа, текст тела которого расположен в поле <paramref name="scriptField"/>, в коллекцию содержащую информацию о дополнительных скриптах этапа.
        /// </summary>
        /// <param name="extraSources">Коллекция содержащая информацию о дополнительных скриптах этапа.</param>
        /// <param name="context">Контекст расширения на сериализацию этапов.</param>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <param name="descriptor">Дескриптор дополнительного метода этапа.</param>
        protected static void MoveSourceFromStageSettingsToExtraSources(
            IList<IExtraSource> extraSources,
            IKrStageRowExtensionContext context,
            CardRow row,
            KrExtraSourceDescriptor descriptor)
        {
            MoveSourceFromStageSettingsToExtraSources(
                extraSources,
                context,
                row,
                descriptor.ScriptField,
                descriptor.DisplayName,
                descriptor.MethodName,
                descriptor.ParameterType,
                descriptor.ParameterName,
                descriptor.ReturnType);
        }

        /// <summary>
        /// Переносит информацию о сценарии этапа, текст тела которого расположен в поле <paramref name="scriptField"/>, в коллекцию содержащую информацию о дополнительных скриптах этапа.
        /// </summary>
        /// <param name="extraSources">Коллекция содержащая информацию о дополнительных скриптах этапа.</param>
        /// <param name="context">Контекст расширения на сериализацию этапов.</param>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <param name="scriptField">Поле содержащее тело метода в параметрах этапа.</param>
        /// <param name="displayName">Отображаемое имя метода.</param>
        /// <param name="name">Имя метода.</param>
        /// <param name="parameterType">Имя типа параметра.</param>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="returnType">Имя типа возвращаемого значения.</param>
        protected static void MoveSourceFromStageSettingsToExtraSources(
            IList<IExtraSource> extraSources,
            IKrStageRowExtensionContext context,
            CardRow row,
            string scriptField,
            string displayName,
            string name,
            string parameterType,
            string parameterName,
            string returnType = SourceIdentifiers.Void)
        {
            if (!context.StageStorages.TryGetValue(row.RowID, out var settings)
                || !SourceChanged(context, row, scriptField))
            {
                return;
            }

            var newSrc = settings.TryGet<string>(scriptField);
            var source = new ExtraSource
            {
                DisplayName = displayName,
                Name = name,
                ReturnType = returnType,
                ParameterType = parameterType,
                ParameterName = parameterName,
                Source = newSrc,
            };

            settings[scriptField] = null;

            int index; // Порядковый индекс метода с именем name содержащийся в extraSources.
            for (index = 0; index < extraSources.Count; index++)
            {
                var item = extraSources[index];
                if (item.Name == name)
                {
                    break;
                }
            }

            if (index >= extraSources.Count)
            {
                extraSources.Add(source);
            }
            else
            {
                extraSources[index] = source;
            }
        }

        /// <summary>
        /// Переносит информацию о сценарии этапа, текст тела которого расположен в поле <see cref="KrConstants.KrStages.ExtraSources"/>, в параметры этапа по ключу <paramref name="scriptField"/>.
        /// </summary>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <param name="scriptField">Поле содержащее тело метода в параметрах этапа.</param>
        /// <param name="sourceName">Имя метода. Если не задано, то перенос выполняется только для первого сценария найденного в <see cref="KrConstants.KrStages.ExtraSources"/>.</param>
        protected static void MoveSourceFromExtraSourcesToStageSettings(
            IList<IExtraSource> extraSources,
            CardRow row,
            string scriptField,
            string sourceName = null)
        {
            IExtraSource source = null;
            if (sourceName is null
                && extraSources.Count > 0)
            {
                source = extraSources[0];
            }
            else if (sourceName is not null)
            {
                foreach (var s in extraSources)
                {
                    if (s.Name == sourceName)
                    {
                        source = s;
                        break;
                    }
                }
            }

            if (source is not null)
            {
                row[scriptField] = source.Source;
            }
        }

        /// <summary>
        /// Сохраняет заданную коллекцию содержащую информацию о дополнительных скриптах этапа в поле <see cref="KrConstants.KrStages.ExtraSources"/>.
        /// </summary>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <param name="extraSources">Коллекция содержащая информацию о дополнительных скриптах этапа.</param>
        protected void SetExtraSources(
            CardRow row,
            IList<IExtraSource> extraSources) =>
            row.Fields[KrConstants.KrStages.ExtraSources] = this.extraSourceSerializer.Serialize(extraSources);

        /// <summary>
        /// Возвращает коллекцию содержащую информацию о дополнительных скриптах этапа из поля <see cref="KrConstants.KrStages.ExtraSources"/>.
        /// </summary>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <returns>Коллекция содержащая информацию о дополнительных скриптах этапа.</returns>
        protected IList<IExtraSource> GetExtraSources(
            CardRow row)
        {
            var extraSourcesStr = row.Fields.TryGet<string>(KrConstants.KrStages.ExtraSources);
            return this.extraSourceSerializer.Deserialize(extraSourcesStr);
        }

        #endregion

        #region Private Methods

        protected static bool SourceChanged(
            IKrStageRowExtensionContext context,
            CardRow row,
            string scriptField)
        {
            return context.OuterCard.StoreMode == CardStoreMode.Insert
                || context.OuterCard.TryGetStagesSection(out var krStages, preferVirtual: true)
                && krStages.Rows.Any(p =>
                    p.RowID == row.RowID && p.ContainsKey(scriptField));
        }

        #endregion
    }
}
