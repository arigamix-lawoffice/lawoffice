using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    public class OutgoingPartnerUIExtension : CardUIExtension
    {
        #region Base Overrides

        public override async Task Initializing(ICardUIExtensionContext context)
        {
            var dciSection = context.Card.Sections["DocumentCommonInfo"];
            if (dciSection == null)
            {
                return;
            }

            dciSection.FieldChanged += (s, e) =>
            {
                if (e.FieldName == "PartnerID")
                {
                    IDictionary<string, object> fields = ((CardSection)s).Fields;
                    fields["ReceiverName"] = null;
                    fields["ReceiverRowID"] = null;
                }
            };
        }

        #endregion
    }
}