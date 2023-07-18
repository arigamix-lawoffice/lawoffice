using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Проверяет, что конфигурация не находится в режиме защиты от изменений <see cref="ConfigurationFlags.Sealed"/>.
    ///
    /// Должно быть зарегистрировано для только определённых карточек <c>.WhenCardTypes(...)</c>.
    /// </summary>
    public sealed class DefaultConfigurationVersionNewGetExtension :
        CardNewGetExtension
    {
        #region Constructors

        public DefaultConfigurationVersionNewGetExtension(
            IConfigurationInfoProvider configurationInfoProvider)
        {
            this.configurationInfoProvider = configurationInfoProvider;
        }

        #endregion

        #region Fields

        private readonly IConfigurationInfoProvider configurationInfoProvider;

        #endregion

        #region Private Methods

        private Task AfterRequest(ICardExtensionContext context, Card card)
        {
            if (this.configurationInfoProvider.GetFlags().Has(ConfigurationFlags.Sealed))
            {
                CardHelper.ProhibitAllPermissions(card, removeOtherPermissions: true);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (context.Request.ServiceType == CardServiceType.Default
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null)
            {
                return Task.CompletedTask;
            }

            return this.AfterRequest(context, card);
        }


        public override Task AfterRequest(ICardNewExtensionContext context)
        {
            Card card;
            if (context.Request.ServiceType == CardServiceType.Default
                || !context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null)
            {
                return Task.CompletedTask;
            }

            return this.AfterRequest(context, card);
        }

        #endregion
    }
}
