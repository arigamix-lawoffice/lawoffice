using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Отображает знак наличия контента в элементе управления.
    /// </summary>
    /// <typeparam name="T">Тип контролируемых элементов управления. Тип должен быть одним из поддерживаемых объектом <see cref="BreadthFirstControlVisitor"/> выполняющим обход дерева элементов управления. Поддерживаемый тип соответствует аргументу типа одной из перегрузок метода <see cref="BreadthFirstControlVisitor.Visit"/>.</typeparam>
    /// <remarks>Контролируются только элементы управления типа <see cref="CardControlTypes.String"/>.</remarks>
    public abstract class ControlContentIndicator<T> :
        IDisposable
    {
        #region Nested types

        /// <summary>
        /// Выполняет обход дерева элементов управления в ширину.
        /// При выполнении обхода составляется массив содержащий связи между элементами управления и физическими полями.
        /// </summary>
        protected sealed class Visitor : BreadthFirstControlVisitor
        {
            /// <summary>
            /// Словарь содержащий: ключ - имя поля; значение - индекс элемента управления содержащего это поле.
            /// </summary>
            private readonly Dictionary<string, int> fieldSectionMapping;

            /// <summary>
            /// Словарь содержащий: ключ - идентификатор поля в схеме; значение - имя поля в схеме.
            /// </summary>
            private readonly IDictionary<Guid, string> fieldIDs;

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="Visitor"/>.
            /// </summary>
            /// <param name="fieldSectionMapping">Словарь содержащий: ключ - имя поля; значение - индекс элемента управления содержащего это поле.</param>
            /// <param name="fieldIDs">Словарь содержащий: ключ - идентификатор поля в схеме; значение - имя поля в схеме.</param>
            public Visitor(
                Dictionary<string, int> fieldSectionMapping,
                IDictionary<Guid, string> fieldIDs)
            {
                this.fieldIDs = fieldIDs;
                this.fieldSectionMapping = fieldSectionMapping;
            }

            /// <summary>
            /// Возвращает или задаёт порядковый номер элемента управления к которому относится поле.
            /// </summary>
            public int Index { get; set; }

            /// <inheritdoc />
            protected override void VisitControl(
                IControlViewModel controlViewModel)
            {
                if (controlViewModel.CardTypeControl is CardTypeEntryControl textBoxControl
                    && textBoxControl.Type == CardControlTypes.String)
                {
                    foreach (var colID in textBoxControl.PhysicalColumnIDList)
                    {
                        this.fieldSectionMapping[this.fieldIDs[colID]] = this.Index;
                    }
                }
            }

            /// <inheritdoc />
            protected override void VisitBlock(
                IBlockViewModel blockViewModel)
            {
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Словарь содержащий: ключ - имя поля; значение - индекс элемента управления содержащего это поле.
        /// </summary>
        private readonly Dictionary<string, int> fieldControlsMapping;

        /// <summary>
        /// Список содержащий списки названий физических полей.
        /// Порядковый номер элемента списка соответсвует порядковому номеру элемента управления его содержащего.
        /// Размерность равна <see cref="controls"/>.
        /// </summary>
        private readonly IReadOnlyList<IReadOnlyList<string>> controlFieldsMapping;

        /// <summary>
        /// Коллекция содержащая верхнеуровневые формы.
        /// </summary>
        private readonly IReadOnlyList<T> controls;

        /// <summary>
        /// Объект, являющийся контейнером для полей карточки. Значения данных полей проверяется на наличие данных.
        /// </summary>
        private readonly ICardFieldContainer cardFieldContainer;

        /// <summary>
        /// Модель представления блока содержащего элемент управления вкладками изменение дочерних элементов которого отслеживается.
        /// </summary>
        private readonly IBlockViewModel parentBlock;

        /// <summary>
        /// Массив содержащий значения отображаемых имён до их изменения.
        /// Размерность равна <see cref="controls"/>.
        /// </summary>
        private readonly string[] originalControlNames;

        /// <summary>
        /// Текст отображаемый в заголовке блока до изменения.
        /// </summary>
        private readonly string originalParentBlockName;

        /// <summary>
        /// Массив содержащий флаги, показывающие, что элемент управления содержит контент.
        /// Размерность равна <see cref="originalControlNames"/>.
        /// </summary>
        private readonly bool[] hasContentControls;

        /// <summary>
        /// Формат индикатора наличия данных.
        /// </summary>
        private readonly string indicatorFormat;

        private bool disposedValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlContentIndicator{T}"/>.
        /// </summary>
        /// <param name="controls">Коллекция контролируетмых элементов управления.</param>
        /// <param name="cardFieldContainer">Объект, являющийся контейнером для полей карточки. Значения данных полей проверяется на наличие данных.</param>
        /// <param name="fieldIDs">Словарь содержащий информацию о контролируемых полях содержащуются в метаданных секции.</param>
        /// <param name="parentBlockViewModel">Модель представления родительского блока в автоматическом UI карточки, в <see cref="IBlockViewModel.Caption"/> которого должен быть добавлен индикатор, если контролиремые элементы управления содержат данные.</param>
        /// <param name="indicatorFormat">Формат индикатора наличия данных.</param>
        protected ControlContentIndicator(
            IReadOnlyList<T> controls,
            ICardFieldContainer cardFieldContainer,
            IDictionary<Guid, string> fieldIDs,
            IBlockViewModel parentBlockViewModel = default,
            string indicatorFormat = "$KrProcess_TabContainsText")
        {
            Check.ArgumentNotNull(controls, nameof(controls));
            Check.ArgumentNotNull(cardFieldContainer, nameof(cardFieldContainer));
            Check.ArgumentNotNull(fieldIDs, nameof(fieldIDs));
            Check.ArgumentNotNull(indicatorFormat, nameof(indicatorFormat));

            this.controls = controls;
            this.cardFieldContainer = cardFieldContainer;
            this.indicatorFormat = indicatorFormat;

            if (parentBlockViewModel != null)
            {
                this.parentBlock = parentBlockViewModel;
                this.originalParentBlockName = this.parentBlock.Caption;
            }

            // Построение связей между физическими полями вложенных элементов управления и элементами управления их содержащими - элементом управления вкладками.
            var fieldControlsMapping = new Dictionary<string, int>(StringComparer.Ordinal);
            var visitor = new Visitor(fieldControlsMapping, fieldIDs);

            var controlsCount = controls.Count;
            this.originalControlNames = new string[controlsCount];

            for (var i = 0; i < controlsCount; i++)
            {
                var control = controls[i];

                if (control is null)
                {
                    throw new ArgumentException("The parameter contains a null value.", nameof(controls));
                }

                this.originalControlNames[i] = this.GetDisplayName(control);
                visitor.Index = i;
                this.VisitControl(visitor, control);
            }

            this.fieldControlsMapping = fieldControlsMapping;

            this.controlFieldsMapping =
                fieldControlsMapping
                    .GroupBy(p => p.Value)
                    .Select(p => new { order = p.Key, fields = p.Select(q => q.Key).ToArray() })
                    .OrderBy(p => p.order)
                    .Select(p => p.fields)
                    .ToArray();

            this.hasContentControls = new bool[controlsCount];

            this.Update();
            this.cardFieldContainer.FieldChanged += this.FieldChangedAction;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Посещает с помощью <paramref name="visitor"/> указанный элемент управления.
        /// </summary>
        /// <param name="visitor">Объект выполняющий обход элемента управления.</param>
        /// <param name="control">Посещаемый элемент управления.</param>
        protected abstract void VisitControl(Visitor visitor, T control);

        /// <summary>
        /// Возвращает отображаемое имя элемента управления.
        /// </summary>
        /// <param name="control">Элемент управления.</param>
        /// <returns>Отображаемое имя.</returns>
        protected abstract string GetDisplayName(T control);

        /// <summary>
        /// Задаёт отображаемое имя элемента управления.
        /// </summary>
        /// <param name="control">Элемент управления.</param>
        /// <param name="name">Отображаемое имя.</param>
        protected abstract void SetDisplayName(T control, string name);

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (this.disposedValue)
            {
                return;
            }

            this.cardFieldContainer.FieldChanged -= this.FieldChangedAction;

            this.disposedValue = true;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Обновляет отображаемый текст формы и, если задан родительский блок, то и его заголовок.
        /// </summary>
        private void Update()
        {
            for (var i = 0; i < this.controls.Count; i++)
            {
                var hasContent = this.CheckContent(i);
                this.UpdateControlName(i, hasContent);

                this.hasContentControls[i] = hasContent;
            }

            this.UpdateParentBlockCaption();
        }

        /// <summary>
        /// Обработчик события изменения значения поля.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void FieldChangedAction(object sender, CardFieldChangedEventArgs e)
        {
            if (!this.fieldControlsMapping.TryGetValue(e.FieldName, out var index))
            {
                return;
            }

            this.Update(index);
            this.UpdateParentBlockCaption();
        }
        
        /// <summary>
        /// Обновляет информацию о наличии изменений в контролируемом элементе управления.
        /// </summary>
        /// <param name="index">Порядковый номер контролируемого элемента управления.</param>
        private void Update(int index)
        {
            var hasContent = this.CheckContent(index);
            this.UpdateControlName(index, hasContent);

            this.hasContentControls[index] = hasContent;
        }

        private void UpdateControlName(int index, bool hasContent)
        {
            var control = this.controls[index];

            this.SetDisplayName(
                control,
                hasContent
                ? this.GetNewName(this.originalControlNames[index])
                : this.originalControlNames[index]);
        }

        private bool CheckContent(int index) =>
            this.controlFieldsMapping[index].Any(i => !string.IsNullOrWhiteSpace(this.cardFieldContainer.Fields.Get<string>(i)));

        private void UpdateParentBlockCaption()
        {
            if (this.parentBlock != null)
            {
                this.parentBlock.Caption = this.hasContentControls.Any(p => p)
                    ? this.GetNewName(this.originalParentBlockName)
                    : this.originalParentBlockName;
            }
        }

        private string GetNewName(string originalName) =>
            LocalizationManager.Format(this.indicatorFormat, originalName);

        #endregion
    }
}