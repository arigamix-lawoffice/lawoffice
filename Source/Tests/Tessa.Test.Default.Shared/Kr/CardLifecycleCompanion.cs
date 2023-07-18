using System;
using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы для управления жизненным циклом карточки.
    /// </summary>
    public sealed class CardLifecycleCompanion :
        CardLifecycleCompanion<CardLifecycleCompanion>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion"/>.
        /// </summary>
        /// <param name="card">Карточка, жизненным циклом которой необходимо управлять.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Card card,
            ICardLifecycleCompanionDependencies deps)
            : base(
                  card,
                  deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion"/>.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardTypeName">Имя типа карточки.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Guid? cardTypeID,
            string cardTypeName,
            ICardLifecycleCompanionDependencies deps)
            : base(
                  cardTypeID,
                  cardTypeName,
                  deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanion"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardTypeName">Имя типа карточки.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public CardLifecycleCompanion(
            Guid cardID,
            Guid? cardTypeID,
            string cardTypeName,
            ICardLifecycleCompanionDependencies deps)
            : base(
                  cardID,
                  cardTypeID,
                  cardTypeName,
                  deps)
        {
        }

        #endregion
    }
}
