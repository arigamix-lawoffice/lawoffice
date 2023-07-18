using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Tessa.Cards;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Компаратор выполняющий сравнение объектов типа <see cref="Card"/> по указанному порядку <see cref="Card.TypeID"/>.
    /// </summary>
    /// <remarks>Реализует сортировку по убыванию.</remarks>
    public sealed class CardTypePriorityComparer
        : Comparer<Card>
    {
        #region Fields

        private readonly Dictionary<Guid, int> orderedCardTypes;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardTypePriorityComparer"/>.
        /// </summary>
        /// <param name="cardTypes">Перечисление типов карточек в соответствие с которым выполняется сортировка карточек.</param>
        public CardTypePriorityComparer(
            IEnumerable<Guid> cardTypes)
        {
            Check.ArgumentNotNull(cardTypes, nameof(cardTypes));

            var orderedCardTypes = new Dictionary<Guid, int>();
            int index = default;

            foreach (var cardType in cardTypes)
            {
                if (!orderedCardTypes.ContainsKey(cardType))
                {
                    orderedCardTypes.Add(cardType, index++);
                }
            }

            this.orderedCardTypes = orderedCardTypes;
        }

        #endregion

        #region Comparer{T} Implementation

        /// <inheritdoc/>
        public override int Compare([AllowNull] Card x, [AllowNull] Card y)
        {
            if (x is null)
            {
                return y is null ? 0 : 1;
            }

            if (y is null)
            {
                return -1;
            }

            // Сравнение типов карточек по их порядковому номеру.
            var notContainsXTypeOrder = !this.orderedCardTypes.TryGetValue(x.TypeID, out var xTypeOrder);
            var notContainsYTypeOrder = !this.orderedCardTypes.TryGetValue(y.TypeID, out var yTypeOrder);

            if (notContainsXTypeOrder)
            {
                return notContainsYTypeOrder ? 0 : 1;
            }

            if (notContainsYTypeOrder)
            {
                return -1;
            }

            if (xTypeOrder == yTypeOrder)
            {
                return 0;
            }

            return xTypeOrder < yTypeOrder ? 1 : -1;
        }

        #endregion
    }
}
