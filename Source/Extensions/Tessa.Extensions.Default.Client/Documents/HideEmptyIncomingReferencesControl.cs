using System.Linq;
using System.Threading.Tasks;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls.AutoComplete;

namespace Tessa.Extensions.Default.Client.Documents
{
    public sealed class HideEmptyIncomingReferencesControl : CardUIExtension
    {
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardModel model = context.Model;

            if (!model.Blocks.TryGet("RefsBlock", out var block))
            {
                return;
            }

            IControlViewModel control = block.Controls.FirstOrDefault(x => x.Name == "IncomingRefsControl");

            if (control is AutoCompleteTableViewModel autoComplete && autoComplete.Items.Count == 0)
            {
                control.ControlVisibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
