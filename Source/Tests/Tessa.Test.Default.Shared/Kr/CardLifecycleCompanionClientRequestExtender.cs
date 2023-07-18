using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Изменяет запросы выполняемые <see cref="CardLifecycleCompanion{T}"/> устанавливая <see cref="CardRequestBase.ServiceType"/> равным <see cref="CardServiceType.Client"/>.
    /// </summary>
    public class CardLifecycleCompanionClientRequestExtender :
        CardLifecycleCompanionRequestExtender
    {
        #region Constants

        private const CardServiceType ServiceType = CardServiceType.Client;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override void ExtendDeleteRequest(CardDeleteRequest request)
        {
            base.ExtendDeleteRequest(request);

            request.ServiceType = ServiceType;
        }

        /// <inheritdoc/>
        public override void ExtendGetRequest(CardGetRequest request)
        {
            base.ExtendGetRequest(request);

            request.ServiceType = ServiceType;
        }

        /// <inheritdoc/>
        public override void ExtendNewRequest(CardNewRequest request)
        {
            base.ExtendNewRequest(request);

            request.ServiceType = ServiceType;
        }

        /// <inheritdoc/>
        public override void ExtendStoreRequest(CardStoreRequest request)
        {
            base.ExtendStoreRequest(request);

            request.ServiceType = ServiceType;
        }

        #endregion
    }
}
