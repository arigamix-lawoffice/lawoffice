using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Notices
{
    public sealed class KrMessageHandler :
        IMessageHandler
    {
        #region MessageInfo Private Class

        private class MessageInfo :
            IMessageInfo
        {
            /// <summary>
            /// Идентификатор задачи, всегда не равен <c>null</c>.
            /// </summary>
            public Guid? TaskRowID { get; set; }

            /// <summary>
            /// Префикс процесса <see cref="KrApprovalPrefix"/> или <see cref="WfResolutionPrefix"/>.
            /// </summary>
            public string ProcessPrefix { get; init; }

            /// <summary>
            /// Комментарий при завершении задачи.
            /// </summary>
            public string Comment { get; set; }

            /// <summary>
            /// Согласовано/Не согласовано для процесса согласования (не актуально для задач).
            /// </summary>
            public bool IsApproved { get; set; }
        }

        #endregion

        #region Fields and Constants

        private const string KrApprovalPrefix = "apr";

        private const string WfResolutionPrefix = "tsk";

        private static readonly Regex krHeaderRegex = new Regex(
            @"^[[](" + KrApprovalPrefix + @"|" + WfResolutionPrefix + @")-([01])-([A-F0-9]{32})](.*)$",
            RegexOptions.IgnoreCase
            | RegexOptions.CultureInvariant
            | RegexOptions.Compiled);

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region IMessageHandler Members

        public async ValueTask<IMessageInfo> TryParseAsync(NoticeMessage message, CancellationToken cancellationToken = default)
        {
            Match match = krHeaderRegex.Match(message.Subject.Trim());
            if (!match.Success)
            {
                return null;
            }

            var taskInfo = new MessageInfo { ProcessPrefix = match.Groups[1].Value };

            switch (taskInfo.ProcessPrefix)
            {
                case KrApprovalPrefix:
                    switch (match.Groups[2].Value)
                    {
                        case "0":
                            taskInfo.IsApproved = false;
                            break;

                        case "1":
                            taskInfo.IsApproved = true;
                            break;

                        default:
                            return null;
                    }
                    break;

                case WfResolutionPrefix:
                    taskInfo.IsApproved = true;
                    break;
            }

            taskInfo.TaskRowID = Guid.Parse(match.Groups[3].Value);

            string messageText = (message.Body ?? string.Empty).Trim();

            int commentIndex = messageText
                .LastIndexOf(
                    "<" + await LocalizationManager.GetStringAsync("KrMessages_CommentTextMessage", cancellationToken) + ">",
                    StringComparison.Ordinal);

            taskInfo.Comment = commentIndex >= 0
                ? messageText.Substring(0, commentIndex).Trim()
                : messageText;

            return taskInfo;
        }


        public async Task HandleAsync(IMessageHandlerContext context)
        {
            var info = (MessageInfo)context.Info;

            // если пользователь не входит в роль исполнителя - не завершаем задание
            if (!context.Task.IsCanPerform)
            {
                logger.Trace("The task can't be completed. User is not its performer.");
                context.Cancel = true;
                return;
            }

            // Завершаем задание
            context.Task.Action = CardTaskAction.Complete;
            context.Task.State = CardRowState.Deleted;
            context.Task.Flags |= CardTaskFlags.HistoryItemCreated;

            // Устанавливаем комментарий при завершении задачи
            string comment =
                info.IsApproved
                    ? info.Comment
                    : string.IsNullOrEmpty(info.Comment)
                        ? "{$KrMessages_ApprovalEmptyCommentMessage}"
                        : info.Comment;

            switch (info.ProcessPrefix)
            {
                case KrApprovalPrefix:
                    context.Task.OptionID = context.Task.TypeID == DefaultTaskTypes.KrSigningTypeID
                        ? info.IsApproved
                            ? DefaultCompletionOptions.Sign
                            : DefaultCompletionOptions.Decline
                        : info.IsApproved
                            ? DefaultCompletionOptions.Approve
                            : DefaultCompletionOptions.Disapprove;

                    if (context.Task.TypeID == DefaultTaskTypes.KrApproveTypeID
                        || context.Task.TypeID == DefaultTaskTypes.KrSigningTypeID)
                    {
                        context.Task.Card.Sections["KrTask"].Fields["Comment"] = comment;
                    }
                    else if (context.Task.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
                    {
                        context.Task.Card.Sections["KrAdditionalApprovalTaskInfo"].Fields["Comment"] = comment;
                    }
                    break;

                case WfResolutionPrefix:
                    context.Task.OptionID = DefaultCompletionOptions.Complete;

                    context.Task.Card.Sections[WfHelper.ResolutionSection].Fields[WfHelper.ResolutionCommentField] = comment;
                    break;
            }

            // Устанавливаем пользователя, если задание не взято в работу
            // для того, чтобы были заполнены поля карточки "Согласовали" и "Не согласовали"
            // на вкладке "Процесс согласования"
            if (context.Task.StoredState == CardTaskState.Created)
            {
                IUser currentUser = context.Session.User;
                context.Task.UserID = currentUser.ID;
                context.Task.UserName = currentUser.Name;
            }
        }

        #endregion
    }
}
