using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class KrAddCycleFilesCardGetExtension : CardGetExtension
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;
        
        private static readonly Guid[] ignoreTypes =
        {
            KrConstants.KrDeregistrationTypeID,
            DefaultTaskTypes.KrRequestCommentTypeID,
            DefaultTaskTypes.KrInfoRequestCommentTypeID
        };

        #endregion

        #region Constructors

        public KrAddCycleFilesCardGetExtension(IKrTypesCache krTypesCache) =>
            this.krTypesCache = krTypesCache ?? throw new ArgumentNullException(nameof(krTypesCache));

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful ||
                context.Request.ServiceType == CardServiceType.Default ||
                !context.ValidationResult.IsSuccessful() ||
                context.CardType.Flags.HasNot(CardTypeFlags.AllowFiles) ||
                context.Request.RestrictionFlags.Has(CardGetRestrictionFlags.RestrictFiles) ||
                (card = context.Response.TryGetCard()) == null ||
                !(await KrComponentsHelper.GetKrComponentsAsync(card, this.krTypesCache, context.CancellationToken)).Has(KrComponents.Routes))
            {
                return;
            }

            var cardID = card.ID;

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                DataParameter? dpTypeIDs = null;

                var builder =
                    context.DbScope.BuilderFactory
                        .Select()
                        .C("f", "RowID")
                        .C("fv", "SourceID", "Size", "CreatedByID", "CreatedByName", "Created", "RowID", "Number")
                        .C("th", "Cycle")
                        .From("Files", "f").NoLock()
                        .InnerJoin("FileVersions", "fv").NoLock()
                        .On().C("f", "RowID").Equals().C("fv", "ID")
                        .InnerJoinLateral(b => b
                            .Select().Top(1)
                            .C("c", "Cycle")
                            .From(c => c
                                    .Select()
                                        .Min("th", "Created").As("CycleStart")
                                        .Max(e => e.Coalesce(q => q.C("th", "Completed").P("DateTimeNow"))).As("CycleEnd")
                                        .C("kr", "Cycle")
                                    .From("TaskHistory", "th").NoLock()
                                    .InnerJoin("KrApprovalHistory", "kr").NoLock()
                                        .On().C("th", "RowID").Equals().C("kr", "HistoryRecord")
                                    .Where()
                                        .C("f", "ID").Equals().C("th", "ID")
                                        .And()
                                        .C("th", "TypeID").NotInArray(ignoreTypes, "IgnoreTypeIDs", out dpTypeIDs)
                                        .And()
                                        .Not().E(q =>
                                            q.C("th", "TypeID").Equals().V(DefaultTaskTypes.KrInfoAdditionalApprovalTypeID)
                                                .And().C("th", "OptionID").Equals().V(DefaultCompletionOptions.AdditionalApproval))
                                    .GroupBy("kr", "Cycle")
                                , "c")
                            .Where()
                                .C("fv", "Created").GreaterOrEquals().C("c", "CycleStart")
                                .And()
                                .C("fv", "Created").LessOrEquals().C("c", "CycleEnd")
                            .Limit(1), "th")
                        .Where()
                            .C("f", "ID").Equals().P("ID")
                            .And()
                            .C("th", "Cycle").IsNotNull()
                            .And()
                            .C("f", "OriginalFileID").IsNull()
                        .OrderBy("th", "Cycle", SortOrder.Descending)
                        .By("fv", "Created", SortOrder.Descending)
                        .By("fv", "RowID", SortOrder.Ascending);

                await db.SetCommand(
                        builder.Build(),
                        DataParameters.Get(
                            db.Parameter("ID", cardID),
                            db.Parameter("DateTimeNow", DateTime.UtcNow),
                            dpTypeIDs))
                    .LogCommand()
                    .ExecuteNonQueryAsync(context.CancellationToken);

                var filesList =
                    new List<(
                        Guid FileRowID,
                        int VersionSource,
                        long VersionSize,
                        Guid VersionCreatedByID,
                        string VersionCreatedByName,
                        DateTime VersionCreated,
                        Guid VersionRowID,
                        int VersionNumber,
                        int Cycle)>();

                int maxCycleNumber = 0;
                await using (var reader = await db.ExecuteReaderAsync(context.CancellationToken))
                {
                    while (await reader.ReadAsync(context.CancellationToken))
                    {
                        Guid fileRowID = reader.GetGuid(0);
                        int versionSource = reader.GetValue<short>(1);
                        long versionSize = reader.GetValue<long>(2);
                        Guid versionCreatedByID = reader.GetGuid(3);
                        string versionCreatedByName = reader.GetValue<string>(4);
                        DateTime versionCreated = reader.GetDateTimeUtc(5);
                        Guid versionRowID = reader.GetGuid(6);
                        int versionNumber = reader.GetValue<int>(7);
                        int cycle = reader.GetValue<short>(8);

                        if (cycle > maxCycleNumber)
                        {
                            maxCycleNumber = cycle;
                        }

                        filesList.Add((
                            fileRowID,
                            versionSource,
                            versionSize,
                            versionCreatedByID,
                            versionCreatedByName,
                            versionCreated,
                            versionRowID,
                            versionNumber,
                            cycle));
                    }
                }

                List<object> resultFileVersions =
                    filesList
                        .GroupBy(p => p.Cycle)
                        .SelectMany(p =>
                        {
                            var groupByFile = p.GroupBy(q => q.FileRowID);

                            return groupByFile.SelectMany(q =>
                            {
                                var groupByCreatedBy = q.GroupBy(e => e.VersionCreatedByID);
                                return groupByCreatedBy.SelectMany(e =>
                                {
                                    var maxAuthorVersion = e.Max(t => t.VersionNumber);
                                    return e.Where(t => t.VersionNumber == maxAuthorVersion);
                                });
                            });
                        })
                        .Select(x =>
                            (object) new Dictionary<string, object>
                            {
                                { "Cycle", x.Cycle },
                                { "FileID", x.FileRowID },
                                { "VersionID", x.VersionRowID },
                                { "Number", x.VersionNumber },
                                { "Size", x.VersionSize },
                                { "SourceID", x.VersionSource },
                                { "Created", x.VersionCreated },
                                { "CreatedByID", x.VersionCreatedByID },
                                { "CreatedByName", x.VersionCreatedByName },
                            })
                        .ToList();

                card.Info[CycleGroupingInfoKeys.FilesModifiedByCyclesKey] = resultFileVersions;

                // MaxCycleNumberKey уже точно мог быть у установлен другим расширением, если были копии.
                // Но он может быть не максимальным, если есть версии на более поздних циклах.
                int? maxCycleNumberFromCard = context.Response.Card.Info.TryGet<int?>(CycleGroupingInfoKeys.MaxCycleNumberKey);
                if (!maxCycleNumberFromCard.HasValue || maxCycleNumberFromCard.Value < maxCycleNumber)
                {
                    context.Response.Card.Info[CycleGroupingInfoKeys.MaxCycleNumberKey] = maxCycleNumber;
                }
            }
        }

        #endregion
    }
}
