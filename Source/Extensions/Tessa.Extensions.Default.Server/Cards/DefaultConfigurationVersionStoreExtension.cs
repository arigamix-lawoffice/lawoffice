using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Увеличивает версию конфигурации при изменении карточек, связанных с типовым решением.
    /// Также проверяет, что конфигурация не находится в режиме защиты от изменений <see cref="ConfigurationFlags.Sealed"/>.
    ///
    /// Должно быть зарегистрировано для только определённых карточек <c>.WhenCardTypes(...)</c>,
    /// а также для всех видов сохранения карточки <c>.WhenAnyStoreMethod()</c>.
    /// </summary>
    public sealed class DefaultConfigurationVersionStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public DefaultConfigurationVersionStoreExtension(
            IConfigurationVersionProvider configurationVersionProvider,
            IConfigurationInfoProvider configurationInfoProvider)
        {
            this.configurationVersionProvider = configurationVersionProvider;
            this.configurationInfoProvider = configurationInfoProvider;
        }

        #endregion

        #region Fields

        private readonly IConfigurationVersionProvider configurationVersionProvider;

        private readonly IConfigurationInfoProvider configurationInfoProvider;

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (context.Request.ServiceType != CardServiceType.Default
                && context.Method != CardStoreMethod.Restore
                && this.configurationInfoProvider.GetFlags().Has(ConfigurationFlags.Sealed))
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(ValidationKeys.ConfigurationIsSealed)
                    .End();
            }

            return Task.CompletedTask;
        }


        public override Task AfterRequestFinally(ICardStoreExtensionContext context)
        {
            // если версия не поменялась, то может быть, что администратор просто нажал Ctrl+S в карточке

            Card card;

            if (!context.RequestIsSuccessful
                || (card = context.Request.TryGetCard()) == null
                || card.StoreMode == CardStoreMode.Update
                && context.Response.CardVersion == card.Version)
            {
                return Task.CompletedTask;
            }

            return this.configurationVersionProvider.IncrementVersionAsync();
        }

        #endregion
    }
}
