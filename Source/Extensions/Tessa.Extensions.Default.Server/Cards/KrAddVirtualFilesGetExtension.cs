using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Files.VirtualFiles;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class KrAddVirtualFilesGetExtension :
        CardGetExtension
    {
        #region Fields

        private readonly IKrVirtualFileManager krVirtualFileManager;

        #endregion

        #region Constructors

        public KrAddVirtualFilesGetExtension(
            IKrVirtualFileManager krVirtualFileManager)
        {
            this.krVirtualFileManager = krVirtualFileManager;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) is null
                || card.StoreMode == CardStoreMode.Insert
                || context.CardType is null
                || context.CardType.Flags.HasNot(CardTypeFlags.AllowFiles))
            {
                return;
            }

            context.ValidationResult.Add(
                await this.krVirtualFileManager.FillCardWithFilesAsync(card, context.CancellationToken));
        }

        #endregion
    }
}
