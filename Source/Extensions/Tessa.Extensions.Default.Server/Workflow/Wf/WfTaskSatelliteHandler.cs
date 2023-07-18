using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Server.Files;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Extensions.Platform.Server.Cards.Satellites.Handlers;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    public sealed class WfTaskSatelliteHandler : SatelliteHandlerBase
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;
        private readonly IKrPermissionsManager permissionsManager;

        private static readonly string[] documentCommonInfoFields =
        {
            "DocTypeID",
            "DocTypeTitle",
            "Number",
            "FullNumber",
            "Sequence",
            "Subject",
            "DocDate",
            "CreationDate",
            "AuthorID",
            "AuthorName",
            "RegistratorID",
            "RegistratorName",
        };

        #endregion

        #region Constructors

        public WfTaskSatelliteHandler(
            IKrTypesCache krTypesCache,
            IKrPermissionsManager permissionsManager)
        {
            this.krTypesCache = krTypesCache;
            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        public override ValueTask<bool> IsMainCardTypeAsync(CardType mainCardType, CancellationToken cancellationToken = default)
        {
            return WfHelper.TypeSupportsWorkflowAsync(this.krTypesCache, mainCardType, cancellationToken);
        }

        public override async ValueTask CheckFileAccessAsync(ICardGetFileContentExtensionContext context, Guid mainCardID)
        {
            try
            {
                // проверяем доступ по основной карточке
                await KrFileAccessHelper.CheckAccessAsync(
                    context.Request,
                    context,
                    mainCardID,
                    permissionsManager,
                    context.CancellationToken);
            }
            finally
            {
                // чтобы типовое расширение на проверку прав не проверяло токен не от своей карточки
                KrToken.Remove(context.Request.Info);
            }
        }

        public override async ValueTask CheckFileVersionsAccessAsync(ICardGetFileVersionsExtensionContext context, Guid mainCardID)
        {
            try
            {
                // проверяем доступ по основной карточке
                await KrFileAccessHelper.CheckAccessAsync(
                    context.Request,
                    context,
                    mainCardID,
                    permissionsManager,
                    context.CancellationToken);
            }
            finally
            {
                // чтобы типовое расширение на проверку прав не проверяло токен не от своей карточки
                KrToken.Remove(context.Request.Info);
            }
        }

        public override async ValueTask PrepareSatelliteForGetAsync(
            ICardGetExtensionContext context,
            Card satellite,
            Guid mainCardID,
            Guid? taskRowID,
            Func<CancellationToken, ValueTask<Card>> getMainCardAsync)
        {
            var mainCard = await getMainCardAsync(context.CancellationToken);
            // Если не удалось получить основную карточку, значит что-то пошло не так.
            if (mainCard is null)
            {
                return;
            }

            await PrepareSatelliteWithMainCardInfoAsync(
                satellite,
                mainCard);

            string digest = context.Request.TryGetDigest();
            satellite.Sections.GetOrAdd("WfTaskCardsVirtual").RawFields["MainCardDigest"] = digest;

            object permissionsCalculated = context.Request.Info.TryGet<object>(KrPermissionsHelper.PermissionsCalculatedMark);
            if (permissionsCalculated is not null)
            {
                satellite.Info[KrPermissionsHelper.PermissionsCalculatedMark] = permissionsCalculated;
            }

            // права на файлы получаем только в случае, если или задание взято в работу (или отложено),
            // или если это автостартуемое задание "Постановка задачи", или текущий сотрудник является автором задания, но не является исполнителем
            if (WfHelper.CanModifyTaskCard(satellite))
            {
                // есть права на задание: запрещаем удаление карточки и действия для файлов основной карточки
                CardPermissionInfo permissions = satellite.Permissions;
                permissions.SetCardPermissions(CardPermissionFlags.ProhibitDeleteCard);

                ListStorage<CardFile> files = satellite.TryGetFiles();
                if (files is not null && files.Count > 0)
                {
                    GuidDictionaryStorage<CardPermissionFlags> filePermissions = permissions.FilePermissions;
                    foreach (CardFile file in files)
                    {
                        if (file.ExternalSource is not null)
                        {
                            filePermissions[file.RowID] = CardPermissionFlagValues.ProhibitAllFile;
                        }
                    }
                }
            }
            else
            {
                // нет прав на задание - нет никаких прав
                CardHelper.ProhibitAllPermissions(satellite, removeOtherPermissions: true);
            }
        }

        public override ValueTask<bool> SetupSatelliteFileAsync(ICardGetExtensionContext context, CardFile file, bool isMainCard)
        {
            if (isMainCard)
            {
                file.CategoryID = WfHelper.MainCardCategoryID;
                file.CategoryCaption = WfHelper.MainCardCategoryCaption;
            }

            return new ValueTask<bool>(true);
        }

        public override ValueTask<bool> IsMainCardFileAsync(ICardStoreExtensionContext context, Card satelliteCard, CardFile file)
        {
            return new ValueTask<bool>(file.CategoryID == WfHelper.MainCardCategoryID);
        }

        public override ValueTask PrepareMainCardFileToStoreAsync(ICardStoreExtensionContext context, Card satelliteCard, CardFile file)
        {
            // файлы основной карточки будут добавлены с пустой категорией
            file.CategoryID = null;
            file.CategoryCaption = null;
            return new ValueTask();
        }

        public override async ValueTask<IEnumerable<(Guid cardID, Guid typeID)>> GetExternalFileSourcesAsync(
            ICardGetExtensionContext context,
            Card satellite,
            Guid mainCardID,
            Guid? taskRowID)
        {
            if (!taskRowID.HasValue)
            {
                return Array.Empty<(Guid, Guid)>();
            }

            var db = context.DbScope.Db;

            db
                .SetCommand(
                    context.DbScope.BuilderFactory
                        .With("TasksCTE", b => b
                            .Select().C("th", "ParentRowID")
                            .From("TaskHistory", "th").NoLock()
                            .Where()
                                .C("th", "RowID").Equals().P("CurrentTaskRowID")
                                .And().C("th", "ParentRowID").IsNotNull()
                            .UnionAll()
                            .Select().C("th", "ParentRowID")
                            .From("TasksCTE", "t")
                            .InnerJoin("TaskHistory", "th").NoLock()
                                .On().C("th", "RowID").Equals().C("t", "RowID")
                            .Where().C("th", "ParentRowID").IsNotNull(),
                            columnNames: new[] { "RowID" },
                            recursive: true)
                        .Select().C("wf", "ID")
                        .From("TasksCTE", "t")
                        .InnerJoin(CardSatelliteHelper.SatellitesSectionName, "wf").NoLock()
                            .On().C("wf", CardSatelliteHelper.TaskRowIDColumn).Equals().C("t", "RowID")
                        .Where()
                            .C("wf", CardSatelliteHelper.SatelliteTypeIDColumn).Equals().V(DefaultCardTypes.WfTaskCardTypeID)
                            .And()
                            .Exists(e => e
                                .Select().V(1)
                                .From("Files", "f").NoLock()
                                .Where().C("f", "ID").Equals().C("wf", "ID"))
                        .Build(),
                    db.Parameter("CurrentTaskRowID", taskRowID.Value))
                .LogCommand();

            List<(Guid, Guid)> result = new List<(Guid, Guid)>();
            await using (var reader = await db.ExecuteReaderAsync(context.CancellationToken))
            {
                while (await reader.ReadAsync(context.CancellationToken))
                {
                    result.Add((
                        reader.GetGuid(0),
                        DefaultCardTypes.WfTaskCardTypeID));
                }
            }

            return result;
        }

        #endregion

        #region Private Methods

        private async ValueTask PrepareSatelliteWithMainCardInfoAsync(
            Card satellite,
            Card mainCard)
        {
            Dictionary<string, object> virtualFields = satellite.Sections["WfTaskCardsVirtual"].RawFields;

            StringDictionaryStorage<CardSection> mainSections = mainCard.TryGetSections();
            if (mainSections is not null)
            {
                if (mainSections.TryGetValue("DocumentCommonInfo", out var mainSection))
                {
                    Dictionary<string, object> fields = mainSection.RawFields;
                    for (int i = 0; i < documentCommonInfoFields.Length; i++)
                    {
                        virtualFields[documentCommonInfoFields[i]] = fields.TryGet<object>(documentCommonInfoFields[i]);
                    }
                }

                if (mainSections.TryGetValue("KrApprovalCommonInfoVirtual", out mainSection))
                {
                    Dictionary<string, object> fields = mainSection.RawFields;
                    virtualFields["StateID"] = fields.TryGet<object>("StateID");
                    virtualFields["StateName"] = fields.TryGet<object>("StateName");
                    virtualFields["StateModified"] = fields.TryGet<object>("StateChangedDateTimeUTC");
                }
            }

            // Перекидываем KrToken в карточку сателлита, т.к. знаем, что данная карточка сателита не добавлена в типовое решение
            var cardToken = KrToken.TryGet(mainCard.Info);
            if (cardToken is not null)
            {
                cardToken.Set(satellite.Info);
            }

            if (cardToken.HasPermission(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles))
            {
                foreach (var task in satellite.Tasks)
                {
                    task.Flags |= CardTaskFlags.CanModifyTaskAssignedRoles;
                }
            }

            if (cardToken.HasPermission(KrPermissionFlagDescriptors.ModifyOwnTaskAssignedRoles))
            {
                foreach (var task in satellite.Tasks)
                {
                    if (task.TaskSessionRoles.Count > 0)
                    {
                        task.Flags |= CardTaskFlags.CanModifyTaskAssignedRoles;
                    }
                }
            }
        }

        #endregion
    }
}
