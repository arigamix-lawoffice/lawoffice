using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Notices
{
    /// <summary>
    /// Вспомогательные средства для использования в уведомлениях.
    /// </summary>
    public static class NotificationHelper
    {
        #region Public Methods

        /// <summary>
        /// Формирует строку для параметра Name для использования в ссылках
        /// </summary>
        /// <param name="digest">Дайджест</param>
        /// <param name="fullNumber">Номер карточки</param>
        /// <param name="typeCaption">Тип Карточки</param>
        /// <returns></returns>
        public static string GetNameForLink(string digest, string fullNumber, string typeCaption)
        {
            if (!string.IsNullOrEmpty(digest))
            {
                return digest;
            }

            if (!string.IsNullOrEmpty(fullNumber))
            {
                return fullNumber;
            }

            if (!string.IsNullOrEmpty(typeCaption))
            {
                return typeCaption;
            }

            return LocalizationManager.GetString("UI_Common_DefaultDigest_Card");
        }


        public static void ModifyTaskCaption(NotificationEmail email, CardTask task)
        {
            if (task.Card.Sections.TryGetValue("TaskCommonInfo", out var section))
            {
                var kindCaption = section.RawFields.TryGet<string>("KindCaption");

                if (!string.IsNullOrWhiteSpace(kindCaption))
                {
                    email.PlaceholderAliases.SetReplacement("taskType", "f:TaskCommonInfo.KindCaption task");
                }
            }
        }

        public static void ModifyEmailForMobileApprovers(
            NotificationEmail email,
            CardTask task,
            string mobileApprovalEmail)
        {
            var taskTypeID = task.TypeID;
            if (WfHelper.TaskTypeIsResolution(taskTypeID))
            {
                email.BodyTemplate = email.BodyTemplate
                    .Replace(
                        "{optionsForMobileApproval}",
                        $@"
                        <!--.MA-->
                        <br/>
		                    <p>{GetCompleteRef(task.RowID, mobileApprovalEmail)}{{$KrMessages_TaskCompleteLink}}</a></p>
		                <br/>
                        <!--.MAE-->", StringComparison.Ordinal);
            }
            else if (taskTypeID == DefaultTaskTypes.KrApproveTypeID || taskTypeID == DefaultTaskTypes.KrAdditionalApprovalTypeID)
            {
                email.BodyTemplate = email.BodyTemplate
                    .Replace(
                        "{optionsForMobileApproval}",
                        $@"
                        <!--.MA-->
                        <br/>
                            <p>{GetApprovalRef(true, task.RowID, mobileApprovalEmail)}{{$KrMessages_ApproveLink}}</a></p>
                            <p>{GetApprovalRef(false, task.RowID, mobileApprovalEmail)}{{$KrMessages_DisapproveLink}}</a></p>
                        <br/>
                        <!--.MAE-->", StringComparison.Ordinal);
            }
            else if (taskTypeID == DefaultTaskTypes.KrSigningTypeID)
            {
                email.BodyTemplate = email.BodyTemplate
                    .Replace(
                        "{optionsForMobileApproval}",
                        $@"
                        <!--.MA-->
                        <br/>
                            <p>{GetSigningRef(true, task.RowID, mobileApprovalEmail)}{{$KrMessages_SignLink}}</a></p>
                            <p>{GetSigningRef(false, task.RowID, mobileApprovalEmail)}{{$KrMessages_DeclineLink}}</a></p>
                        <br/>
                        <!--.MAE-->", StringComparison.Ordinal);
            }

            email.BodyTemplate = email.BodyTemplate.Replace(
                "{filesForMobileApproval}",
                @"
                <!--.MA-->
                <br/>
	            <!-- .MISSED_FILES -->
	            <!-- .OVERSIZED_FILES -->
                <!--.MAE-->", StringComparison.Ordinal);

            List<MailFile> mailFiles = null;

            email.GetMailInfoFuncAsync = async (user, card, ct) =>
            {
                if (user.HasMobileApproval)
                {
                    if (mailFiles == null)
                    {
                        mailFiles = new List<MailFile>();
                        foreach (var file in card.Files)
                        {
                            mailFiles.Add(
                                file.ToMailFile());
                        }
                    }

                    var mailInfo = new MailInfo
                    {
                        CardID = card.ID,
                        CardTypeID = card.TypeID,
                        CardTypeName = card.TypeName,
                        LanguageCode = user.LanguageCode,
                        FormatName = user.FormatName,
                        UserID = user.UserID,
                        UserName = user.UserName,
                        TimeZoneUtcOffsetMinutes = user.TimeZoneUtcOffsetMinutes,
                        CalendarID = user.CalendarID,
                    };
                    mailInfo.Files.AddItems(mailFiles);

                    return mailInfo;
                }

                return null;
            };

            email.ModifyBodyFuncAsync = async (body, user, ct) =>
            {
                if (!user.HasMobileApproval)
                {
                    // Удаляем блок с вариантами завершения
                    var startIndex = body.IndexOf("<!--.MA-->", StringComparison.Ordinal);
                    var endIndex = body.IndexOf("<!--.MAE-->", StringComparison.Ordinal);
                    body = body.Remove(startIndex, endIndex - startIndex + 11);
                }

                return body;
            };
        }

        public static Dictionary<string, object> GetInfoWithTask(CardTask task) =>
            new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [PlaceholderHelper.TaskKey] = task
            };

        public static async ValueTask<string> GetMobileApprovalEmailAsync(ICardCache cardCache, CancellationToken cancellationToken = default)
        {
            var serverInstance = await cardCache.Cards.GetAsync(CardHelper.ServerInstanceTypeName, cancellationToken).ConfigureAwait(false);

            return serverInstance.IsSuccess
                ? serverInstance.GetValue().Sections["ServerInstances"].RawFields.Get<string>("MobileApprovalEmail")
                : null;
        }

        public static string GetCompleteRef(
            Guid taskID,
            string mobileApprovalEmail)
        {
            StringBuilder approvalRef = StringBuilderHelper.Acquire(128);

            const string subject = "{$KrMessages_CompleteTaskResultMessage}: {f:DocumentCommonInfo.FullNumber}{f:DocumentCommonInfo.Subject format as (, [0])}";

            // это текст HTML, поэтому должен использоваться метод HtmlEncode, а не UrlEncode
            approvalRef
                .Append("<a href=\"mailto:")
                .Append(mobileApprovalEmail)
                .Append("?subject=[tsk-1-")
                .Append(taskID.ToString("N"))
                .Append("] ")
                .Append(HttpUtility.HtmlEncode(subject))
                .Append("&body=%0A%0A%0A%3C")
                .Append(HttpUtility.HtmlEncode(LocalizationManager.GetString("KrMessages_CommentTextMessage")))
                .Append("%3E");

            // 940 - максимальная длина; минус 2 символа - кавычка и скобка ниже
            if (approvalRef.Length > 938)
            {
                approvalRef.Remove(938, approvalRef.Length - 938);
            }

            return approvalRef
                .Append("\">")
                .ToStringAndRelease();
        }


        public static string GetRef(
            bool isApprove,
            Guid taskID,
            string mobileApprovalEmail,
            string locApproveSubject,
            string locDisapproveSubject)
        {
            StringBuilder approvalRef = StringBuilderHelper.Acquire(128);

            int approve;
            string subject;

            if (isApprove)
            {
                approve = 1;
                subject = $"{{{locApproveSubject}}}";
            }
            else
            {
                approve = 0;
                subject = $"{{{locDisapproveSubject}}}";
            }

            subject += ": {f:DocumentCommonInfo.FullNumber}{f:DocumentCommonInfo.Subject format as (, [0])}";

            // это текст HTML, поэтому должен использоваться метод HtmlEncode, а не UrlEncode
            approvalRef
                .Append("<a href=\"mailto:")
                .Append(mobileApprovalEmail)
                .Append("?subject=[apr-")
                .Append(approve)
                .Append('-')
                .Append(taskID.ToString("N"))
                .Append("] ")
                .Append(HttpUtility.HtmlEncode(subject))
                .Append("&body=%0A%0A%0A%3C")
                .Append(HttpUtility.HtmlEncode(LocalizationManager.GetString("KrMessages_CommentTextMessage")))
                .Append("%3E");

            // 940 - максимальная длина; минус 2 символа - кавычка и скобка ниже
            if (approvalRef.Length > 938)
            {
                approvalRef.Remove(938, approvalRef.Length - 938);
            }

            return approvalRef
                .Append("\">")
                .ToStringAndRelease();
        }

        public static string GetApprovalRef(bool isApprove, Guid taskID, string mobileApprovalEmail) =>
            GetRef(isApprove, taskID, mobileApprovalEmail, "$KrMessages_ApprovalResultMessage", "$KrMessages_DisapprovalResultMessage");

        public static string GetSigningRef(bool isApprove, Guid taskID, string mobileApprovalEmail) =>
            GetRef(isApprove, taskID, mobileApprovalEmail, "$KrMessages_SigningResultMessage", "$KrMessages_DecliningResultMessage");

        #endregion
    }
}
