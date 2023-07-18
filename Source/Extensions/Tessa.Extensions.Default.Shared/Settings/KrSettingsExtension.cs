using System.Threading.Tasks;
using Tessa.Platform.Settings;

namespace Tessa.Extensions.Default.Shared.Settings
{
    public sealed class KrSettingsExtension :
        SettingsExtension
    {
        public override Task Initialize(ISettingsExtensionContext context)
        {
            context.Settings.Set(new KrSettings());
            return Task.CompletedTask;
        }
    }
}
