#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Базовый класс расширения, выполняющего сброс кэша с данными карточек подсистемы маршрутов и компиляцию сценариев.
    /// </summary>
    public abstract class KrCompileSourceStoreExtensionBase :
        CardStoreExtension
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="ProcessCache" path="/summary"/></param>
        /// <param name="commonMethodCompilationCache"><inheritdoc cref="CommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="stageTemplateCompilationCache"><inheritdoc cref="StageTemplateCompilationCache" path="/summary"/></param>
        /// <param name="stageGroupCompilationCache"><inheritdoc cref="StageGroupCompilationCache" path="/summary"/></param>
        /// <param name="secondaryProcessCompilationCache"><inheritdoc cref="SecondaryProcessCompilationCache" path="/summary"/></param>
        protected KrCompileSourceStoreExtensionBase(
            IKrProcessCache processCache,
            IKrCommonMethodCompilationCache commonMethodCompilationCache,
            IKrStageTemplateCompilationCache stageTemplateCompilationCache,
            IKrStageGroupCompilationCache stageGroupCompilationCache,
            IKrSecondaryProcessCompilationCache secondaryProcessCompilationCache)
        {
            this.ProcessCache = NotNullOrThrow(processCache);
            this.CommonMethodCompilationCache = NotNullOrThrow(commonMethodCompilationCache);
            this.StageTemplateCompilationCache = NotNullOrThrow(stageTemplateCompilationCache);
            this.StageGroupCompilationCache = NotNullOrThrow(stageGroupCompilationCache);
            this.SecondaryProcessCompilationCache = NotNullOrThrow(secondaryProcessCompilationCache);
        }

        #endregion

        #region Properties

        /// <inheritdoc cref="IKrProcessCache"/>
        protected IKrProcessCache ProcessCache { get; }

        /// <inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/>
        protected IKrCommonMethodCompilationCache CommonMethodCompilationCache { get; }

        /// <inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/>
        protected IKrStageTemplateCompilationCache StageTemplateCompilationCache { get; }

        /// <inheritdoc cref="IKrStageGroupCompilationCache" path="/summary"/>
        protected IKrStageGroupCompilationCache StageGroupCompilationCache { get; }

        /// <inheritdoc cref="IKrSecondaryProcessCompilationCache" path="/summary"/>
        protected IKrSecondaryProcessCompilationCache SecondaryProcessCompilationCache { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Возвращает значение, показывающее возможна ли компиляция сценариев или нет.
        /// </summary>
        /// <param name="context"><inheritdoc cref="ICardStoreExtensionContext" path="/summary"/></param>
        /// <returns>Значение, показывающее возможна ли компиляция сценариев или нет.</returns>
        protected virtual bool CanBuild(
            ICardStoreExtensionContext context) => true;

        /// <summary>
        /// Выполняет компиляцию сценариев.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns><inheritdoc cref="ValidationResult" path="/summary"/></returns>
        protected abstract Task<ValidationResult> BuildAsync(
            Guid cardID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет инвалидацию кэша компиляции сценариев.
        /// </summary>
        /// <param name="card">Сохраняемая карточка.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected abstract Task InvalidateAsync(
            Card card,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает значение, показывающее наличие изменений в сценариях.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <returns>Значение, показывающее наличие изменений в сценариях.</returns>
        protected abstract bool SourceChanged(Card card);

        /// <summary>
        /// Возвращает значение, показывающее наличие изменений в карточке.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <returns>Значение, показывающее наличие изменений в карточке.</returns>
        protected abstract bool CardChanged(Card card);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var card = context.Request.Card;

            if (card.StoreMode == CardStoreMode.Insert
                || this.CardChanged(card)
                || this.SourceChanged(card))
            {
                await this.ProcessCache.InvalidateAsync(
                    context.CancellationToken);
            }

            if (this.SourceChanged(card))
            {
                await this.InvalidateAsync(
                    card,
                    context.CancellationToken);
            }

            var info = context.Request.Info;

            if (info.Count == 0
                || !this.CanBuild(context))
            {
                return;
            }

            if (info.ContainsKey(KrConstants.Keys.CompileWithValidationResult))
            {
                var compilationResult = await this.BuildAsync(
                    card.ID,
                    context.CancellationToken);

                await this.FillValidationResultAsync(
                    compilationResult,
                    context.ValidationResult,
                    context.CancellationToken);
            }
            else if (info.ContainsKey(KrConstants.Keys.CompileAllWithValidationResult))
            {
                var compilationResult = await this.RebuildAllAsync(
                    context.CancellationToken);

                await this.FillValidationResultAsync(
                    compilationResult,
                    context.ValidationResult,
                    context.CancellationToken);
            }
        }

        #endregion

        #region Private Methods

        private async ValueTask FillValidationResultAsync(
            ValidationResult compilationResult,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken)
        {
            validationResult.AddInfo(
                this,
                compilationResult.IsSuccessful ?
                    await LocalizationManager.GetStringAsync(
                        "KrMessages_KrStageSourceSuccessfulBuild",
                        cancellationToken) :
                    await LocalizationManager.GetStringAsync(
                        "KrMessages_KrStageSourceFailedBuild",
                        cancellationToken));

            validationResult.Add(compilationResult);
        }

        private async Task<ValidationResult> RebuildAllAsync(
            CancellationToken cancellationToken)
        {
            await this.ProcessCache.InvalidateAsync(
                cancellationToken);

            var commonMethodsResults = await this.CommonMethodCompilationCache.RebuildAllAsync(
                cancellationToken);

            var stageTemplateCompilationResults = await this.StageTemplateCompilationCache.RebuildAllAsync(
                cancellationToken);

            var stageGroupCompilationResults = await this.StageGroupCompilationCache.RebuildAllAsync(
                cancellationToken);

            var secondaryProcessCompilationResults = await this.SecondaryProcessCompilationCache.RebuildAllAsync(
                cancellationToken);

            var validationResult = new ValidationResultBuilder();
            MergeResults(commonMethodsResults, validationResult);
            MergeResults(stageTemplateCompilationResults, validationResult);
            MergeResults(stageGroupCompilationResults, validationResult);
            MergeResults(secondaryProcessCompilationResults, validationResult);

            return validationResult.Build();
        }

        private static void MergeResults(
            IList<ITessaCompilationObject<string, IKrScript>> compilationResults,
            IValidationResultBuilder validationResult)
        {
            foreach (var compilationResult in compilationResults)
            {
                var result = compilationResult.Result.ValidationResult;

                if (result.Items.Count > 0)
                {
                    validationResult.Add(result);
                }
            }
        }

        #endregion
    }
}
