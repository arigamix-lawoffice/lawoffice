using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.GetKey
{
    public static class Command
    {
        [Verb("GetKey")]
        [LocalizableDescription("Common_CLI_GetKey")]
        public static async Task GetKey(
            [Output] TextWriter output,
            [Argument, LocalizableDescription("Common_CLI_KeyType")] KeyType key)
        {
            int result = await Operation.ExecuteAsync(output, key);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}