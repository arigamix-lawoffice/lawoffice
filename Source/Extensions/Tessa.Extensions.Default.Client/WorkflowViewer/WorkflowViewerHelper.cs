#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;
using Tessa.UI.WorkflowViewer;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Shapes;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using KrStageState = Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrStageState;

namespace Tessa.Extensions.Default.Client.WorkflowViewer
{
    public static class WorkflowViewerHelper
    {
        #region Localization string aliases

        /// <summary>
        /// Заголовок ноды старта процесса согласования
        /// </summary>
        private const string LocStartNodeHeader = "WorkflowViewer_Nodes_StartNodeHeader";

        /// <summary>
        /// Заглушка для инициатора
        /// </summary>
        private const string LocInitiatorNotSpecified = "WorkflowViewer_Nodes_InitiatorNotSpecified";

        /// <summary>
        /// Заглушка процесс не запущен для ноды начала согласования
        /// </summary>
        private const string LocNotStarted = "WorkflowViewer_Nodes_ProcessNotStarted";

        /// <summary>
        /// Заголовок ноды этапа согласования
        /// </summary>
        private const string LocStageHeader = "WorkflowViewer_Nodes_ApprovalStageHeader";

        /// <summary>
        /// Поле вопрос в ноде комментирования
        /// </summary>
        private const string LocQuestionTitle = "WorkflowViewer_Nodes_CommentNodeQuestionTitle";

        /// <summary>
        /// Поле ответ в ноде комментированя
        /// </summary>
        private const string LocAnswerTitle = "WorkflowViewer_Nodes_CommentNodeAnswerTitle";

        /// <summary>
        /// Поле комментарий исполнителя в доп. согласовании
        /// </summary>
        private const string LocPerformerCommentTitle = "WorkflowViewer_Node_AdditionalApprovalNode_PerformerComment";

        /// <summary>
        /// Поле комментарий автора в доп. согласовании
        /// </summary>
        private const string LocAuthorCommentTitle = "WorkflowViewer_Node_AdditionalApprovalNode_AuthorComment";

        /// <summary>
        /// Поле времени вопроса в ноде комментирования
        /// </summary>
        private const string LocQuesctionDateTimeTitle = "WorkflowViewer_Nodes_CommentNodeQuestionDateTimeTitle";

        /// <summary>
        /// Поле времени ответа в ноде комментирования
        /// </summary>
        private const string LocAnswerDateTimeTitle = "WorkflowViewer_Nodes_CommentNodeAnswerDateTimeTitle";

        /// <summary>
        /// Заголовок ноды регистрации
        /// </summary>
        private const string LocRegisterHeader = "WorkflowViewer_Nodes_RegisterNodeHeader";

        /// <summary>
        /// Заголовок ноды дерегистрации
        /// </summary>
        private const string LocDeregisterHeader = "WorkflowViewer_Nodes_DeregisterNodeHeader";

        /// <summary>
        /// Заголовок ноды завершения согласования
        /// </summary>
        private const string LocFinishHeader = "WorkflowViewer_Nodes_FinishNodeHeader";

        /// <summary>
        /// Заголовок ноды редактирования
        /// </summary>
        private const string LocEditHeader = "WorkflowViewer_Nodes_EditNodeHeader";

        /// <summary>
        /// Подпись к стрелке - согласовано
        /// </summary>
        private const string LocAppoved = "WorkflowViewer_Arrows_Approved";

        /// <summary>
        /// Подпись к стрелке - подписано
        /// </summary>
        private const string LocSigned = "WorkflowViewer_Arrows_Signed";

        /// <summary>
        /// Подпись к стрелке - запрос комментария
        /// </summary>
        private const string LocCommentRequired = "WorkflowViewer_Arrows_CommentRequired";

        /// <summary>
        /// Подпись к стрелке - доп. согласование
        /// </summary>
        private const string LocAdditionalApproval = "WorkflowViewer_Arrows_AdditionalApproval";

        /// <summary>
        /// Подпись к стрелке - делегировано
        /// </summary>
        private const string LocDelegated = "WorkflowViewer_Arrows_Delegated";

        /// <summary>
        /// Подпись к стрелке - не согласовано
        /// </summary>G
        private const string LocNotApproved = "WorkflowViewer_Arrows_NotApproved";

        /// <summary>
        /// Подпись к стрелке - отказано
        /// </summary>G
        private const string LocDeclined = "WorkflowViewer_Arrows_Declined";

        #endregion

        #region GetNodeContent methods

        /// <summary>
        /// Возвращает текстблок для ноды старта процесса согласования.
        /// </summary>
        /// <param name="initiator">Инициатор согласования.</param>
        /// <param name="comment">Комментарий к циклу согласования.</param>
        /// <param name="dateTime">Дата/время запуска согласования.</param>
        /// <returns>Текстблок для ноды старта процесса согласования.</returns>
        private static TextBlock GetStartNodeContent(
            string? initiator,
            string? comment,
            DateTime? dateTime = null)
        {
            TextBlock result = new();

            Run headerRun = new() { Text = LocalizationManager.GetString(LocStartNodeHeader), FontSize = 14 };
            result.Inlines.Add(headerRun);

            string? localizedInitiator = LocalizationManager.Format(initiator);
            Run initiatorRun = string.IsNullOrWhiteSpace(localizedInitiator)
                ? new Run("\r\n" + LocalizationManager.GetString(LocInitiatorNotSpecified))
                : new Run("\r\n" + localizedInitiator);

            initiatorRun.FontSize = 12;
            result.Inlines.Add(initiatorRun);

            Run dateTimeRun = dateTime.HasValue
                ? new Run("\r\n" + FormattingHelper.FormatDateTime(dateTime))
                : new Run("\r\n" + LocalizationManager.GetString(LocNotStarted));

            dateTimeRun.FontSize = 12;
            result.Inlines.Add(dateTimeRun);

            string? localizedComment = LocalizationManager.Format(comment);
            if (!string.IsNullOrWhiteSpace(localizedComment))
            {
                result.ToolTip = new TextBlock { Text = localizedComment, FontSize = 12 };

                string displayComment = localizedComment.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(100);
                result.Inlines.Add(new Run("\r\n" + displayComment) { FontSize = 12 });
            }

            return result;
        }


