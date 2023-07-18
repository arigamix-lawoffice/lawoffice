using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.Wf;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    public sealed class WfTasksPermissionsExtension : ITaskPermissionsExtension
    {
        public Task ExtendPermissionsAsync(ITaskPermissionsExtensionContext context)
        {
            var task = context.Task;

            bool supportedTaskType = WfHelper.TaskTypeIsResolution(task.TypeID);
            if (!supportedTaskType)
            {
                return Task.CompletedTask;
            }

            context.Descriptor.Set(KrPermissionFlagDescriptors.ReadCard, true);
            context.Descriptor.Set(KrPermissionFlagDescriptors.SignFiles, true);

            // "Постановка задачи" автоматически берётся в работу, поэтому сразу даём права на файлы тем, кто входит в роль исполнителя (но не автора);
            // для всех остальных задач даём права исполнителям, когда задание взято в работу или отложено

            if (!task.IsCanPerform
                || task.StoredState != CardTaskState.InProgress
                && task.StoredState != CardTaskState.Postponed
                && task.TypeID != DefaultTaskTypes.WfResolutionProjectTypeID)
            {
                return Task.CompletedTask;
            }

            context.Descriptor.Set(KrPermissionFlagDescriptors.AddFiles, true);
            context.Descriptor.Set(KrPermissionFlagDescriptors.EditOwnFiles, true);

            return Task.CompletedTask;
        }
    }
}
