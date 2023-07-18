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
    /// Расширение, выполняющее сброс кэша с данными карточек типа <see cref="DefaultCardTypes.KrStageTemplateTypeID"/> и компиляцию сценариев.
    /// </summary>
    public sealed class KrCompileStageTemplateStoreExtension :
        KrCompileSourceStoreExtensionBase
    {
        #region Constructors

        /// <inheritdoc/>
        public KrCompileStageTemplateStoreExtension(
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
            var compilationResult = await this.StageTemplateCompilationCache.RebuildAsync(
                cardID,
                cancellationToken: cancellationToken);

            return compilationResult.Result.ValidationResult;
        }

        /// <inheritdoc/>
        protected override Task InvalidateAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            if (card.StoreMode == CardStoreMode.Update)
            {
                return this.StageTemplateCompilationCache.InvalidateAsync(
                    new[] { card.ID },
                    cancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override bool SourceChanged(Card card)
        {
            return KrCompilersHelper.HasStageTemplateScriptsChanged(card)
                || KrCompilersHelper.HasStagesScriptsChanged(card)
                || card.TryGetInfo()?.TryGet<bool?>(KrConstants.Keys.ExtraSourcesChanged) == true;
        }

        /// <inheritdoc/>
        protected override bool CardChanged(Card card) =>
            card.TryGetSections()?.Count > 0;

        #endregion
    }
}
