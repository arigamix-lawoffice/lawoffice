using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Предоставляет информацию о этапе маршрута.
    /// </summary>
    public sealed class Stage :
        IEquatable<Stage>,
        ISealable
    {
        #region Nested Types

        private sealed class PerformerObjectComparer : IComparer<object>
        {
            /// <inheritdoc />
            public int Compare(
                object x,
                object y)
            {
                var firstOrder = (x as IDictionary<string, object>)?.TryGet(KrConstants.KrStages.Order, 0) ?? 0;
                var secondOrder = (y as IDictionary<string, object>)?.TryGet(KrConstants.KrStages.Order, 0) ?? 0;
                return firstOrder - secondOrder;
            }
        }

        #endregion

        #region Fields

        private const double DefaultTimeLimit = 1.0;

        private const double Epsilon = 0.01;

        private static readonly PerformerObjectComparer performerObjectComparer = new PerformerObjectComparer();

        private static readonly IStorageValueFactory<int, Performer> multiPerformerFactory =
            new DictionaryStorageValueFactory<int, Performer>((key, storage) => new MultiPerformer(storage));

        private string name;
        private double? timeLimit;
        private DateTime? planned;
        private KrStageState state = KrStageState.Inactive;
        private int? templateStageOrder;
        private Guid? stageTypeID;
        private string stageTypeCaption;
        private IDictionary<string, object> settings;
        private Lazy<dynamic> settingsDynamicLazy;
        private Lazy<dynamic> infoDynamicLazy;
        private bool hidden;
        private ListStorage<Performer> performers;
        private bool? authorExists;
        private AuthorProxy author;
        private bool? performerExists;
        private SinglePerformerProxy performer;

        /// <summary>
        /// Признак пропуска этапа.
        /// </summary>
        private bool skip;

        /// <summary>
        /// Флаг, показывающий, разрешён ли пропуск этапа.
        /// </summary>
        private bool canBeSkipped;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый пустой экземпляр класса <see cref="Stage"/>.
        /// </summary>
        public Stage() => this.InitLazyDynamics();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Stage"/> на основе другого экземпляра. Запечатанность не переносится.
        /// </summary>
        /// <param name="stage">Объект, на основании которого выполняется инициализация.</param>
        public Stage(Stage stage) : this()
        {
            Check.ArgumentNotNull(stage, nameof(stage));

            this.TemplateID = stage.TemplateID;
            this.TemplateName = stage.TemplateName;
            this.GroupPosition = stage.GroupPosition;
            this.CanChangeOrder = stage.CanChangeOrder;
            this.TemplateOrder = stage.TemplateOrder;
            this.IsStageReadonly = stage.IsStageReadonly;

            this.RowID = stage.RowID;
            this.ID = stage.ID;

            this.StageTypeID = stage.StageTypeID;
            this.StageTypeCaption = stage.StageTypeCaption;

            this.StageGroupID = stage.StageGroupID;
            this.StageGroupName = stage.StageGroupName;
            this.StageGroupOrder = stage.StageGroupOrder;

            this.BasedOnTemplateStage = stage.BasedOnTemplateStage;
            this.Name = stage.Name;
            this.TimeLimit = stage.TimeLimit;
            this.Planned = stage.Planned;
            this.Hidden = stage.Hidden;
            this.State = stage.State;
            this.SqlPerformers = stage.SqlPerformers ?? string.Empty;
            this.SqlPerformersIndex = stage.SqlPerformersIndex;
            this.Skip = stage.Skip;
            this.CanBeSkipped = stage.CanBeSkipped;

            this.RowChanged = stage.RowChanged;
            this.OrderChanged = stage.OrderChanged;

            this.SettingsStorage = StorageHelper.Clone(stage.SettingsStorage);
            this.InfoStorage = StorageHelper.Clone(stage.InfoStorage);

            this.InitialStage = stage.InitialStage;
            this.Ancestor = stage.Ancestor;
            this.TemplateStageOrder = stage.TemplateStageOrder;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Stage"/>.
        /// </summary>
        /// <param name="name">Название этапа.</param>
        /// <param name="stageTypeID">Идентификатор типа этапа.</param>
        /// <param name="stageTypeCaption">Отображаемое имя типа этапа.</param>
        public Stage(string name, Guid stageTypeID, string stageTypeCaption)
            : this(Guid.NewGuid(),
                 name,
                 stageTypeID,
                 stageTypeCaption,
                 Guid.Empty,
                 -1,
                 null,
                 null,
                 null,
                 false,
                 GroupPosition.Unspecified)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Stage"/> с привязкой к шаблону этапов.
        /// </summary>
        /// <param name="id">Идентификатор этапа.</param>
        /// <param name="name">Название этапа.</param>
        /// <param name="stageTypeID">Идентификатор типа этапа.</param>
        /// <param name="stageTypeCaption">Отображаемое имя типа этапа.</param>
        /// <param name="stageGroupID">Идентификатор группы этапов, к которой принадлежит этап.</param>
        /// <param name="stageGroupOrder">Порядок сортировки для группы этапов, к которой относится этап.</param>
        /// <param name="templateID">Идентификатор шаблона этапов.</param>
        /// <param name="templateName">Имя шаблона этапов.</param>
        /// <param name="templateOrder">Порядок в группе по условию группировки пары (<see cref="GroupPosition"/>, <see cref="CanChangeOrder"/>).</param>
        /// <param name="canChangeOrder">Значение <see langword="true"/>, если пользователь может поменять порядок текущего этапа, иначе - <see langword="false"/>.</param>
        /// <param name="groupPosition">Расположение шаблона этапов относительно этапов, добавленных вручную. Если задано значение <see langword="null"/>, то считается равным <see cref="GroupPosition.Unspecified"/>.</param>
        /// <param name="ancestor">Предок этапа, если он есть, который был изначально в маршруте вместо текущего этапа.</param>
        /// <param name="isStageReadonly">Значение <see langword="true"/>, если может ли пользователь редактировать этап, иначе - <see langword="false"/>.</param>
        /// <param name="timeLimit">Срок (рабочие дни).</param>
        /// <param name="planned">Дата выполнения.</param>
        /// <param name="hidden">Значение <see langword="true"/>, этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <param name="stageState">Состояние этапа. Если задано значение <see langword="null"/>, то считается равным <see cref="KrStageState.Inactive"/>.</param>
        /// <param name="skip">Значение <see langword="true"/>, если этап пропущен, иначе - <see langword="false"/>.</param>
        /// <param name="canBeSkipped">Значение <see langword="true"/>, если разрешено пропускать этап, иначе - <see langword="false"/>.</param>
        public Stage(
            Guid id,
            string name,
            Guid stageTypeID,
            string stageTypeCaption,
            Guid stageGroupID,
            int stageGroupOrder,
            Guid? templateID,
            string templateName,
            int? templateOrder,
            bool canChangeOrder,
            GroupPosition groupPosition,
            Stage ancestor = null,
            bool isStageReadonly = true,
            int timeLimit = 1,
            DateTime? planned = null,
            bool hidden = false,
            KrStageState? stageState = null,
            bool skip = default,
            bool canBeSkipped = default) : this()
        {
            this.RowID = id;
            this.ID = id;
            this.Name = name;

            this.TemplateID = templateID;
            this.TemplateName = templateName;
            this.TemplateOrder = templateOrder;
            this.TemplateStageOrder = 0;

            this.StageTypeID = stageTypeID;
            this.StageTypeCaption = stageTypeCaption;

            this.StageGroupID = stageGroupID;
            this.StageGroupOrder = stageGroupOrder;

            this.CanChangeOrder = canChangeOrder;
            this.GroupPosition = groupPosition ?? GroupPosition.Unspecified;
            this.Ancestor = ancestor;
            this.IsStageReadonly = isStageReadonly;
            this.TimeLimit = timeLimit;
            this.Planned = planned;
            this.Hidden = hidden;
            this.State = stageState ?? KrStageState.Inactive;
            this.Skip = skip;
            this.CanBeSkipped = canBeSkipped;

            this.SettingsStorage = new Dictionary<string, object>(StringComparer.Ordinal);
            this.InfoStorage = new Dictionary<string, object>(StringComparer.Ordinal);

            this.BasedOnTemplateStage = false;
            this.InitialStage = false;
            this.SqlPerformers = string.Empty;
            this.SqlPerformersIndex = -1;

            this.RowChanged = false;
            this.OrderChanged = false;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Stage"/>.
        /// </summary>
        /// <param name="runtimeStage">Объект, содержащий информацию об этапе в шаблоне этапов <paramref name="stageTemplate"/>.</param>
        /// <param name="stageTemplate">Объект, содержащий информацию о шаблоне этапов.</param>
        /// <param name="initialStage">Значение <see langword="true"/>, если объект создан при первичном построении исходного маршрута, иначе - <see langword="false"/>.</param>
        /// <returns>Новый экземпляр класса <see cref="Stage"/>.</returns>
        public static async ValueTask<Stage> InitializeAsync(
            IKrRuntimeStage runtimeStage,
            IKrStageTemplate stageTemplate,
            bool initialStage = false)
        {
            Check.ArgumentNotNull(runtimeStage, nameof(runtimeStage));
            Check.ArgumentNotNull(stageTemplate, nameof(stageTemplate));

            var instance = new Stage
            {
                InitialStage = initialStage,
                // Создавая по IKrRuntimeStage не будет объекта-предшественника.
                Ancestor = null
            };
            instance.FillStageProperties(runtimeStage);

            instance.SettingsStorage = await runtimeStage.GetSettingsAsync();
            instance.InfoStorage = new Dictionary<string, object>(StringComparer.Ordinal);

            // Полное создание по шаблону.
            // StageRow - строка из карточки KrStageTemplates
            instance.ID = runtimeStage.StageID;
            instance.BasedOnTemplateStage = true;
            // Только создаем строку - ID для новой строки в документе
            instance.RowID = Guid.NewGuid();

            int? sqlApproverIndex = instance.Performers.IndexOf(p => p.PerformerID == KrConstants.SqlApproverRoleID);
            if (sqlApproverIndex != -1)
            {
                instance.Performers.RemoveAt(sqlApproverIndex.Value);
            }

            var performersObj = instance.SettingsStorage[KrConstants.KrPerformersVirtual.Synthetic];
            if (performersObj is IList perfList)
            {
                for (var i = 0; i < perfList.Count; i++)
                {
                    if (perfList[i] is IDictionary<string, object> perf
                        && perf.TryGet<Guid?>(KrConstants.KrPerformersVirtual.PerformerID) ==
                        KrConstants.SqlApproverRoleID
                        && perf.TryGetValue(KrConstants.KrPerformersVirtual.Order, out var ord)
                        && ord is int order)
                    {
                        instance.SqlPerformersIndex = order;
                    }
                }
            }

            instance.SqlPerformers = runtimeStage.SqlRoles ?? string.Empty;

            instance.FillTemplateProperties(stageTemplate, runtimeStage);

            return instance;
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="Stage"/>.
        /// </summary>
        /// <param name="stageRow">Строковое представление этапа.</param>
        /// <param name="settings">Коллекция пар ключ-значение содержащая настройки этапа.</param>
        /// <param name="infoStorage">Коллекция пар ключ-значение содержащая данные этапа.</param>
        /// <param name="stageTemplate">Шаблон этапов содержащий этап или значение по умолчанию для типа, если этап не связан с шаблоном этапов.</param>
        /// <param name="stageTemplateStages">Коллекция этапов содержащаяся в шаблоне этапов <paramref name="stageTemplate"/> или значение по умолчанию для типа, если этап не связан с шаблоном этапов.</param>
        /// <param name="initialStage">Значение <see langword="true"/>, если объект создан при первичном построении исходного маршрута, иначе - <see langword="false"/>.</param>
        public Stage(
            CardRow stageRow,
            IDictionary<string, object> settings,
            IDictionary<string, object> infoStorage,
            IKrStageTemplate stageTemplate = default,
            IReadOnlyCollection<IKrRuntimeStage> stageTemplateStages = default,
            bool initialStage = default) : this()
        {
            Check.ArgumentNotNull(stageRow, nameof(stageRow));
            Check.ArgumentNotNull(settings, nameof(settings));
            Check.ArgumentNotNull(infoStorage, nameof(infoStorage));

            this.InitialStage = initialStage;
            // Создавая по CardRow не будет объекта-предшественника.
            this.Ancestor = null;
            this.FillStageProperties(stageRow);

            this.SettingsStorage = settings;
            this.InfoStorage = infoStorage;

            var basedOnTemplateID = stageRow.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageTemplateID);
            var basedOnStageRowID = stageRow.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageRowID);

            if (stageTemplate is not null
                && stageTemplateStages is not null)
            {
                IKrRuntimeStage stagePrototype = null;
                var templateStage = stageTemplateStages.FirstOrDefault(p => p.StageID == stageRow.RowID);
                if (templateStage is not null)
                {
                    // Полное создание по шаблону.
                    // StageRow - строка из карточки KrStageTemplates
                    this.ID = stageRow.RowID;
                    // Только создаем строку - ID для новой строки в документе
                    this.RowID = Guid.NewGuid();
                    this.BasedOnTemplateStage = true;

                    stagePrototype = templateStage;

                    var sqlApproverIndex = this.Performers.IndexOf(static p => p.PerformerID == KrConstants.SqlApproverRoleID);
                    if (sqlApproverIndex != -1)
                    {
                        this.Performers.RemoveAt(sqlApproverIndex);
                    }
                }
                else if (basedOnTemplateID.HasValue && basedOnStageRowID.HasValue)
                {
                    // Этап в карточке был ранее создан подстановкой из таблицы этапов в шаблоне
                    // в таблицу этапов карточки
                    this.ID = basedOnStageRowID.Value;
                    // ID для строки в документе
                    this.RowID = stageRow.RowID;
                    this.BasedOnTemplateStage = true;

                    stagePrototype = stageTemplateStages.FirstOrDefault(p => basedOnStageRowID.Value == p.StageID);
                }
                else if (basedOnTemplateID.HasValue)
                {
                    // Этап был создан с привязкой к карточке,
                    // но без привязки к конкретному этапу в шаблоне
                    this.ID = stageRow.RowID;
                    this.RowID = stageRow.RowID;
                    this.BasedOnTemplateStage = false;
                }

                var performersObj = this.SettingsStorage[KrConstants.KrPerformersVirtual.Synthetic];
                if (performersObj is IList perfList)
                {
                    for (var i = 0; i < perfList.Count; i++)
                    {
                        if (perfList[i] is IDictionary<string, object> perf
                            && perf.TryGet<Guid?>(KrConstants.KrPerformersVirtual.PerformerID) == KrConstants.SqlApproverRoleID
                            && perf.TryGetValue(KrConstants.KrPerformersVirtual.Order, out var ord)
                            && ord is int order)
                        {
                            this.SqlPerformersIndex = order;
                        }
                    }
                }

                this.SqlPerformers = stagePrototype?.SqlRoles ?? string.Empty;

                this.FillTemplateProperties(stageRow, stageTemplate, stagePrototype);
            }
            else
            {
                // stageTemplate == null - этап ручной или шаблон удален.
                this.ID = stageRow.RowID;
                this.RowID = stageRow.RowID;
                this.BasedOnTemplateStage = false;
                this.FillTemplatePropertiesByDefaultValues();

                if (basedOnTemplateID.HasValue)
                {
                    // Этап был создан по шаблону, но сам шаблон уже удален
                    if (basedOnStageRowID.HasValue)
                    {
                        // Это этап из таблицы карточки шаблона, которая была удалена.
                        this.ID = basedOnStageRowID.Value;
                        this.BasedOnTemplateStage = true;
                    }

                    this.TemplateID = basedOnTemplateID;
                    this.TemplateName = stageRow.TryGet<string>(KrConstants.KrStages.BasedOnStageTemplateName);
                }
            }
        }

        #endregion

        #region Properties

        #region Template properties

        /// <summary>
        /// Возвращает идентификатор шаблона этапов.
        /// </summary>
        public Guid? TemplateID { get; private set; }

        /// <summary>
        /// Возвращает имя шаблона этапов.
        /// </summary>
        public string TemplateName { get; private set; }

        /// <summary>
        /// Возвращает расположение шаблона этапов относительно этапов, добавленных вручную.
        /// </summary>
        public GroupPosition GroupPosition { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, может ли пользователь поменять порядок текущего этапа.
        /// Если это запрещено, то для этапов "В начале группы" этап окажется перед теми, для которых разрешено менять порядок; для этапов "В конце группы" этапы, для которых разрешено менять порядок, будут выше, чем строго зафиксированные.
        /// </summary>
        public bool CanChangeOrder { get; private set; }

        /// <summary>
        /// Возвращает порядок в группе по условию группировки пары (<see cref="GroupPosition"/>, <see cref="CanChangeOrder"/>).
        /// </summary>
        public int? TemplateOrder { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, может ли пользователь редактировать этап.
        /// </summary>
        public bool IsStageReadonly { get; private set; }

        #endregion

        #region stage properties

        /// <summary>
        /// Возвращает идентификатор строки (<see cref="CardRow.RowID"/>) в конкретном документе.
        /// Если этап только создан по шаблону, то здесь будет новый идентификатор.
        /// </summary>
        public Guid RowID { get; private set; }

        /// <summary>
        /// Возвращает идентификатор этапа.
        /// </summary>
        /// <remarks>
        /// Если этап создан по шаблону, то идентификатор строки (<see cref="CardRow.RowID"/>) этапа из карточки шаблона.<para/>
        /// Если этап создан вручную, то идентификатор строки (<see cref="CardRow.RowID"/>) из карточки документа.
        /// </remarks>
        public Guid ID { get; private set; }

        /// <summary>
        /// Возвращает идентификатор группы этапов, к которой принадлежит этап.
        /// </summary>
        public Guid StageGroupID { get; private set; }

        /// <summary>
        /// Возвращает название группы этапов, к которой принадлежит этап.
        /// </summary>
        public string StageGroupName { get; private set; }

        /// <summary>
        /// Возвращает порядок сортировки для группы этапов, к которой относится этап.
        /// </summary>
        public int StageGroupOrder { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, что этап добавлен из шаблона этапов - задан шаблон этапов.
        /// </summary>
        [JsonIgnore]
        public bool BasedOnTemplate => this.TemplateID.HasValue;

        /// <summary>
        /// Возвращает значение, показывающее, что этап добавлен из шаблона этапов - задан идентификатор строки шаблона этапов из карточки шаблона этапов.
        /// </summary>
        public bool BasedOnTemplateStage { get; private set; }

        /// <summary>
        /// Возвращает или задаёт название этапа.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                this.CheckSealed();
                this.name = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт текущее состояние этапа согласования. Актуально только при работе процесса.
        /// </summary>
        public KrStageState State
        {
            get => this.state;
            set
            {
                this.CheckSealed();
                this.state = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт срок (рабочие дни).
        /// </summary>
        public double? TimeLimit
        {
            get => this.timeLimit;
            set
            {
                this.CheckSealed();
                this.timeLimit = value;

                if (value.HasValue)
                {
                    this.planned = null;
                }
            }
        }

        /// <summary>
        /// Возвращает срок (рабочие дни), если указан, иначе стандартное значение <see cref="DefaultTimeLimit"/>.
        /// </summary>
        [JsonIgnore]
        public double TimeLimitOrDefault => this.TimeLimit ?? DefaultTimeLimit;

        /// <summary>
        /// Возвращает или задаёт дату выполнения.
        /// </summary>
        public DateTime? Planned
        {
            get => this.planned;
            set
            {
                this.CheckSealed();
                this.planned = value;

                if (value.HasValue)
                {
                    this.timeLimit = null;
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, что этап является скрытым.
        /// </summary>
        public bool Hidden
        {
            get => this.hidden;
            set
            {
                this.CheckSealed();
                this.hidden = value;
            }
        }

        /// <summary>
        /// Возвращает запрос на получение SQL-согласующих.
        /// </summary>
        public string SqlPerformers { get; private set; } = string.Empty;

        /// <summary>
        /// Возвращает индекс в массиве, куда необходимо подставлять SQL согласующих при пересчете.
        /// Загружается при создании на основе строки карточки с указанием карточки-шаблона.
        /// Никак не отображает куда были подставлены SQL согласующие за предыдущий пересчет.
        /// Чтобы это узнать, нужно найти индекс первого согласующего с флагом SqlApprover = true,
        /// однако если этап изменялся вручную (RowChanged), о предыдущей подстановке SQL согласующих
        /// делать выводы нельзя.
        /// </summary>
        public int? SqlPerformersIndex { get; private set; }

        /// <summary>
        /// Возвращает признак того, что порядок менялся пользователем.
        /// Не зависит от изменения порядка в коде
        /// </summary>
        public bool OrderChanged { get; private set; }

        /// <summary>
        /// Возвращает признак того, что этап менялся пользователем.
        /// Не зависит от изменений в коде.
        /// </summary>
        public bool RowChanged { get; private set; }

        /// <summary>
        /// Возвращает или задаёт порядок сортировки для этапа в рамках шаблона этапов.
        /// Необходимо для обнаружения изменений в подмаршруте из конкретного шаблона этапа
        /// (например, если в шаблоне был добавлен еще один этап на первое место, при построении этот этап необходимо поместить также выше).
        /// </summary>
        public int? TemplateStageOrder
        {
            get => this.templateStageOrder;
            set
            {
                this.CheckSealed();
                this.templateStageOrder = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт идентификатор типа этапа.
        /// </summary>
        public Guid? StageTypeID
        {
            get => this.stageTypeID;
            set
            {
                this.CheckSealed();
                this.stageTypeID = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт отображаемое имя типа этапа.
        /// </summary>
        public string StageTypeCaption
        {
            get => this.stageTypeCaption;
            set
            {
                this.CheckSealed();
                this.stageTypeCaption = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт настройки этапа.
        /// </summary>
        public IDictionary<string, object> SettingsStorage
        {
            get => this.settings;
            set
            {
                this.CheckSealed();
                this.settings = value;
            }
        }

        /// <summary>
        /// Возвращает dynamic-обёртку над настройками этапа.
        /// </summary>
        [JsonIgnore]
        public dynamic Settings => this.settingsDynamicLazy.Value;

        /// <summary>
        /// Возвращает дополнительную информацию этапа.
        /// </summary>
        public IDictionary<string, object> InfoStorage { get; private set; }

        /// <summary>
        /// Возвращает dynamic-обёртку над дополнительной информацией этапа.
        /// </summary>
        [JsonIgnore]
        public dynamic Info => this.infoDynamicLazy.Value;

        /// <summary>
        /// Возвращает или задаёт исполнителя текущего этапа. Актуально только для режима <see cref="PerformerUsageMode.Single"/>.
        /// </summary>
        [JsonIgnore]
        public Performer Performer
        {
            get
            {
                if (!this.performerExists.HasValue)
                {
                    this.performer = new SinglePerformerProxy(this.SettingsStorage);
                    this.performerExists = this.SettingsStorage.TryGet<Guid?>(KrConstants.KrSinglePerformerVirtual.PerformerID).HasValue;
                }

                return this.performerExists.Value
                    ? this.performer
                    : null;
            }
            set
            {
                this.CheckSealed();
                if (!this.performerExists.HasValue)
                {
                    this.performer = new SinglePerformerProxy(this.SettingsStorage);
                    this.performerExists = this.SettingsStorage.TryGet<Guid?>(KrConstants.KrSinglePerformerVirtual.PerformerID).HasValue;
                }

                if (value is not null)
                {
                    this.performerExists = true;
                    this.SettingsStorage[KrConstants.KrSinglePerformerVirtual.PerformerID] = value.PerformerID;
                    this.SettingsStorage[KrConstants.KrSinglePerformerVirtual.PerformerName] = value.PerformerName;
                }
                else
                {
                    this.performerExists = false;
                    this.SettingsStorage[KrConstants.KrSinglePerformerVirtual.PerformerID] = null;
                    this.SettingsStorage[KrConstants.KrSinglePerformerVirtual.PerformerName] = null;
                }
            }
        }

        /// <summary>
        /// Возвращает список исполнителей текущего этапа. Актуально только для режима <see cref="PerformerUsageMode.Multiple"/>.
        /// </summary>
        [JsonIgnore]
        public ListStorage<Performer> Performers
        {
            get
            {
                if (this.performers is null)
                {
                    if (!this.SettingsStorage.TryGetValue(KrConstants.KrPerformersVirtual.Synthetic, out var kpvObj)
                        || kpvObj is not IList kvp)
                    {
                        kvp = new List<object>();
                    }
                    else
                    {
                        kvp = kvp.Cast<object>().OrderBy(x => x, performerObjectComparer).ToList();
                    }

                    this.SettingsStorage[KrConstants.KrPerformersVirtual.Synthetic] = kvp;

                    this.performers = new ListStorage<Performer>(kvp, multiPerformerFactory);
                }

                return this.performers;
            }
        }

        /// <summary>
        /// Возвращает или задаёт автора этапа. Переопределяет автора заданного в параметрах этапа.
        /// </summary>
        [JsonIgnore]
        public Author Author
        {
            get
            {
                if (this.author is null)
                {
                    this.author = new AuthorProxy(this.SettingsStorage);

                    if (!this.authorExists.HasValue)
                    {
                        this.authorExists = this.SettingsStorage.TryGet<Guid?>(KrConstants.KrAuthorSettingsVirtual.AuthorID).HasValue;
                    }
                }

                return this.authorExists.Value
                    ? this.author
                    : null;
            }
            set
            {
                this.CheckSealed();

                if (value is not null)
                {
                    this.authorExists = true;
                    this.SettingsStorage[KrConstants.KrAuthorSettingsVirtual.AuthorID] = value.AuthorID;
                    this.SettingsStorage[KrConstants.KrAuthorSettingsVirtual.AuthorName] = value.AuthorName;
                }
                else
                {
                    this.authorExists = false;
                    this.SettingsStorage[KrConstants.KrAuthorSettingsVirtual.AuthorID] = null;
                    this.SettingsStorage[KrConstants.KrAuthorSettingsVirtual.AuthorName] = null;
                }
            }
        }

        /// <summary>
        /// Возвращает или задаёт флаг регулирующий, в каком объеме информация о заданиях будет указываться по ключу <see cref="KrConstants.Keys.Tasks"/>
        /// в <see cref="InfoStorage"/>. Если указано <see langword="true"/> - информация будет полной, включая карточку задания. Иначе перед записью будут удалены некоторые поля.
        /// </summary>
        /// <remarks>Является оберткой над флагом, расположенным в <see cref="InfoStorage"/> по ключу, равному названию свойства. Отсутствие значения в <see cref="InfoStorage"/> трактуется как <see langword="false"/>.</remarks>
        /// <seealso cref="KrProcess.Workflow.Handlers.HandlerHelper.AppendToCompletedTasksWithPreparing"/>
        [JsonIgnore]
        public bool WriteTaskFullInformation
        {
            get => this.InfoStorage.TryGet<bool>(nameof(this.WriteTaskFullInformation));
            set => this.InfoStorage[nameof(this.WriteTaskFullInformation)] = BooleanBoxes.Box(value);
        }

        /// <summary>
        /// Возвращает или задаёт признак пропуска этапа.
        /// </summary>
        public bool Skip
        {
            get => this.skip;
            set
            {
                this.CheckSealed();
                this.skip = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, разрешен ли пропуск этапа.
        /// </summary>
        public bool CanBeSkipped
        {
            get => this.canBeSkipped;
            set
            {
                this.CheckSealed();
                this.canBeSkipped = value;
            }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Объект создан при первичном построении исходного маршрута.
        /// </summary>
        internal bool InitialStage { get; set; }

        /// <summary>
        /// Признак того, что этап должен быть отвязан от шаблона.
        /// </summary>
        internal bool UnbindTemplate { get; set; } = false;

        /// <summary>
        /// Возвращает предка этапа, если он есть, который был изначально в маршруте вместо текущего этапа.
        /// </summary>
        internal Stage Ancestor { get; private set; }

        /// <summary>
        /// Сообщение runner-у о том, что необходимо для данного этапа
        /// попытаться переключить контекст на указанную карточку.
        /// </summary>
        internal Guid? ChangeContextToCardID { get; set; } = null;

        /// <summary>
        /// Признак того, что при обработке <see cref="ChangeContextToCardID"/>
        /// обработка будет переключена на всю группу.
        /// </summary>
        internal bool ChangeContextWholeGroupToDifferentCard { get; set; } = false;

        /// <summary>
        /// Дополнительная пользовательская информация для процесса, который будет создан при переключении контекста.
        /// </summary>
        internal IDictionary<string, object> ChangeContextProcessInfo { get; set; }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Устанавливает информацию о положении относительно этапов, добавленных вручную, шаблона этапов с котором связан этап как "не определено". Удаляет информацию из этапа о позиции шаблона этапов.
        /// </summary>
        public void SetGroupPositionUnspecified()
        {
            this.CheckSealed();
            this.GroupPosition = GroupPosition.Unspecified;
            this.TemplateOrder = null;
            this.TemplateStageOrder = null;
        }

        /// <summary>
        /// Переносит служебную информацию (о положении, внесенных изменениях и др.) из указанного этапа в этот экземпляр.
        /// </summary>
        /// <param name="stage">Этап, из которого переносится информация.</param>
        /// <remarks>
        /// При пересчете, когда имеются новая и старая версия этапа,
        /// нужно сохранить информацию о том, как пользователь воздействовал на этап,
        /// а также актуализировать поле <see cref="GroupPosition"/> для корректной сортировки этапов.
        /// Помимо этого переносятся SQL-согласующие, поскольку иначе информация о них будет утеряна.
        /// Переносить SQL согласующих нужно для определения изменений в выборке SQL согласующих.
        /// </remarks>
        public void Inherit(Stage stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));
            this.CheckSealed();

            this.RowID = stage.RowID;
            this.State = stage.State;
            StorageHelper.Merge(stage.InfoStorage, this.InfoStorage);

            if (stage.GroupPosition == GroupPosition.Unspecified)
            {
                this.GroupPosition = stage.GroupPosition;
                this.TemplateOrder = stage.TemplateOrder;
                this.TemplateStageOrder = stage.TemplateStageOrder;
            }

            this.CanChangeOrder = stage.CanChangeOrder;
            this.IsStageReadonly = stage.IsStageReadonly;

            this.RowChanged = stage.RowChanged;
            this.OrderChanged = stage.OrderChanged;

            this.InfoStorage = stage.Info;

            this.Ancestor = null;
            if (stage.Ancestor?.InitialStage == true)
            {
                this.Ancestor = stage.Ancestor;
            }
            else if (stage.Ancestor is null && stage.InitialStage)
            {
                this.Ancestor = stage;
            }

            this.Skip = stage.Skip;
            if (this.Skip)
            {
                this.Hidden = stage.Hidden;
            }
        }

        /// <summary>
        /// Переносит информацию о положении этапа из указанного этапа.
        /// </summary>
        /// <param name="stage">Этап, из которого переносится информация.</param>
        public void InheritPosition(Stage stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));
            this.CheckSealed();

            this.GroupPosition = stage.GroupPosition;
            this.TemplateOrder = stage.TemplateOrder;
            this.TemplateStageOrder = stage.TemplateStageOrder;
        }

        /// <summary>
        /// Задаёт значение <see langword="true"/> флагу <see cref="CanChangeOrder"/>.
        /// Разрешено изменять только для неподтверждённых и изменённых пользователем этапов.
        /// </summary>
        public void SetCanChangeOrderTrue()
        {
            this.CheckSealed();

            if (this.CanChangeOrder)
            {
                return;
            }

            if (this.BasedOnTemplate
                && this.InitialStage
                && (this.RowChanged || this.OrderChanged))
            {
                this.CanChangeOrder = true;
                return;
            }

            // Если это делают с нормальными этапами, нужно поругаться.
            throw new InvalidOperationException("It is allowed to change only for unconfirmed and user-modified stages.");
        }

        /// <summary>
        /// Возвращает значение, показывающее, что изменилась дополнительная информация этапа (<see cref="InfoStorage"/>).
        /// </summary>
        /// <param name="currentStageFromThePast">Этап, содержащий дополнительную информация этапа с которой выполняется сравнение.</param>
        /// <returns>Значение <see langword="true"/>, если изменилась пользовательская информация внутри этапа, иначе - <see langword="false"/>.</returns>
        public bool IsInfoChanged(Stage currentStageFromThePast)
        {
            if (currentStageFromThePast.ID != this.ID)
            {
                throw new ArgumentException("Can compare only with the same stage.");
            }

            return !StorageHelper.Equals(this.InfoStorage, currentStageFromThePast.InfoStorage);
        }

        #endregion

        #region AutomaticallyChangedProperties

        /// <summary>
        /// Добавляет указанное свойство в список автоматически изменённых значений этапа.
        /// </summary>
        /// <param name="name">Имя добавляемого значения.</param>
        /// <remarks>Не выполняет действий, если указанное значение содержится в списке.</remarks>
        public void AddAutomaticallyChangedValue(
            string name)
        {
            var list = this.GetAutomaticallyChangedValues();

            if (!list.Contains(name))
            {
                list.Add(name);
            }
        }

        /// <summary>
        /// Удаляет указанное свойство из списка автоматически изменённых значений этапа.
        /// </summary>
        /// <param name="name">Имя удаляемого значения.</param>
        public void RemoveAutomaticallyChangedValue(
            string name)
        {
            var list = this.GetAutomaticallyChangedValues();
            list.Remove(name);
        }

        /// <summary>
        /// Очищает список автоматически изменённых значений этапа.
        /// </summary>
        public void ClearAutomaticallyChangedValues() =>
            this.InfoStorage.Remove(KrConstants.Keys.AutomaticallyChangedValues);

        /// <summary>
        /// Сравнивает этап по значимым полям с учётом автоматически изменённых значений.
        /// </summary>
        /// <param name="other">Объект, с которым выполняется сравнение.</param>
        /// <returns>Значение <see langword="true"/>, если объекты равны, иначе - <see langword="false"/>.</returns>
        /// <remarks>
        /// Не выполняет сравнение <see cref="InfoStorage"/>. Для его сравнения используйте метод <see cref="IsInfoChanged(Stage)"/>.<para/>
        /// Автоматически изменённое значение - это значение изменённое при обработке этапа в обработчике из-за наличия соответствующего параметра. Его изменение не оказывает влияния при определении равенства этапов с помощью этого метода. Пример автоматически изменяемого значения: свойство <see cref="Hidden"/> в этапе "Доработка" (<see cref="KrProcess.Workflow.Handlers.EditStageTypeHandler"/>) при изменении его в соответствии с параметром "Управлять видимостью этапа" (<see cref="KrConstants.KrEditSettingsVirtual.ManageStageVisibility"/>).
        /// </remarks>
        public bool EqualsWithAutomaticallyChangedValues(
            Stage other)
        {
            var automaticallyChangedProperties = this.TryGetAutomaticallyChangedValues();
            return this.EqualsWithAutomaticallyChangedValuesCore(other, automaticallyChangedProperties);
        }

        #endregion

        #region Operators

        public static bool operator ==(Stage left, Stage right)
        {
            if (left is null
                && right is null)
            {
                return true;
            }
            return left?.Equals(right) == true;
        }

        public static bool operator !=(Stage left, Stage right)
        {
            if (left is null
                && right is null)
            {
                return false;
            }
            return left?.Equals(right) != true;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"{nameof(this.RowID)} = {this.RowID:B}," +
                $" {nameof(this.ID)} = {this.ID:B}," +
                $" {nameof(this.Name)} = {this.Name}," +
                $" {nameof(this.TemplateName)} = {this.TemplateName}," +
                $" {nameof(this.BasedOnTemplateStage)} = {this.BasedOnTemplateStage}," +
                $" {nameof(this.Performers)} = {this.Performers.Count}";
        }

        /// <inheritdoc/>
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
            return obj is Stage stage && this.Equals(stage);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            // ID setter используется при десериализации. В процесс работы не меняется.
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            this.ID.GetHashCode();

        #endregion

        #region IEquatable<T> Members

        /// <summary>
        /// Сравнивает этап по значимым полям.
        /// </summary>
        /// <param name="other">Объект, с которым выполняется сравнение.</param>
        /// <returns>Значение <see langword="true"/>, если объекты равны, иначе - <see langword="false"/>.</returns>
        /// <remarks>Не выполняет сравнение <see cref="InfoStorage"/>. Для его сравнения используйте метод <see cref="IsInfoChanged(Stage)"/>.</remarks>
        public bool Equals(Stage other) =>
            this.EqualsWithAutomaticallyChangedValuesCore(other, null);

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed { get; private set; }  // = false

        /// <inheritdoc/>
        public void Seal() => this.IsSealed = true;

        #endregion

        #region Private Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool NullableDoubleNumbersIsEqual(
            double? first,
            double? second) =>
            first is null && second is null || Math.Abs((first - second) ?? Epsilon) < Epsilon;

        /// <summary>
        /// Выбрасывает исключение Tessa.Platform.ObjectSealedException",
        /// если объект был защищён от изменений.
        /// </summary>
        private void CheckSealed() => Check.ObjectNotSealed(this);

        private void InitLazyDynamics()
        {
            this.settingsDynamicLazy = new Lazy<dynamic>(() => DynamicStorageAccessor.Create(this.SettingsStorage), LazyThreadSafetyMode.PublicationOnly);
            this.infoDynamicLazy = new Lazy<dynamic>(() => DynamicStorageAccessor.Create(this.InfoStorage), LazyThreadSafetyMode.PublicationOnly);
        }

        private void FillStageProperties(CardRow stageRow)
        {
            this.State = (KrStageState) stageRow.Fields.TryGet<int>(KrConstants.KrStages.StateID);
            this.StageGroupID = stageRow.Fields.Get<Guid>(KrConstants.KrStages.StageGroupID);
            this.StageGroupName = stageRow.Fields.Get<string>(KrConstants.KrStages.StageGroupName);
            this.StageGroupOrder = stageRow.Fields.Get<int>(KrConstants.KrStages.StageGroupOrder);
            this.Name = stageRow.Fields.Get<string>(KrConstants.KrStages.NameField);
            this.TimeLimit = stageRow.Fields[KrConstants.KrStages.TimeLimit] as double?;
            this.Planned = stageRow.Fields[KrConstants.KrStages.Planned] as DateTime?;
            this.Hidden = stageRow.Fields.TryGet<bool?>(KrConstants.KrStages.Hidden) ?? false;
            this.Skip = stageRow.Fields.TryGet<bool?>(KrConstants.KrStages.Skip) ?? false;
            this.CanBeSkipped = stageRow.Fields.TryGet<bool?>(KrConstants.KrStages.CanBeSkipped) ?? false;

            this.RowChanged = stageRow.Fields.Get<bool>(KrConstants.KrStages.RowChanged);
            this.OrderChanged = stageRow.Fields.Get<bool>(KrConstants.KrStages.OrderChanged);

            this.StageTypeID = stageRow.Fields.Get<Guid?>(KrConstants.KrStages.StageTypeID);
            this.StageTypeCaption = stageRow.Fields.Get<string>(KrConstants.KrStages.StageTypeCaption);
        }

        private void FillStageProperties(IKrRuntimeStage runtimeStage)
        {
            this.State = KrStageState.Inactive;
            this.StageGroupID = runtimeStage.GroupID;
            this.StageGroupName = runtimeStage.GroupName;
            this.StageGroupOrder = runtimeStage.GroupOrder;
            this.Name = runtimeStage.StageName;
            this.TimeLimit = runtimeStage.TimeLimit;
            this.Planned = runtimeStage.Planned;
            this.Hidden = runtimeStage.Hidden;
            this.Skip = runtimeStage.Skip;
            this.CanBeSkipped = runtimeStage.CanBeSkipped;

            this.RowChanged = false;
            this.OrderChanged = false;

            this.StageTypeID = runtimeStage.StageTypeID;
            this.StageTypeCaption = runtimeStage.StageTypeCaption;
        }

        private void FillTemplateProperties(
            IKrStageTemplate stageTemplate,
            IKrRuntimeStage runtimeStage)
        {
            this.TemplateID = stageTemplate.ID;
            this.TemplateName = stageTemplate.Name;
            this.TemplateOrder = stageTemplate.Order;
            this.GroupPosition = stageTemplate.Position;

            this.CanChangeOrder = stageTemplate.CanChangeOrder;
            this.IsStageReadonly = stageTemplate.IsStagesReadonly;

            // Если чекбоксы запрещают изменения, нужно сбросить флаги и установить значения из шаблонов.
            if (!this.CanChangeOrder
                && this.OrderChanged)
            {
                this.OrderChanged = false;
            }
            if (this.IsStageReadonly
                && this.RowChanged)
            {
                this.RowChanged = false;
            }

            this.TemplateStageOrder = runtimeStage.Order;
        }

        private void FillTemplateProperties(
            CardRow stageRow,
            IKrStageTemplate stageTemplate,
            IKrRuntimeStage runtimeStage)
        {
            var stageFromTemplate = stageRow.RowID == runtimeStage?.StageID;

            this.TemplateID = stageTemplate.ID;
            this.TemplateName = stageFromTemplate
                ? stageTemplate.Name
                : stageRow.Fields.TryGet<string>(KrConstants.KrStages.BasedOnStageTemplateName);
            this.TemplateOrder = stageFromTemplate
                ? stageTemplate.Order
                : stageRow.Fields.TryGet<int?>(KrConstants.KrStages.BasedOnStageTemplateOrder);
            this.GroupPosition = stageFromTemplate
                ? stageTemplate.Position
                : GroupPosition.GetByID(stageRow.Fields.TryGet<int?>(KrConstants.KrStages.BasedOnStageTemplateGroupPositionID));

            this.CanChangeOrder = stageTemplate.CanChangeOrder;
            this.IsStageReadonly = stageTemplate.IsStagesReadonly;

            // Если чекбоксы запрещают изменения, нужно сбросить флаги и установить значения из шаблонов.
            if (!this.CanChangeOrder
                && this.OrderChanged)
            {
                this.OrderChanged = false;
            }
            if (this.IsStageReadonly
                && this.RowChanged)
            {
                this.RowChanged = false;
            }

            if (stageFromTemplate)
            {
                this.TemplateStageOrder = runtimeStage.Order;
            }
        }

        private void FillTemplatePropertiesByDefaultValues()
        {
            this.TemplateID = null;
            this.TemplateName = null;
            this.TemplateOrder = null;
            this.CanChangeOrder = true;
            this.GroupPosition = GroupPosition.Unspecified;
            this.IsStageReadonly = false;
            this.TemplateStageOrder = null;
        }

        /// <summary>
        /// Возвращает список автоматически изменённых значений этапа.
        /// </summary>
        /// <returns>Список автоматически изменённых значений этапа. Если список отсутствовал в <see cref="Stage.InfoStorage"/>, то создаётся список в <see cref="Stage.InfoStorage"/> и возвращается. Тип элемента: <see cref="string"/>.</returns>
        private IList GetAutomaticallyChangedValues()
        {
            var list = this.TryGetAutomaticallyChangedValues();

            if (list is null)
            {
                list = new List<string>();

                this.InfoStorage[KrConstants.Keys.AutomaticallyChangedValues] = list;
            }

            return list;
        }

        /// <summary>
        /// Возвращает список автоматически изменённых значений этапа или значение <see langword="null"/>, если он не определён.
        /// </summary>
        /// <returns>Список автоматически изменённых значений этапа или значение <see langword="null"/>, если он не определён. Тип элемента: <see cref="string"/>.</returns>
        private IList TryGetAutomaticallyChangedValues() =>
            this.InfoStorage.TryGet<IList>(KrConstants.Keys.AutomaticallyChangedValues);

        /// <inheritdoc cref="EqualsWithAutomaticallyChangedValues"/>
        private bool EqualsWithAutomaticallyChangedValuesCore(
            Stage other,
            IList automaticallyChangedProperties = null)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // Состояние при этом сравнении не учитывается,
            // т.к. в рантайме оно может отличаться, но по факту этапы одинаковы.

            var equal =
                this.ID == other.ID
                && this.StageTypeID == other.StageTypeID
                && this.TemplateID == other.TemplateID
                && this.TemplateOrder == other.TemplateOrder
                && this.StageGroupID == other.StageGroupID
                && this.StageGroupOrder == other.StageGroupOrder
                && this.GroupPosition == other.GroupPosition
                && this.OrderChanged == other.OrderChanged
                && this.RowChanged == other.RowChanged
                && this.BasedOnTemplateStage == other.BasedOnTemplateStage
                && this.IsStageReadonly == other.IsStageReadonly
                && this.CanChangeOrder == other.CanChangeOrder
                && this.SqlPerformersIndex == other.SqlPerformersIndex
                && string.Equals(this.SqlPerformers, other.SqlPerformers, StringComparison.Ordinal)
                && string.Equals(this.StageTypeCaption, other.StageTypeCaption, StringComparison.Ordinal)
                && string.Equals(this.TemplateName, other.TemplateName, StringComparison.Ordinal)
                && string.Equals(this.Name, other.Name, StringComparison.Ordinal);

            if (!equal)
            {
                return false;
            }

            // Сравнение свойств, которые могли быть изменены автоматически.
            var hasAutomaticallyChangedProperties = automaticallyChangedProperties?.Count > 0;

            equal = (this.Planned == other.Planned
                    || hasAutomaticallyChangedProperties
                    && automaticallyChangedProperties.Contains(nameof(this.Planned)))
                && (this.Hidden == other.Hidden
                    || hasAutomaticallyChangedProperties
                    && automaticallyChangedProperties.Contains(nameof(this.Hidden)))
                && (this.CanBeSkipped == other.CanBeSkipped
                    || hasAutomaticallyChangedProperties
                    && automaticallyChangedProperties.Contains(nameof(this.CanBeSkipped)))
                && (NullableDoubleNumbersIsEqual(this.TimeLimit, other.TimeLimit)
                    || hasAutomaticallyChangedProperties
                    && automaticallyChangedProperties.Contains(nameof(this.TimeLimit)));

            if (!equal)
            {
                return false;
            }

            return StorageHelper.Equals(this.SettingsStorage, other.SettingsStorage);
        }

        #endregion

    }
}
