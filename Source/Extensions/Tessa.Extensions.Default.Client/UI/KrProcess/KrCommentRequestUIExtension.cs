using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// Скрывает поле Комментарий в задании Запрос комментария для автора задания
    /// </summary>
    public sealed class KrRequestCommentUIExtension : CardUIExtension
    {
        public override Task Initialized(ICardUIExtensionContext context)
        {
            CardType cardType = context.Model.CardType;
            if (cardType.Flags.HasNot(CardTypeFlags.AllowTasks))
            {
                return Task.CompletedTask;
            }

            return context.Model.ModifyTasksAsync((task, model) =>
            {
                ICardModel taskModel = task.TaskModel;
                if (taskModel.CardType.ID == DefaultTaskTypes.KrRequestCommentTypeID
                    && taskModel.CardTask.Flags.HasNot(CardTaskFlags.CanPerform)
                    && !taskModel.CardTask.IsLockedEffective)
                {
                    if (taskModel.Controls.TryGet("Comment", out IControlViewModel commentControl))
                    {
                        commentControl.ControlVisibility = Visibility.Collapsed;
                        commentControl.Block.Rearrange();
                    }
                }

                return new ValueTask();
            });
        }
    }
}
