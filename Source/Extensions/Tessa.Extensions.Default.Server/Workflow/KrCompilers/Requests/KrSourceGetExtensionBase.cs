#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Базовый класс расширения, выполняющего заполнение виртуальных секций результата
    /// компиляции для карточек, содержащих шаблоны маршрутов.
    /// </summary>
    public abstract class KrSourceGetExtensionBase :
        CardGetExtension
    {
        #region Fields

        private readonly IKrCompilationCacheBase currentCompilationCache;
        private readonly IKrCommonMethodCompilationCache krCommonMethodCompilationCache;
        private readonly IKrStageTemplateCompilationCache krStageTemplateCompilationCache;
        private readonly IKrStageGroupCompilationCache krStageGroupCompilationCache;
        private readonly IKrSecondaryProcessCompilationCache krSecondaryProcessCompilationCache;
        private readonly IKrProcessCache krProcessCache;
        private readonly ICardMetadata cardMetadata;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="currentCompilationCache">Кэш, содержащий результаты компиляции текущего объекта.</param>
        /// <param name="krCommonMethodCompilationCache"><inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="krStageTemplateCompilationCache"><inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/></param>
        /// <param name="krStageGroupCompilationCache"><inheritdoc cref="IKrStageGroupCompilationCache" path="/summary"/></param>
        /// <param name="krSecondaryProcessCompilationCache"><inheritdoc cref="IKrSecondaryProcessCompilationCache" path="/summary"/></param>
        /// <param name="krProcessCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="cardMetadata"><inheritdoc cref="ICardMetadata" path="/summary"/></param>
        protected KrSourceGetExtensionBase(
            IKrCompilationCacheBase currentCompilationCache,
            IKrCommonMethodCompilationCache krCommonMethodCompilationCache,
            IKrStageTemplateCompilationCache krStageTemplateCompilationCache,
            IKrStageGroupCompilationCache krStageGroupCompilationCache,
            IKrSecondaryProcessCompilationCache krSecondaryProcessCompilationCache,
            IKrProcessCache krProcessCache,
            ICardMetadata cardMetadata)
        {
            this.currentCompilationCache = NotNullOrThrow(currentCompilationCache);
            this.krCommonMethodCompilationCache = NotNullOrThrow(krCommonMethodCompilationCache);
            this.krStageTemplateCompilationCache = NotNullOrThrow(krStageTemplateCompilationCache);
            this.krStageGroupCompilationCache = NotNullOrThrow(krStageGroupCompilationCache);
            this.krSecondaryProcessCompilationCache = NotNullOrThrow(krSecondaryProcessCompilationCache);
            this.krProcessCache = NotNullOrThrow(krProcessCache);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardGetExtensionContext context)
        {
            if (context.Response!.TryGetCard() is not { } card)
            {
                return;
            }

            await this.FillLocalBuildOutputAsync(
                card,
                context.CancellationToken);

            await this.FillGlobalBuildOutputAsync(
                card,
                context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async ValueTask FillLocalBuildOutputAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            if (card.TryGetSections() is not { } sections
                || !sections.TryGetValue(KrConstants.KrBuildLocalOutputVirtual.Name, out var stageBuildLocalOutputSection))
            {
                return;
            }

            var compilationResult = await this.currentCompilationCache.TryGetAlreadyCompiledAsync(
                card.ID,
                cancellationToken);

            stageBuildLocalOutputSection.RawFields[KrConstants.KrBuildLocalOutputVirtual.Output] = GetOutputString(
                compilationResult?.Result.ValidationResult);
        }

        private async ValueTask FillGlobalBuildOutputAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            if (card.TryGetSections() is not { } sections
                || !sections.TryGetValue(KrConstants.KrBuildGlobalOutputVirtual.Name, out var stageBuildGlobalOutputSection))
            {
                return;
            }

            var commonMethods = await this.krProcessCache.GetAllCommonMethodsAsync(
                cancellationToken);
            var templates = await this.krProcessCache.GetAllStageTemplatesAsync(
                cancellationToken);
            var groups = await this.krProcessCache.GetAllStageGroupsAsync(
                cancellationToken);
            var actions = await this.krProcessCache.GetAllActionsAsync(
                cancellationToken);
            var buttons = await this.krProcessCache.GetAllButtonsAsync(
                cancellationToken);
            var pureProcesses = await this.krProcessCache.GetAllPureProcessesAsync(
                cancellationToken);
            var rows = stageBuildGlobalOutputSection.Rows;

            await this.AddRowsAsync(
                commonMethods,
                this.krCommonMethodCompilationCache,
                DefaultCardTypes.KrStageCommonMethodTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);

            await this.AddRowsAsync(
                templates.Values.Where(i =>
                    !actions.ContainsKey(i.ID)
                    && !buttons.ContainsKey(i.ID)
                    && !pureProcesses.ContainsKey(i.ID)),
                this.krStageTemplateCompilationCache,
                DefaultCardTypes.KrStageTemplateTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);

            await this.AddRowsAsync(
                groups.Values.Where(static i => i.SecondaryProcessID != i.ID),
                this.krStageGroupCompilationCache,
                DefaultCardTypes.KrStageGroupTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);

            await this.AddRowsAsync(
                actions.Values,
                this.krSecondaryProcessCompilationCache,
                DefaultCardTypes.KrSecondaryProcessTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);

            await this.AddRowsAsync(
                buttons.Values,
                this.krSecondaryProcessCompilationCache,
                DefaultCardTypes.KrSecondaryProcessTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);

            await this.AddRowsAsync(
                pureProcesses.Values,
                this.krSecondaryProcessCompilationCache,
                DefaultCardTypes.KrSecondaryProcessTypeCaption,
                static i => i.ID,
                static i => i.Name,
                rows,
                cancellationToken);
        }

        private async ValueTask AddRowsAsync<T>(
            IEnumerable<T> values,
            IKrCompilationCacheBase compilationCache,
            string typeCaption,
            Func<T, Guid> getIDFunc,
            Func<T, string> getNameFunc,
            ListStorage<CardRow> rows,
            CancellationToken cancellationToken = default)
        {
            foreach (var value in values)
            {
                var id = getIDFunc(value);

                var compilationResult = await compilationCache.TryGetAlreadyCompiledAsync(
                    id,
                    cancellationToken);

                var newRow = rows.Add();
                newRow.RowID = Guid.NewGuid();
                newRow[KrConstants.KrBuildGlobalOutputVirtual.ObjectID] = id;
                newRow[KrConstants.KrBuildGlobalOutputVirtual.ObjectName] = getNameFunc(value);
                newRow[KrConstants.KrBuildGlobalOutputVirtual.ObjectTypeCaption] = typeCaption;
                newRow[KrConstants.KrBuildGlobalOutputVirtual.CompilationDateTime] = compilationResult?.Result.CompilationDateTime;

                var buildState = compilationResult?.Result.ValidationResult.IsSuccessful switch
                {
                    null => KrBuildStates.None,
                    true => KrBuildStates.Success,
                    false => KrBuildStates.Error
                };

                newRow[KrConstants.KrBuildGlobalOutputVirtual.StateID] = Int32Boxes.Box((int) buildState);
                newRow[KrConstants.KrBuildGlobalOutputVirtual.StateName] = await this.GetBuildStateNameAsync(
                    buildState,
                    cancellationToken);

                newRow[KrConstants.KrBuildGlobalOutputVirtual.Output] = GetOutputString(
                    compilationResult?.Result.ValidationResult);
            }
        }

        private async ValueTask<string> GetBuildStateNameAsync(
            KrBuildStates state,
            CancellationToken cancellationToken = default)
        {
            (await this.cardMetadata.GetEnumerationsAsync(cancellationToken)).TryGetValue(KrConstants.KrBuildStates.Name, out var krBuildStatesSection);

            return (string?) krBuildStatesSection?.Records.FirstOrDefault(i =>
                (KrBuildStates) (int) i[KrConstants.KrBuildStates.ID]! == state)
                    ?[KrConstants.KrBuildStates.NameField]
                ?? state.ToString();
        }

        private static string? GetOutputString(
            ValidationResult? validationResult)
        {
            if (validationResult?.Items.Count > 0 != true)
            {
                return null;
            }

            var builder = StringBuilderHelper.Acquire();
            foreach (var item in validationResult.Items)
            {
                if (builder.Length > 0)
                {
                    builder.AppendLine();
                }

                builder.AppendLine(item.ToString(ValidationLevel.Detailed));
            }

            return builder.ToStringAndRelease().Trim();
        }

        #endregion
    }
}
