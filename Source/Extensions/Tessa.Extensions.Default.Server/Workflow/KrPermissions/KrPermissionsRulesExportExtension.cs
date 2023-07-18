using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// При экспорте карточки "Правила доступа" очищает строковые поля для представления.
    /// Поля будут заполнены в расширении <see cref="KrPermissionsRulesStoreExtension"/>, которое также выполняется и на импорт.
    /// Десериализует настройки в таблице с условиями так, чтобы они выгружались в файл как единый json вместо строки с json.
    /// </summary>
    public sealed class KrPermissionsRulesExportExtension :
        CardGetExtension
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;

            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Response.TryGetCard()) is null
                || (sections = card.TryGetSections()) is null
                || !sections.TryGetValue("KrPermissions", out CardSection section))
            {
                return Task.CompletedTask;
            }

            Dictionary<string, object> fields = section.RawFields;
            fields["Permissions"] = null;
            fields["States"] = null;
            fields["Types"] = null;

            if (context.Request.ShouldExpandJson()
                && fields["Conditions"] is IList list)
            {
                foreach (object item in list)
                {
                    if (item is Dictionary<string, object> obj
                        && obj.TryGetValue("Settings", out object settingsObj)
                        && settingsObj is string settings
                        && settings.StartsWith('{'))
                    {
                        obj["Settings"] = StorageHelper.DeserializeFromTypedJson(settings);
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
