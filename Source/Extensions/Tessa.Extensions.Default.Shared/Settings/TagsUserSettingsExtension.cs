using System.Threading.Tasks;
using Tessa.Platform.Settings;

namespace Tessa.Extensions.Default.Shared.Settings
{
    /// <summary>
    /// Добавляет настройки тегов для пользователя.
    /// </summary>
    public sealed class TagsUserSettingsExtension :
        SettingsExtension
    {
        /// <inheritdoc />
        public override Task Initialize(ISettingsExtensionContext context)
        {
            context.Settings.UserSettingsCardTypeIDList.Add(DefaultCardTypes.TagsUserSettingsTypeID);
            return Task.CompletedTask;
        }
    }
}