        /// <summary>
        /// Возвращает текстблок для ноды этапа
        /// </summary>
        /// <param name="stageName">Название этапа</param>
        /// <returns>Текстблок для ноды этапа</returns>
        private static TextBlock GetStageNodeContent(string? stageName)
        {
            TextBlock result = new();

            string? localizedStageName = LocalizationManager.Format(stageName);
            if (!string.IsNullOrWhiteSpace(localizedStageName))
            {
                string displayStageName = localizedStageName.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(100);
                result.Inlines.Add(new Run
                {
                    Text = LocalizationManager.GetString(LocStageHeader) + ":\r\n" + displayStageName,
                    FontSize = 14
                });
            }

            result.MinWidth = 120;
            result.MaxWidth = 120;
            result.MaxHeight = 120;
            result.TextAlignment = TextAlignment.Center;
            result.TextWrapping = TextWrapping.Wrap;

            return result;
        }


        /// <summary>
        /// Создает и возвращает шапку задания для ноды согласования по имени пользователя, названию
        /// роли и комментария к завершению задания.
        /// </summary>
        /// <param name="approverUser">Имя пользователя.</param>
        /// <param name="approverRole">Название роли.</param>
        /// <param name="approverComment">Комментарии к заданию.</param>
        /// <returns>Возвращает шапку задания для ноды согласования по имени пользователя, названию
        /// роли и комментария к завершению задания.</returns>
        private static TextBlock GetApprovalNodeContent(
            string? approverUser,
            string? approverRole,
            string? approverComment)
        {
            TextBlock result = new();

            string? localizedApproverRole = LocalizationManager.Format(approverRole);
            if (string.IsNullOrWhiteSpace(approverUser))
            {
                result.Inlines.Add(new Run { Text = localizedApproverRole, FontSize = 14 });
            }
            else
            {
                result.Inlines.Add(new Run { Text = approverUser, FontSize = 14 });
                result.Inlines.Add(new Run { Text = "\r\n" + localizedApproverRole, FontSize = 12 });
            }

            string? localizedApproverComment = LocalizationManager.Format(approverComment);
            if (!string.IsNullOrWhiteSpace(localizedApproverComment))
            {
                //Полный комментарий в тултипе
                result.ToolTip = new TextBlock { Text = localizedApproverComment, FontSize = 12 };

                string displayApproverComment = localizedApproverComment.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(100);
                result.Inlines.Add(new Run { Text = "\r\n\r\n" + displayApproverComment, FontSize = 10 });
            }

            result.MinWidth = 120;
            result.MaxWidth = 120;
            result.MaxHeight = 120;
            result.TextAlignment = TextAlignment.Center;
            result.TextWrapping = TextWrapping.Wrap;
            return result;
        }


        /// <summary>
        /// Создает и возвращает шапку задания для ноды для задания комментирования по имени пользователя,
        /// имени роли, вопросу и ответу
        /// </summary>
        /// <param name="approverUser">Имя пользователя</param>
        /// <param name="approverRole">Название роли</param>
        /// <param name="question">Вопрос</param>
        /// <param name="answer">Ответ</param>
        /// <returns>Возвращает шапку задания для ноды для задания комментирования по имени пользователя,
        /// имени роли, вопросу и ответу</returns>
        private static TextBlock GetCommentNodeContent(
            string? approverUser,
            string? approverRole,
            string? question,
            string? answer)
        {
            TextBlock result = new();

            string? localizedApproverRole = LocalizationManager.Format(approverRole);
            if (string.IsNullOrWhiteSpace(approverUser))
            {
                result.Inlines.Add(new Run { Text = localizedApproverRole, FontSize = 14 });
            }
            else
            {
                result.Inlines.Add(new Run { Text = approverUser, FontSize = 14 });
                result.Inlines.Add(new Run { Text = "\r\n" + localizedApproverRole, FontSize = 12 });
            }

            //Полный комментарий в тултипе
            var tooltipTextBlock = new TextBlock { FontSize = 12, TextAlignment = TextAlignment.Left };
            string? localizedQuestion = LocalizationManager.Format(question);
            if (!string.IsNullOrWhiteSpace(localizedQuestion))
            {
                string title = LocalizationManager.GetString(LocQuestionTitle);
                tooltipTextBlock.Text = title + ": " + localizedQuestion;
                result.ToolTip = tooltipTextBlock;

                string displayQuestion = localizedQuestion.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(50);
                result.Inlines.Add(new Run("\r\n\r\n" + title + ": " + displayQuestion) { FontSize = 10 });
            }

            string? localizedAnswer = LocalizationManager.Format(answer);
            if (!string.IsNullOrWhiteSpace(localizedAnswer))
            {
                string title = LocalizationManager.GetString(LocAnswerTitle);
                if (string.IsNullOrWhiteSpace(tooltipTextBlock.Text))
                {
                    tooltipTextBlock.Text = title + ": " + localizedAnswer;
                }
                else
                {
                    tooltipTextBlock.Text =
                        tooltipTextBlock.Text +
                        "\r\n\r\n" + title + ": " +
                        localizedAnswer;
                }

                string displayAnswer = localizedAnswer.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(50);
                result.Inlines.Add(new Run("\r\n" + title + ": " + displayAnswer) { FontSize = 10 });
            }

            result.MinWidth = 120;
            result.MaxWidth = 120;
            result.MaxHeight = 120;
            result.TextAlignment = TextAlignment.Center;
            result.TextWrapping = TextWrapping.Wrap;

            return result;
        }

