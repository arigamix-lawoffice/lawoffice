using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Абстрактный класс предоставляющий свойства и методы доступные в скриптах маршрутов.
    /// </summary>
    public abstract class KrScript : IKrScript
    {
        #region fields

        internal const string ExtraMethodsName = nameof(ExtraMethods);

        // ReSharper disable once CollectionNeverUpdated.Global
        protected readonly Dictionary<string, Func<object, ValueTask<object>>> ExtraMethods = new
            Dictionary<string, Func<object, ValueTask<object>>>(StringComparer.Ordinal);

        private IKrSecondaryProcess secondaryProcess;
        private Guid stageGroupID;
        private string stageGroupName;
        private int stageGroupOrder;
        private Guid templateID;
        private string templateName;
        private int order;
        private GroupPosition position;
        private bool canChangeOrder;
        private bool isStagesReadonly;

        private StagesContainer stagesContainer;
        private WorkflowProcess workflowProcess;
        private Stage currentStage;

        private ICardExtensionContext cardContext;
        private ISession session;
        private IDbScope dbScope;
        private IUnityContainer unityContainer;
        private IValidationResultBuilder validationResult;
        private Guid? processID;
        private string processTypeName;
        private Card contextualSatellite;
        private Card processHolderSatellite;
        private ICardMetadata cardMetadata;
        private IKrScope krScope;

        private bool alreadyTriedToLoadContextualSatellite = false;
        private IKrTypesCache krTypesCache;
        private ICardCache cardCache;
        private IMainCardAccessStrategy mainCardAccessStrategy;
        private Guid cardID;
        private CardType cardType;
        private Guid docTypeID;
        private IKrTaskHistoryResolver taskHistoryResolver;
        private KrProcessRunnerInitiationCause? initiationCause;
        private KrComponents? krComponents;
        private IWorkflowProcessInfo workflowProcessInfo;
        private IKrStageSerializer stageSerializer;

        #endregion

        #region IKrScope Members

        /// <inheritdoc />
        public Guid? ProcessID
        {
            get => this.processID;
            set
            {
                this.CheckSealed();
                this.processID = value;
            }
        }

        /// <inheritdoc />
        public string ProcessTypeName
        {
            get => this.processTypeName;
            set
            {
                this.CheckSealed();
                this.processTypeName = value;
            }
        }

        /// <inheritdoc />
        public KrProcessRunnerInitiationCause InitiationCause
        {
            get => this.initiationCause ??
                throw new InvalidOperationException(
                    $"{nameof(this.InitiationCause)} available only in runtime scripts.");
            set
            {
                this.CheckSealed();
                this.initiationCause = value;
            }
        }

        /// <inheritdoc />
        public async ValueTask<Card> GetContextualSatelliteAsync()
        {
            if (!this.alreadyTriedToLoadContextualSatellite
                && this.contextualSatellite is null
                && this.CardID != Guid.Empty)
            {
                this.contextualSatellite = await this.krScope.GetKrSatelliteAsync(this.cardID, cancellationToken: this.CancellationToken);
                this.alreadyTriedToLoadContextualSatellite = true;
            }

            return this.contextualSatellite;
        }

        /// <inheritdoc />
        public void SetContextualSatellite(Card contextualSatellite)
        {
            this.CheckSealed();
            this.contextualSatellite = contextualSatellite;
        }

        /// <inheritdoc />
        public Card ProcessHolderSatellite
        {
            get => this.processHolderSatellite;
            set
            {
                this.CheckSealed();
                this.processHolderSatellite = value;
            }
        }

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess
        {
            get => this.secondaryProcess;
            set
            {
                this.CheckSealed();
                this.secondaryProcess = value;
            }
        }

        /// <inheritdoc />
        public IKrPureProcess PureProcess => this.secondaryProcess as IKrPureProcess;

        /// <inheritdoc />
        public IKrProcessButton Button => this.secondaryProcess as IKrProcessButton;

        /// <inheritdoc />
        public IKrAction Action => this.secondaryProcess as IKrAction;

        /// <inheritdoc />
        public Guid StageGroupID
        {
            get => this.stageGroupID;
            set
            {
                this.CheckSealed();
                this.stageGroupID = value;
            }
        }

        /// <inheritdoc />
        public string StageGroupName
        {
            get => this.stageGroupName;
            set
            {
                this.CheckSealed();
                this.stageGroupName = value;
            }
        }

        /// <inheritdoc />
        public int StageGroupOrder
        {
            get => this.stageGroupOrder;
            set
            {
                this.CheckSealed();
                this.stageGroupOrder = value;
            }
        }

        /// <inheritdoc />
        public Guid TemplateID
        {
            get => this.templateID;
            set
            {
                this.CheckSealed();
                this.templateID = value;
            }
        }

        /// <inheritdoc />
        public string TemplateName
        {
            get => this.templateName;
            set
            {
                this.CheckSealed();
                this.templateName = value;
            }
        }

        /// <inheritdoc />
        public int Order
        {
            get => this.order;
            set
            {
                this.CheckSealed();
                this.order = value;
            }
        }

        /// <inheritdoc />
        public GroupPosition Position
        {
            get => this.position;
            set
            {
                this.CheckSealed();
                this.position = value;
            }
        }

        /// <inheritdoc />
        public bool CanChangeOrder
        {
            get => this.canChangeOrder;
            set
            {
                this.CheckSealed();
                this.canChangeOrder = value;
            }
        }

        /// <inheritdoc />
        public bool IsStagesReadonly
        {
            get => this.isStagesReadonly;
            set
            {
                this.CheckSealed();
                this.isStagesReadonly = value;
            }
        }

        /// <inheritdoc />
        public StagesContainer StagesContainer
        {
            get => this.stagesContainer;
            set
            {
                this.CheckSealed();
                this.stagesContainer = value;
            }
        }

        /// <inheritdoc />
        public WorkflowProcess WorkflowProcess
        {
            get => this.workflowProcess;
            set
            {
                this.CheckSealed();
                this.workflowProcess = value;
            }
        }

        /// <inheritdoc />
        public SealableObjectList<Stage> InitialStages => this.WorkflowProcess.InitialWorkflowProcess.Stages;

        /// <inheritdoc />
        public SealableObjectList<Stage> Stages => this.WorkflowProcess.Stages;

        /// <inheritdoc />
        public ReadOnlyCollection<Stage> CurrentStages
        {
            get
            {
                if (this.Stage is not null)
                {
                    return new ReadOnlyCollection<Stage>(new[] { this.Stage });
                }

                if (this.TemplateID != Guid.Empty)
                {
                    return new ReadOnlyCollection<Stage>(
                        this.WorkflowProcess.Stages.Where(p => p.TemplateID == this.templateID).ToList());
                }

                return new ReadOnlyCollection<Stage>(
                    this.WorkflowProcess.Stages.Where(p => p.StageGroupID == this.StageGroupID).ToList());
            }
        }

        /// <inheritdoc />
        public Stage Stage
        {
            get => this.currentStage;
            set
            {
                this.CheckSealed();
                this.currentStage = value;
            }
        }

        /// <inheritdoc />
        public ValueTask<int> GetCycleAsync() =>
            UserAPIHelper.GetCycleAsync(this);

        /// <inheritdoc />
        public ValueTask<bool> SetCycleAsync(int newValue)
        {
            this.CheckSealed();
            return UserAPIHelper.SetCycleAsync(this, newValue);
        }

        /// <inheritdoc />
        public Author Initiator
        {
            get => this.WorkflowProcess.Author;
            set
            {
                this.CheckSealed();
                this.WorkflowProcess.Author = value;
            }
        }

        /// <inheritdoc />
        public string InitiatorComment
        {
            get => this.WorkflowProcess.AuthorComment;
            set
            {
                this.CheckSealed();
                this.WorkflowProcess.AuthorComment = value;
            }
        }

        /// <inheritdoc />
        public Author ProcessOwner
        {
            get => this.WorkflowProcess.ProcessOwner;
            set
            {
                this.CheckSealed();
                this.WorkflowProcess.ProcessOwner = value;
            }
        }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy
        {
            get => this.mainCardAccessStrategy;
            set
            {
                this.CheckSealed();
                this.mainCardAccessStrategy = value;
            }
        }

        /// <inheritdoc />
        public ValueTask<Card> GetCardObjectAsync() => this.mainCardAccessStrategy.GetCardAsync(cancellationToken: this.CancellationToken);

        /// <inheritdoc />
        public IWorkflowProcessInfo WorkflowProcessInfo
        {
            get => this.workflowProcessInfo;
            set
            {
                this.CheckSealed();
                this.workflowProcessInfo = value;
            }
        }

        /// <inheritdoc />
        public IWorkflowTaskInfo WorkflowTaskInfo => this.workflowProcessInfo as IWorkflowTaskInfo;

        /// <inheritdoc />
        public IWorkflowSignalInfo WorkflowSignalInfo => this.workflowProcessInfo as IWorkflowSignalInfo;

        /// <inheritdoc />
        public Guid CardID
        {
            get => this.cardID;
            set
            {
                this.CheckSealed();
                this.cardID = value;
            }
        }

        /// <inheritdoc />
        public Guid CardTypeID => this.cardType?.ID ?? Guid.Empty;

        /// <inheritdoc />
        public string CardTypeName => this.cardType?.Name;

        /// <inheritdoc />
        public string CardTypeCaption => this.cardType?.Caption;

        /// <inheritdoc />
        public CardType CardType
        {
            get => this.cardType;
            set
            {
                this.CheckSealed();
                this.cardType = value;
            }
        }

        /// <inheritdoc />
        public Guid DocTypeID
        {
            get => this.docTypeID;
            set
            {
                this.CheckSealed();
                this.docTypeID = value;
            }
        }

        /// <inheritdoc />
        public Guid TypeID =>
            this.DocTypeID != Guid.Empty
                ? this.DocTypeID
                : this.CardTypeID;

        /// <inheritdoc />
        public KrComponents KrComponents
        {
            get => this.krComponents ??
                throw new InvalidOperationException($"{nameof(this.KrComponents)} available only in local scripts.");
            set
            {
                this.CheckSealed();
                this.krComponents = value;
            }
        }

        /// <inheritdoc />
        public async ValueTask<int> GetVersionAsync() => (await this.mainCardAccessStrategy.GetCardAsync(cancellationToken: this.CancellationToken))?.Version ?? -1;

        /// <inheritdoc />
        public async ValueTask<dynamic> GetCardAsync() => (await this.mainCardAccessStrategy.GetCardAsync(cancellationToken: this.CancellationToken))?.DynamicEntries;

        /// <inheritdoc />
        public async ValueTask<dynamic> GetCardTablesAsync() => (await this.mainCardAccessStrategy.GetCardAsync(cancellationToken: this.CancellationToken))?.DynamicTables;

        /// <inheritdoc />
        public async ValueTask<ListStorage<CardFile>> GetFilesAsync() => (await this.mainCardAccessStrategy.GetCardAsync(cancellationToken: this.CancellationToken))?.Files;

        /// <inheritdoc />
        public ICardExtensionContext CardContext
        {
            get => this.cardContext;
            set
            {
                this.CheckSealed();
                this.cardContext = value;
            }
        }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult
        {
            get => this.validationResult;
            set
            {
                this.CheckSealed();
                this.validationResult = value;
            }
        }

        /// <inheritdoc />
        public IDictionary<string, object> Info { get; } = new Dictionary<string, object>(StringComparer.Ordinal);

        /// <inheritdoc />
        public IDictionary<string, object> StageInfoStorage => this.Stage?.InfoStorage;

        /// <inheritdoc />
        public dynamic StageInfo => this.Stage?.Info;

        /// <inheritdoc />
        public IDictionary<string, object> ProcessInfoStorage => this.WorkflowProcess.InfoStorage;

        /// <inheritdoc />
        public dynamic ProcessInfo => this.WorkflowProcess.Info;

        /// <inheritdoc />
        public ValueTask<Guid?> GetCurrentTaskHistoryGroupAsync() => UserAPIHelper.GetCurrentTaskHistoryGroupAsync(this);

        /// <inheritdoc />
        public IDictionary<string, object> MainProcessInfoStorage => this.WorkflowProcess.MainProcessInfoStorage;

        /// <inheritdoc />
        public dynamic MainProcessInfo => this.WorkflowProcess.MainProcessInfo;

        /// <inheritdoc />
        public async ValueTask<dynamic> NewCardAsync() => (await this.GetNewCardAsync())?.Entries;

        /// <inheritdoc />
        public async ValueTask<dynamic> NewCardTablesAsync() => (await this.GetNewCardAsync())?.Tables;

        /// <inheritdoc />
        public ISession Session
        {
            get => this.session;
            set
            {
                this.CheckSealed();
                this.session = value;
            }
        }

        /// <inheritdoc />
        public IDbScope DbScope
        {
            get => this.dbScope;
            set
            {
                this.CheckSealed();
                this.dbScope = value;
            }
        }

        /// <inheritdoc />
        public DbManager Db => this.DbScope.Db;

        /// <inheritdoc />
        public IUnityContainer UnityContainer
        {
            get => this.unityContainer;
            set
            {
                this.CheckSealed();
                this.unityContainer = value;
            }
        }

        /// <inheritdoc />
        public ICardMetadata CardMetadata
        {
            get => this.cardMetadata;
            set
            {
                this.CheckSealed();
                this.cardMetadata = value;
            }
        }

        /// <inheritdoc />
        public IKrScope KrScope
        {
            get => this.krScope;
            set
            {
                this.CheckSealed();
                this.krScope = value;
            }
        }

        /// <inheritdoc />
        public ICardCache CardCache
        {
            get => this.cardCache;
            set
            {
                this.CheckSealed();
                this.cardCache = value;
            }
        }

        /// <inheritdoc />
        public IKrTypesCache KrTypesCache
        {
            get => this.krTypesCache;
            set
            {
                this.CheckSealed();
                this.krTypesCache = value;
            }
        }

        /// <inheritdoc />
        public ICardTaskHistoryManager TaskHistoryManager => this.taskHistoryResolver.TaskHistoryManager;

        /// <inheritdoc />
        public IKrTaskHistoryResolver TaskHistoryResolver
        {
            get => this.taskHistoryResolver;
            set
            {
                this.CheckSealed();
                this.taskHistoryResolver = value;
            }
        }

        /// <inheritdoc />
        public IKrStageSerializer StageSerializer
        {
            get => this.stageSerializer;
            set
            {
                this.CheckSealed();
                this.stageSerializer = value;
            }
        }

        /// <inheritdoc />
        public bool Confirmed { get; set; }

        /// <inheritdoc />
        public KrScriptType KrScriptType { get; set; }

        /// <inheritdoc />
        public ValueTask<ListStorage<CardRow>> CardRowsAsync(
            string sectionName) => UserAPIHelper.CardRowsAsync(this, sectionName);

        /// <inheritdoc />
        public bool IsMainProcess() => UserAPIHelper.IsMainProcess(this);

        /// <inheritdoc />
        public ValueTask<bool> IsMainProcessStartedAsync() => UserAPIHelper.IsMainProcessStartedAsync(this);

        /// <inheritdoc />
        public ValueTask<bool> IsMainProcessInactiveAsync() => UserAPIHelper.IsMainProcessInactiveAsync(this, this.contextualSatellite);

        /// <inheritdoc />
        public Task RunNextStageInContextAsync(
            Guid cID,
            bool wholeCurrentGroup = false,
            IDictionary<string, object> processInfo = null) =>
            UserAPIContextChangeableHelper.RunNextStageInContextAsync(this, this, cID, wholeCurrentGroup, processInfo);

        /// <inheritdoc />
        public bool ContextChangePending() => UserAPIContextChangeableHelper.ContextChangePending(this);

        /// <inheritdoc />
        public void DoNotChangeContext() => UserAPIContextChangeableHelper.DoNotChangeContext(this);

        /// <inheritdoc />
        public T Resolve<T>(
            string name = null) => UserAPIHelper.Resolve<T>(this.unityContainer, name);

        /// <inheritdoc />
        public void Show(
            object obj) => UserAPIIOHelper.Show(this, obj);

        /// <inheritdoc />
        public void Show(
            string message,
            string details = "") => UserAPIIOHelper.Show(this, message, details);

        /// <inheritdoc />
        public void Show(
            Stage stage) => UserAPIIOHelper.Show(this, stage);

        /// <inheritdoc />
        public void Show(
            IEnumerable<Stage> stages) => UserAPIIOHelper.Show(this, stages);

        /// <inheritdoc />
        public void Show(
            Performer performer) => UserAPIIOHelper.Show(this, performer);

        /// <inheritdoc />
        public void Show(
            IEnumerable<Performer> performers) => UserAPIIOHelper.Show(this, performers);

        /// <inheritdoc />
        public void Show(
            IDictionary<string, object> storage) => UserAPIIOHelper.Show(this, storage);

        /// <inheritdoc />
        public void Show(
            IStorageDictionaryProvider storage) => UserAPIIOHelper.Show(this, storage);

        /// <inheritdoc />
        public void AddError(
            string text) => UserAPIIOHelper.AddError(this, text);

        /// <inheritdoc />
        public void AddWarning(
            string text) => UserAPIIOHelper.AddWarning(this, text);

        /// <inheritdoc />
        public void AddInfo(
            string text) => UserAPIIOHelper.AddInfo(this, text);

        /// <inheritdoc />
        public void ForEachStage(
            Action<CardRow> rowAction,
            bool withNesteds = false) => UserAPIHelper.ForEachStage(this, rowAction, withNesteds);

        /// <inheritdoc />
        public Task ForEachStageInMainProcessAsync(
            Func<CardRow, Task> rowActionAsync,
            bool withNesteds = false) => UserAPIHelper.ForEachStageInMainProcessAsync(this, rowActionAsync, withNesteds);

        /// <inheritdoc />
        public Task SetStageStateAsync(
            CardRow stage,
            KrStageState stageState) => UserAPIHelper.SetStageStateAsync(this, stage, stageState);

        /// <inheritdoc />
        public ValueTask<Stage> GetOrAddStageAsync(
            string name,
            StageTypeDescriptor descriptor,
            int pos = int.MaxValue) =>
            UserAPIHelper.GetOrAddStageAsync(this, name, descriptor, pos);

        /// <inheritdoc />
        public ValueTask<Stage> AddStageAsync(
            string name,
            StageTypeDescriptor descriptor,
            int pos = int.MaxValue) =>
            UserAPIHelper.AddStageAsync(this, name, descriptor, pos);

        /// <inheritdoc />
        public bool RemoveStage(
            string name) =>
            UserAPIHelper.RemoveStage(this, name);

        /// <inheritdoc />
        public void SetSinglePerformer(
            Guid id,
            string name,
            Stage intoStage,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.SetSinglePerformer(id, name, intoStage, ignoreManualChanges);

        /// <inheritdoc />
        public void SetSinglePerformer(
            string id,
            string name,
            Stage intoStage,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.SetSinglePerformer(Guid.Parse(id), name, intoStage, ignoreManualChanges);

        /// <inheritdoc />
        public void ResetSinglePerformer(
            Stage stage,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.ResetSinglePerformer(stage, ignoreManualChanges);

        /// <inheritdoc />
        public Performer AddPerformer(
            Guid id,
            string name,
            Stage intoStage,
            int pos = int.MaxValue,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.AddPerformer(id, name, intoStage, pos, ignoreManualChanges);

        /// <inheritdoc />
        public Performer AddPerformer(
            string id,
            string name,
            Stage intoStage,
            int pos = int.MaxValue,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.AddPerformer(Guid.Parse(id), name, intoStage, pos, ignoreManualChanges);

        /// <inheritdoc />
        public void RemovePerformer(
            Guid performerID,
            Stage stage,
            bool ignoreManualChanges = false) =>
            UserAPIHelper.RemovePerformer(
                stage.Performers.Where(p => p.PerformerID == performerID).Select(p => p.RowID).ToArray(),
                stage,
                ignoreManualChanges);

        /// <inheritdoc />
        public void RemovePerformer(
            string performerID,
            Stage stage,
            bool ignoreManualChanges = false) =>
            this.RemovePerformer(Guid.Parse(performerID), stage, ignoreManualChanges);

        /// <inheritdoc />
        public void RemovePerformer(
            int index,
            Stage stage,
            bool ignoreManualChanges = false)
        {
            Check.ArgumentNotNull(stage, nameof(stage));

            UserAPIHelper.RemovePerformer(
                new[] { stage.Performers[index].RowID },
                stage,
                ignoreManualChanges);
        }

        /// <inheritdoc />
        public async Task AddTaskHistoryRecordAsync(
            Guid typeID,
            string typeName,
            string typeCaption,
            Guid optionID,
            string result = null,
            Guid? performerID = null,
            string performerName = null,
            int? cycle = null,
            int? timeZoneID = null,
            TimeSpan? timeZoneUTCOffset = null,
            Guid? calendarID = null,
            Func<CardTaskHistoryItem, ValueTask> modifyActionAsync = null)
        {
            await UserAPIHelper.AddTaskHistoryRecordAsync(
                this,
                await this.GetCurrentTaskHistoryGroupAsync(),
                typeID,
                typeName,
                typeCaption,
                optionID,
                result,
                performerID,
                performerName,
                cycle,
                timeZoneID,
                timeZoneUTCOffset,
                calendarID,
                modifyActionAsync);
        }

        /// <inheritdoc />
        public Task AddTaskHistoryRecordAsync(
            Guid? taskHistoryGroup,
            Guid typeID,
            string typeName,
            string typeCaption,
            Guid optionID,
            string result = null,
            Guid? performerID = null,
            string performerName = null,
            int? cycle = null,
            int? timeZoneID = null,
            TimeSpan? timeZoneUTCOffset = null,
            Guid? calendarID = null,
            Func<CardTaskHistoryItem, ValueTask> modifyActionAsync = null) =>
            UserAPIHelper.AddTaskHistoryRecordAsync(
                this,
                taskHistoryGroup,
                typeID,
                typeName,
                typeCaption,
                optionID,
                result,
                performerID,
                performerName,
                cycle,
                timeZoneID,
                timeZoneUTCOffset,
                calendarID,
                modifyActionAsync);

        /// <inheritdoc />
        public CardTaskHistoryGroup ResolveTaskHistoryGroup(
            Guid groupTypeID,
            Guid? parentGroupTypeID = null,
            bool newIteration = false) =>
            UserAPIHelper.ResolveTaskHistoryGroup(this, groupTypeID, parentGroupTypeID, newIteration);

        /// <inheritdoc />
        public bool HasKrComponents(
            KrComponents components) => UserAPIHelper.HasKrComponents(this, components);

        /// <inheritdoc />
        public bool HasKrComponents(
            params KrComponents[] components) => UserAPIHelper.HasKrComponents(this, components);

        /// <inheritdoc />
        public ValueTask<ISerializableObject> GetPrimaryProcessInfoAsync(
            Guid? mainCardID = null) => UserAPIHelper.GetPrimaryProcessInfoAsync(this, mainCardID);

        /// <inheritdoc />
        public ValueTask<ISerializableObject> GetSecondaryProcessInfoAsync(
            Guid secondaryProcessID,
            Guid? mainCardID = null) => UserAPIHelper.GetSecondaryProcessInfoAsync(this, secondaryProcessID, mainCardID);

        /// <inheritdoc />
        public async ValueTask InvokeExtraAsync(
            string name,
            object context,
            bool throwOnError = true)
        {
            await this.InvokeExtraAsync<object>(name, context, throwOnError);
        }

        /// <inheritdoc />
        public async ValueTask<T> InvokeExtraAsync<T>(
            string name,
            object context,
            bool throwOnError = true)
        {
            Check.ArgumentNotNull(name, nameof(name));

            if (!this.ExtraMethods.TryGetValue(name, out var method))
            {
                if (throwOnError)
                {
                    throw new KeyNotFoundException(
                        $"Method {name} does not initialized properly in {this.GetType().FullName}.");
                }

                return default;
            }

            return (T) await method.Invoke(context);
        }

        /// <inheritdoc />
        public ValueTask<Card> GetNewCardAsync() => UserAPIHelper.GetNewCardAsync(this);

        /// <inheritdoc />
        public IMainCardAccessStrategy GetNewCardAccessStrategy() => UserAPIHelper.GetNewCardAccessStrategy(this);

        /// <inheritdoc />
        public IDictionary<string, object> GetProcessInfoForBranch(
            Guid rowID) => UserAPIHelper.GetProcessInfoForBranch(this, rowID);

        /// <inheritdoc />
        public IDictionary<string, object> GetProcessInfoForBranch(
            string rowID) => UserAPIHelper.GetProcessInfoForBranch(this, Guid.Parse(rowID));

        #endregion

        #region IKrProcessItemScript Members

        /// <inheritdoc />
        public virtual Task RunBeforeAsync() => UserAPIKrProcessItemHelper.RunBeforeAsync(this, this);

        /// <inheritdoc />
        public virtual Task BeforeAsync() => UserAPIKrProcessItemHelper.DefaultActionAsync();

        /// <inheritdoc />
        public virtual Task RunAfterAsync() => UserAPIKrProcessItemHelper.RunAfterAsync(this, this);

        /// <inheritdoc />
        public virtual Task AfterAsync() => UserAPIKrProcessItemHelper.DefaultActionAsync();

        /// <inheritdoc />
        public ValueTask<bool> RunConditionAsync() => UserAPIKrProcessItemHelper.RunConditionAsync(this, this);

        /// <inheritdoc />
        public virtual ValueTask<bool> ConditionAsync() => new ValueTask<bool>(UserAPIKrProcessItemHelper.DefaultActionAsync());

        #endregion

        #region IKrSecondaryProcessScript Members

        /// <inheritdoc />
        public ValueTask<bool> RunVisibilityAsync() => UserAPIKrProcessVisibilityHelper.RunVisibilityAsync(this, this);

        /// <inheritdoc />
        public virtual ValueTask<bool> VisibilityAsync() => UserAPIKrProcessVisibilityHelper.DefaultActionAsync();

        /// <inheritdoc />
        public ValueTask<bool> RunExecutionAsync() => UserAPIKrProcessExecutionHelper.RunExecutionAsync(this, this);

        /// <inheritdoc />
        public virtual ValueTask<bool> ExecutionAsync() => UserAPIKrProcessVisibilityHelper.DefaultActionAsync();

        #endregion

        #region IContextChangeableScript Members

        /// <inheritdoc />
        public Guid? DifferentContextCardID { get; set; }

        /// <inheritdoc />
        public bool DifferentContextWholeCurrentGroup { get; set; }

        /// <inheritdoc />
        public KrScriptType? DifferentContextSetupScriptType { get; set; }

        /// <inheritdoc />
        public IDictionary<string, object> DifferentContextProcessInfo { get; set; }

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed { get; private set; } = false;

        /// <inheritdoc/>
        public void Seal()
        {
            this.IsSealed = true;
        }

        /// <inheritdoc/>
        private void CheckSealed()
        {
            Check.ObjectNotSealed(this);
        }

        #endregion

        #region IExtensionContext Members

        /// <inheritdoc/>
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}
