using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// В режиме <see cref="ConfigurationFlags.Sealed"/> недоступно удаление объектов маршрутов, связанных со скриптами.
    /// </summary>
    public sealed class KrStrictSecurityCardDeleteExtension :
        CardDeleteExtension
    {
        #region Constructors

        public KrStrictSecurityCardDeleteExtension(IConfigurationInfoProvider configurationInfoProvider)
        {
            this.configurationInfoProvider = configurationInfoProvider;
        }

        #endregion

        #region Fields

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

        #endregion
    }
}