        /// <summary>
        /// Создает и возвращает шапку задания для ноды для задания доп. согласования
        /// </summary>
        /// <param name="approverUser">Имя пользователя</param>
        /// <param name="approverRole">Название роли</param>
        /// <param name="approverComment">Комментарии к заданию</param>
        /// <param name="additionalApproverComment">Комментарий исполнителя</param>
        /// <returns>Возвращает шапку задания для ноды доп. согласования по имени пользователя, названию
        /// роли и комментария к завершению задания</returns>
        private static TextBlock GetAdditionalApprovalNodeContent(
            string? approverUser,
            string? approverRole,
            string? approverComment,
            string? additionalApproverComment)
        {
            TextBlock result = new();

            string? localizedApproverRole = LocalizationManager.Format(approverRole);
            if (string.IsNullOrWhiteSpace(approverUser))
            {
                result.Inlines.Add(new Run { Text = localizedApproverRole, FontSize = 14 });
            }
            else
            {
                result.Inlines.Add(new Run { Text = approverUser, FontSize = 14 });
                result.Inlines.Add(new Run { Text = "\r\n" + localizedApproverRole, FontSize = 12 });
            }

            //Полный комментарий в тултипе
            var tooltipTextBlock = new TextBlock { FontSize = 12, TextAlignment = TextAlignment.Left };
            string? localizedApproverComment = LocalizationManager.Format(approverComment);
            if (!string.IsNullOrWhiteSpace(localizedApproverComment))
            {
                string title = LocalizationManager.GetString(LocAuthorCommentTitle);
                tooltipTextBlock.Text = title + ": " + localizedApproverComment;
                result.ToolTip = tooltipTextBlock;

                string displayApproverComment = localizedApproverComment.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(50);
                result.Inlines.Add(new Run("\r\n\r\n" + title + ": " + displayApproverComment) { FontSize = 10 });
            }

            string? localizedAdditionalApproverComment = LocalizationManager.Format(additionalApproverComment);
            if (!string.IsNullOrWhiteSpace(localizedAdditionalApproverComment))
            {
                string title = LocalizationManager.GetString(LocPerformerCommentTitle);
                if (string.IsNullOrWhiteSpace(tooltipTextBlock.Text))
                {
                    tooltipTextBlock.Text = title + ": " + localizedAdditionalApproverComment;
                }
                else
                {
                    tooltipTextBlock.Text =
                        tooltipTextBlock.Text +
                        "\r\n\r\n" + title + ": " +
                        localizedAdditionalApproverComment;
                }

                string displayAdditionalApproverComment = localizedAdditionalApproverComment.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(50);
                result.Inlines.Add(new Run("\r\n" + title + ": " + displayAdditionalApproverComment) { FontSize = 10 });
            }

            result.MinWidth = 120;
            result.MaxWidth = 120;
            result.MaxHeight = 120;
            result.TextAlignment = TextAlignment.Center;
            result.TextWrapping = TextWrapping.Wrap;
            return result;
        }

        /// <summary>
        /// Возвращает дополнительный контент для ноды комментирования
        /// </summary>
        /// <param name="commentatorUser">Польователь - комментатор</param>
        /// <param name="commentatorRole">Роль комментатора</param>
        /// <param name="questionDateTime">Дата/время запроса комментария</param>
        /// <param name="question">Вопрос</param>
        /// <param name="answerDateTime">Дата/время ответа на вопрос</param>
        /// <param name="answer">Ответ</param>
        /// <returns>Дополнительный контент для ноды комментирования</returns>
        private static object GetCommentNodeExpandedContent(
            string? commentatorUser,
            string? commentatorRole,
            DateTime questionDateTime,
            string? question,
            DateTime? answerDateTime,
            string? answer)
        {
            TextBlock commentInfo = new();

            // commentator
            string? localizedCommentatorRole = LocalizationManager.Format(commentatorRole);
            if (string.IsNullOrWhiteSpace(commentatorUser))
            {
                commentInfo.Inlines.Add(new Run { Text = localizedCommentatorRole, FontSize = 16 });
            }
            else
            {
                commentInfo.Inlines.Add(new Run { Text = commentatorUser, FontSize = 16 });
                commentInfo.Inlines.Add(new Run { Text = "\r\n" + localizedCommentatorRole, FontSize = 14 });
            }

            // ... question date/time
            string displayQuestionDateTime =
                "\r\n\r\n"
                + LocalizationManager.GetString(LocQuesctionDateTimeTitle)
                + ": "
                + FormattingHelper.FormatDateTime(questionDateTime);

            commentInfo.Inlines.Add(new Run { Text = displayQuestionDateTime, FontSize = 12 });

            // ... question text
            string? localizedQuestion = LocalizationManager.Format(question);
            string displayQuestionText =
                "\r\n\r\n"
                + LocalizationManager.GetString(LocQuestionTitle)
                + ": "
                + localizedQuestion;

            commentInfo.Inlines.Add(new Run { Text = displayQuestionText, FontSize = 12 });

            // ... answer date/time
            if (answerDateTime.HasValue)
            {
                string displayAnswerDateTime =
                    "\r\n\r\n"
                    + LocalizationManager.GetString(LocAnswerDateTimeTitle)
                    + ": "
                    + FormattingHelper.FormatDateTime(answerDateTime);

                commentInfo.Inlines.Add(new Run { Text = displayAnswerDateTime, FontSize = 12 });
            }

            // ... answer text
            string? localizedAnswer = LocalizationManager.Format(answer);
            if (!string.IsNullOrWhiteSpace(localizedAnswer))
            {
                string displayAnswerText =
                    "\r\n\r\n"
                    + LocalizationManager.GetString(LocAnswerTitle)
                    + ": "
                    + localizedAnswer;

                commentInfo.Inlines.Add(new Run { Text = displayAnswerText, FontSize = 12 });
            }

            // ... styles
            commentInfo.MaxWidth = 300;
            commentInfo.TextWrapping = TextWrapping.Wrap;

            commentInfo.TextAlignment = TextAlignment.Center;
            commentInfo.TextWrapping = TextWrapping.Wrap;

            return commentInfo;
        }

        /// <summary>
        /// Возвращает дополнительный контент для ноды доп согласования
        /// </summary>
        /// <param name="approverUser">Имя пользователя</param>
        /// <param name="approverRole">Название роли</param>
        /// <param name="approverComment">Комментарии к заданию</param>
        /// <param name="additionalApproverComment">Комментарий исполнителя</param>
        /// <returns>Возвращает шапку задания для ноды доп. согласования по имени пользователя, названию
        /// роли и комментария к завершению задания</returns>
        private static object GetAdditionbalApprovalNodeExpandedContent(
            string? approverUser,
            string? approverRole,
            string? approverComment,
            string? additionalApproverComment)
        {
            TextBlock result = new();

            string? localizedApproverRole = LocalizationManager.Format(approverRole);
            if (string.IsNullOrWhiteSpace(approverUser))
            {
                result.Inlines.Add(new Run { Text = localizedApproverRole, FontSize = 14 });
            }
            else
            {
                result.Inlines.Add(new Run { Text = approverUser, FontSize = 14 });
                result.Inlines.Add(new Run { Text = "\r\n" + localizedApproverRole, FontSize = 12 });
            }


            //Полный комментарий в тултипе
            string? localizedApproverComment = LocalizationManager.Format(approverComment);
            if (!string.IsNullOrWhiteSpace(localizedApproverComment))
            {
                string title = LocalizationManager.GetString(LocAuthorCommentTitle);
                string displayApproverComment = localizedApproverComment.ReplaceLineEndingsAndTrim().NormalizeSpaces();
                result.Inlines.Add(new Run("\r\n\r\n" + title + ": " + displayApproverComment) { FontSize = 10 });
            }

            string? localizedAdditionalApproverComment = LocalizationManager.Format(additionalApproverComment);
            if (!string.IsNullOrWhiteSpace(localizedAdditionalApproverComment))
            {
                string title = LocalizationManager.GetString(LocPerformerCommentTitle);
                string displayAdditionalApproverComment = localizedAdditionalApproverComment.ReplaceLineEndingsAndTrim().NormalizeSpaces();
                result.Inlines.Add(new Run("\r\n\r\n" + title + ": " + displayAdditionalApproverComment) { FontSize = 10 });
            }

            // ... styles
            result.MaxWidth = 300;
            result.TextWrapping = TextWrapping.Wrap;
            result.Padding = new Thickness(15);

            return result;
        }


