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
    /// Расширение, выполняющее сброс кэша с данными карточек типа <see cref="DefaultCardTypes.KrStageCommonMethodTypeID"/> и компиляцию сценариев.
    /// </summary>
    public sealed class KrCompileCommonMethodStoreExtension :
        KrCompileSourceStoreExtensionBase
    {
        #region Constructors

        /// <inheritdoc/>
        public KrCompileCommonMethodStoreExtension(
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

        /// <inheritdoc/>
        protected override async Task<ValidationResult> BuildAsync(
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            var compilationResult = await this.CommonMethodCompilationCache.RebuildAsync(
                cardID,
                cancellationToken: cancellationToken);

            return compilationResult.Result.ValidationResult;
        }

        /// <inheritdoc/>
        protected override Task InvalidateAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            // Сбрасываем кэш при любом изменении или создании карточки.
            // От него зависят другие кэши и успешность компиляции методов расширений оказывает на них влияние.
            return this.CommonMethodCompilationCache.InvalidateAsync(
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        protected override bool SourceChanged(Card card)
        {
            return card.TryGetSections() is { } sections
                && sections.TryGetValue(KrConstants.KrStageCommonMethods.Name, out var section)
                && section.TryGetRawFields() is { } fields
                && fields.ContainsKey(KrConstants.KrStageCommonMethods.Source);
        }

        /// <inheritdoc/>
        protected override bool CardChanged(Card card) =>
            card
                .TryGetSections()
                ?.ContainsKey(KrConstants.KrStageCommonMethods.Name) == true;

        #endregion
    }
}
