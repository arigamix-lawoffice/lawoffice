using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение на процесс сохранения карточки, выполняющее пересчёт маршрута по требованию.
    /// </summary>
    public sealed class KrRecalcStagesStoreExtension :
        CardStoreExtension
    {
        #region Constants

        private const string UnknownLiteral = "unknown";

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;
        private readonly IKrExecutor executor;
        private readonly IKrScope krScope;
        private readonly IObjectModelMapper mapper;
        private readonly IKrProcessCache processCache;

        private bool? hasChangesInfo;

        private IList<RouteDiff> diffsInfo;

        #endregion

        #region Constructors

        public KrRecalcStagesStoreExtension(
            IKrTypesCache typesCache,
            [Unity.Dependency(KrExecutorNames.CacheExecutor)] IKrExecutor executor,
            IKrScope krScope,
            IObjectModelMapper mapper,
            IKrProcessCache processCache)
        {
            this.typesCache = NotNullOrThrow(typesCache);
            this.executor = NotNullOrThrow(executor);
            this.krScope = NotNullOrThrow(krScope);
            this.mapper = NotNullOrThrow(mapper);
            this.processCache = NotNullOrThrow(processCache);
        }

        #endregion

        #region Private Methods

        private static string FormatActualName(RouteDiff diff) =>
            $"\"{LocalizationManager.Localize(diff.ActualName ?? diff.OldName ?? UnknownLiteral)}\"";

        private static string FormatOldName(RouteDiff diff)
        {
            if (!string.Equals(diff.ActualName, diff.OldName, StringComparison.Ordinal))
            {
                var renamedFrom = LocalizationManager.GetString("KrCompilation_RouteElementRenamedFrom");
                return $"({renamedFrom} \"{LocalizationManager.Localize(diff.OldName)}\")";
            }

            return string.Empty;
        }

        private static string FormatHidden(RouteDiff diff) =>
            diff.HiddenStage
                ? $" ({LocalizationManager.GetString("KrCompilation_RecalcChanges_HiddenStage")})"
                : string.Empty;

        private void ChangesToResponse(
            InfoAboutChanges infoAboutChanges,
            IList<RouteDiff> diffs,
            IValidationResultBuilder validationResult)
        {
            if (infoAboutChanges == InfoAboutChanges.None)
            {
                return;
            }

            if (infoAboutChanges.HasNot(InfoAboutChanges.ChangesInHiddenStages))
            {
                diffs = diffs
                    .Where(static p => !p.HiddenStage)
                    .ToList();
            }

            var hasChanges = diffs.Count > 0;

            if (infoAboutChanges.Has(InfoAboutChanges.HasChangesToInfo))
            {
                this.hasChangesInfo = hasChanges;
            }

            if (hasChanges)
            {
                if (infoAboutChanges.Has(InfoAboutChanges.HasChangesToValidationResult)
                    && infoAboutChanges.HasNot(InfoAboutChanges.ChangesListToValidationResult))
                {
                    ValidationSequence
                        .Begin(validationResult)
                        .Warning(DefaultValidationKeys.RecalcWithChanges)
                        .End();
                }
            }
            else
            {
                if (infoAboutChanges.HasAny(InfoAboutChanges.ToValidationResult))
                {
                    ValidationSequence
                        .Begin(validationResult)
                        .Warning(DefaultValidationKeys.RecalcWithoutChanges)
                        .End();
                }
            }

            if (infoAboutChanges.Has(InfoAboutChanges.ChangesListToInfo))
            {
                this.diffsInfo = diffs;
            }

            if (infoAboutChanges.Has(InfoAboutChanges.ChangesListToValidationResult))
            {
                this.ChangesListToValidationResult(diffs, validationResult);
            }
        }

        private void ChangesListToValidationResult(
            IList<RouteDiff> diffs,
            IValidationResultBuilder validationResult)
        {
            foreach (var diff in diffs)
            {
                var validator = ValidationSequence
                    .Begin(validationResult)
                    .SetObjectName(this);

                switch (diff.Action)
                {
                    case RouteDiffAction.Insert:
                        validator.Warning(DefaultValidationKeys.StageAdded, FormatActualName(diff), FormatHidden(diff));
                        break;
                    case RouteDiffAction.Delete:
                        validator.Warning(DefaultValidationKeys.StageDeleted, FormatActualName(diff), FormatHidden(diff));
                        break;
                    case RouteDiffAction.Modify:
                        validator.Warning(DefaultValidationKeys.StageModified, FormatActualName(diff), FormatOldName(diff), FormatHidden(diff));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(diff) + "." + nameof(diff.Action), diff.Action, null);
                }

                validator.End();
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task BeforeRequest(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !context.Request.GetRecalcFlag())
            {
                return Task.CompletedTask;
            }

            context.Request.ForceTransaction = true;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !context.Request.GetRecalcFlag())
            {
                return;
            }

            if (!await KrProcessHelper.CardSupportsRoutesAsync(
                context.Request.Card,
                context.DbScope,
                this.typesCache,
                context.CancellationToken))
            {
                return;
            }

            var satellite = await this.krScope.GetKrSatelliteAsync(
                context.Request.Card.ID,
                cancellationToken: context.CancellationToken);

            if (satellite is null)
            {
                return;
            }

            if (satellite.IsMainProcessStarted())
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .Error(DefaultValidationKeys.MainProcessStarted)
                    .End();
                return;
            }

            var card = context.Request.Card;
            var (templates, stages) = await this.processCache.GetRelatedTemplatesAsync(
                satellite,
                cancellationToken: context.CancellationToken);
            var pci = this.mapper.GetMainProcessCommonInfo(satellite);

            NestedStagesCleaner.ClearAll(satellite);

            var objectModel = await this.mapper.CardRowsToObjectModelAsync(
                satellite,
                pci,
                pci,
                templates,
                stages,
                KrConstants.KrProcessName,
                initialStage: true,
                cancellationToken: context.CancellationToken);

            this.mapper.FillWorkflowProcessFromPci(
                objectModel,
                pci,
                pci);

            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                context.DbScope,
                context.CancellationToken);

            var components = await KrComponentsHelper.GetKrComponentsAsync(
                card.TypeID,
                docTypeID,
                this.typesCache,
                context.CancellationToken);

            await using var mainCardAccessStrategy = new KrScopeMainCardAccessStrategy(card.ID, this.krScope);

            var ctx = new KrExecutionContext(
                cardContext: context,
                mainCardAccessStrategy: mainCardAccessStrategy,
                cardID: card.ID,
                cardType: context.CardType,
                docTypeID: docTypeID,
                krComponents: components,
                workflowProcess: objectModel,
                cancellationToken: context.CancellationToken);

            var result = await this.executor.ExecuteAsync(ctx);
            context.ValidationResult.Add(result.Result);

            if (result.Result.HasErrors)
            {
                return;
            }

            var diffs = await this.mapper.ObjectModelToCardRowsAsync(
                ctx.WorkflowProcess,
                satellite,
                null,
                context.CancellationToken);

            this.mapper.ObjectModelToPci(
                ctx.WorkflowProcess,
                pci,
                pci,
                pci);

            var mainCard = await mainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

            if (mainCard is null)
            {
                return;
            }

            await this.mapper.SetMainProcessCommonInfoAsync(
                mainCard,
                satellite,
                pci,
                context.CancellationToken);

            if (context.ValidationResult.IsSuccessful())
            {
                // Информация о скрытых этапах отображается, если они отображаются в карточке.
                var mode = context.Request.GetInfoAboutChanges() ??
                    (card.StoreMode == CardStoreMode.Update
                    && card.GetStagePositions()?.All(static i => i.ShiftedOrder.HasValue) == true
                    ? InfoAboutChanges.ChangesListToValidationResult | InfoAboutChanges.ChangesInHiddenStages
                    : InfoAboutChanges.ChangesListToValidationResult);

                this.ChangesToResponse(
                    mode,
                    diffs,
                    context.ValidationResult);
            }
        }

        /// <inheritdoc/>
        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (this.hasChangesInfo.HasValue)
            {
                context.Response.SetHasRecalcChanges(this.hasChangesInfo.Value);
            }

            if (this.diffsInfo is not null)
            {
                context.Response.SetRecalcChanges(this.diffsInfo);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
