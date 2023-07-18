using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Предоставляет объектную модель процесса.
    /// </summary>
    public sealed class WorkflowProcess :
        IEquatable<WorkflowProcess>,
        ISealable
    {
        #region Fields

        private string authorComment;
        private KrState state;
        private SealableObjectList<Stage> stages;
        private readonly Lazy<dynamic> infoDynamicLazy;
        private readonly Lazy<dynamic> mainProcessInfoLazy;
        private readonly bool isMainProcess;

        private bool affectMainCardVersionWhenStateChanged = true;
        private Guid? currentApprovalStageRowID;
        private Author author;
        private long authorCommentTimestamp;
        private long authorTimestamp;
        private long stateTimestamp;
        private long affectMainCardVersionWhenStateChangedTimestamp;
        private Author processOwner;
        private long processOwnerTimestamp;
        private Author authorCurrentProcess;
        private long authorCurrentProcessTimestamp;
        private Author processOwnerCurrentProcess;
        private long processOwnerCurrentProcessTimestamp;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WorkflowProcess"/>.
        /// </summary>
        private WorkflowProcess()
        {
            this.infoDynamicLazy = new Lazy<dynamic>(() => DynamicStorageAccessor.Create(this.InfoStorage), LazyThreadSafetyMode.PublicationOnly);
            this.mainProcessInfoLazy = new Lazy<dynamic>(() => DynamicStorageAccessor.Create(this.MainProcessInfoStorage), LazyThreadSafetyMode.PublicationOnly);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WorkflowProcess"/>.
        /// </summary>
        /// <param name="infoStorage">Дополнительная информация по процессу.</param>
        /// <param name="mainProcessInfoStorage">Дополнительная информация по родительскому процессу.</param>
        /// <param name="stages">Коллекция этапов процесса.</param>
        /// <param name="saveInitialStages">Значение <see langword="true"/>, если необходимо сохранить текущее состояние процесса в <see cref="InitialWorkflowProcess"/>, иначе - <see langword="false"/>.</param>
        /// <param name="nestedProcessID">Идентификатор вложенного процесса.</param>
        public WorkflowProcess(
            IDictionary<string, object> infoStorage,
            IDictionary<string, object> mainProcessInfoStorage,
            SealableObjectList<Stage> stages,
            bool saveInitialStages,
            Guid? nestedProcessID,
            bool isMainProcess) : this()
        {
            this.InfoStorage = infoStorage;
            this.MainProcessInfoStorage = mainProcessInfoStorage;
            this.Stages = stages;
            this.NestedProcessID = nestedProcessID;
            this.isMainProcess = isMainProcess;

            if (saveInitialStages)
            {
                this.UpdateInitialWorkflowProcess();
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WorkflowProcess"/>.
        /// </summary>
        /// <param name="workflowProcess">Объект <see cref="WorkflowProcess"/> на основе которого выполняется инициализация.</param>
        /// <param name="stageFilterPredicate">Условие в соответствии с которым будет выполнено копирование этапов в новый объект, если условие не задано, то копируются все этапы.</param>
        /// <param name="saveInitialStages">Значение <see langword="true"/>, если необходимо сохранить текущее состояние процесса в <see cref="InitialWorkflowProcess"/>, иначе - <see langword="false"/>.</param>
        private WorkflowProcess(
            WorkflowProcess workflowProcess,
            Func<Stage, bool> stageFilterPredicate = null,
            bool saveInitialStages = false) : this()
        {
            this.SetAuthor(workflowProcess.Author, false);
            this.SetAuthorComment(workflowProcess.AuthorComment, false);
            this.SetState(workflowProcess.State, false);
            this.InfoStorage = StorageHelper.Clone(workflowProcess.InfoStorage);
            this.MainProcessInfoStorage = StorageHelper.Clone(workflowProcess.MainProcessInfoStorage);
            this.NestedProcessID = workflowProcess.NestedProcessID;
            this.SetProcessOwner(workflowProcess.ProcessOwner, false);
            this.SetAuthorCurrentProcess(workflowProcess.AuthorCurrentProcess, false);
            this.SetProcessOwnerCurrentProcess(workflowProcess.ProcessOwnerCurrentProcess, false);
            this.Stages = CopyStages(workflowProcess, stageFilterPredicate);

            if (saveInitialStages)
            {
                this.UpdateInitialWorkflowProcess();
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// Возвращает идентификатор вложенного процесса.
        /// </summary>
        public Guid? NestedProcessID { get; private set; }

        /// <summary>
        /// Возвращает идентификатор текущего активного этапа процесса.
        /// </summary>
        public Guid? CurrentApprovalStageRowID
        {
            get => this.currentApprovalStageRowID;
            set
            {
                Check.ObjectNotSealed(this);
                this.currentApprovalStageRowID = value;
            }
        }

        /// <summary>
        /// Возвращает хранилище с дополнительной информацией по процессу.
        /// </summary>
        public IDictionary<string, object> InfoStorage { get; private set; }

        /// <summary>
        /// Возвращает дополнительную информацию по процессу.
        /// </summary>
        [JsonIgnore]
        public dynamic Info => this.infoDynamicLazy.Value;

        /// <summary>
        /// Возвращает хранилище с дополнительной информацией по родительскому процессу. Актуально для вложенных,
        /// для родительского MainProcessInfo = Info.
        /// </summary>
        public IDictionary<string, object> MainProcessInfoStorage { get; private set; }

        /// <summary>
        /// Возвращает дополнительную информация по родительскому процессу. Актуально для вложенных,
        /// для родительского MainProcessInfo = Info.
        /// </summary>
        [JsonIgnore]
        public dynamic MainProcessInfo => this.mainProcessInfoLazy.Value;

        /// <summary>
        /// Возвращает или задаёт автора (инициатора) основного процесса.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="AuthorTimestamp"/>.</remarks>
        public Author Author
        {
            get => this.author;
            set => this.SetAuthor(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения автора (инициатора) основного процесса.
        /// </summary>
        public long AuthorTimestamp => this.authorTimestamp;

        /// <summary>
        /// Возвращает или задаёт комментарий автора (инициатора) основного процесса.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="AuthorCommentTimestamp"/>.</remarks>
        public string AuthorComment
        {
            get => this.authorComment;
            set => this.SetAuthorComment(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения комментария автора (инициатора) основного процесса.
        /// </summary>
        public long AuthorCommentTimestamp => this.authorCommentTimestamp;

        /// <summary>
        /// Возвращает или задаёт состояние основного процесса.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="StateTimestamp"/>.</remarks>
        public KrState State
        {
            get => this.state;
            set => this.SetState(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения состояния основного процесса.
        /// </summary>
        public long StateTimestamp => this.stateTimestamp;

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, что версию основной карточки должна быть изменена, если состояние документа изменилось.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="AffectMainCardVersionWhenStateChangedTimestamp"/>.</remarks>
        public bool AffectMainCardVersionWhenStateChanged
        {
            get => this.affectMainCardVersionWhenStateChanged;
            set => this.SetAffectMainCardVersionWhenStateChanged(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения флага версии основной карточки.
        /// </summary>
        public long AffectMainCardVersionWhenStateChangedTimestamp => this.affectMainCardVersionWhenStateChangedTimestamp;

        /// <summary>
        /// Возвращает или задаёт коллекцию этапов процесса.
        /// </summary>
        public SealableObjectList<Stage> Stages
        {
            get => this.stages;
            set
            {
                Check.ObjectNotSealed(this);
                this.stages = value;
            }
        }

        /// <summary>
        /// Возвращает состояние процесса до выполнения обработчиков этапов.
        /// </summary>
        public WorkflowProcess InitialWorkflowProcess { get; private set; }

        /// <summary>
        /// Возвращает или задаёт владельца основного процесса.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="ProcessOwnerTimestamp"/>.</remarks>
        public Author ProcessOwner
        {
            get => this.processOwner;
            set => this.SetProcessOwner(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения владельца основного процесса.
        /// </summary>
        public long ProcessOwnerTimestamp => this.processOwnerTimestamp;

        /// <summary>
        /// Возвращает или задаёт владельца текущего процесса. Значение совпадает с <see cref="ProcessOwner"/>, если текущий процесс является основным.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="ProcessOwnerCurrentProcessTimestamp"/>.</remarks>
        public Author ProcessOwnerCurrentProcess
        {
            get => this.processOwnerCurrentProcess;
            set => this.SetProcessOwnerCurrentProcess(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения автора (инициатора) текущего процесса.
        /// </summary>
        public long ProcessOwnerCurrentProcessTimestamp => this.processOwnerCurrentProcessTimestamp;

        /// <summary>
        /// Возвращает или задаёт автора (инициатора) текущего процесса. Значение совпадает с <see cref="Author"/>, если текущий процесс является основным.
        /// </summary>
        /// <remarks>При задании значения изменяется штамп времени изменения <see cref="AuthorCurrentProcessTimestamp"/>.</remarks>
        public Author AuthorCurrentProcess
        {
            get => this.authorCurrentProcess;
            set => this.SetAuthorCurrentProcess(value);
        }

        /// <summary>
        /// Возвращает штамп времени изменения автора (инициатора) текущего процесса.
        /// </summary>
        public long AuthorCurrentProcessTimestamp => this.authorCurrentProcessTimestamp;

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed { get; private set; }  // = false

        /// <inheritdoc/>
        public void Seal()
        {
            this.IsSealed = true;
            this.Stages.Seal();
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is WorkflowProcess other
                && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);

        #endregion

        #region Operators

        public static bool operator ==(
            WorkflowProcess left,
            WorkflowProcess right)
        {
            if (left is null
                && right is null)
            {
                return true;
            }
            return left?.Equals(right) == true;
        }

        public static bool operator !=(WorkflowProcess left, WorkflowProcess right)
        {
            if (left is null
                && right is null)
            {
                return false;
            }
            return left?.Equals(right) != true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Установить инициатора основного процесса. В общем случае необходимо использовать свойство <see cref="Author"/>.
        /// </summary>
        /// <param name="value">Новый инициатор процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetAuthor(
            Author value,
            bool withTimestamp = true)
        {
            this.SetAuthorInternal(value, withTimestamp);

            if (this.isMainProcess)
            {
                this.SetAuthorCurrentProcessInternal(value, withTimestamp);
            }
        }

        /// <summary>
        /// Установить комментарий инициатора основного процесса.
        /// В общем случае необходимо использовать свойство <see cref="AuthorComment"/>.
        /// </summary>
        /// <param name="value">Новый комментарий.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetAuthorComment(
            string value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.authorComment,
                ref this.authorCommentTimestamp);

        /// <summary>
        /// Устанавливает состояние основного процесса.
        /// В общем случае необходимо использовать свойство <see cref="State"/>.
        /// </summary>
        /// <param name="value">Новое состояние.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetState(
            KrState value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.state,
                ref this.stateTimestamp);

        /// <summary>
        /// Устанавливает флаг изменения версии документа при изменении состояния документа.
        /// В общем случае необходимо использовать свойство <see cref="AffectMainCardVersionWhenStateChanged"/>.
        /// </summary>
        /// <param name="value">Новое значение.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetAffectMainCardVersionWhenStateChanged(
            bool value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.affectMainCardVersionWhenStateChanged,
                ref this.affectMainCardVersionWhenStateChangedTimestamp);

        /// <summary>
        /// Устанавливает владельца основного процесса. В общем случае необходимо использовать свойство <see cref="ProcessOwner"/>.
        /// </summary>
        /// <param name="value">Новый владелец процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetProcessOwner(
            Author value,
            bool withTimestamp = true)
        {
            this.SetProcessOwnerInternal(value, withTimestamp);

            if (this.isMainProcess)
            {
                this.SetProcessOwnerCurrentProcessInternal(value, withTimestamp);
            }
        }

        /// <summary>
        /// Установить инициатора текущего процесса. В общем случае необходимо использовать свойство <see cref="AuthorCurrentProcess"/>.
        /// </summary>
        /// <param name="value">Новый инициатор процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetAuthorCurrentProcess(
            Author value,
            bool withTimestamp = true)
        {
            this.SetAuthorCurrentProcessInternal(value, withTimestamp);

            if (this.isMainProcess)
            {
                this.SetAuthorInternal(value, withTimestamp);
            }
        }

        /// <summary>
        /// Устанавливает владельца текущего процесса. В общем случае необходимо использовать свойство <see cref="ProcessOwnerCurrentProcess"/>.
        /// </summary>
        /// <param name="value">Новый владелец процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        public void SetProcessOwnerCurrentProcess(
            Author value,
            bool withTimestamp = true)
        {
            this.SetProcessOwnerCurrentProcessInternal(value, withTimestamp);

            if (this.isMainProcess)
            {
                this.SetProcessOwnerInternal(value, withTimestamp);
            }
        }

        /// <summary>
        /// Создаёт частичную копию текущего объекта. Клонирует этапы входящие в группу с указанным идентификатором.
        /// </summary>
        /// <param name="groupID">Идентификатор группы этапов, которые должны быть склонированы в новый объект.</param>
        /// <param name="saveInitialStages">Значение <see langword="true"/>, если состояние процесса должно быть сохранено в <see cref="InitialWorkflowProcess"/>, иначе - <see langword="false"/>.</param>
        /// <returns>Частичная копия текущего экземпляра объекта.</returns>
        public WorkflowProcess CloneWithDedicatedStageGroup(
            Guid groupID,
            bool saveInitialStages = false)
        {
            return new WorkflowProcess(
                this,
                s => s.StageGroupID == groupID,
                saveInitialStages);
        }

        /// <inheritdoc />
        public bool Equals(WorkflowProcess other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var currentAuthor = this.Author;
            var otherAuthor = other.Author;
            var authorsAreEqual = (currentAuthor is null && otherAuthor is null)
                || currentAuthor?.Equals(otherAuthor) == true;

            return authorsAreEqual
                && string.Equals(this.AuthorComment, other.AuthorComment, StringComparison.Ordinal)
                && this.State == other.State;
        }

        #endregion

        #region Internal Methods

        internal void UpdateInitialWorkflowProcess()
        {
            this.InitialWorkflowProcess = new WorkflowProcess(this);
            this.InitialWorkflowProcess.Seal();
        }

        #endregion

        #region Private Methods

        private static SealableObjectList<Stage> CopyStages(
            WorkflowProcess workflowProcess,
            Func<Stage, bool> actionPredicate = null)
        {
            return actionPredicate is not null
                ? workflowProcess
                    .Stages
                    .Where(actionPredicate)
                    .Select(static p => new Stage(p))
                    .ToSealableObjectList()
                : workflowProcess
                    .Stages
                    .Select(static p => new Stage(p))
                    .ToSealableObjectList();
        }

        private static long GetTimestamp() => DateTime.UtcNow.Ticks;

        private void SetWorkflowProcessValue<T>(
            T value,
            bool withTimestamp,
            out T field,
            ref long timestamp)
        {
            Check.ObjectNotSealed(this);
            field = value;
            if (withTimestamp)
            {
                timestamp = GetTimestamp();
            }
        }

        /// <summary>
        /// Установить инициатора основного процесса. В общем случае необходимо использовать свойство <see cref="Author"/>.
        /// </summary>
        /// <param name="value">Новый инициатор процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        private void SetAuthorInternal(
            Author value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.author,
                ref this.authorTimestamp);

        /// <summary>
        /// Установить инициатора текущего процесса. В общем случае необходимо использовать свойство <see cref="AuthorCurrentProcess"/>.
        /// </summary>
        /// <param name="value">Новый инициатор процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        private void SetAuthorCurrentProcessInternal(
            Author value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.authorCurrentProcess,
                ref this.authorCurrentProcessTimestamp);


        /// <summary>
        /// Устанавливает владельца основного процесса. В общем случае необходимо использовать свойство <see cref="ProcessOwner"/>.
        /// </summary>
        /// <param name="value">Новый владелец процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        private void SetProcessOwnerInternal(
            Author value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.processOwner,
                ref this.processOwnerTimestamp);

        /// <summary>
        /// Устанавливает владельца текущего процесса. В общем случае необходимо использовать свойство <see cref="ProcessOwnerCurrentProcess"/>.
        /// </summary>
        /// <param name="value">Новый владелец процесса.</param>
        /// <param name="withTimestamp">Значение <see langword="true"/>, если необходимо проставить время изменения, иначе - <see langword="false"/>.</param>
        private void SetProcessOwnerCurrentProcessInternal(
            Author value,
            bool withTimestamp = true) =>
            this.SetWorkflowProcessValue(
                value,
                withTimestamp,
                out this.processOwnerCurrentProcess,
                ref this.processOwnerCurrentProcessTimestamp);

        #endregion
    }
}
