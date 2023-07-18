using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на сохранение карточки основного сателлита.<para/>
    /// Проверяет правильность расположения этапов маршрута в основном сателлите (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>).
    /// </summary>
    public sealed class KrCheckGroupBoundariesInSatelliteStoreExtension :
        CardStoreExtension
    {
        #region Nested Types

        private readonly record struct CardRowExt(CardRow Row, CardRowState State);

        #endregion

        #region Fields

        private readonly IKrScope krScope;

        private readonly IKrProcessCache processCache;

        #endregion

        #region Constructors

        public KrCheckGroupBoundariesInSatelliteStoreExtension(
            IKrScope krScope,
            IKrProcessCache processCache)
        {
            this.krScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
            this.processCache = processCache ?? throw new ArgumentNullException(nameof(processCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var satellite = context.Request.Card;
            ListStorage<CardRow> currentSatelliteStagesRows;

            if (!satellite.TryGetStagesSection(out var currentSatelliteStagesSection)
                || (currentSatelliteStagesRows = currentSatelliteStagesSection.TryGetRows()) is null
                || currentSatelliteStagesRows.Count == 0)
            {
                return;
            }

            IList<CardRowExt> rows = null;

            if (satellite.StoreMode == CardStoreMode.Update)
            {
                // Получение сателлита, содержащего полный набор этапов.
                // Если выполнение происходит вне KrScopeContext, то будет возвращён сохранённый в БД сателлит,
                // иначе сохраняемый, из которого была удалена информация об изменениях
                // (см. KrScopeLevel.ApplyChangesAsync).
                satellite = await this.krScope.GetMainCardAsync(
                    satellite.ID,
                    context.ValidationResult,
                    true,
                    context.CancellationToken);

                if (satellite is null)
                {
                    return;
                }

                ListStorage<CardRow> originalSatelliteStagesRows;

                if (satellite.TryGetStagesSection(out var originalSatelliteStagesSection)
                    && (originalSatelliteStagesRows = originalSatelliteStagesSection.TryGetRows())?.Count > 0)
                {
                    rows = new List<CardRowExt>(originalSatelliteStagesRows.Count);

                    var currentRowsDict = currentSatelliteStagesRows
                        .ToDictionary(static i => i.RowID);

                    for (var i = 0; i < originalSatelliteStagesRows.Count; i++)
                    {
                        var originalRow = originalSatelliteStagesRows[i];

                        var state = currentRowsDict.TryGetValue(originalRow.RowID, out var currentRow)
                            ? currentRow.State
                            : originalRow.State;

                        rows.Add(new CardRowExt(originalRow, state));
                    }
                }
            }

            rows ??= currentSatelliteStagesRows
                .Select(static i => new CardRowExt(i, i.State))
                .ToArray();

            await this.CheckStageGroupBoundsAsync(
                rows,
                context.ValidationResult,
                context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task CheckStageGroupBoundsAsync(
            IList<CardRowExt> satelliteStageRows,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var groupsHashSet = await this.processCache.GetAllStageGroupsAsync(cancellationToken);

            var rowsInfo = satelliteStageRows
                // Исключаем как удаленные этапы, так и этапы с удаленными группами
                // А также нестеды, т.к. они по факту относятся к другим процессам
                .Where(p => p.State != CardRowState.Deleted
                    && !p.Row.TryGet<Guid?>(KrConstants.KrStages.ParentStageRowID).HasValue
                    && groupsHashSet.ContainsKey(p.Row.Get<Guid>(KrConstants.KrStages.StageGroupID)))
                .OrderBy(static p => p.Row.Get<int>(KrConstants.KrStages.Order))
                .ToArray();

            if (rowsInfo.Length == 0)
            {
                return;
            }

            var orderedStageGroups = await this.processCache.GetOrderedStageGroupsAsync(cancellationToken);
            var groups = new Dictionary<Guid, int>(orderedStageGroups.Count);

            for (var i = 0; i < orderedStageGroups.Count; i++)
            {
                groups.Add(orderedStageGroups[i].ID, i);
            }

            var currentStageGroupIndexAt = int.MinValue;
            for (var i = 0; i < rowsInfo.Length; i++)
            {
                var rowInfo = rowsInfo[i];
                var currentStageGroupID = rowInfo.Row.Get<Guid>(KrConstants.KrStages.StageGroupID);
                var groupIndexAt = groups[currentStageGroupID];

                if (currentStageGroupIndexAt <= groupIndexAt)
                {
                    currentStageGroupIndexAt = groupIndexAt;
                }
                else
                {
                    var foundRow = LastOrDefault(rowsInfo, i) ?? rowInfo.Row;
                    var stageName = foundRow.Get<string>(KrConstants.KrStages.NameField);
                    var groupName = foundRow.Get<string>(KrConstants.KrStages.StageGroupName);

                    validationResult.AddError(
                        this,
                        "$KrMessages_ViolationOfGroupBoundaries",
                        await LocalizationManager.LocalizeAsync(stageName, cancellationToken),
                        await LocalizationManager.LocalizeAsync(groupName, cancellationToken));

                    return;
                }
            }
        }

        private static CardRow LastOrDefault(
            IList<CardRowExt> rowsInfo,
            int startIndex)
        {
            for (var i = startIndex; i >= 0; i--)
            {
                var row = rowsInfo[i];
                if (row.State is CardRowState.Modified or CardRowState.Inserted)
                {
                    return row.Row;
                }
            }

            return null;
        }

        #endregion
    }
}
