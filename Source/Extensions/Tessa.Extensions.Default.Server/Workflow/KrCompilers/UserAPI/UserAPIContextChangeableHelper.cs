using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    public static class UserAPIContextChangeableHelper
    {
        public static async Task RunNextStageInContextAsync(
            IKrScript script,
            IContextChangeableScript contextChangeable,
            Guid cardID,
            bool wholeCurrentGroup = false,
            IDictionary<string, object> processInfo = null)
        {
            if (await script.GetCardObjectAsync() is null
                || cardID != script.CardID)
            {
                contextChangeable.DifferentContextCardID = cardID;
                contextChangeable.DifferentContextWholeCurrentGroup = wholeCurrentGroup;
                contextChangeable.DifferentContextSetupScriptType = script.KrScriptType;
                contextChangeable.DifferentContextProcessInfo = processInfo;
            }
        }

        public static bool ContextChangePending(IContextChangeableScript contextChangeable) => contextChangeable.DifferentContextCardID.HasValue;

        public static void DoNotChangeContext(IContextChangeableScript contextChangeable)
        {
            contextChangeable.DifferentContextCardID = null;
            contextChangeable.DifferentContextWholeCurrentGroup = false;
            contextChangeable.DifferentContextSetupScriptType = null;
            contextChangeable.DifferentContextProcessInfo = null;
        }
    }
}