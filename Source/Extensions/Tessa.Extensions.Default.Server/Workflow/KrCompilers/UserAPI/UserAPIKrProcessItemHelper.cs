using System;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    public static class UserAPIKrProcessItemHelper
    {
        public static object NotSupportedAction() => throw new NotSupportedException("Generated class doesn't support specified action.");

        public static Task<bool> DefaultActionAsync() => TaskBoxes.True;

        public static async Task RunBeforeAsync(
            IKrScript script,
            IKrProcessItemScript item)
        {
            script.KrScriptType = KrScriptType.Before;
            await item.BeforeAsync();
        }
        
        public static async Task RunAfterAsync(
            IKrScript script,
            IKrProcessItemScript item)
        {
            script.KrScriptType = KrScriptType.After;
            await item.AfterAsync();
        }

        public static async ValueTask<bool> RunConditionAsync(
            IKrScript script,
            IKrProcessItemScript item)
        {
            script.KrScriptType = KrScriptType.Condition;
            return await item.ConditionAsync();
        }
    }
}