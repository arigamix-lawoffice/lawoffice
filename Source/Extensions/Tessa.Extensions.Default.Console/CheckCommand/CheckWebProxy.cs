using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.CheckCommand
{
    public sealed class CheckWebProxy :
        WebProxy
    {
        #region Methods

        public Task<string> GetAsync(bool checkHealth, CancellationToken cancellationToken = default) =>
            this.SendAsync<string>(
                HttpMethod.Get,
                checkHealth ? "hcheck" : "check",
                checkHealth ? RequestFlags.OmitInstanceInUri : RequestFlags.None,
                cancellationToken: cancellationToken);

        #endregion
    }
}
