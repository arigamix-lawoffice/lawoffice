using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Десериализует настройки в таблице с условиями так, чтобы они выгружались в файл как единый json вместо строки с json.
    /// </summary>
    public sealed class KrSecondaryProcessExportExtension :
        CardGetExtension
    {
        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;

            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Response.TryGetCard()) is null
                || (sections = card.TryGetSections()) is null
                || !sections.TryGetValue(KrConstants.KrSecondaryProcesses.Name, out CardSection section))
            {
                return Task.CompletedTask;
            }

            Dictionary<string, object> fields = section.RawFields;

            if (context.Request.ShouldExpandJson()
                && fields[KrConstants.KrSecondaryProcesses.Conditions] is IList list)
            {
                foreach (object item in list)
                {
                    if (item is Dictionary<string, object> obj
                        && obj.TryGetValue("Settings", out object settingsObj)
                        && settingsObj is string settings
                        && settings.StartsWith('{'))
                    {
                        obj["Settings"] = StorageHelper.DeserializeFromTypedJson(settings);
                        obj["Description"] = null;
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
