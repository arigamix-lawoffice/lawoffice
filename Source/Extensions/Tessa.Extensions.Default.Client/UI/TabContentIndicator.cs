using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Отображает знак наличия контента в ярлычке элемента управления кладками и, если разрешено, то и заголовке блока его содержащего, если поля элементов управления расположенных на вкладках контрола управления владками содержат значения.
    /// </summary>
    public sealed class TabContentIndicator
        : ControlContentIndicator<IFormWithBlocksViewModel>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TabContentIndicator"/>.
        /// </summary>
        /// <param name="tabControl">Элемент управления вкладками отслеживание полекй которого должно выполняться.</param>
        /// <param name="cardFieldContainer">Объект, являющийся контейнером для полей карточки. Значения данных полей проверяется на наличие данных.</param>
        /// <param name="fieldIDs">Словарь содержащий информацию о контролируемых полях содержащуются в метаданных секции.</param>
        /// <param name="updateBlockHeader">Значение <see langword="true"/>, если необходимо обновлять также заголовок блока содержащего <paramref name="tabControl"/>.</param>
        public TabContentIndicator(
            TabControlViewModel tabControl,
            ICardFieldContainer cardFieldContainer,
            IDictionary<Guid, string> fieldIDs,
            bool updateBlockHeader = default)
            : base(
                  tabControl?.Tabs,
                  cardFieldContainer,
                  fieldIDs,
                  updateBlockHeader ? tabControl.Block : default)
        {
        }

        #endregion

        #region Base Overriden

        /// <inheritdoc/>
        protected override string GetDisplayName(IFormWithBlocksViewModel control)
        {
            return control.TabCaption;
        }

        /// <inheritdoc/>
        protected override void SetDisplayName(IFormWithBlocksViewModel control, string name)
        {
            control.TabCaption = name;
        }

        /// <inheritdoc/>
        protected override void VisitControl(Visitor visitor, IFormWithBlocksViewModel control)
        {
            visitor.Visit(control);
        }

        #endregion
    }
}