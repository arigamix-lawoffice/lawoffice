using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public static class KrCompilersSqlHelper
    {
        #region Nested Types

        private sealed class KrSecondaryProcessData
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public int Mode { get; set; }
            public bool IsGlobal { get; set; }
            public bool Async { get; set; }
            public string Caption { get; set; }
            public string Icon { get; set; }
            public TileSize TileSize { get; set; }
            public string Tooltip { get; set; }
            public string TileGroup { get; set; }
            public string ExecutionAccessDeniedMessage { get; set; }
            public bool RefreshAndNotify { get; set; }
            public bool AskConfirmation { get; set; }
            public string ConfirmationMessage { get; set; }
            public bool ActionGrouping { get; set; }
            public string VisibilitySqlCondition { get; set; }
            public string ExecutionSqlCondition { get; set; }
            public string VisibilitySourceCondition { get; set; }
            public string ExecutionSourceCondition { get; set; }
            public string EventType { get; set; }
            public bool AllowClientSideLaunch { get; set; }
            public bool CheckRecalcRestrictions { get; set; }
            public bool RunOnce { get; set; }
            public string ButtonHotkey { get; set; }
            public int Order { get; set; }
            public bool NotMessageHasNoActiveStages { get; set; }
            public ICollection<Guid> ContextRolesIDs { get; set; }

            /// <summary>
            /// Возвращает перечисление параметров условий.
            /// </summary>
            public IEnumerable<ConditionSettings> Conditions { get; private set; }

            /// <summary>
            /// Выполняет чтение потока набора данных.
            /// </summary>
            /// <param name="reader">Средство чтения одного или нескольких прямонаправленных потоков наборов результатов, полученных вследствие выполнения команды в источнике данных.</param>
            public void Read(
                IDataReader reader)
            {
                var cnt = 0;
                this.ID = reader.GetGuid(cnt++);
                this.Name = reader.GetNullableString(cnt++);
                this.Mode = reader.GetInt32(cnt++);
                this.IsGlobal = reader.GetBoolean(cnt++);
                this.Async = reader.GetBoolean(cnt++);
                this.Caption = reader.GetNullableString(cnt++);
                this.Icon = reader.GetNullableString(cnt++);
                this.TileSize = (TileSize) reader.GetInt16(cnt++);
                this.Tooltip = reader.GetNullableString(cnt++);
                this.TileGroup = reader.GetNullableString(cnt++);
                this.ExecutionAccessDeniedMessage = reader.GetNullableString(cnt++);
                this.RefreshAndNotify = reader.GetBoolean(cnt++);
                this.AskConfirmation = reader.GetBoolean(cnt++);
                this.ConfirmationMessage = reader.GetNullableString(cnt++);
                this.ActionGrouping = reader.GetBoolean(cnt++);
                this.VisibilitySqlCondition = reader.GetNullableString(cnt++);
                this.ExecutionSqlCondition = reader.GetNullableString(cnt++);
                this.VisibilitySourceCondition = reader.GetNullableString(cnt++);
                this.ExecutionSourceCondition = reader.GetNullableString(cnt++);
                this.EventType = reader.GetNullableString(cnt++);
                this.AllowClientSideLaunch = reader.GetBoolean(cnt++);
                this.CheckRecalcRestrictions = reader.GetBoolean(cnt++);
                this.RunOnce = reader.GetBoolean(cnt++);
                this.ButtonHotkey = reader.GetNullableString(cnt++);
                this.Order = reader.GetInt32(cnt++);
                var conditions = reader.GetNullableString(cnt++);

                this.Conditions = string.IsNullOrWhiteSpace(conditions)
                    ? Enumerable.Empty<ConditionSettings>()
                    : ConditionSettings.GetFromList(StorageHelper.DeserializeListFromTypedJson(conditions));

                this.NotMessageHasNoActiveStages = reader.GetBoolean(cnt);
            }

            public IKrPureProcess ToPureProcess()
            {
                return new KrPureProcess(
                    id: this.ID,
                    name: this.Name,
                    isGlobal: this.IsGlobal,
                    async: this.Async,
                    executionAccessDeniedMessage: this.ExecutionAccessDeniedMessage,
                    runOnce: false, // RunOnce актуально только для вторичных процессов типа "Действие".
                    notMessageHasNoActiveStages: this.NotMessageHasNoActiveStages,
                    contextRolesIDs: this.ContextRolesIDs,
                    executionSqlCondition: this.ExecutionSqlCondition,
                    executionSourceCondition: this.ExecutionSourceCondition,
                    allowClientSideLaunch: this.AllowClientSideLaunch,
                    checkRecalcRestrictions: this.CheckRecalcRestrictions);
            }

            public IKrAction ToAction()
            {
                return new KrAction(
                    id: this.ID,
                    name: this.Name,
                    isGlobal: this.IsGlobal,
                    async: this.Async,
                    executionAccessDeniedMessage: this.ExecutionAccessDeniedMessage,
                    runOnce: this.RunOnce,
                    notMessageHasNoActiveStages: this.NotMessageHasNoActiveStages,
                    contextRolesIDs: this.ContextRolesIDs,
                    executionSqlCondition: this.ExecutionSqlCondition,
                    executionSourceCondition: this.ExecutionSourceCondition,
                    eventType: this.EventType);
            }

            public IKrProcessButton ToProcessButton()
            {
                return new KrProcessButton(
                    id: this.ID,
                    name: this.Name,
                    isGlobal: this.IsGlobal,
                    async: this.Async,
                    executionAccessDeniedMessage: this.ExecutionAccessDeniedMessage,
                    runOnce: false, // RunOnce актуально только для вторичных процессов типа "Действие".
                    notMessageHasNoActiveStages: this.NotMessageHasNoActiveStages,
                    contextRolesIDs: this.ContextRolesIDs,
                    executionSqlCondition: this.ExecutionSqlCondition,
                    executionSourceCondition: this.ExecutionSourceCondition,
                    caption: this.Caption,
                    icon: this.Icon,
                    tileSize: this.TileSize,
                    tooltip: this.Tooltip,
                    tileGroup: this.TileGroup,
                    refreshAndNotify: this.RefreshAndNotify,
                    askConfirmation: this.AskConfirmation,
                    confirmationMessage: this.ConfirmationMessage,
                    actionGrouping: this.ActionGrouping,
                    buttonHotkey: this.ButtonHotkey,
                    order: this.Order,
                    visibilitySqlCondition: this.VisibilitySqlCondition,
                    visibilitySourceCondition: this.VisibilitySourceCondition,
                    conditions: this.Conditions);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Загружает из БД шаблоны этапов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="id">ИД загружаемого шаблона этапов, если задано значение по умолчанию для типа, то будут загружены все шаблоны этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция загруженных шаблонов этапов.</returns>
        public static async Task<List<IKrStageTemplate>> SelectStageTemplatesAsync(
            IDbScope dbScope,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C(null,
                        KrStageTemplates.ID, KrStageTemplates.NameField, KrStageTemplates.Order, KrStageTemplates.StageGroupID, KrStageTemplates.StageGroupName,
                        KrStageTemplates.GroupPositionID, KrStageTemplates.CanChangeOrder,
                        KrStageTemplates.IsStagesReadonly,
                        KrStageTemplates.SqlCondition, KrStageTemplates.SourceCondition, KrStageTemplates.SourceBefore, KrStageTemplates.SourceAfter)
                    .From(KrStageTemplates.Name).NoLock();
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C(KrStageTemplates.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }
                var stages = new List<IKrStageTemplate>();
                await using (var reader = await db
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        stages.Add(new KrStageTemplate(
                            reader.GetGuid(0), // ID
                            reader.GetString(1), // Name
                            reader.GetInt32(2), // Order
                            reader.GetGuid(3), // StageGroupID
                            reader.GetNullableString(4), // StageGroupName
                            GroupPosition.GetByID(reader.GetNullableInt32(5)), // GroupPosition
                            reader.GetBoolean(6), // CanChangeOrder
                            reader.GetBoolean(7), // IsStagesReadonly
                            reader.GetNullableString(8), // SQLCondition
                            reader.GetNullableString(9), // SourceCondition
                            reader.GetNullableString(10), // SourceBefore
                            reader.GetNullableString(11) // SourceAfter
                        ));
                    }
                }

                return stages;
            }
        }

        /// <summary>
        /// Возвращает список содержащий информацию по виртуальным шаблонам вторичных процессов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="id">ИД вторичного процесса или значение по умолчанию для типа, если требуется получить информацию по шаблонам по всем вторичным процессам.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список содержащий информацию по виртуальным шаблонам вторичных процессов.</returns>
        public static async Task<List<IKrStageTemplate>> SelectVirtualStageTemplatesAsync(
            IDbScope dbScope,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C(null, KrSecondaryProcesses.ID, KrSecondaryProcesses.NameField)
                    .From(KrSecondaryProcesses.Name).NoLock();
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C(KrSecondaryProcesses.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }
                var stages = new List<IKrStageTemplate>();
                await using (var reader = await db
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var rowID = reader.GetGuid(0);
                        var groupID = rowID;
                        var name = reader.GetString(1);

                        stages.Add(new KrStageTemplate(
                            rowID, // ID
                            name, // Name
                            DefaultSecondaryProcessTemplateOrder, // Order
                            groupID, // StageGroupID
                            name, // StageGroupName
                            GroupPosition.AtFirst, // GroupPosition
                            false, // CanChangeOrder
                            true, // IsStagesReadonly
                            string.Empty, // SQLCondition
                            string.Empty, // SourceCondition
                            string.Empty, // SourceBefore
                            string.Empty // SourceAfter
                        ));
                    }
                }

                return stages;
            }
        }

        /// <summary>
        /// Возвращает список идентификаторов шаблонов для указанных: типа карточки/документа, пользователя и группы этапов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="typeID">ИД типа документа документов для шаблона этапа и группы этапов.</param>
        /// <param name="userID">ИД роли (пользователя) для шаблона этапа и группы этапов.</param>
        /// <param name="stageGroupID">ИД группы этапов.</param>
        /// <param name="secondaryProcessID">ИД вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список идентификаторов шаблонов.</returns>
        public static async Task<List<Guid>> GetFilteredStageTemplates(
            IDbScope dbScope,
            Guid typeID,
            Guid userID,
            Guid stageGroupID,
            Guid? secondaryProcessID = null,
            CancellationToken cancellationToken = default)
        {
            if (secondaryProcessID == stageGroupID)
            {
                return new List<Guid> { secondaryProcessID.Value };
            }

            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C("t", KrStageTemplates.ID)
                    .From(KrStageTemplates.Name, "t").NoLock()
                    .LeftJoin(KrStageTypes.Name, "tt").NoLock()
                        .On().C("tt", KrStageTypes.ID).Equals().C("t", KrStageTemplates.ID)
                    .Where()
                        .C("t", KrStageTemplates.StageGroupID).Equals().P("StageGroupID")
                        .And()
                        .E(w => w
                            .C("tt", KrStageTypes.TypeID).IsNull()
                            .Or()
                            .C("tt", KrStageTypes.TypeID).Equals().P("TypeID"))
                        .And()
                        .E(w => w
                            .NotExists(e => e
                                .Select().V(null)
                                .From(KrStageRoles.Name, "r").NoLock()
                                .Where().C("r", KrStageRoles.ID).Equals().C("t", KrStageTemplates.ID))
                            .Or()
                            .Exists(e => e
                                .Select().V(null)
                                .From(KrStageRoles.Name, "r").NoLock()
                                .InnerJoin("RoleUsers", "ru").NoLock()
                                    .On().C("ru", "ID").Equals().C("r", "RoleID")
                                .Where().C("r", KrStageRoles.ID).Equals().C("t", KrStageTemplates.ID)
                                    .And().C("ru", "UserID").Equals().P("UserID")));

                return await db
                    .SetCommand(
                        builder.Build(),
                        db.Parameter("TypeID", typeID),
                        db.Parameter("UserID", userID),
                        db.Parameter("StageGroupID", stageGroupID))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает список содержащий информацию по группам этапов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="id">ИД группы этапов или значение по умолчанию для типа, если требуется получить информацию по всем группам этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список содержащий информацию по группам этапов.</returns>
        public static async Task<List<IKrStageGroup>> SelectStageGroupsAsync(
            IDbScope dbScope,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C(null,
                        KrStageGroups.ID, KrStageGroups.NameField, KrStageGroups.Order,
                        KrStageGroups.IsGroupReadonly, KrStageGroups.KrSecondaryProcessID, KrStageGroups.SqlCondition, KrStageGroups.RuntimeSqlCondition,
                        KrStageGroups.SourceCondition, KrStageGroups.SourceBefore, KrStageGroups.SourceAfter,
                        KrStageGroups.RuntimeSourceCondition, KrStageGroups.RuntimeSourceBefore, KrStageGroups.RuntimeSourceAfter)
                    .From(KrStageGroups.Name).NoLock();
                if (id is null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C(KrStageGroups.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }
                var stages = new List<IKrStageGroup>();
                await using (var reader = await db
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        stages.Add(new KrStageGroup(
                            reader.GetGuid(0), // ID
                            reader.GetString(1), // Name
                            reader.GetInt32(2), // Order
                            reader.GetBoolean(3), // IsGroupReadonly
                            reader.GetNullableGuid(4), // SecondaryProcessID
                            reader.GetNullableString(5), // SQLCondition
                            reader.GetNullableString(6), // RuntimeSQLCondition
                            reader.GetNullableString(7), // SourceCondition
                            reader.GetNullableString(8), // SourceBefore
                            reader.GetNullableString(9), // SourceAfter
                            reader.GetNullableString(10), // RuntimeSourceCondition
                            reader.GetNullableString(11), // RuntimeSourceBefore
                            reader.GetNullableString(12) // RuntimeSourceAfter
                        ));
                    }
                }

                return stages;
            }
        }

        /// <summary>
        /// Возвращает список виртуальных групп по вторичным процессам.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="id">ИД вторичного процесса или значение по умолчанию для типа, если требуется получить все виртуальные группы вторичных процессов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список виртуальных групп по вторичным процессам.</returns>
        public static async Task<List<IKrStageGroup>> SelectVirtualStageGroupsAsync(
            IDbScope dbScope,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select()
                    .C(null, KrSecondaryProcesses.ID, KrSecondaryProcesses.NameField)
                    .From(KrSecondaryProcesses.Name).NoLock();
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C(KrSecondaryProcesses.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }

                var stages = new List<IKrStageGroup>();
                await using (var reader = await db
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var spID = reader.GetGuid(0);
                        var groupID = spID;
                        stages.Add(new KrStageGroup(
                            groupID, // ID
                            reader.GetString(1), // Name
                            DefaultSecondaryProcessGroupOrder, // Order
                            true, // IsGroupReadonly
                            spID, // SecondaryProcessID
                            string.Empty, // SQLCondition
                            string.Empty, // RuntimeSQLCondition
                            string.Empty, // SourceCondition
                            string.Empty, // SourceBefore
                            string.Empty, // SourceAfter
                            string.Empty, // RuntimeSourceCondition
                            string.Empty, // RuntimeSourceBefore
                            string.Empty // RuntimeSourceAfter
                        ));
                    }
                }

                return stages;
            }
        }

        /// <summary>
        /// Возвращает список идентификаторов групп этапов удовлетворяющих условиям: типа карточки/документа, идентификатора пользователя, порядковый номер лежит в интервале [<paramref name="orderFrom"/>; <paramref name="orderTo"/>] и связанного со вторичным процессом имеющим указанный идентификатор.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="typeID">Идентификатр типа карточки или документа.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="orderFrom">Нижняя граница диапазона порядковых номеров.</param>
        /// <param name="orderTo">Верхняя граница диапазона порядковых номеров.</param>
        /// <param name="secondaryProcessID">ИД вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список идентификаторов групп этапов.</returns>
        public static async Task<List<Guid>> SelectFilteredStageGroupsAsync(
            IDbScope dbScope,
            Guid typeID,
            Guid userID,
            int? orderFrom = null,
            int? orderTo = null,
            Guid? secondaryProcessID = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C("t", KrStageGroups.ID)
                    .From(KrStageGroups.Name, "t").NoLock()
                    .LeftJoin(KrStageTypes.Name, "tt").NoLock()
                    .On().C("tt", KrStageTypes.ID).Equals().C("t", KrStageGroups.ID)
                    .Where()
                    .C("t", KrStageGroups.Ignore).Equals().V(BooleanBoxes.False)
                    .And().E(w => w
                        .C("tt", KrStageTypes.TypeID).IsNull()
                        .Or()
                        .C("tt", KrStageTypes.TypeID).Equals().P("TypeID"))
                    .And()
                    .E(w => w
                        .NotExists(e => e
                            .Select().V(null)
                            .From(KrStageRoles.Name, "r").NoLock()
                            .Where().C("r", KrStageRoles.ID).Equals().C("t", KrStageGroups.ID))
                        .Or()
                        .Exists(e => e
                            .Select().V(null)
                            .From(KrStageRoles.Name, "r").NoLock()
                            .InnerJoin("RoleUsers", "ru").NoLock()
                            .On().C("ru", "ID").Equals().C("r", KrStageRoles.RoleID)
                            .Where().C("r", KrStageRoles.ID).Equals().C("t", KrStageGroups.ID)
                            .And().C("ru", "UserID").Equals().P("UserID")));
                var parameters = new List<DataParameter>
                {
                    db.Parameter("TypeID", typeID),
                    db.Parameter("UserID", userID),
                };

                if (secondaryProcessID.HasValue)
                {
                    builder.And().C("t", KrStageGroups.KrSecondaryProcessID).Equals().P("processID");
                    parameters.Add(db.Parameter("processID", secondaryProcessID.Value));
                }
                else
                {
                    builder.And().C("t", KrStageGroups.KrSecondaryProcessID).IsNull();
                }

                if (orderFrom.HasValue)
                {
                    builder.And().C("t", KrStageGroups.Order).GreaterOrEquals().P("OrderFrom");
                    parameters.Add(db.Parameter("OrderFrom", orderFrom.Value));
                }

                if (orderTo.HasValue)
                {
                    builder.And().C("t", KrStageGroups.Order).LessOrEquals().P("OrderTo");
                    parameters.Add(db.Parameter("OrderTo", orderTo.Value));
                }

                builder.OrderBy("t", KrStageGroups.Order);

                var ids = await db
                    .SetCommand(builder.Build(), parameters.ToArray())
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken);
                if (secondaryProcessID != null
                    && (orderFrom is null || orderFrom <= 0)
                    && (orderTo is null || orderTo >= 0))
                {
                    ids.Add(secondaryProcessID.Value);
                }

                return ids;
            }
        }

        /// <summary>
        /// Возвращает список с информацией по рантайм скриптам этапов полученный из БД.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="stageSerializer"></param>
        /// <param name="extraSourceSerializer"></param>
        /// <param name="id">ID карточки шаблона этапов или значение по умолчанию для типа, если требуется получить информацию по всем шаблонам этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns></returns>
        public static async Task<List<IKrRuntimeStage>> SelectRuntimeStagesAsync(
            IDbScope dbScope,
            IKrStageSerializer stageSerializer,
            IExtraSourceSerializer extraSourceSerializer,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select()
                    .C("kst",
                        KrStageTemplates.ID, // 0
                        KrStageTemplates.NameField, // 1
                        KrStageTemplates.StageGroupID, // 2
                        KrStageTemplates.StageGroupName) // 3
                    .C("ksg",
                        KrStageGroups.Order) // 4
                    .C("ks",
                        KrStages.RowID, // 5
                        KrStages.NameField, // 6
                        KrStages.Order, // 7
                        KrStages.TimeLimit, // 8
                        KrStages.Planned, // 9
                        KrStages.Hidden, // 10
                        KrStages.StageTypeID, // 11
                        KrStages.StageTypeCaption, // 12
                        KrStages.SqlApproverRole, // 13
                        KrStages.Settings, // 14
                        KrStages.ExtraSources, // 15
                        KrStages.RuntimeSqlCondition, // 16
                        KrStages.RuntimeSourceCondition, // 17
                        KrStages.RuntimeSourceBefore, // 18
                        KrStages.RuntimeSourceAfter, // 19
                        KrStages.Skip, // 20
                        KrStages.CanBeSkipped) // 21
                    .From(KrStages.Name, "ks").NoLock()
                    .InnerJoin(KrStageTemplates.Name, "kst").NoLock()
                    .On().C("ks", KrStages.ID).Equals().C("kst", "ID")
                    .LeftJoin(KrStageGroups.Name, "ksg").NoLock()
                    .On().C("kst", KrStageTemplates.StageGroupID).Equals().C("ksg", "ID");
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C("kst", KrStageTemplates.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }

                return await ReadRuntimeStagesAsync(db, stageSerializer, extraSourceSerializer, cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает список с информацией по рантайм скриптам этапов вторичных процессов полученный из БД.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="stageSerializer"></param>
        /// <param name="extraSourceSerializer"></param>
        /// <param name="id">Идентификатор этапа вторичного процесса для которого требуется получить список этапов или значение по умолчанию для типа, если требуется получить информацию обо всех этапах всех вторичных процессов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список этапов вторичных процессов.</returns>
        public static async Task<List<IKrRuntimeStage>> SelectSecondaryProcessRuntimeStagesAsync(
            IDbScope dbScope,
            IKrStageSerializer stageSerializer,
            IExtraSourceSerializer extraSourceSerializer,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select()
                    .C("ksp",
                        KrSecondaryProcesses.ID, // 0
                        KrSecondaryProcesses.NameField, // 1
                        KrSecondaryProcesses.ID, // 2
                        KrSecondaryProcesses.NameField) // 3
                    .V(KrConstants.DefaultSecondaryProcessGroupOrder) // 4 - StageGroupOrder
                    .C("ks",
                        KrStages.RowID, // 5
                        KrStages.NameField, // 6
                        KrStages.Order, // 7
                        KrStages.TimeLimit, // 8
                        KrStages.Planned, // 9
                        KrStages.Hidden, // 10
                        KrStages.StageTypeID, // 11
                        KrStages.StageTypeCaption, // 12
                        KrStages.SqlApproverRole, // 13
                        KrStages.Settings, // 14
                        KrStages.ExtraSources, // 15
                        KrStages.RuntimeSqlCondition, // 16
                        KrStages.RuntimeSourceCondition, // 17
                        KrStages.RuntimeSourceBefore, // 18
                        KrStages.RuntimeSourceAfter, // 19
                        KrStages.Skip, // 20
                        KrStages.CanBeSkipped) // 21
                    .From(KrStages.Name, "ks").NoLock()
                    .InnerJoin(KrSecondaryProcesses.Name, "ksp").NoLock()
                    .On().C("ks", KrStages.ID).Equals().C("ksp", KrSecondaryProcesses.ID);
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C("ksp", KrSecondaryProcesses.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }

                return await ReadRuntimeStagesAsync(db, stageSerializer, extraSourceSerializer, cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает информацию по вторичному процессу из БД.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="secondaryProcessID">ИД вторичного процесса или значение по умолчанию для типа, если требуется получить информацию по всем вторичным процессам.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Кортеж содержащий информацию по: простым процессам, действия, кнопкам вторичных процессов.</returns>
        public static async Task<(List<IKrPureProcess> pureProcesses, List<IKrAction> actions, List<IKrProcessButton> buttons)> SelectKrSecondaryProcessesAsync(
            IDbScope dbScope,
            Guid? secondaryProcessID,
            CancellationToken cancellationToken = default)
        {
            var pureProcesses = new List<IKrPureProcess>();
            var actions = new List<IKrAction>();
            var buttons = new List<IKrProcessButton>();

            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var query = dbScope.BuilderFactory
                    .Select()
                    .C(null,
                        KrSecondaryProcesses.ID,
                        KrSecondaryProcesses.NameField,
                        KrSecondaryProcesses.ModeID,
                        KrSecondaryProcesses.IsGlobal,
                        KrSecondaryProcesses.Async,
                        KrSecondaryProcesses.Caption,
                        KrSecondaryProcesses.Icon,
                        KrSecondaryProcesses.TileSizeID,
                        KrSecondaryProcesses.Tooltip,
                        KrSecondaryProcesses.TileGroup,
                        KrSecondaryProcesses.ExecutionAccessDeniedMessage,
                        KrSecondaryProcesses.RefreshAndNotify,
                        KrSecondaryProcesses.AskConfirmation,
                        KrSecondaryProcesses.ConfirmationMessage,
                        KrSecondaryProcesses.ActionGrouping,
                        KrSecondaryProcesses.VisibilitySqlCondition,
                        KrSecondaryProcesses.ExecutionSqlCondition,
                        KrSecondaryProcesses.VisibilitySourceCondition,
                        KrSecondaryProcesses.ExecutionSourceCondition,
                        KrSecondaryProcesses.ActionEventType,
                        KrSecondaryProcesses.AllowClientSideLaunch,
                        KrSecondaryProcesses.CheckRecalcRestrictions,
                        KrSecondaryProcesses.RunOnce,
                        KrSecondaryProcesses.ButtonHotkey,
                        KrSecondaryProcesses.Order,
                        KrSecondaryProcesses.Conditions,
                        KrSecondaryProcesses.NotMessageHasNoActiveStages)
                    .From(KrSecondaryProcesses.Name).NoLock();

                if (secondaryProcessID is null)
                {
                    db.SetCommand(query.Build());
                }
                else
                {
                    db.SetCommand(
                        query.Where().C(KrSecondaryProcesses.ID).Equals().P("ID").Build(),
                        db.Parameter("ID", secondaryProcessID));
                }

                db.LogCommand();

                var spData = new List<KrSecondaryProcessData>();
                await using (var reader = await db.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var data = new KrSecondaryProcessData();
                        data.Read(reader);
                        spData.Add(data);
                    }
                }

                if (spData.Count == 0)
                {
                    return (pureProcesses, actions, buttons);
                }

                var processIDs = spData.Select(p => p.ID).ToArray();
                var contextRoles = await ReadContextRolesAsync(dbScope, processIDs, cancellationToken);

                foreach (var secProcess in spData)
                {
                    var processContextRoles = contextRoles.TryGetValue(secProcess.ID, out var cr)
                        ? (ICollection<Guid>) cr
                        : EmptyHolder<Guid>.Collection;

                    secProcess.ContextRolesIDs = processContextRoles;

                    if (secProcess.Mode == KrSecondaryProcessModes.PureProcess.ID)
                    {
                        pureProcesses.Add(secProcess.ToPureProcess());
                    }
                    else if (secProcess.Mode == KrSecondaryProcessModes.Action.ID)
                    {
                        actions.Add(secProcess.ToAction());
                    }
                    else if (secProcess.Mode == KrSecondaryProcessModes.Button.ID)
                    {
                        buttons.Add(secProcess.ToProcessButton());
                    }
                    else
                    {
                        throw new InvalidOperationException("Undefined secondary process mode.");
                    }
                }
            }

            return (pureProcesses, actions, buttons);
        }

        /// <summary>
        /// Возвращает список базовых методов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="id">ИД метода или значение по умолчанию для типа, если требуется получить информацию по всем базовым методам.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список базовых методов.</returns>
        public static async Task<List<IKrCommonMethod>> SelectCommonMethodsAsync(
            IDbScope dbScope,
            Guid? id = null,
            CancellationToken cancellationToken = default)
        {
            var methods = new List<IKrCommonMethod>();
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory
                    .Select().C(null, KrStageCommonMethods.ID, KrStageCommonMethods.NameField, KrStageCommonMethods.Source)
                    .From(KrStageCommonMethods.Name).NoLock();
                if (id == null)
                {
                    db.SetCommand(
                        builder.Build());
                }
                else
                {
                    db.SetCommand(
                        builder
                            .Where().C(KrStageCommonMethods.ID).Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id));
                }

                await using var reader = await db
                    .LogCommand()
                    .ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    methods.Add(new KrCommonMethod(
                        reader.GetGuid(0),          // ID
                        reader.GetString(1),        // Name
                        reader.GetNullableString(2) // Source
                    ));
                }
            }

            return methods;
        }

        #endregion

        #region Private Methods

        private static async Task<Dictionary<Guid, List<Guid>>> ReadContextRolesAsync(
            IDbScope dbScope,
            Guid[] processIDs,
            CancellationToken cancellationToken = default)
        {
            var contextRolesQuery = dbScope.BuilderFactory
                .Select()
                    .C(null, KrSecondaryProcessRoles.RoleID, KrSecondaryProcessRoles.ID)
                .From(KrSecondaryProcessRoles.Name).NoLock()
                .Where()
                    .C(KrSecondaryProcessRoles.ID).InArray(processIDs, "ProcessIDs", out var dpIDs)
                    .And()
                    .C(KrSecondaryProcessRoles.IsContext).Equals().V(true)
                .Build();
            dbScope.Db.SetCommand(contextRolesQuery, DataParameters.Get(dpIDs)).LogCommand();
            var contextRolesButtons = new Dictionary<Guid, List<Guid>>((int) (1.5 * processIDs.Length));
            await using var reader = await dbScope.Db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var roleID = reader.GetGuid(0);
                var buttonID = reader.GetGuid(1);
                if (!contextRolesButtons.TryGetValue(buttonID, out var contextRolesList))
                {
                    contextRolesList = new List<Guid>();
                    contextRolesButtons.Add(buttonID, contextRolesList);
                }

                contextRolesList.Add(roleID);
            }

            return contextRolesButtons;
        }

        private static async Task<List<IKrRuntimeStage>> ReadRuntimeStagesAsync(
            DbManager db,
            IKrStageSerializer stageSerializer,
            IExtraSourceSerializer extraSourceSerializer,
            CancellationToken cancellationToken = default)
        {
            var stages = new List<IKrRuntimeStage>();
            await using (var reader = await db
                .LogCommand()
                .ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    stages.Add(new KrRuntimeStage(
                        templateID: reader.GetGuid(0),
                        templateName: reader.GetNullableString(1),
                        groupID: reader.GetGuid(2),
                        groupName: reader.GetNullableString(3),
                        groupOrder: reader.GetNullableInt32(4) ?? 0,
                        stageID: reader.GetGuid(5),
                        stageName: reader.GetNullableString(6),
                        order: reader.GetNullableInt32(7),
                        timeLimit: reader.GetNullableDouble(8),
                        planned: reader.GetNullableDateTimeUtc(9),
                        hidden: reader.GetBoolean(10),
                        stageTypeID: reader.GetGuid(11),
                        stageTypeCaption: reader.GetNullableString(12),
                        sqlRoles: reader.GetNullableString(13),
                        settings: reader.GetNullableString(14),
                        extraSources: reader.GetNullableString(15),
                        runtimeSqlCondition: reader.GetNullableString(16),
                        sourceCondition: reader.GetNullableString(17),
                        sourceBefore: reader.GetNullableString(18),
                        sourceAfter: reader.GetNullableString(19),
                        skip: reader.GetBoolean(20),
                        canBeSkipped: reader.GetBoolean(21),
                        extraSourceSerializer: extraSourceSerializer,
                        serializer: stageSerializer));
                }
            }

            return stages;
        }

        #endregion

    }
}
