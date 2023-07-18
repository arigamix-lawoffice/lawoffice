using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Увеличивает версию конфигурации при удалении карточек, связанных с типовым решением.
    /// Также проверяет, что конфигурация не находится в режиме защиты от изменений <see cref="ConfigurationFlags.Sealed"/>.
    ///
    /// Должно быть зарегистрировано для только определённых карточек <c>.WhenCardTypes(...)</c>,
    /// а также для всех видов удаления карточки <c>.WhenAnyDeleteMethod()</c>.
    /// </summary>
    public sealed class DefaultConfigurationVersionDeleteExtension :
        CardDeleteExtension
    {
        #region Constructors

        public DefaultConfigurationVersionDeleteExtension(
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

        public override Task BeforeRequest(ICardDeleteExtensionContext context)
        {
            if (context.Request.ServiceType != CardServiceType.Default
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


        public override Task AfterRequestFinally(ICardDeleteExtensionContext context) =>
            context.RequestIsSuccessful
                ? this.configurationVersionProvider.IncrementVersionAsync()
                : Task.CompletedTask;

        #endregion
    }
}
