using System;
using System.Collections.Generic;
using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет информацию по последним запросам и ответам выполненным <see cref="ICardLifecycleCompanion{T}"/>.
    /// </summary>
    internal sealed class CardLifecycleCompanionData :
        ICardLifecycleCompanionData
    {
        #region Properties
        
        /// <inheritdoc/>
        public CardNewRequest NewRequest { get; internal set; }

        /// <inheritdoc/>
        public CardNewResponse NewResponse { get; internal set; }

        /// <inheritdoc/>
        public CardGetRequest GetRequest { get; internal set; }

        /// <inheritdoc/>
        public CardGetResponse GetResponse { get; internal set; }

        /// <inheritdoc/>
        public CardStoreRequest StoreRequest { get; internal set; }

        /// <inheritdoc/>
        public CardStoreResponse StoreResponse { get; internal set; }

        /// <inheritdoc/>
        public CardDeleteRequest DeleteRequest { get; internal set; }

        /// <inheritdoc/>
        public CardDeleteResponse DeleteResponse { get; internal set; }

        /// <inheritdoc/>
        public Dictionary<string, object> OtherRequests { get; } = new Dictionary<string, object>(StringComparer.Ordinal);

        /// <inheritdoc/>
        public Dictionary<string, object> OtherResponses { get; } = new Dictionary<string, object>(StringComparer.Ordinal);

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public void Reset()
        {
            this.NewRequest = default;
            this.NewResponse = default;
            this.GetRequest = default;
            this.GetResponse = default;
            this.StoreRequest = default;
            this.StoreResponse = default;
            this.DeleteRequest = default;
            this.DeleteResponse = default;
            this.OtherRequests.Clear();
            this.OtherResponses.Clear();
        }

        #endregion
    }
}
