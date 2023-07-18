#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэша с данными карточек типа <see cref="DefaultCardTypes.KrStageGroupTypeID"/> и компиляцию сценариев.
    /// </summary>
    public sealed class KrCompileStageGroupStoreExtension :
        KrCompileSourceStoreExtensionBase
    {
        #region Constructors

        /// <inheritdoc/>
        public KrCompileStageGroupStoreExtension(
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
            var compilationResult = await this.StageGroupCompilationCache.RebuildAsync(
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
                return this.StageGroupCompilationCache.InvalidateAsync(
                    new[] { card.ID },
                    cancellationToken);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override bool SourceChanged(
            Card card)
        {
            return card.TryGetSections() is { } sections
                && sections.TryGetValue(KrConstants.KrStageGroups.Name, out var sec)
                && sec.TryGetRawFields() is { } fields
                && (fields.ContainsKey(KrConstants.KrStageGroups.SourceAfter)
                    || fields.ContainsKey(KrConstants.KrStageGroups.SourceBefore)
                    || fields.ContainsKey(KrConstants.KrStageGroups.SourceCondition)
                    || fields.ContainsKey(KrConstants.KrStageGroups.RuntimeSourceAfter)
                    || fields.ContainsKey(KrConstants.KrStageGroups.RuntimeSourceBefore)
                    || fields.ContainsKey(KrConstants.KrStageGroups.RuntimeSourceCondition));
        }

        /// <inheritdoc />
        protected override bool CardChanged(
            Card card) => card.TryGetSections()?.Count > 0;

        #endregion
    }
}
