using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Localization;
using Tessa.UI.Workflow;
using Tessa.Workflow.Helpful;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    public sealed class KrCheckStateTileManagerUIExtension : IWorkflowEngineTileManagerUIExtension
    {
        private static readonly string extendedSectionName = WorkflowEngineHelper.GetExtendedSectionName("KrCheckStateTileExtension");

        /// <inheritdoc />
        public Guid ID => KrTileManagerExtensionsHelper.KrCheckStateExtensionID;

        /// <inheritdoc />
        public Task ModifyButtonRowAsync(IWorkflowEngineTileManagerUIExtensionContext context)
        {
            if (context.AllButtonRows.TryGetValue(extendedSectionName, out var contextRoleRows)
                && contextRoleRows.Count > 0)
            {
                if (context.Result.Length > 0)
                {
                    context.Result.AppendLine();
                }

                context.Result.Append(LocalizationManager.Localize("$KrTileExtensions_CheckState_States"));
                context.Result.Append(string.Join("; ", contextRoleRows.Select(x => LocalizationManager.Localize((string)x["StateName"]))));
            }

            return Task.CompletedTask;
        }
    }
}
