using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение проверяет, изменилась ли строка с этапом согласования в карточке.
    /// Если изменилась, делает соответствующую отметку в поле <see cref="KrConstants.KrStages.RowChanged"/> и <see cref="KrConstants.KrStages.OrderChanged"/>.
    /// </summary>
    public sealed class KrCheckStageRowModifiedStoreExtension :
        CardStoreExtension
    {
        #region Constants And Static Fields

        private const string ChangedOrderInfoKey = nameof(ChangedOrderInfoKey);

        private const string ChangedRowInfoKey = nameof(ChangedRowInfoKey);

        /// <summary>
        /// Поля в строке этапа, игнорируемые при отслеживании изменений.
        /// </summary>
        private static readonly HashSet<string> serviceFields = new HashSet<string>(StringComparer.Ordinal)
        {
            KrConstants.Keys.ParentStageRowID,
            CardRow.SystemStateKey,
            CardRow.SystemChangedKey,
            KrConstants.KrStages.RowID,
            KrConstants.KrStages.Order,
            KrConstants.KrStages.BasedOnStageRowID,
            KrConstants.KrStages.BasedOnStageTemplateID,
            KrConstants.KrStages.OrderChanged,
            KrConstants.KrStages.RowChanged,
            KrConstants.KrStages.StateID,
            KrConstants.KrStages.StateName,
            KrConstants.KrStages.DisplayTimeLimit,
            KrConstants.KrStages.DisplayParticipants,
            KrConstants.KrStages.DisplaySettings,
            KrConstants.KrStages.Skip,
            KrConstants.KrStages.OriginalOrder,
            KrConstants.Keys.NestedStage,
            KrConstants.Keys.RootStage,
        };

        #endregion

        #region Fields

        private readonly IKrStageSerializer serializer;

        private readonly IKrTypesCache typesCache;

        private readonly IKrScope scope;

        private readonly ISignatureProvider signatureProvider;

        private readonly IKrProcessCache krProcessCache;

        #endregion

        #region Constructors

        public KrCheckStageRowModifiedStoreExtension(
            IKrStageSerializer serializer,
            IKrTypesCache typesCache,
            IKrScope scope,
            ISignatureProvider signatureProvider,
            IKrProcessCache krProcessCache)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.scope = scope ?? throw new ArgumentNullException(nameof(scope));
            this.signatureProvider = signatureProvider ?? throw new ArgumentNullException(nameof(signatureProvider));
            this.krProcessCache = krProcessCache ?? throw new ArgumentNullException(nameof(krProcessCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            Card card;
            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) is null)
            {
                return;
            }

            var krComponents = await KrComponentsHelper.GetKrComponentsAsync(
                card,
                this.typesCache,
                context.CancellationToken);

            if (krComponents.HasNot(KrComponents.Routes)
                || !this.HasAnySettingsSection(card))
            {
                return;
            }

            var satellite = await this.scope.TryGetKrSatelliteAsync(
                card.ID,
                cancellationToken: context.CancellationToken);

            if (satellite?.TryGetStagesSection(out var satelliteStagesSection) != true)
            {
                return;
            }

            var changedRows = new HashSet<Guid>();
            var changedOrders = new HashSet<Guid>();
            if (card.TryGetStagesSection(out var mainCardStages))
            {
                CheckOrderSections(mainCardStages, satelliteStagesSection, changedOrders);
                CheckPlainRowChanges(mainCardStages, changedRows);
            }

            this.CheckChildRowsChanges(card, changedRows);
            context.Info[ChangedOrderInfoKey] = changedOrders;
            context.Info[ChangedRowInfoKey] = changedRows;
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !context.Info.TryGetValue(ChangedRowInfoKey, out var crObj)
                || crObj is not HashSet<Guid> changedRows
                || !context.Info.TryGetValue(ChangedOrderInfoKey, out var coObj)
                || coObj is not HashSet<Guid> changedOrders)
            {
                return;
            }

            var card = context.Request.Card;
            var satellite = await this.scope.GetKrSatelliteAsync(card.ID, cancellationToken: context.CancellationToken);
            if (satellite?.TryGetStagesSection(out var satelliteStagesSection) != true)
            {
                return;
            }

            var signatures = StageRowMigrationHelper.GetSignatures(card.Info);
            var orders = StageRowMigrationHelper.GetOrders(card.Info);

            foreach (var row in satelliteStagesSection.Rows)
            {
                if (row.State == CardRowState.Inserted)
                {
                    StageRowMigrationHelper.VerifyRow(
                        row,
                        signatures,
                        orders,
                        out var rowChanged,
                        out var orderChaged,
                        this.serializer,
                        this.signatureProvider);

                    if (rowChanged)
                    {
                        await this.SetRowChangedAsync(
                            row,
                            context.ValidationResult,
                            context.CancellationToken);

                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }

                    if (orderChaged)
                    {
                        await this.SetOrderChangedAsync(
                            row,
                            context.ValidationResult,
                            context.CancellationToken);

                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (changedRows.Contains(row.RowID))
                    {
                        await this.SetRowChangedAsync(
                            row,
                            context.ValidationResult,
                            context.CancellationToken);

                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }

                    if (changedOrders.Contains(row.RowID))
                    {
                        await this.SetOrderChangedAsync(
                            row,
                            context.ValidationResult,
                            context.CancellationToken);

                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return;
                        }
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private static void CheckOrderSections(
            CardSection mainStagesSection,
            CardSection satelliteStagesSection,
            HashSet<Guid> changedOrders)
        {
            // 1. Сортировка сохранённых в сателлите этапов.
            var originalStages = satelliteStagesSection
                .Rows
                .Where(static p => p.ContainsKey(KrConstants.KrStages.Order))
                .OrderBy(static p => p.Get<int>(KrConstants.KrStages.Order))
                .ToArray();

            var originalLength = originalStages.Length;

            // 2. Сортировка изменённых этапов (все этапы кроме удаленных).
            var changedStages = mainStagesSection
                .Rows
                .Where(static i =>
                    i.State is CardRowState.Modified or CardRowState.Inserted
                    && i.ContainsKey(KrConstants.KrStages.Order))
                .OrderBy(static i => i.Get<int>(KrConstants.KrStages.Order))
                .ToArray();

            if (changedStages.Length == 0)
            {
                return;
            }

            var changedStagesModified = changedStages
                .Where(static i => i.State == CardRowState.Modified)
                .ToArray();

            if (changedStagesModified.Length > 0)
            {
                // 3. Будем вычеркивать исходные этапы, которые отсутствуют в измененном наборе.
                var crossedOutItems = new bool[originalLength];

                // 4. Спроецировать измененные на исходные - получаем измененные, но в исходном порядке.
                for (var originalStageIDIndex = 0;
                    originalStageIDIndex < originalLength;
                    originalStageIDIndex++)
                {
                    var hasStage = changedStagesModified
                        .Any(i => i.RowID == originalStages[originalStageIDIndex].RowID);

                    if (!hasStage)
                    {
                        // Для простоты лишние элементы не удаляются, а лишь помечаются.
                        crossedOutItems[originalStageIDIndex] = true;
                    }
                }

                // 5. Проходимся попарно по массивам - несовпадающие пары свидетельствуют об изменении порядка.
                var changedStageIndex = 0;
                for (var originalStageIDIndex = 0;
                    originalStageIDIndex < originalLength;
                    originalStageIDIndex++)
                {
                    // Этапа нет среди изменённых?
                    if (crossedOutItems[originalStageIDIndex])
                    {
                        continue;
                    }

                    var changedStageRowID = changedStagesModified[changedStageIndex].RowID;
                    changedStageIndex++;

                    if (originalStages[originalStageIDIndex].RowID != changedStageRowID)
                    {
                        changedOrders.Add(changedStageRowID);
                    }
                }
            }

            // 6. Определение изменения положения этапов из-за изменения положения несохранённых вручную добавленных этапов.
            foreach (var row in mainStagesSection
                        .Rows
                        .Where(static p =>
                            p.State == CardRowState.Inserted
                            && !p.Fields.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageRowID).HasValue))
            {
                var order = row.TryGet<int?>(KrConstants.KrStages.Order);
                var originalOrder = row.TryGet<int?>(KrConstants.KrStages.OriginalOrder);

                if (order.HasValue
                    && originalOrder.HasValue
                    && order != originalOrder)
                {
                    int startOrder;
                    int endOrder;

                    if (order < originalOrder)
                    {
                        startOrder = order.Value;
                        endOrder = originalOrder.Value;
                    }
                    else
                    {
                        startOrder = originalOrder.Value;
                        endOrder = order.Value;
                    }

                    foreach (var changedStage in changedStages)
                    {
                        var changedStageOrder = changedStage.Get<int>(KrConstants.KrStages.Order);

                        if (changedStageOrder < startOrder)
                        {
                            continue;
                        }

                        changedOrders.Add(changedStage.RowID);

                        if (changedStageOrder == endOrder)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private static void CheckPlainRowChanges(
            CardSection mainStageSection,
            HashSet<Guid> changedStages)
        {
            // Определяем изменения для секции KrStages
            foreach (var modifiedStage in mainStageSection.Rows)
            {
                if (modifiedStage.State != CardRowState.Modified
                    || modifiedStage.Fields.Keys.All(serviceFields.Contains))
                {
                    continue;
                }

                changedStages.Add(modifiedStage.RowID);
            }
        }

        private void CheckChildRowsChanges(
            Card mainCard,
            HashSet<Guid> changedStages)
        {
            foreach (var settingsSectionName in this.serializer.SettingsSectionNames)
            {
                if (settingsSectionName == KrConstants.KrStages.Virtual)
                {
                    continue;
                }

                ListStorage<CardRow> rows;
                if (mainCard.Sections.TryGetValue(settingsSectionName, out var settingsSec)
                    && (rows = settingsSec.TryGetRows()) is not null)
                {
                    foreach (var row in rows)
                    {
                        var parentID = row.TryGet<Guid?>(KrConstants.Keys.ParentStageRowID);
                        if (parentID.HasValue)
                        {
                            changedStages.Add(parentID.Value);
                        }
                    }
                }
            }
        }

        private bool HasAnySettingsSection(
            Card mainCard)
        {
            var settingSectionNames = this.serializer.SettingsSectionNames;
            var mainCardSections = mainCard.Sections;
            // Там есть KrStagesVirtual
            return settingSectionNames.Any(settingSectionName => mainCardSections.ContainsKey(settingSectionName));
        }

        private async ValueTask SetRowChangedAsync(
            CardRow row,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var basedOnStageTemplateID = row.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageTemplateID);

            if (basedOnStageTemplateID.HasValue
                && !await this.GetIsStageReadonlyAsync(basedOnStageTemplateID.Value, cancellationToken))
            {
                validationResult.AddError(
                    this,
                    "$KrProcess_ChangingStageIsProhibited",
                    row.TryGet<string>(KrConstants.KrStages.NameField));
                return;
            }

            row.Fields[KrConstants.KrStages.RowChanged] = BooleanBoxes.True;
        }

        private async ValueTask SetOrderChangedAsync(
            CardRow row,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var basedOnStageTemplateID = row.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageTemplateID);

            if (basedOnStageTemplateID.HasValue
                && !await this.GetCanChangeOrderAsync(basedOnStageTemplateID.Value, cancellationToken))
            {
                validationResult.AddError(
                    this,
                    "$KrProcess_ChangingOrderStageIsProhibited",
                    row.TryGet<string>(KrConstants.KrStages.NameField));
                return;
            }

            row.Fields[KrConstants.KrStages.OrderChanged] = BooleanBoxes.True;
            row.Fields[KrConstants.KrStages.BasedOnStageTemplateGroupPositionID] =
                Int32Boxes.Box(GroupPosition.Unspecified.ID);
            row.Fields[KrConstants.KrStages.BasedOnStageTemplateOrder] = null;
        }

        /// <summary>
        /// Возвращает значение, показывающее, может ли быть изменён порядок этапа.
        /// </summary>
        /// <param name="basedOnStageTemplateID">Идентификатор шаблона этапов на основе которого был создан этап.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если порядок этапа может быть изменён, иначе - <see langword="false"/>.</returns>
        private async ValueTask<bool> GetCanChangeOrderAsync(
            Guid basedOnStageTemplateID,
            CancellationToken cancellationToken = default)
        {
            var stageTemplates = await this.krProcessCache.GetAllStageTemplatesAsync(
                cancellationToken);

            if (!stageTemplates.TryGetValue(basedOnStageTemplateID, out var stageTemplate))
            {
                return true;
            }

            var stageGroups = await this.krProcessCache.GetAllStageGroupsAsync(
                cancellationToken);

            if (!stageGroups.TryGetValue(stageTemplate.StageGroupID, out var stageGroup))
            {
                return true;
            }

            return stageTemplate.CanChangeOrder && !stageGroup.IsGroupReadonly;
        }

        /// <summary>
        /// Возвращает значение, показывающее, может ли этап быть изменён.
        /// </summary>
        /// <param name="basedOnStageTemplateID">Идентификатор шаблона этапов на основе которого был создан этап.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если этап может быть изменён, иначе - <see langword="false"/>.</returns>
        private async ValueTask<bool> GetIsStageReadonlyAsync(
            Guid basedOnStageTemplateID,
            CancellationToken cancellationToken = default)
        {
            var stageTemplates = await this.krProcessCache.GetAllStageTemplatesAsync(
                cancellationToken);

            if (!stageTemplates.TryGetValue(basedOnStageTemplateID, out var stageTemplate))
            {
                return true;
            }

            var stageGroups = await this.krProcessCache.GetAllStageGroupsAsync(
                cancellationToken);

            if (!stageGroups.TryGetValue(stageTemplate.StageGroupID, out var stageGroup))
            {
                return true;
            }

            return !stageTemplate.IsStagesReadonly && !stageGroup.IsGroupReadonly;
        }

        #endregion
    }
}
