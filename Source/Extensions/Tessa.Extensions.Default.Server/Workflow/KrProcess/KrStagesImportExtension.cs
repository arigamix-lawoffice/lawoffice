using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrStagesImportExtension :
        CardStoreExtension
    {
        #region Constructors

        public KrStagesImportExtension(
            IStageTypeFormatterContainer formatterContainer,
            ISession session)
        {
            this.formatterContainer = formatterContainer;
            this.session = session;
        }

        #endregion

        #region Fields

        private readonly IStageTypeFormatterContainer formatterContainer;

        private readonly ISession session;

        #endregion

        #region Private Methods

        private async Task FormatRowAsync(
            CardRow innerRow,
            Card innerCard,
            Guid stageTypeID,
            IDictionary<string, object> settings,
            CancellationToken cancellationToken = default)
        {
            var formatter = this.formatterContainer.ResolveFormatter(stageTypeID);
            if (formatter is null)
            {
                innerRow.Fields[KrConstants.KrStages.DisplaySettings] = string.Empty;
                return;
            }

            var info = new Dictionary<string, object>();
            var ctx = new StageTypeFormatterContext(
                this.session,
                info,
                innerCard,
                innerRow,
                settings,
                cancellationToken)
            {
                DisplayTimeLimit = innerRow.TryGet<string>(KrConstants.KrStages.DisplayTimeLimit) ?? string.Empty,
                DisplayParticipants = innerRow.TryGet<string>(KrConstants.KrStages.DisplayParticipants) ?? string.Empty,
                DisplaySettings = innerRow.TryGet<string>(KrConstants.KrStages.DisplaySettings) ?? string.Empty
            };

            await formatter.FormatServerAsync(ctx);

            innerRow.Fields[KrConstants.KrStages.DisplaySettings] = ctx.DisplaySettings;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;
            ListStorage<CardRow> rows;

            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) is null
                || (sections = card.TryGetSections()) is null
                || !sections.TryGetValue(nameof(KrConstants.KrStages), out CardSection section)
                || (rows = section.TryGetRows()) is null
                || rows.Count == 0)
            {
                return;
            }

            var stageInfoByRowID = new Dictionary<Guid, (Guid StageTypeID, string Settings)>();
            if (card.StoreMode == CardStoreMode.Update && rows.Any(x => x.State == CardRowState.Modified))
            {
                var db = context.DbScope.Db;
                var queryBuilder = context.DbScope.BuilderFactory;

                await using DbDataReader reader = await db
                    .SetCommand(
                        queryBuilder
                            .Select().C(null, "RowID", KrConstants.KrStages.StageTypeID, KrConstants.KrStages.Settings)
                            .From(nameof(KrConstants.KrStages)).NoLock()
                            .Where().C("ID").Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", card.ID, DataType.Guid))
                    .LogCommand()
                    .ExecuteReaderAsync(CommandBehavior.SequentialAccess, context.CancellationToken);

                while (await reader.ReadAsync(context.CancellationToken))
                {
                    Guid rowID = reader.GetGuid(0);
                    Guid? stageTypeID = reader.GetNullableGuid(1);
                    string settings = await reader.GetSequentialNullableStringAsync(2, context.CancellationToken);

                    if (stageTypeID.HasValue && !string.IsNullOrEmpty(settings))
                    {
                        stageInfoByRowID[rowID] = (stageTypeID.Value, settings);
                    }
                }
            }

            foreach (CardRow row in rows)
            {
                if (card.StoreMode == CardStoreMode.Insert
                    || row.State is CardRowState.Inserted or CardRowState.Modified)
                {
                    if (row.State == CardRowState.None)
                    {
                        row.State = CardRowState.Inserted;
                    }

                    Dictionary<string, object> settings = null;

                    Guid? stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);
                    if (!stageTypeID.HasValue
                        && stageInfoByRowID.TryGetValue(row.RowID, out var info))
                    {
                        stageTypeID = info.StageTypeID;
                    }

                    if (!stageTypeID.HasValue)
                    {
                        continue;
                    }

                    string json = row.TryGet<string>(KrConstants.KrStages.Settings);
                    if (json is null
                        && stageInfoByRowID.TryGetValue(row.RowID, out info))
                    {
                        json = info.Settings;
                    }

                    if (!string.IsNullOrEmpty(json))
                    {
                        settings = StorageHelper.DeserializeFromTypedJson(json);
                    }

                    await FormatRowAsync(row, card, stageTypeID.Value, settings ?? new Dictionary<string, object>(), context.CancellationToken);
                }
            }
        }

        #endregion
    }
}