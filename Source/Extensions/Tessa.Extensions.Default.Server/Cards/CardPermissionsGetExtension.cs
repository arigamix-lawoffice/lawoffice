using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class CardPermissionsGetExtension :
        CardGetExtension
    {
        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null)
            {
                return;
            }

            bool isAdministrator = context.Session.User.IsAdministrator();
            if (isAdministrator || CardExtensionHelper.CheckUserPermissions(context.CardType))
            {
                await CardExtensionHelper.GrantAllPermissionsExceptAdministrativeFilesAsync(
                    card,
                    isAdministrator,
                    context.CardMetadata,
                    context.CancellationToken);
            }
            else
            {
                CardHelper.ProhibitAllPermissions(card, removeOtherPermissions: true);
            }
        }

        #endregion
    }
}