        /// <summary>
        /// Возвращает текстблок для ноды регистрации
        /// </summary>
        /// <param name="registrator">Регистратор</param>
        /// <param name="dateTime">Дата/время регистрации</param>
        /// <returns>Текстблок для ноды регистрации</returns>
        private static TextBlock GetRegisterNodeContent(string? registrator, DateTime? dateTime)
        {
            TextBlock result = new();

            result.Inlines.Add(new Run { Text = LocalizationManager.GetString(LocRegisterHeader), FontSize = 14 });
            result.Inlines.Add(new Run { Text = "\r\n" + registrator, FontSize = 12 });
            result.Inlines.Add(new Run { Text = "\r\n" + FormattingHelper.FormatDateTime(dateTime), FontSize = 12 });

            return result;
        }


        /// <summary>
        /// Возвращает текстблок для ноды дерегистрации
        /// </summary>
        /// <param name="deregistrator">Дерегистратор</param>
        /// <param name="dateTime">Дата/время дерегистрации</param>
        /// <returns>Текстблок для ноды дерегистрации</returns>
        private static TextBlock GetDeregisterNodeContent(string? deregistrator, DateTime? dateTime)
        {
            TextBlock result = new();

            result.Inlines.Add(new Run { Text = LocalizationManager.GetString(LocDeregisterHeader), FontSize = 14 });
            result.Inlines.Add(new Run { Text = "\r\n" + deregistrator, FontSize = 12 });
            result.Inlines.Add(new Run { Text = "\r\n" + FormattingHelper.FormatDateTime(dateTime), FontSize = 12 });

            return result;
        }

        private static TextBlock GetNodeContentWithLocalizedHeader(string headerString)
        {
            TextBlock result = new()
            {
                Text = LocalizationManager.GetString(headerString),
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14,
                MinWidth = 120,
                MaxWidth = 120,
            };

            return result;
        }

        #endregion

        #region AddNode methods

        /// <summary>
        /// Добавляет на макет ВФ ноду старта согласования
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        public static void AddStartNode(
            INodeFactory factory,
            INodeLayout layout,
            Card card)
        {
            Dictionary<string, object?> fields = card.Sections[KrApprovalCommonInfo.Virtual].RawFields;
            CardTaskHistoryItem? task = card.TaskHistory.FirstOrDefault(x => x.TypeID == DefaultTaskTypes.KrStartApprovalProcessTypeID);

            // если процесс не был запущен - возьмем указанную в карточке информацию
            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
            node.ContentControl = task is not null
                ? GetStartNodeContent(task.UserName, task.Result, task.Completed)
                : GetStartNodeContent(fields.Get<string>("AuthorName"), fields.Get<string>("AuthorComment"));

            var stateID = (KrState)fields.Get<int>("StateID");

            if (stateID == KrState.Draft)
            {
                node.Background = RouteBrushes.NotInWork;
            }
            else if (stateID == KrState.Cancelled)
            {
                node.Background = RouteBrushes.NotCreatedNode;
                node.Foreground = RouteBrushes.ObscureNodeForeground;
            }
            else
            {
                node.Background = RouteBrushes.Finished;
            }

            layout.Nodes.Add(node);

            // стрелка от старта процесса согласования, оставляем ее "открытой"
            layout.Connections.Add(Connection.CreateArrow(node, null));
        }


        /// <summary>
        /// Добавляет на макет ВФ ноду окончания согласования
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        /// <param name="isApproved"></param>
        public static void AddFinishNode(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            bool isApproved)
        {
            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
            node.ContentControl = GetNodeContentWithLocalizedHeader(LocFinishHeader);

            KrState state = (KrState)card.Sections[KrApprovalCommonInfo.Virtual].RawFields.Get<int>("StateID");
            if (state == KrState.Draft
                || state == KrState.Active
                || state == KrState.Cancelled
                || state == KrState.Editing
                || state == KrState.Signing)
            {
                node.Foreground = RouteBrushes.ObscureNodeForeground;
                node.Background = RouteBrushes.NotCreatedNode;
            }
            else if (state == KrState.Approved || state == KrState.Signed)
            {
                node.Background = RouteBrushes.Approved;
            }
            else if (state == KrState.Disapproved || state == KrState.Declined)
            {

                node.Background = RouteBrushes.Disapproved;
            }
            else if (state == KrState.Registered)
            {
                //TODO: как-то отработать несогласование; перед регистрацией?
                node.Background = isApproved
                    ? RouteBrushes.Approved
                    : RouteBrushes.NotCreatedNode;
            }
            else
            {
                node.Background = RouteBrushes.Finished;
            }

            layout.Nodes.Add(node);

            //Закрываем входящую стрелку
            IConnection inputArrow = layout.Connections.First(x => x.To is null);
            inputArrow.To = node;

            if (card.Sections[KrApprovalCommonInfo.Virtual].RawFields.Get<int>("StateID") == (int)KrState.Registered)
            {
                AddRegisteredNodeIfNecessary(factory, layout, card);
            }
        }


        /// <summary>
        /// При необходимости добавляет ноду регистрации документа
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        public static void AddRegisteredNodeIfNecessary(
            INodeFactory factory,
            INodeLayout layout,
            Card card)
        {
            CardTaskHistoryItem? task = card.TaskHistory.FirstOrDefault(x => x.TypeID == DefaultTaskTypes.KrRegistrationTypeID);
            if (task is null)
            {
                return;
            }

            card.TaskHistory.Remove(task);

            INode node = factory.CreateBorderedWithForeground<RectangleNode>();
            node.ContentControl = GetRegisterNodeContent(task.UserName, task.Completed);
            node.Background = RouteBrushes.RegisteredNode;

            layout.Nodes.Add(node);

            //Нода, от которой нет исходящих стрелок и которая не является дочерней
            //является либо конечной нодой процесса согласования либо нодой дерегистрации,
            //если документ регестрировался/дерегистрировался несколько раз
            INode? finishingNode = layout.Nodes.FirstOrDefault(x =>
                x != node
                && layout.Connections.All(c => c.From != x)
                && !x.IsChildNode);
            if (finishingNode is not null)
            {
                //проведем стрелку от ноды окончания процесса к ноде
                layout.Connections.Add(Connection.CreateArrow(finishingNode, node));
            }

            //Добавим ноду дерегистрации, если нужно
            AddDeregisteredNodeIfNeeded(factory, layout, card);
        }


