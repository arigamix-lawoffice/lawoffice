using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.BuildVersion
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(IConsoleLogger logger, bool fullName)
        {
            string buildName = fullName ? BuildInfo.Version : BuildInfo.MajorVersion + BuildInfo.MinorVersion;

            await logger.InfoAsync("Acquired current version: {0}", buildName);
            await logger.WriteAsync(buildName);

            return 0;
        }
    }
}