using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowViewer
{
    public sealed class GetCardForWorkflowViewerGetExtension : CardGetExtension
    {
        #region Fields

        private readonly ILicenseManager licenseManager;

        #endregion

        #region Constructors

        public GetCardForWorkflowViewerGetExtension(
            ILicenseManager licenseManager)
        {
            this.licenseManager = licenseManager;
        }

        #endregion

        public override async Task BeforeRequest(ICardGetExtensionContext context)
        {
            if (!context.Request.Info.ContainsKey("GetCurrentApprovalStageTasks"))
            {
                //Нам здесь не рады
                return;
            }

            ILicense license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
            if (!LicensingHelper.CheckWorkflowViewerLicense(license, out string licenseErrorMessage))
            {
                context.ValidationResult.AddError(this, licenseErrorMessage);
                return;
            }

            // такая загрузка задач выполняется после платформенных расширений, которые явно её запрещают, поэтому это возможно
            context.Request.GetTaskMode = CardGetTaskMode.All;
        }


        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || !context.Request.Info.ContainsKey("GetCurrentApprovalStageTasks"))
            {
                //Нам здесь не рады
                return;
            }

            Card card = context.Response.Card;

            CardSection info = card.Sections[KrApprovalCommonInfo.Virtual];

            int? state = info.Fields.Get<int?>("StateID");
            if (!state.HasValue
                || state.Value == (int)KrState.Cancelled
                || state.Value == (int)KrState.Draft)
            {
                //Если согласование было отменено или карточка в "проект" - значит не возвращаем задания
                //т.к. на них не надо ориентироваться
                card.TaskHistory.Clear();
                card.Sections[KrApprovalHistory.Virtual].Rows.Clear();
                return;
            }

            List<Guid> activeTasksIds = new List<Guid>();

            foreach (CardRow task in card.Sections[KrActiveTasks.Virtual].Rows)
            {
                activeTasksIds.Add(task.Fields.Get<Guid>("TaskID"));
            }

            //Убиваем лишние текущие задания
            for (int i = 0; i < card.Tasks.Count; i++)
            {
                if (!activeTasksIds.Contains(card.Tasks[i].RowID))
                {
                    card.Tasks.RemoveAt(i--);
                }
            }

            //Подчищаем историю заданий, оставляя только задания согласований для текущего цикла
            var cycle = card.Sections[KrApprovalHistory.Virtual].Rows.Max(p => p.Fields.Get<int>("Cycle"));

            // при редактировании показываем предыдущий цикл
            if (cycle > 1
                && (state == (int)KrState.Editing           // редактирование при возврате на доработку или каком-то кастомном переходе
                    || state == (int)KrState.Disapproved    // редактирование после несогласования
                    || state == (int)KrState.Declined))     // редактирование после отказа в подписании
            {
                cycle--;
            }

            for (int i = 0; i < card.Sections[KrApprovalHistory.Virtual].Rows.Count; i++)
            {
                if (card.Sections[KrApprovalHistory.Virtual].Rows[i].Fields.Get<int>("Cycle") != cycle)
                {
                    card.Sections[KrApprovalHistory.Virtual].Rows.RemoveAt(i--);
                }
            }

            for (int i = 0; i < card.TaskHistory.Count; i++)
            {
                // Удаляем задания неподходящие по типу или относящиеся к другому циклу
                // цикл проверяем по оставшейся истории согласования - мы из нее уже удалили таски
                // с предыдущих циклов.
                if (card.TaskHistory[i].TypeID != DefaultTaskTypes.KrInfoRequestCommentTypeID
                    && card.TaskHistory[i].TypeID != DefaultTaskTypes.KrInfoAdditionalApprovalTypeID
                    && (!CheckTypeID(card.TaskHistory[i].TypeID)
                        || card.Sections[KrApprovalHistory.Virtual].Rows.All(x => x.Fields.Get<Guid>("HistoryRecord") != card.TaskHistory[i].RowID)))
                {
                    card.TaskHistory.RemoveAt(i--);
                }
            }
        }

        private static bool CheckTypeID(Guid typeID) =>
            typeID == DefaultTaskTypes.KrApproveTypeID
            || typeID == DefaultTaskTypes.KrAdditionalApprovalTypeID
            || typeID == KrConstants.KrDeregistrationTypeID
            || typeID == DefaultTaskTypes.KrEditInterjectTypeID
            || typeID == DefaultTaskTypes.KrEditTypeID
            || typeID == DefaultTaskTypes.KrRequestCommentTypeID
            || typeID == DefaultTaskTypes.KrRegistrationTypeID
            || typeID == DefaultTaskTypes.KrSigningTypeID
            || typeID == DefaultTaskTypes.KrStartApprovalProcessTypeID;
    }
}
