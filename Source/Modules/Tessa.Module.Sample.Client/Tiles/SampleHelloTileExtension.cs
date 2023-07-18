using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Module.Sample.Shared;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Module.Sample.Client.Tiles
{
    public sealed class SampleHelloTileExtension : TileExtension
    {
        public SampleHelloTileExtension(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }


        private readonly ICardRepository cardRepository;


        private async Task SampleActionAsync()
        {
            var request = new CardRequest { RequestType = RequestTypes.SampleAction };
            var response = await this.cardRepository.RequestAsync(request);
            TessaDialog.ShowNotEmpty(response.ValidationResult);
        }


        public override async Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            context.Workspace.RightPanel.Tiles.Add(
                new Tile(
                    "SampleAction",
                    "Sample action",
                    context.Icons.Get("Thin42"),
                    context.Workspace.RightPanel,
                    new DelegateCommand(async p => await this.SampleActionAsync()),
                    TileGroups.Bottom));
        }
    }
}
