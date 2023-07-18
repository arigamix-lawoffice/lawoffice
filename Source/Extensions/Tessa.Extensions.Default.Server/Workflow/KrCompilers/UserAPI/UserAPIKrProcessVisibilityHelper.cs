using System;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    public static class UserAPIKrProcessVisibilityHelper
    {
        public static object NotSupportedAction() => throw new NotSupportedException("Generated class doesn't support specified action.");

        public static ValueTask<bool> DefaultActionAsync() => new ValueTask<bool>(true);

        public static async ValueTask<bool> RunVisibilityAsync(
            IKrScript script,
            IKrProcessVisibilityScript visibility)
        {
            script.KrScriptType = KrScriptType.ProcessVisibility;
            return await visibility.VisibilityAsync();
        }
    }
}