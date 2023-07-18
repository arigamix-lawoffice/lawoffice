using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Platform.Validation;

namespace Tessa.Module.Sample.Server.Requests
{
    public sealed class SampleActionRequestExtension :
        CardRequestExtension
    {
        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            ValidationSequence
                .Begin(context.ValidationResult)
                .SetObjectName(this)
                .InfoText("Hello, world!")
                .End();
        }
    }
}
