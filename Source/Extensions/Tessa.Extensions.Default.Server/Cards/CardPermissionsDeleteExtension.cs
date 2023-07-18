using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class CardPermissionsDeleteExtension :
        CardDeleteExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardDeleteExtensionContext context)
        {
            if (!context.Session.User.IsAdministrator()
                && !CardExtensionHelper.CheckUserPermissions(context.CardType))
            {
                context.ValidationResult.AddError(this, "$UI_Common_Messages_CardDeletionIsProhibited");
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