        /// <summary>
        /// При необходимости добавляет ноду отмены регистрации документа
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        public static void AddDeregisteredNodeIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card)
        {
            CardTaskHistoryItem? task = card.TaskHistory.FirstOrDefault(x => x.TypeID == KrConstants.KrDeregistrationTypeID);
            if (task is null)
            {
                return;
            }

            card.TaskHistory.Remove(task);

            INode node = factory.CreateBorderedWithForeground<RectangleNode>();
            node.ContentControl = GetDeregisterNodeContent(task.UserName, task.Completed);
            node.Background = RouteBrushes.DeregisteredNode;

            layout.Nodes.Add(node);

            //Нода, от которой нет исходящих стрелок и которая не является дочерней
            //является либо конечной нодой процесса согласования либо нодой дерегистрации,
            //если документ регестрировался/дерегистрировался несколько раз
            INode? finishNode = layout.Nodes.FirstOrDefault(x =>
                x != node
                && layout.Connections.All(c => c.From != x)
                && !x.IsChildNode);
            if (finishNode is not null)
            {
                //проведем стрелку от ноды окончания процесса к ноде
                layout.Connections.Add(Connection.CreateArrow(finishNode, node));
            }

            //Добавим ноду регистрации, если нужно
            AddRegisteredNodeIfNecessary(factory, layout, card);
        }


        /// <summary>
        /// Добавляет на макет ВФ ноду этапа согласования
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка документа</param>
        /// <param name="stage">Этап согласования</param>
        public static void AddStageNode(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            CardRow stage)
        {
            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
            node.ContentControl = GetStageNodeContent(stage.Get<string>("Name"));

            var state = (KrStageState)stage.Get<int>("StateID");
            if (state == KrStageState.Inactive)
            {
                //Для несогласованного документа отображаем прошедший процесс, а не плановый-значит
                //этапы будем отображать уже завершенными, даже если этапы уже "сброшены" для редактирования
                int stateID = card.Sections[KrApprovalCommonInfo.Virtual].RawFields.Get<int>("StateID");
                if ((stateID == (int)KrState.Disapproved || stateID == (int)KrState.Declined || stateID == (int)KrState.Editing)
                    //Чтобы понять что этап запускался - проверим остались ли необработанные задания согласования или подписания в истории
                    && card.TaskHistory.Any(x => x.TypeID == DefaultTaskTypes.KrApproveTypeID || x.TypeID == DefaultTaskTypes.KrSigningTypeID))
                {
                    node.Background = RouteBrushes.Finished;
                }
                else
                {
                    node.Background = RouteBrushes.NotCreatedNode;
                    node.Foreground = RouteBrushes.ObscureNodeForeground;
                }
            }
            else
            {
                node.Background = RouteBrushes.Finished;
            }

            layout.Nodes.Add(node);

            //В этап всегда ведет только одна стрелка, закрываем ее
            IConnection inputArrow = layout.Connections.First(x => x.To is null);
            inputArrow.To = node;

            //Открываем исходящую стрелку
            layout.Connections.Add(Connection.CreateArrow(node, null));
        }


