using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    public static class KrUIHelper
    {
        public static async Task SendCompileRequestAsync(string compileFlag)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;
            if (editor == null)
            {
                return;
            }

            ICardModel model = editor.CardModel;
            if (model != null && await model.HasChangesAsync())
            {
                bool success = await editor.SaveCardAsync(context);
                if (!success)
                {
                    return;
                }
            }

            await editor.SaveCardAsync(
                context,
                new Dictionary<string, object>
                {
                    [compileFlag] = BooleanBoxes.True
                });
        }
    }
}