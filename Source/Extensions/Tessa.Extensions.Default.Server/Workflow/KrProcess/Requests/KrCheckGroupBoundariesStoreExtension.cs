using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на сохранение карточки, для которой включены маршруты документов.<para/>
    /// Проверяет правильность расположения этапов маршрута в сохраняемой карточке.
    /// </summary>
    public sealed class KrCheckGroupBoundariesStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        private readonly IKrProcessCache processCache;

        #endregion

        #region Constructors

        public KrCheckGroupBoundariesStoreExtension(
            IKrTypesCache typesCache,
            IKrProcessCache processCache)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.processCache = processCache ?? throw new ArgumentNullException(nameof(processCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || (await KrComponentsHelper.GetKrComponentsAsync(
                        context.Request.Card,
                        this.typesCache,
                        context.CancellationToken))
                        .HasNot(KrComponents.Routes))
            {
                return;
            }

            var card = context.Request.Card;
            if (!card.TryGetStagesSection(out var mainCardStagesSection)
                || !card.TryGetStagePositions(out var stagesPositions))
            {
                return;
            }

            await this.CheckMainCardBoundariesAsync(
                mainCardStagesSection.Rows,
                stagesPositions,
                context.ValidationResult,
                context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task CheckMainCardBoundariesAsync(
            IList<CardRow> mainCardStagesRows,
            List<KrStagePositionInfo> stagesPositions,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            if (mainCardStagesRows.Count == 0)
            {
                return;
            }

            var rows = mainCardStagesRows
                // Исключаем удаленные этапы
                .Where(static p => p.State != CardRowState.Deleted)
                .OrderBy(static p => p.TryGet<int?>(KrConstants.KrStages.Order));

            var currentOrder = int.MinValue;
            var groupsHashSet = await this.processCache.GetAllStageGroupsAsync(cancellationToken);

            foreach (var row in rows)
            {
                if ((row.TryGet<bool>(KrConstants.Keys.RootStage)
                    || row.TryGet<bool>(KrConstants.Keys.NestedStage))
                    && !row.All(static p =>
                        StorageHelper.IsUserKey(p.Key)
                        || p.Key == CardRow.SystemStateKey
                        || p.Key == CardRow.SystemChangedKey
                        || p.Key == KrConstants.KrStages.RowID
                        || p.Key == KrConstants.KrStages.Order))
                {
                    // Кто-то каким-то образом модифицировал нестед, что делать мы не разрешаем вообще.
                    validationResult.AddError(this, "$KrProcess_Error_TreeStructureStageModified");
                    return;
                }

                if (row.TryGet<int?>(KrConstants.KrStages.Order).HasValue)
                {
                    var rowID = row.RowID;
                    foreach (var position in stagesPositions)
                    {
                        if (position.RowID == rowID)
                        {
                            if (position.GroupOrder < currentOrder)
                            {
                                var stageName = position.Name;
                                var stageGroupName = groupsHashSet.TryGetValue(position.StageGroupID, out var group)
                                    ? group.Name
                                    : "unknown";
                                validationResult.AddWarning(
                                    this,
                                    "$KrMessages_ViolationOfGroupBoundaries",
                                    await LocalizationManager.LocalizeAsync(stageName, cancellationToken),
                                    await LocalizationManager.LocalizeAsync(stageGroupName, cancellationToken));
                                return;
                            }

                            currentOrder = position.GroupOrder;
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
