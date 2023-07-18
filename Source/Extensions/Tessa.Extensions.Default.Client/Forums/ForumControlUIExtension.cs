using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Controls.Forums;

namespace Tessa.Extensions.Default.Client.Forums
{
    public sealed class ForumControlUIExtension : CardUIExtension
    {
        public override Task Initializing(ICardUIExtensionContext context)
        {
            context.Model.ControlInitializers.Add((control, m, r, ct) =>
            {
                // если токена на клиенте нет, то карточка не добавлена в типовое решение -> не ограничиваем права
                KrToken token;
                if (control is not ForumControlViewModel forumControlViewModel
                    || (token = KrToken.TryGet(m.Card.Info)) is null)
                {
                    return ValueTask.CompletedTask;
                }

                var vm = forumControlViewModel.ForumViewModel;
                vm.IsSuperModeratorModeEnabled = token.HasPermission(KrPermissionFlagDescriptors.SuperModeratorMode);
                vm.IsAddTopicEnabled = token.HasPermission(KrPermissionFlagDescriptors.AddTopics);

                return ValueTask.CompletedTask;
            });

            return Task.CompletedTask;
        }
    }
}
