using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Базовый класс предоставляющий методы расширяющие запросы выполняемые <see cref="CardLifecycleCompanion{T}"/>.
    /// </summary>
    public class CardLifecycleCompanionRequestExtender :
        ICardLifecycleCompanionRequestExtender
    {
        /// <inheritdoc/>
        public virtual void ExtendDeleteRequest(CardDeleteRequest request)
        {
        }

        /// <inheritdoc/>
        public virtual void ExtendGetRequest(CardGetRequest request)
        {
        }

        /// <inheritdoc/>
        public virtual void ExtendNewRequest(CardNewRequest request)
        {
        }

        /// <inheritdoc/>
        public virtual void ExtendStoreRequest(CardStoreRequest request)
        {
        }
    }
}
