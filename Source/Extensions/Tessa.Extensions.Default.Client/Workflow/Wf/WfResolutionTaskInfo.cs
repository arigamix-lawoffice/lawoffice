using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Tasks;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Информация по резолюции Workflow, которая используется для построения UI заданий.
    /// </summary>
    public sealed class WfResolutionTaskInfo
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений его свойств.
        /// </summary>
        /// <param name="control">
        /// Модель представления для элемента управления, выводящего задание.
        /// </param>
        /// <param name="settings">
        /// Настройки решения для Wf.
        /// </param>
        /// <param name="hasChildren">
        /// Признак того, что резолюция содержит дочерние резолюции,
        /// которые должны выводиться таблицей.
        /// </param>
        /// <param name="hasIncompleteChildren">
        /// Признак того, что резолюция содержит незавершённые дочерние резолюции.
        /// </param>
        public WfResolutionTaskInfo(
            TaskViewModel control,
            KrSettings settings,
            bool hasChildren,
            bool hasIncompleteChildren)
        {
            this.control = control;
            this.settings = settings;
            this.hasChildren = hasChildren;
            this.hasIncompleteChildren = hasIncompleteChildren;
        }

        #endregion

        #region Fields

        private CardSection resolutionSection;          // = null

        private KrSettings settings;

        private ListStorage<CardRow> performersRows;    // = null

        private List<CardRow> subscribedPerformers;     // = null

        /// <summary>
        /// Признак того, что галка MassCreation "Отдельная задача каждому исполнителю"
        /// была снята, т.к. количество исполнителей меньше двух.
        /// </summary>
        private bool massCreationWasReset;              // = false

        #endregion

        #region Private Methods

        private void GetMultiplePerformersSettings(
            out object massCreation,
            out object majorPerformer)
        {
            Func<object, WfMultiplePerformersDefaults> func = this.settings.GetMultiplePerformersDefaults;
            WfMultiplePerformersDefaults defaults = func != null ? func(this) : WfMultiplePerformersDefaults.Default;

            switch (defaults)
            {
                case WfMultiplePerformersDefaults.MergeIntoSingleTask:
                    massCreation = BooleanBoxes.False;
                    majorPerformer = BooleanBoxes.False;
                    break;

                case WfMultiplePerformersDefaults.CreateMultipleTasks:
                    massCreation = BooleanBoxes.True;
                    majorPerformer = BooleanBoxes.False;
                    break;

                case WfMultiplePerformersDefaults.CreateMultipleTasksWithMajorPerformer:
                    massCreation = BooleanBoxes.True;
                    majorPerformer = BooleanBoxes.True;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("defaults");
            }
        }

        #endregion

        #region TryGetBlock Private Methods

        private IFormWithBlocksViewModel cachedForMainInfoForm;

        private IBlockViewModel cachedMainInfoBlock;

        private IFormWithBlocksViewModel cachedForPerformersForm;

        private IBlockViewModel cachedPerformersBlock;

        private IBlockViewModel TryGetBlock(string blockName, ref IFormWithBlocksViewModel cachedForm, ref IBlockViewModel cachedBlock)
        {
            IFormWithBlocksViewModel form = this.control.Workspace.Form;
            if (form == null)
            {
                return null;
            }

            if (form == cachedForm)
            {
                // && form != null
                return cachedBlock;
            }

            cachedForm = form;
            return cachedBlock = form.Blocks.FirstOrDefault(x => x.Name == blockName);
        }

        private IBlockViewModel TryGetMainInfoBlock()
        {
            return this.TryGetBlock(WfUIHelper.MainInfoBlockName, ref this.cachedForMainInfoForm, ref this.cachedMainInfoBlock);
        }

        private IBlockViewModel TryGetPerformersBlock()
        {
            return this.TryGetBlock(WfUIHelper.PerformersBlockName, ref this.cachedForPerformersForm, ref this.cachedPerformersBlock);
        }

        #endregion

        #region Properties

        private readonly TaskViewModel control;

        /// <summary>
        /// Модель представления для элемента управления, выводящего задание.
        /// </summary>
        public TaskViewModel Control
        {
            get { return this.control; }
        }


        private readonly bool hasChildren;

        /// <summary>
        /// Признак того, что резолюция содержит дочерние резолюции,
        /// которые должны выводиться таблицей.
        /// </summary>
        public bool HasChildren
        {
            get { return this.hasChildren; }
        }


        private readonly bool hasIncompleteChildren;

        /// <summary>
        /// Признак того, что резолюция содержит незавершённые дочерние резолюции.
        /// </summary>
        public bool HasIncompleteChildren
        {
            get { return this.hasIncompleteChildren; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Выполняет отписку от всех событий, подписка которых контролируется.
        /// </summary>
        public void Unsubscribe()
        {
            this.UnsubscribeFromResolutionSection();
            this.UnsubscribeFromPerformers();
        }


        /// <summary>
        /// Обновляет состояние всех элементов управления, подписка на события которых контролируется.
        /// Выполняется при обновлении формы с элементами управления.
        /// </summary>
        public void Update()
        {
            this.UpdateWithControl();
            this.UpdateShowAdditionalControls();
            this.UpdateMassCreation();
            this.UpdateIncompleteChildResolutionsControls();

            this.UpdateMultiplePerformersControls();
        }

        #endregion

        #region ResolutionSection Methods

        /// <summary>
        /// Выполняет подписку на основную секции резолюции <paramref name="resolutionSection"/>.
        /// Перед подпиской выполняется отписка от предыдущей секции, установленной другим вызовом этого метода.
        /// </summary>
        /// <param name="resolutionSection">Основная секция резолюции. Не может быть равна <c>null</c>.</param>
        public void SubscribeToResolutionSectionAndUpdate(CardSection resolutionSection)
        {
            if (resolutionSection == null)
            {
                throw new ArgumentNullException("resolutionSection");
            }

            this.UnsubscribeFromResolutionSection();

            resolutionSection.FieldChanged -= this.OnResolutionFieldChanged;
            resolutionSection.FieldChanged += this.OnResolutionFieldChanged;

            this.resolutionSection = resolutionSection;

            this.UpdateWithControl();
            this.UpdateShowAdditionalControls();
            this.UpdateMassCreation();
            this.UpdateIncompleteChildResolutionsControls();
        }


        /// <summary>
        /// Выполняет отписку от основной секции резолюции, подписка на которую была выполнена
        /// методом <see cref="SubscribeToPerformersAndUpdate"/>.
        /// </summary>
        public void UnsubscribeFromResolutionSection()
        {
            if (this.resolutionSection != null)
            {
                this.resolutionSection.FieldChanged -= this.OnResolutionFieldChanged;
                this.resolutionSection = null;
            }
        }


        /// <summary>
        /// Обработчик события для изменения полей в основной секции задания.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnResolutionFieldChanged(object sender, CardFieldChangedEventArgs e)
        {
            switch (e.FieldName)
            {
                case WfHelper.ResolutionWithControlField:
                    this.UpdateWithControl();
                    break;

                case WfHelper.ResolutionShowAdditionalField:
                    this.UpdateShowAdditionalControls();
                    break;

                case WfHelper.ResolutionMassCreationField:
                    this.UpdateMassCreation();
                    break;
            }
        }


        /// <summary>
        /// Обновляет состояние элементов управления с суффиксом <see cref="WfUIHelper.WithControlSuffix"/>.
        /// Перед вызовом метода должен быть вызван метод <see cref="SubscribeToResolutionSectionAndUpdate"/>,
        /// в противном случае метод не выполняет действий.
        /// </summary>
        private void UpdateWithControl()
        {
            if (this.resolutionSection == null)
            {
                return;
            }

            IBlockViewModel mainInfoBlock = this.TryGetMainInfoBlock();
            if (mainInfoBlock != null)
            {
                bool withControl = resolutionSection.RawFields.TryGet<bool>(WfHelper.ResolutionWithControlField);
                WfUIHelper.SetControlVisibility(mainInfoBlock, WfUIHelper.WithControlSuffix, withControl);
            }
        }


        /// <summary>
        /// Обновляет состояние элементов управления с суффиксом <see cref="WfUIHelper.AdditionalSuffix"/>.
        /// Перед вызовом метода должен быть вызван метод <see cref="SubscribeToResolutionSectionAndUpdate"/>,
        /// в противном случае метод не выполняет действий.
        /// </summary>
        private void UpdateShowAdditionalControls()
        {
            if (this.resolutionSection == null)
            {
                return;
            }

            IBlockViewModel mainInfoBlock = this.TryGetMainInfoBlock();
            if (mainInfoBlock != null)
            {
                bool showAdditional = resolutionSection.RawFields.TryGet<bool>(WfHelper.ResolutionShowAdditionalField);
                WfUIHelper.SetControlVisibility(mainInfoBlock, WfUIHelper.AdditionalSuffix, showAdditional);
            }
        }


        /// <summary>
        /// Обновляет состояние элементов управления с суффиксом <see cref="WfUIHelper.WithControlSuffix"/>.
        /// Перед вызовом метода должен быть вызван метод <see cref="SubscribeToResolutionSectionAndUpdate"/>,
        /// в противном случае метод не выполняет действий.
        /// </summary>
        private void UpdateMassCreation()
        {
            if (this.resolutionSection == null)
            {
                return;
            }

            bool massCreation = resolutionSection.RawFields.TryGet<bool>(WfHelper.ResolutionMassCreationField);
            if (!massCreation)
            {
                resolutionSection.Fields[WfHelper.ResolutionMajorPerformerField] = BooleanBoxes.False;
            }

            IBlockViewModel performersBlock = this.TryGetPerformersBlock();
            if (performersBlock != null)
            {
                WfUIHelper.SetControlVisibility(performersBlock, WfUIHelper.MassCreationSuffix, massCreation);
            }
        }


        /// <summary>
        /// Обновляет состояние элементов управления с суффиксом <see cref="WfUIHelper.ChildResolutionsSuffix"/>.
        /// Перед вызовом метода должен быть вызван метод <see cref="SubscribeToResolutionSectionAndUpdate"/>,
        /// в противном случае метод не выполняет действий.
        /// </summary>
        private void UpdateIncompleteChildResolutionsControls()
        {
            if (this.resolutionSection == null)
            {
                return;
            }

            IBlockViewModel mainInfoBlock = this.TryGetMainInfoBlock();
            if (mainInfoBlock != null)
            {
                WfUIHelper.SetControlVisibility(mainInfoBlock, WfUIHelper.ChildResolutionsSuffix, this.hasIncompleteChildren);
            }
        }

        #endregion

        #region PerformersRows Methods

        /// <summary>
        /// Выполняет подписку на исполнителей задания <paramref name="performersRows"/>.
        /// Перед подпиской выполняется отписка от предыдущих исполнителей, установленных другим вызовом этого метода.
        /// </summary>
        /// <param name="performersRows">Строки исполнителей резолюции. Не могут быть равны <c>null</c>.</param>
        public void SubscribeToPerformersAndUpdate(ListStorage<CardRow> performersRows)
        {
            if (performersRows == null)
            {
                throw new ArgumentNullException("performersRows");
            }

            this.UnsubscribeFromPerformers();

            foreach (CardRow performer in performersRows)
            {
                performer.StateChanged -= this.OnPerformerStateChanged;
                performer.StateChanged += this.OnPerformerStateChanged;

                if (this.subscribedPerformers == null)
                {
                    this.subscribedPerformers = new List<CardRow>();
                }

                this.subscribedPerformers.Add(performer);
            }

            performersRows.ItemChanged -= this.OnPerformersItemChanged;
            performersRows.ItemChanged += this.OnPerformersItemChanged;

            this.performersRows = performersRows;

            this.UpdateMultiplePerformersControls();
        }


        /// <summary>
        /// Выполняет отписку от исполнителей задания, подписка на которые была выполнена
        /// методом <see cref="SubscribeToPerformersAndUpdate"/>.
        /// </summary>
        public void UnsubscribeFromPerformers()
        {
            if (this.subscribedPerformers != null)
            {
                foreach (CardRow performer in this.subscribedPerformers)
                {
                    performer.StateChanged -= this.OnPerformerStateChanged;
                }

                this.subscribedPerformers.Clear();
            }

            if (this.performersRows != null)
            {
                this.performersRows.ItemChanged -= this.OnPerformersItemChanged;
                this.performersRows = null;
            }
        }


        /// <summary>
        /// Обработчик события по изменению состояния строк в секции с исполнителями резолюции.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnPerformerStateChanged(object sender, CardRowStateEventArgs e)
        {
            if (e.OldState == CardRowState.Deleted || e.NewState == CardRowState.Deleted)
            {
                this.UpdateMultiplePerformersControls();
            }
        }


        /// <summary>
        /// Обработчик события по изменению набора строк в секции с исполнителями резолюции.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void OnPerformersItemChanged(object sender, ListStorageItemEventArgs<CardRow> e)
        {
            switch (e.Action)
            {
                case ListStorageAction.Insert:
                    e.Item.StateChanged -= this.OnPerformerStateChanged;
                    e.Item.StateChanged += this.OnPerformerStateChanged;
                    if (this.subscribedPerformers == null)
                    {
                        this.subscribedPerformers = new List<CardRow>();
                    }
                    this.subscribedPerformers.Add(e.Item);
                    break;

                case ListStorageAction.Remove:
                    if (e.HasItem)
                    {
                        CardRow item = e.Item;
                        item.StateChanged -= this.OnPerformerStateChanged;

                        if (this.subscribedPerformers != null)
                        {
                            this.subscribedPerformers.Remove(item);
                        }
                    }
                    break;

                case ListStorageAction.Clear:
                    if (this.subscribedPerformers != null)
                    {
                        foreach (CardRow performer in this.subscribedPerformers)
                        {
                            performer.StateChanged -= this.OnPerformerStateChanged;
                        }

                        this.subscribedPerformers.Clear();
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.UpdateMultiplePerformersControls();
        }


        /// <summary>
        /// Обновляет состояние элементов управления с суффиксом <see cref="WfUIHelper.MultiplePerformersSuffix"/>.
        /// Перед вызовом метода должен быть вызван метод <see cref="SubscribeToPerformersAndUpdate"/>,
        /// в противном случае метод не выполняет действий.
        /// </summary>
        private void UpdateMultiplePerformersControls()
        {
            if (this.performersRows == null)
            {
                return;
            }

            IBlockViewModel performersBlock = this.TryGetPerformersBlock();
            if (performersBlock != null)
            {
                // вызов Count() выполняет полный проход коллекции, а нам надо узнать, есть ли хотя бы два элемента
                bool hasMultiplePerformers = this.performersRows.Where(x => x.State != CardRowState.Deleted).Skip(1).Any();

                // метод выполняется каждый раз для новой формы
                WfUIHelper.SetControlVisibility(performersBlock, WfUIHelper.MultiplePerformersSuffix, hasMultiplePerformers);

                // сбрасываем значение галки "Отправить каждому исполнителю" при её скрытии
                if (this.resolutionSection != null)
                {
                    if (hasMultiplePerformers)
                    {
                        if (this.massCreationWasReset)
                        {
                            object massCreation;
                            object majorPerformer;
                            GetMultiplePerformersSettings(out massCreation, out majorPerformer);

                            this.resolutionSection.Fields[WfHelper.ResolutionMassCreationField] = massCreation;
                            this.resolutionSection.Fields[WfHelper.ResolutionMajorPerformerField] = majorPerformer;
                            this.massCreationWasReset = false;
                        }
                    }
                    else
                    {
                        this.resolutionSection.Fields[WfHelper.ResolutionMassCreationField] = BooleanBoxes.False;
                        this.resolutionSection.Fields[WfHelper.ResolutionMajorPerformerField] = BooleanBoxes.False;
                        this.massCreationWasReset = true;
                    }
                }
            }
        }

        #endregion
    }
}
