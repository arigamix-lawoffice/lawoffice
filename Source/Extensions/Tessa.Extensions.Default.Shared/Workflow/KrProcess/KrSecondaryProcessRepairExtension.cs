using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Platform.Shared.Cards;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Расширение должно выполняться перед <see cref="JsonRepairExtension"/>, чтобы весь объект ещё не был сериализован в строку.
    /// </summary>
    public sealed class KrSecondaryProcessRepairExtension :
        CardRepairExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardRepairExtensionContext context)
        {
            StringDictionaryStorage<CardSection> sections;

            if (!context.ValidationResult.IsSuccessful()
                || (sections = context.Card.TryGetSections()) is null
                || !sections.TryGetValue(KrConstants.KrSecondaryProcesses.Name, out CardSection section))
            {
                return Task.CompletedTask;
            }

            Dictionary<string, object> fields = section.RawFields;
            if (fields.TryGetValue(KrConstants.KrSecondaryProcesses.Conditions, out object conditionsObj)
                && conditionsObj is IList list)
            {
                foreach (object item in list)
                {
                    if (item is Dictionary<string, object> obj
                        && obj.TryGetValue("Settings", out object settingsObj)
                        && settingsObj is Dictionary<string, object> settings)
                    {
                        obj["Settings"] = StorageHelper.SerializeToTypedJson(settings);
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
