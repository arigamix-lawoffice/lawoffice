#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    public interface IOnlyOfficeSettingsProvider
    {
        ValueTask<IOnlyOfficeSettings> GetSettingsAsync(CancellationToken cancellationToken = default);
    }
}
