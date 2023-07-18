using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Предоставляет методы для манипулирования этапами процесса.
    /// </summary>
    public sealed class StagesContainer
    {
        #region boolean comparer for CanChangeOrder

        private sealed class CanChangeOrderComparer : IComparer<Tuple<int?, bool>>
        {
            #region IComparer Members

            /// <summary>
            /// Сравнение для сортировки этапов согласования в подгруппах "в начале", "в конце", "не определено". 
            /// Если группа "В начале", ID == 0, то порядок false меньше true
            /// Если группа "В конце", ID == 1, то порядок true меньше false
            /// Если группа "не определено" и ID == null, то true == false
            /// </summary>
            /// <param name="x">Первый сравниваемый объект.</param>
            /// <param name="y">Второй сравниваемый объект.</param>
            /// <returns>
            /// Знаковое целое число, которое определяет относительные значения параметров <paramref name="x"/>
            /// и <paramref name="y"/>, как показано в следующей таблице.
            /// Значение – Значение
            /// Меньше нуля – Значение <paramref name="x"/> меньше <paramref name="y"/>.
            /// Нуль – <paramref name="x"/> равняется <paramref name="y"/>.
            /// Больше нуля – Значение <paramref name="x"/> больше значения <paramref name="y"/>.
            /// </returns>
            public int Compare(Tuple<int?, bool> x, Tuple<int?, bool> y)
            {
                // на этом моменте предполагается x.Item1 == x.Item2,
                // т.к. сортировка по CanChangeOrder ведется второй
                if (x.Item1 is null || x.Item2 == y.Item2)
                {
                    return 0;
                }

                if (x.Item1 == 0 && !x.Item2 && y.Item2
                    || x.Item1 == 1 && x.Item2 && !y.Item2)
                {
                    return -1;
                }

                return 1;
            }

            #endregion

            #region Instance Static Property

            /// <doc path='info[@type="object" and @item="Instance"]'/>
            public static CanChangeOrderComparer Instance { get; } = new CanChangeOrderComparer();

            #endregion
        }

        #endregion

        #region Fields

        private readonly IObjectModelMapper objectModelMapper;

        private readonly WorkflowProcess process;

        private bool needSorting;

        private readonly Guid stageGroupID;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StagesContainer"/>.
        /// </summary>
        /// <param name="objectModelMapper">Объект обеспечивающий работу с хранилищами Kr процесса и объектной моделью процесса.</param>
        /// <param name="process">Объектная модель процесса.</param>
        /// <param name="stageGroupID">Идентификатор группы этапов для которой был создан контейнер.</param>
        /// <remarks>
        /// Конструктор создает контейнер для этапов наследования 
        /// на основе существующей карточки с этапом согласования.
        /// Предполагается, что в карточке есть секции с этапами, согласующими и доп. согласующими.
        /// Необходимо учитывать, что этапы в карточке уже могли быть созданы на основе шаблона.
        /// Так как для этапов хранится только ID, KrStageTemplates и StageRowID, необходима исходная карточка шаблона.
        /// </remarks>
        public StagesContainer(
            IObjectModelMapper objectModelMapper,
            WorkflowProcess process,
            Guid stageGroupID)
        {
            Check.ArgumentNotNull(objectModelMapper, nameof(objectModelMapper));
            Check.ArgumentNotNull(process, nameof(process));

            this.objectModelMapper = objectModelMapper;
            this.process = process;
            this.stageGroupID = stageGroupID;

            this.DetermineUserModifiedArea();

            this.needSorting = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает коллекцию этапов на момент формирования маршрута.
        /// </summary>
        public SealableObjectList<Stage> InitialStages => this.process.InitialWorkflowProcess.Stages;

        /// <summary>
        /// Возвращает коллекцию этапов.
        /// </summary>
        /// <remarks>При необходимости выполняет сортировку этапов.</remarks>
        public SealableObjectList<Stage> Stages
        {
            get
            {
                if (this.needSorting)
                {
                    this.process.Stages = GetSortedStages(this.process.Stages).ToSealableObjectList();
                    this.needSorting = false;
                }
                return this.process.Stages;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Вставляет новые этапы в список существующих этапов.
        /// </summary>
        /// <param name="stage">Массив новых этапов.</param>
        public void InsertStage(params Stage[] stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));

            this.MergeStages(stage);
        }

        /// <summary>
        /// Заменяет этап расположенный по индексу <paramref name="index"/> на новый.
        /// </summary>
        /// <param name="index">Порядковый индекс заменяемого этапа.</param>
        /// <param name="stage">Новый этап.</param>
        public void ReplaceStage(int index, Stage stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));

            this.process.Stages[index] = stage;
            this.needSorting = true;
        }

        /// <summary>
        /// Объединяет существующие этапы с этапами из карточки шаблона этапов.
        /// </summary>
        /// <param name="template">Шаблон этапов.</param>
        /// <param name="stages">Доступная только для чтения коллекция этапов содержащихся в шаблоне <paramref name="template"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task MergeWithAsync(
            IKrStageTemplate template,
            IReadOnlyList<IKrRuntimeStage> stages,
            CancellationToken cancellationToken = default)
        {
            // Параметры template и stages будут проверены в CardRowsToObjectModelAsync.
            var templateStages = await this.objectModelMapper.CardRowsToObjectModelAsync(
                stageTemplate: template,
                runtimeStages: stages,
                primaryPci: null,
                initialStage: false,
                saveInitialStages: false,
                cancellationToken: cancellationToken);

            this.MergeStages(templateStages.Stages);
        }

        /// <summary>
        /// Объединяет существующие этапы с этапами из карточки шаблона этапов.
        /// </summary>
        /// <param name="templates">Перечисление шаблонов этапов.</param>
        /// <param name="stages">Доступная только для чтения коллекция пар ключ - значение содержащая: ключ - идентификатор шаблона этапов, значение - доступная только для чтения коллекция этапов содержащихся в шаблоне имеющим заданный идентификатор.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task MergeWithAsync(
            IEnumerable<IKrStageTemplate> templates,
            IReadOnlyDictionary<Guid, IReadOnlyList<IKrRuntimeStage>> stages,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(templates, nameof(templates));
            Check.ArgumentNotNull(stages, nameof(stages));

            var templateStages = new List<Stage>();

            foreach (var template in templates)
            {
                var st = stages.TryGetValue(template.ID, out var s) ? s : EmptyHolder<IKrRuntimeStage>.Collection;
                var templateStagesTemp =
                    (await this.objectModelMapper.CardRowsToObjectModelAsync(
                        stageTemplate: template,
                        runtimeStages: st,
                        primaryPci: null,
                        initialStage: false,
                        saveInitialStages: false,
                        cancellationToken: cancellationToken)).Stages;

                templateStages.AddRange(templateStagesTemp);
            }

            this.MergeStages(templateStages);
        }

        /// <summary>
        /// Удалить этапы, подставленные из шаблонов ранее, которые при текущем пересчете не заменены.
        /// </summary>
        public void DeleteUnconfirmedStages()
        {
            // Оставляем только ручные, обновленные или измененные пользователем
            this.process.Stages = this.process.Stages
                .Where(this.StageHasRightToLive)
                .ToSealableObjectList();

            foreach (var stage in this.process.Stages)
            {
                // Этапы с флагом InitialStage, которые дошли до этого момента, изменены пользователем
                // Значит шаблон не подтвержден или удален.
                // Нельзя позволять таким этапам лезть наверх.
                if (KrCompilersHelper.ReferToGroup(this.stageGroupID, stage)
                    && stage.BasedOnTemplateStage
                    && stage.InitialStage)
                {
                    stage.SetCanChangeOrderTrue();
                }
            }

            this.needSorting = true;
        }

        /// <summary>
        /// Восстановление всем этапам внутри контейнера флага "Начальный этап".
        /// </summary>
        public void RestoreFlags()
        {
            var currentGroupProcessed = false;
            foreach (var stage in this.process.Stages)
            {
                if (KrCompilersHelper.ReferToGroup(this.stageGroupID, stage))
                {
                    currentGroupProcessed = true;

                    if (stage.InitialStage)
                    {
                        // Если этап дожил до текущего момента как начальный,
                        // то плохие новости - его шаблон потерялся и этап теперь без шаблона.
                        stage.UnbindTemplate = true;
                    }
                    else
                    {
                        stage.InitialStage = true;
                    }
                }
                else if (currentGroupProcessed)
                {
                    break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает значение, показывающее, может ли этап остаться после пересчёта.
        /// </summary>
        /// <param name="stage">Проверяемый этап.</param>
        /// <returns>Значение <see langword="true"/>, если этап может остаться после пересчёта, иначе - <see langword="false"/>.</returns>
        private bool StageHasRightToLive(
            Stage stage) =>
            !KrCompilersHelper.ReferToGroup(this.stageGroupID, stage)
            || !stage.BasedOnTemplate
            || !stage.InitialStage
            || stage.RowChanged
            || stage.OrderChanged;

        /// <summary>
        /// Сортирует указанную коллекцию этапов.
        /// </summary>
        /// <returns>
        /// Перечисление содержащее отсортированные этапы.
        /// </returns>
        /// <remarks>
        /// Правило сортировки.<para/>
        /// Сортировка сначала разделяет все этапы по группам.
        /// Сортировка для групп происходит по паре (<see cref="Stage.StageGroupOrder"/>, <see cref="Stage.StageGroupID"/>),
        /// что позволяет получить уникальность каждого элемента и стабильность сортировки.<para/>
        ///
        /// В каждой группе проводится сортировка по следующим признакам:<para/>
        /// <see cref="GroupPosition.AtFirst"/>(ID=0) &amp;&amp; !<see cref="Stage.CanChangeOrder"/><para/>
        /// <see cref="GroupPosition.AtFirst"/>(ID=0) &amp;&amp; <see cref="Stage.CanChangeOrder"/><para/>
        /// <see cref="GroupPosition.Unspecified"/>(ID=0.5)<para/>
        /// <see cref="GroupPosition.AtLast"/>(ID=1) &amp;&amp; <see cref="Stage.CanChangeOrder"/><para/>
        /// <see cref="GroupPosition.AtLast"/>(ID=1) &amp;&amp; !<see cref="Stage.CanChangeOrder"/><para/>
        /// 
        /// Внутри каждой такой подгруппы производится дополнительная сортировка по <see cref="Stage.TemplateOrder"/>.
        /// Это поле хранит TemplateOrder из карточки шаблона этапов KrStageTemplates. Данный <see cref="Stage.TemplateOrder"/> 
        /// не имеет отношения к обычному Order в таблице этапов карточки.<para/>
        /// 
        /// Последним ключом сортировки является <see cref="Stage.TemplateStageOrder"/> этапов из шаблона.
        /// Это необходимо для переноса порядка сортировки из дочернего маршрута шаблона в целый маршрут документа.<para/>
        /// 
        /// Важным свойством является стабильность <see cref="Enumerable.OrderBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/> сортировки.
        /// Источник: https://docs.microsoft.com/ru-ru/dotnet/api/system.linq.enumerable.orderby абзац "комментарии", предпоследнее предложение.
        /// "This method performs a stable sort; that is, if the keys of two elements are equal, the order of the elements is preserved."
        /// Это позволяет при сортировке сохранить порядок Unspecified этапов таким, каким его указал пользователь.
        /// </remarks>
        private static IOrderedEnumerable<Stage> GetSortedStages(ICollection<Stage> stages)
        {
            return stages
                .OrderBy(p => p.StageGroupOrder)
                .ThenBy(p => p.StageGroupID)
                .ThenBy(x => x.GroupPosition.ID ?? 0.5)
                .ThenBy(x => new Tuple<int?, bool>(x.GroupPosition.ID, x.CanChangeOrder), CanChangeOrderComparer.Instance)
                .ThenBy(x => x.TemplateOrder)
                .ThenBy(x => x.TemplateStageOrder);
        }

        /// <summary>
        /// Выполняет поиск области в середине списка, в которую пользователь вносил изменения.
        /// </summary>
        /// <remarks>
        /// Для изменённых этапов будет выставлен <see cref="GroupPosition.Unspecified"/>, что позволит сохранить положение, указанное пользователем.
        /// </remarks>
        private void DetermineUserModifiedArea()
        {
            // 1. Группировка по группам этапов.
            var groupedCurrentStages = this.process.Stages
                .GroupBy(i => i.StageGroupID)
                .Select(i => i.ToArray());

            // 2. Определение областей модифицированных этапов в каждой из групп.
            foreach (var currentStages in groupedCurrentStages)
            {
                var firstUserModifiedStageIndex = currentStages.IndexOf(
                    p => p.GroupPosition == GroupPosition.Unspecified
                        || p.GroupPosition == GroupPosition.AtLast
                        || p.OrderChanged);

                if (firstUserModifiedStageIndex == -1)
                {
                    // Все этапы по шаблонам.
                    continue;
                }

                var lastUserModifiedStageIndex = currentStages.LastIndexOf(
                    p => p.GroupPosition == GroupPosition.Unspecified
                        || p.GroupPosition == GroupPosition.AtFirst
                        || p.OrderChanged);

                if (lastUserModifiedStageIndex == -1)
                {
                    // Довольно странное поведение. Если нашли выше, 
                    // то скорее всего и здесь должны что нибудь найти
                    continue;
                }

                // Помечаем область, тронутую пользователем, как "неопределенную"
                // Для всех элементов сортировка применяется по одному ключу,
                // а, за счет стабильности linq-сортировки, этапы передвинуты не будут.
                for (var currentStageIndex = firstUserModifiedStageIndex;
                    currentStageIndex <= lastUserModifiedStageIndex;
                    currentStageIndex++)
                {
                    var currentStage = currentStages[currentStageIndex];

                    // Необходимо исключить возможные вкрапления неперемещаемых этапов из 
                    // центральной области "неопределенной" группы, чтобы при сортировки
                    // они встали на свои места.
                    if (currentStage.CanChangeOrder)
                    {
                        currentStage.SetGroupPositionUnspecified();
                    }
                }
            }
        }

        #endregion

        #region Merge stages

        /// <summary>
        /// Объединяет существующие этапы с новыми.
        /// </summary>
        /// <param name="newStages">Коллекция содержащее новые этапы.</param>
        private void MergeStages(ICollection<Stage> newStages)
        {
            if (!newStages.Any())
            {
                return;
            }

            var currentStages = this.process.Stages;
            var oldStagesTable = new Dictionary<Guid, int>(currentStages.Count);

            for (var stageIndex = 0; stageIndex < currentStages.Count; stageIndex++)
            {
                oldStagesTable.Add(currentStages[stageIndex].ID, stageIndex);
            }

            foreach (var newStage in newStages)
            {
                if (oldStagesTable.TryGetValue(newStage.ID, out var oldStageIndex))
                {
                    var currentStage = currentStages[oldStageIndex];

                    // Меняем только если строка не изменена пользователем.
                    if (!currentStage.RowChanged)
                    {
                        newStage.Inherit(currentStage);
                        currentStages[oldStageIndex] = newStage;
                    }
                    else
                    {
                        currentStage.InitialStage = false;
                        // Если порядок не изменен, нужно добавить для обновления порядка сортировки
                        if (!currentStage.OrderChanged)
                        {
                            currentStage.InheritPosition(newStage);
                        }
                    }
                }
                else
                {
                    currentStages.Add(newStage);
                }
            }

            this.needSorting = true;
        }

        #endregion

    }
}
