using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    /// <summary>
    /// Возвращает сжатую карточку для визуализации резолюций Workflow.
    /// Карточка содержит все задания резолюций с их секциями (без календаря),
    /// а также все записи в истории заданий, относящиеся к заданиям резолюций.
    /// Не содержит секций карточки, файлов и прочих заданий.
    /// </summary>
    /// <remarks>
    /// Расширение должно быть зарегистрировано по типу
    /// <see cref="Shared.DefaultRequestTypes.GetResolutionVisualizationData"/>.
    /// </remarks>
    public sealed class WfGetResolutionVisualizationDataRequestExtension :
        CardRequestExtension
    {
        #region Constructors

        public WfGetResolutionVisualizationDataRequestExtension(
            ICardTransactionStrategy transactionStrategy,
            ICardGetStrategy getStrategy,
            ILicenseManager licenseManager)
        {
            this.transactionStrategy = transactionStrategy ?? throw new ArgumentNullException(nameof(transactionStrategy));
            this.getStrategy = getStrategy ?? throw new ArgumentNullException(nameof(getStrategy));
            this.licenseManager = licenseManager ?? throw new ArgumentNullException(nameof(licenseManager));
        }

        #endregion

        #region Fields

        private readonly ICardTransactionStrategy transactionStrategy;

        private readonly ICardGetStrategy getStrategy;

        private readonly ILicenseManager licenseManager;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            Guid? cardID;
            if (!context.RequestIsSuccessful
                || !(cardID = context.Request.CardID).HasValue)
            {
                return;
            }

            ILicense license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
            if (!LicensingHelper.CheckWorkflowViewerLicense(license, out string licenseErrorMessage))
            {
                context.ValidationResult.AddError(this, licenseErrorMessage);
                return;
            }

            Card loadedCard = null;
            bool success = await this.transactionStrategy.ExecuteInReaderLockAsync(
                cardID.Value,
                context.ValidationResult,
                async p =>
                {
                    var db = p.DbScope.Db;
                    var builderFactory = p.DbScope.BuilderFactory;

                    CardGetContext getContext = await this.getStrategy
                        .TryLoadCardInstanceAsync(
                            cardID.Value,
                            db,
                            context.CardMetadata,
                            p.ValidationResult,
                            cancellationToken: p.CancellationToken);

                    if (getContext == null)
                    {
                        p.ReportError = true;
                        return;
                    }

                    // загружаем только задания резолюций
                    var tasksByRowID = new Dictionary<Guid, CardTask>();
                    IList<CardGetContext> taskContextList = await this.getStrategy
                        .TryLoadTaskInstancesAsync(
                            getContext.CardID,
                            getContext.Card,
                            getContext.Db,
                            context.CardMetadata,
                            p.ValidationResult,
                            context.Session,
                            CardNewMode.Default,
                            CardGetTaskMode.All,
                            loadCalendarInfo: false,
                            tasksByRowID: tasksByRowID,
                            taskTypeIDList: WfHelper.ResolutionTaskTypeIDList,
                            cancellationToken: p.CancellationToken);

                    // загружаем секции для всех этих заданий
                    if (taskContextList != null && taskContextList.Count > 0)
                    {
                        foreach (CardGetContext taskContext in taskContextList)
                        {
                            if (!await this.getStrategy.LoadSectionsAsync(taskContext, p.CancellationToken))
                            {
                                p.ReportError = true;
                                return;
                            }
                        }
                    }

                    // загружаем только записи в истории, относящиеся к резолюциям
                    bool historyLoaded = await this.getStrategy
                        .LoadTaskHistoryAsync(
                            getContext.CardID,
                            getContext.Card,
                            getContext.Db,
                            context.CardMetadata,
                            p.ValidationResult,
                            tasksByRowID,
                            itemTypeIDList: WfHelper.ResolutionTaskTypeIDList,
                            cancellationToken: p.CancellationToken);

                    if (!historyLoaded)
                    {
                        p.ReportError = true;
                        return;
                    }

                    // загружаем дополнительную информацию к этим записям
                    ListStorage<CardTaskHistoryItem> taskHistory = getContext.Card.TryGetTaskHistory();
                    if (taskHistory != null && taskHistory.Count > 0)
                    {
                        var historyItemsByRowID = new Dictionary<Guid, CardTaskHistoryItem>();
                        foreach (CardTaskHistoryItem historyItem in taskHistory)
                        {
                            historyItemsByRowID.Add(historyItem.RowID, historyItem);
                        }

                        if (historyItemsByRowID.Count > 0)
                        {
                            await WfHelper.LoadHistoryWorkflowInfoAsync(
                                cardID.Value,
                                historyItemsByRowID,
                                db,
                                builderFactory,
                                loadCalendarInfo: true,
                                cancellationToken: context.CancellationToken);
                        }
                    }

                    loadedCard = getContext.Card;
                },
                context.CancellationToken);

            if (success && loadedCard != null)
            {
                WfHelper.SetResponseCard(context.Response, loadedCard);
            }
        }

        #endregion
    }
}
