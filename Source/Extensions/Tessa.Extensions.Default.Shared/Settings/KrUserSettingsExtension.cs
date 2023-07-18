using System.Threading.Tasks;
using Tessa.Platform.Settings;

namespace Tessa.Extensions.Default.Shared.Settings
{
    public sealed class KrUserSettingsExtension :
        SettingsExtension
    {
        public override Task Initialize(ISettingsExtensionContext context)
        {
            context.Settings.UserSettingsCardTypeIDList.Add(DefaultCardTypes.KrUserSettingsTypeID);
            return Task.CompletedTask;
        }
    }
}
