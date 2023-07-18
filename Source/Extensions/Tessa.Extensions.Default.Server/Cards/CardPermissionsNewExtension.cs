using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class CardPermissionsNewExtension :
        CardNewExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardNewExtensionContext context)
        {
            if (context.CardType?.InstanceType == CardInstanceType.Card
                && !context.Session.User.IsAdministrator()
                && !CardExtensionHelper.CheckUserPermissions(context.CardType))
            {
                context.ValidationResult.AddError(this, "$UI_Common_Messages_CardCreationIsProhibited");
            }

            return Task.CompletedTask;
        }

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (context.CardType?.InstanceType == CardInstanceType.Card
                && context.RequestIsSuccessful)
            {
                Card card = context.Response.TryGetCard();
                if (card != null)
                {
                    await CardExtensionHelper.GrantAllPermissionsExceptAdministrativeFilesAsync(
                        card,
                        context.Session.User.IsAdministrator(),
                        context.CardMetadata,
                        context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
