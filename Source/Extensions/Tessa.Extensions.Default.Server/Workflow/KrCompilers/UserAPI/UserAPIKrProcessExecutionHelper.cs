using System;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    public static class UserAPIKrProcessExecutionHelper
    {
        public static object NotSupportedAction() => throw new NotSupportedException("Generated class doesn't support specified action.");

        public static object DefaultAction() => new ValueTask<object>(BooleanBoxes.True);

        public static async ValueTask<bool> RunExecutionAsync(
            IKrScript script,
            IKrProcessExecutionScript execution)
        {
            script.KrScriptType = KrScriptType.ProcessExecution;
            return await execution.ExecutionAsync();
        }
    }
}