        /// <summary>
        /// Добавляет на макет ВФ ноду этапа согласования
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        /// <param name="stage">Этап согласования</param>
        public static void AddEditInterjectIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            CardRow stage)
        {
            //Если нет отметки "вернуть автору" - ничего не добавляем
            if (!stage.TryGet<bool>(KrApprovalSettingsVirtual.ReturnToAuthor))
            {
                return;
            }

            //История согласования для текущего цикла
            ListStorage<CardRow> cycleHistory = card.Sections[KrApprovalHistory.Virtual].Rows;

            //Завершенные задания редактирования для текущего этапа согласования
            IEnumerable<CardRow> cycleEditInterjectTasks
                = cycleHistory.Where(x =>
                    card.TaskHistory.Any(
                        c => c.RowID == x.Get<Guid>("HistoryRecord")
                            && c.TypeID == DefaultTaskTypes.KrEditInterjectTypeID));

            //Завершено ли задание редактирования
            bool editTaskIsFinished =
                stage.Get<int>("StateID") == (int)KrStageState.Completed &&
                card.TaskHistory.Any(x =>
                    cycleEditInterjectTasks.Any(c => c.Get<Guid>("HistoryRecord") == x.RowID));

            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();

            if (editTaskIsFinished)
            {
                node.Background = RouteBrushes.Finished;
                //Вообще у нас нигде не отражается к какому этапу согласования относится задание редактирования,
                //поэтому для того чтобы правильно отследить завершенные и не завершенные - будем удалять
                //из истории завершенные. Пока никаких эффектов это действо возыметь не должно, т.к.
                //вариант завершения все равно один
                card.TaskHistory.Remove(
                    card.TaskHistory.First(x =>
                        cycleEditInterjectTasks.Any(c => c.Get<Guid>("HistoryRecord") == x.RowID)));
            }
            else
            {
                //Задание не завершено, значит стоит поискать его в активных на карточке
                CardTask? editTask = card.Tasks.FirstOrDefault(x => x.TypeID == DefaultTaskTypes.KrEditInterjectTypeID);
                if (editTask is null)
                {
                    node.Background = RouteBrushes.NotCreatedNode;
                    node.Foreground = RouteBrushes.ObscureNodeForeground;
                }
                else if (editTask.InProgress.HasValue)
                {
                    node.Background = RouteBrushes.InWork;
                }
                else
                {
                    node.Background = RouteBrushes.NotInWork;
                }
            }

            node.ContentControl = GetNodeContentWithLocalizedHeader(LocEditHeader);

            layout.Nodes.Add(node);

            IConnection inputArrow = layout.Connections.First(x => x.To is null);
            inputArrow.To = node;

            layout.Connections.Add(Connection.CreateArrow(node, null));
        }


        /// <summary>
        /// Добавляет на макет ВФ ноду, в которую сходятся все стрелки от согласующих параллельного этапа
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка документа</param>
        /// <param name="stage">Этап согласования</param>
        /// <param name="stageApprovers">Согласующие этапа</param>
        public static void AddMergeNodeIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            CardRow stage,
            ICollection<CardRow> stageApprovers)
        {
            if (stageApprovers.Count > 1)
            {
                INode node = factory.CreateBorderedWithForeground<RhombusNode>();
                var state = (KrStageState)stage.Get<int>("StateID");
                if (state == KrStageState.Inactive || state == KrStageState.Active)
                {
                    int stateID = card.Sections[KrApprovalCommonInfo.Virtual].RawFields.Get<int>("StateID");
                    node.Background =
                        stateID == (int)KrState.Disapproved || stateID == (int)KrState.Declined
                            ? RouteBrushes.Finished
                            : RouteBrushes.NotCreatedNode;
                }
                else
                {
                    node.Background = RouteBrushes.Finished;
                }

                foreach (IConnection inputArrow in layout.Connections)
                {
                    inputArrow.To ??= node;
                }

                layout.Nodes.Add(node);
                layout.Connections.Add(Connection.CreateArrow(node, null));
            }
        }


        /// <summary>
        /// Добавляет ноду-согласанта на схему ВФ
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Схема ВФ</param>
        /// <param name="card">Карточка документа загруженная со всеми заданиями и историей цикла</param>
        /// <param name="stage">Этап согласования, который строится в схеме</param>
        /// <param name="approverID">Идентификатор согласанта</param>
        /// <param name="approverName">Имя согласанта</param>
        /// <param name="approverRowID">RowID</param>
        /// <param name="childNode">Признак того что нода дочерняя (делегирование напр)</param>
        /// <param name="taskRowID">RowID задания</param>
        /// <returns>"Открытую" стрелку с началом в свежепоставленной ноде</returns>
        public static IConnection AddApproverNode(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            CardRow stage,
            Guid approverID,
            string? approverName,
            Guid approverRowID,
            bool childNode = false,
            Guid? taskRowID = null)
        {
            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
            //Предустановим шапку, если задание было создано/завершено, тогда забьем в ноду другую шапку
            node.ContentControl = GetApprovalNodeContent(null, approverName, null);

            var outputArrow = Connection.CreateArrow(node, null);

            CardTaskHistoryItem? task = null;

            //Если этап не начался
            int stateID = card.Sections[KrApprovalCommonInfo.Virtual].RawFields.Get<int>("StateID");
            bool stageInactive = stage.Get<int>("StateID") == (int)KrStageState.Inactive
                && stateID != (int)KrState.Disapproved && stateID != (int)KrState.Declined && stateID != KrState.Editing;

            if (stageInactive)
            {
                node.Background = RouteBrushes.NotCreatedNode;
                node.Foreground = RouteBrushes.ObscureNodeForeground;
            }
            else
            {
                CardRow[] cycleApprovalTasks =
                    card.Sections[KrApprovalHistory.Virtual].Rows
                        .Where(x =>
                            card.TaskHistory.Any(
                                c => c.RowID == x.Get<Guid>("HistoryRecord")
                                    && (c.TypeID == DefaultTaskTypes.KrApproveTypeID || c.TypeID == DefaultTaskTypes.KrSigningTypeID)))
                        .ToArray();

                CardTaskHistoryItem[] cycleApprovalTasksHistory =
                    card.TaskHistory
                        .Where(x => cycleApprovalTasks.Any(c => c.Get<Guid>("HistoryRecord") == x.RowID))
                        .ToArray();

                if (taskRowID.HasValue)
                {
                    task = cycleApprovalTasksHistory.FirstOrDefault(x => x.RowID == taskRowID.Value);
                }
                else
                {
                    task = cycleApprovalTasksHistory
                        .Where(x =>
                            x.Settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID) == approverID)
                        // сортировка сначала по ParentRowID нужна для исправления бага с несколькими делегированиями на параллельном этапе
                        .OrderBy(x => x.ParentRowID)
                        .ThenBy(x => x.Created)
                        .FirstOrDefault();
                }

                //Если задание еще не было создано
                if (task is null)
                {
                    node.Background = RouteBrushes.NotCreatedNode;
                    node.Foreground = RouteBrushes.ObscureNodeForeground;
                    layout.Nodes.Add(node);

                    IConnection inputArrowInner = layout.Connections.First(x => x.To is null);
                    inputArrowInner.To = node;

                    return outputArrow;
                }

                var performerRoleID = task.Settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID);
                var performerRoleName = task.Settings.TryGet<string>(TaskHistorySettingsKeys.PerformerName);

                //Перезабьем шапку по результатам задания
                node.ContentControl = GetApprovalNodeContent(
                    task.UserID == performerRoleID 
                        ? null
                        : task.UserName,
                    performerRoleName, 
                    task.Result);

                if (task.OptionID is null)
                {
                    node.Background = task.UserID.HasValue ? RouteBrushes.InWork : RouteBrushes.NotInWork;
                }
                //Если согласование было отозвано
                else if (task.OptionID == DefaultCompletionOptions.Cancel
                    || task.OptionID == DefaultCompletionOptions.RebuildDocument
                    || task.OptionID == DefaultCompletionOptions.RejectApproval)
                {
                    node.Background = RouteBrushes.Finished;
                }
                else if (task.OptionID == DefaultCompletionOptions.Approve)
                {
                    node.Background = RouteBrushes.Approved;
                    outputArrow.Caption = LocalizationManager.GetString(LocAppoved);
                }
                else if (task.OptionID == DefaultCompletionOptions.Sign)
                {
                    node.Background = RouteBrushes.Signed;
                    outputArrow.Caption = LocalizationManager.GetString(LocSigned);
                }
                else if (task.OptionID == DefaultCompletionOptions.Disapprove)
                {
                    var advisory =
                        cycleApprovalTasks.FirstOrDefault(p => p.Get<Guid>(KrApprovalHistory.HistoryRecord) == task.RowID)?[KrApprovalHistory.Advisory] as bool? ?? false;

                    node.Background =
                        advisory
                            ? RouteBrushes.Finished
                            : RouteBrushes.Disapproved;
                    outputArrow.Caption = LocalizationManager.GetString(LocNotApproved);
                }
                else if (task.OptionID == DefaultCompletionOptions.Decline)
                {
                    node.Background = RouteBrushes.Declined;
                    outputArrow.Caption = LocalizationManager.GetString(LocDeclined);
                }
                else if (task.OptionID == DefaultCompletionOptions.Delegate)
                {
                    node.Background = RouteBrushes.Finished;
                    layout.Nodes.Add(node);

                    IConnection inputArrowInner = layout.Connections.First(x => x.To is null);
                    inputArrowInner.To = node;

                    outputArrow.Caption = LocalizationManager.GetString(LocDelegated);
                    layout.Connections.Add(outputArrow);

                    //Ищем на кого делегировано задание
                    //Находим дочернее задание
                    CardTaskHistoryItem childTask = cycleApprovalTasksHistory.First(
                            x => (x.TypeID == DefaultTaskTypes.KrApproveTypeID || x.TypeID == DefaultTaskTypes.KrSigningTypeID)
                            && x.ParentRowID == task.RowID);

                    AddCommentsNodesIfNeeded(
                        factory,
                        layout,
                        card,
                        node,
                        task.RowID);

                    AddAdditionalApprovalIfNeeded(
                        factory,
                        layout,
                        card,
                        node,
                        task.RowID);

                    DeleteTaskFormHistories(card, task.RowID);

                    var childTaskPerformerRoleID = childTask.Settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID);
                    var childTaskPerformerRoleName = childTask.Settings.TryGet<string>(TaskHistorySettingsKeys.PerformerName);

                    return AddApproverNode(
                        factory,
                        layout,
                        card,
                        stage,
                        childTaskPerformerRoleID ?? Guid.Empty,
                        childTaskPerformerRoleName,
                        approverRowID,
                        true,
                        taskRowID: childTask.RowID);
                }
                else
                {
                    node.Background = RouteBrushes.Finished;
                }
            }

            layout.Nodes.Add(node);

            IConnection inputArrow = layout.Connections.First(x => x.To is null);
            inputArrow.To = node;

            if (task is not null)
            {
                AddCommentsNodesIfNeeded(
                    factory,
                    layout,
                    card,
                    node,
                    task.RowID);

                AddAdditionalApprovalIfNeeded(
                        factory,
                        layout,
                        card,
                        node,
                        task.RowID);

                DeleteTaskFormHistories(card, task.RowID);
            }
            else
            {
                if (card.Sections.TryGetValue(KrAdditionalApprovalUsersCardVirtual.Synthetic, out var additionalApprovalUsers))
                {
                    foreach (var row in additionalApprovalUsers.Rows)
                    {
                        var parenUserRowID = row.Fields.Get<Guid>("MainApproverRowID");
                        if (parenUserRowID == approverRowID)
                        {
                            var roleName = row.Fields.Get<string>("RoleName");
                            AddAdditionalApprovalIfNeeded(
                                factory,
                                layout,
                                card,
                                node,
                                roleName);
                        }
                    }
                }
            }

            return outputArrow;
        }


        /// <summary>
        /// Добавляет ноды комментирования на макет схемы ВФ, если это нужно
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет схемы ВФ</param>
        /// <param name="card">Карточка</param>
        /// <param name="parentNode">Родительская нода на макете</param>
        /// <param name="parentTaskID">Идентификатор родительского задания</param>
        public static void AddCommentsNodesIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            INode parentNode,
            Guid parentTaskID)
        {
            CardTaskHistoryItem[] commentTaskRequests
                = card.TaskHistory.Where(
                        x => x.TypeID == DefaultTaskTypes.KrInfoRequestCommentTypeID
                            && x.ParentRowID == parentTaskID).ToArray();

            foreach (var request in commentTaskRequests)
            {
                INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
                node.IsChildNode = true;

                var arrow = Connection.CreateArrow(parentNode, node, LocalizationManager.GetString(LocCommentRequired));

                var result = card.TaskHistory.FirstOrDefault(x => x.ParentRowID == request.RowID);
                if (result is not null
                    && result.Settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID) is { } performerID
                    && result.Settings.TryGet<string>(TaskHistorySettingsKeys.PerformerName) is { } performerName)
                {
                    node.ContentControl = GetCommentNodeContent(
                        result.UserID == performerID ? null : result.UserName,
                        performerName,
                        request.Result,
                        result.Result);
                    node.ExpandedControl = GetCommentNodeExpandedContent(
                        result.UserID == performerID ? null : result.UserName,
                        performerName,
                        result.Created,
                        request.Result,
                        result.Completed,
                        result.Result);

                    if (result.OptionID is null)
                    {
                        node.Background = string.IsNullOrEmpty(result.UserName)
                            ? RouteBrushes.NotInWork
                            : RouteBrushes.InWork;
                    }
                    else
                    {
                        node.Background = RouteBrushes.Finished;
                    }

                    layout.Nodes.Add(node);
                    layout.Connections.Add(arrow);

                    DeleteTaskFormHistories(card, result.RowID);
                }
            }
        }

        /// <summary>
        /// Добавляет ноды доп.согласования на макет схемы ВФ, если это нужно
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет схемы ВФ</param>
        /// <param name="card">Карточка</param>
        /// <param name="parentNode">Родительская нода на макете</param>
        /// <param name="parentTaskID">Идентификатор родительского задания</param>
        public static void AddAdditionalApprovalIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            INode parentNode,
            Guid parentTaskID)
        {
            CardTaskHistoryItem[] additionalApprovalTaskRequests
                = card.TaskHistory.Where(
                        x => x.TypeID == DefaultTaskTypes.KrInfoAdditionalApprovalTypeID
                            && x.ParentRowID == parentTaskID).ToArray();

            foreach (var request in additionalApprovalTaskRequests)
            {
                INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
                node.IsChildNode = true;

                var arrow = Connection.CreateArrow(parentNode, node, LocalizationManager.GetString(LocAdditionalApproval));

                var result = card.TaskHistory.FirstOrDefault(x => x.ParentRowID == request.RowID);
                if (result is not null
                    && result.Settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID) is { } performerID
                    && result.Settings.TryGet<string>(TaskHistorySettingsKeys.PerformerName) is { } performerName)
                {
                    node.ContentControl = GetAdditionalApprovalNodeContent(
                        result.UserID == performerID ? null : result.UserName,
                        performerName,
                        request.Result,
                        result.Result);

                    node.ExpandedControl = GetAdditionbalApprovalNodeExpandedContent(
                        result.UserID == performerID ? null : result.UserName,
                        performerName,
                        request.Result,
                        result.Result);

                    if (result.OptionID is null)
                    {
                        node.Background = string.IsNullOrEmpty(result.UserName)
                            ? RouteBrushes.NotInWork
                            : RouteBrushes.InWork;
                    }
                    else
                    {
                        node.Background = RouteBrushes.Finished;
                    }

                    layout.Nodes.Add(node);
                    layout.Connections.Add(arrow);

                    // Находим дочерние комментирования
                    CardTaskHistoryItem[] additionalApprovalTaskRequestsChilds
                        = card.TaskHistory.Where(
                            x => x.TypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID
                                 && x.ParentRowID == request.RowID).ToArray();

                    foreach (var additionalApprovalTaskRequestsChild in additionalApprovalTaskRequestsChilds)
                    {
                        AddCommentsNodesIfNeeded(
                            factory,
                            layout,
                            card,
                            node,
                            additionalApprovalTaskRequestsChild.RowID);
                    }

                    DeleteTaskFormHistories(card, result.RowID);
                }
            }
        }


        /// <summary>
        /// Добавляет ноды доп.согласования на макет схемы ВФ, если это нужно. Для запланированных этапов.
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет схемы ВФ</param>
        /// <param name="card">Карточка</param>
        /// <param name="parentNode">Родительская нода на макете</param>
        /// <param name="roleName">Имя роли</param>
        public static void AddAdditionalApprovalIfNeeded(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            INode parentNode,
            string? roleName)
        {
            INode node = factory.CreateBorderedWithForeground<RoundedRectangleNode>();
            node.IsChildNode = true;

            var arrow = Connection.CreateArrow(parentNode, node, LocalizationManager.GetString(LocAdditionalApproval));

            node.ContentControl = GetAdditionalApprovalNodeContent(
                null,
                roleName,
                string.Empty,
                string.Empty);

            node.Background = RouteBrushes.NotCreatedNode;
            node.Foreground = RouteBrushes.ObscureNodeForeground;

            layout.Nodes.Add(node);
            layout.Connections.Add(arrow);

        }

        /// <summary>
        /// Добавляет на макет ВФ согласующих этапа
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        /// <param name="stage">Этап согласования</param>
        /// <param name="stageApprovers">Согласующие этапа</param>
        public static void AddStageApprovers(
            INodeFactory factory,
            INodeLayout layout,
            Card card,
            CardRow stage,
            ICollection<CardRow> stageApprovers)
        {
            if (stage.Get<bool>(KrApprovalSettingsVirtual.IsParallel)
                || stage.Get<bool>(KrSigningStageSettingsVirtual.IsParallel))
            {
                //Исходная стрелка от этапа, запоминаем ее, чтобы "размножить" в параллельном этапе
                IConnection inputArrow = layout.Connections.First(x => x.To is null);
                //И удалим ее, чтобы не думать о ней, а для каждого согласанта просто добавлять копию
                //с установленной целевой нодой
                layout.Connections.Remove(inputArrow);

                //Стрелки от каждого согласанта
                List<IConnection> stageArrows = new List<IConnection>();

                foreach (CardRow approver in stageApprovers)
                {
                    layout.Connections.Add(inputArrow.Clone());

                    //Выходную стрелку для каждого согласанта запоминаем
                    Guid approverID = approver.Get<Guid>(KrPerformersVirtual.PerformerID);
                    string? approverName = approver.Get<string>(KrPerformersVirtual.PerformerName);
                    stageArrows.Add(AddApproverNode(factory, layout, card, stage, approverID, approverName, approver.RowID));
                }

                layout.Connections.AddRange(stageArrows);
                AddMergeNodeIfNeeded(factory, layout, card, stage, stageApprovers);
            }
            else
            {
                foreach (CardRow approver in stageApprovers)
                {
                    Guid approverID = approver.Get<Guid>(KrPerformersVirtual.PerformerID);
                    string? approverName = approver.Get<string>(KrPerformersVirtual.PerformerName);
                    layout.Connections.Add(AddApproverNode(factory, layout, card, stage, approverID, approverName, approver.RowID));
                }
            }
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Удаляет задание из истории заданий и истории согласования
        /// </summary>
        /// <param name="card">Карточка</param>
        /// <param name="taskID">ИД удаляемого задания</param>
        private static void DeleteTaskFormHistories(Card card, Guid taskID)
        {
            card.TaskHistory.Remove(
                    card.TaskHistory.First(x => x.RowID == taskID));

            card.Sections[KrApprovalHistory.Virtual].Rows.Remove(
                card.Sections[KrApprovalHistory.Virtual].Rows.First(x => x.Get<Guid>("HistoryRecord") == taskID));
        }

        #endregion

        #region CreateWorkflow Method

        /// <summary>
        /// Заполняет макет ВФ
        /// </summary>
        /// <param name="factory">Фабрика узлов ВФ</param>
        /// <param name="layout">Макет ВФ</param>
        /// <param name="card">Карточка с процессом согласования</param>
        public static void CreateWorkflow(INodeFactory factory, INodeLayout layout, Card card)
        {
            Dictionary<string, object?> fields = card.Sections[KrApprovalCommonInfo.Virtual].RawFields;

            // TODO: предполагаем, что все завершенные этапы согласования говорят о том, что документ был согласован
            // Это криво и потом нужно сделать по другому
            var isApproved = card.Sections[KrStages.Virtual]
                .Rows
                .Where(p => p[KrStages.StageTypeID]?.Equals(StageTypeDescriptors.ApprovalDescriptor.ID) == true)
                .All(p => p[KrStages.StateID]?.Equals(KrStageState.Completed.ID) == true);

            if (fields.Get<int>("StateID") != (int)KrState.Registered
                || isApproved)
            {
                AddStartNode(factory, layout, card);

                //Этапы согласования
                CardRow[] stages = card.Sections[KrStages.Virtual].Rows
                    .OrderBy(x => x.Get<int>("Order"))
                    .ToArray();

                //Копируем оставшихся согласующих
                //На этап можно добавлять одну роль несколько раз, если роль добавили несколько раз,
                //то нам абсолютно параллельно какого из оставшихся мы проверим для параллельного этапа,
                //однака, нужна учитывать скольких мы проверили, чтобы не поставить всем одинаковый статус ноды,
                //для этого каждый раз когда найдем согласанта в активных - убъем его

                ListStorage<CardRow> approverRows = card.Sections[KrPerformersVirtual.Synthetic].Rows;
                foreach (CardRow stage in stages)
                {
                    AddStageNode(factory, layout, card, stage);

                    Guid stageRowID = stage.RowID;
                    CardRow[] stageApprovers = approverRows
                        .Where(x => x.Get<Guid>("StageRowID") == stageRowID)
                        .OrderBy(x => x.Get<int>("Order"))
                        .ToArray();

                    // если исполнителей нет даже с учётом роли "Вычисляемые исполнители", то не показываем список исполнителей в визуализаторе
                    if (stageApprovers.Length > 0)
                    {
                        AddStageApprovers(factory, layout, card, stage, stageApprovers);
                        AddEditInterjectIfNeeded(factory, layout, card, stage);
                    }
                }

                AddFinishNode(factory, layout, card, isApproved);
            }
            else
            {
                AddRegisteredNodeIfNecessary(factory, layout, card);
            }
        }

        #endregion
    }
}
