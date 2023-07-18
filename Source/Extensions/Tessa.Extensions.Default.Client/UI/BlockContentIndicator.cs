using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Отображает знак наличия контента в заданном блоке.
    /// </summary>
    public sealed class BlockContentIndicator
        : ControlContentIndicator<IBlockViewModel>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BlockContentIndicator"/>.
        /// </summary>
        /// <param name="block">Элемент управления, отслеживание полей которого должно выполняться.</param>
        /// <param name="cardFieldContainer">Объект, являющийся контейнером для полей карточки. Значения данных полей проверяется на наличие данных.</param>
        /// <param name="fieldIDs">Словарь содержащий информацию о контролируемых полях содержащуются в метаданных секции.</param>
        public BlockContentIndicator(
            IBlockViewModel block,
            ICardFieldContainer cardFieldContainer,
            IDictionary<Guid, string> fieldIDs)
            : base(
                  new[] { block },
                  cardFieldContainer,
                  fieldIDs)
        {

        }

        #endregion

        #region Base Overriden
        
        /// <inheritdoc/>
        protected override string GetDisplayName(IBlockViewModel control)
        {
            return control.Caption;
        }

        /// <inheritdoc/>
        protected override void SetDisplayName(IBlockViewModel control, string name)
        {
            control.Caption = name;
        }

        /// <inheritdoc/>
        protected override void VisitControl(Visitor visitor, IBlockViewModel control)
        {
            visitor.Visit(control);
        }

        #endregion
    }
}