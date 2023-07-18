#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэша с данными карточек типа <see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/> и компиляцию сценариев.
    /// </summary>
    public sealed class KrCompileSecondaryProcessStoreExtension :
        KrCompileSourceStoreExtensionBase
    {
        #region Constructors

        /// <inheritdoc/>
        public KrCompileSecondaryProcessStoreExtension(
            IKrProcessCache processCache,
            IKrCommonMethodCompilationCache commonMethodCompilationCache,
            IKrStageTemplateCompilationCache stageTemplateCompilationCache,
            IKrStageGroupCompilationCache stageGroupCompilationCache,
            IKrSecondaryProcessCompilationCache secondaryProcessCompilationCache)
            : base(
                  processCache,
                  commonMethodCompilationCache,
                  stageTemplateCompilationCache,
                  stageGroupCompilationCache,
                  secondaryProcessCompilationCache)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override async Task<ValidationResult> BuildAsync(
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            var compilationResult = await this.SecondaryProcessCompilationCache.RebuildAsync(
                cardID,
                cancellationToken: cancellationToken);

            return compilationResult.Result.ValidationResult;
        }

        /// <inheritdoc/>
        protected override async Task InvalidateAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            if (card.StoreMode == CardStoreMode.Update)
            {
                var cardIDArr = new[] { card.ID };

                await this.SecondaryProcessCompilationCache.InvalidateAsync(
                    cardIDArr,
                    cancellationToken);

                await this.StageTemplateCompilationCache.InvalidateAsync(
                    cardIDArr,
                    cancellationToken);

                await this.StageGroupCompilationCache.InvalidateAsync(
                    cardIDArr,
                    cancellationToken);
            }
        }

        /// <inheritdoc/>
        protected override bool SourceChanged(Card card)
        {
            return HasSecondaryProcessScriptsChanged(card)
                || KrCompilersHelper.HasStagesScriptsChanged(card)
                || card.TryGetInfo()?.TryGet<bool?>(KrConstants.Keys.ExtraSourcesChanged) == true;
        }

        /// <inheritdoc />
        protected override bool CardChanged(
            Card card) => card.TryGetSections()?.Count > 0;

        #endregion

        #region Private Methods

        private static bool HasSecondaryProcessScriptsChanged(Card card)
        {
            return card.TryGetSections() is { } sections
                && sections.TryGetValue(KrConstants.KrSecondaryProcesses.Name, out var sec)
                && sec.TryGetRawFields() is { } fields
                && (fields.ContainsKey(KrConstants.KrSecondaryProcesses.VisibilitySourceCondition)
                || fields.ContainsKey(KrConstants.KrSecondaryProcesses.ExecutionSourceCondition))
                || KrCompilersHelper.HasStageTemplateScriptsChanged(card);
        }

        #endregion
    }
}
