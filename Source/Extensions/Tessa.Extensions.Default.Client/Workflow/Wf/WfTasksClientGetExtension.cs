using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Загрузка карточки с управлением секциями заданий на клиенте.
    /// </summary>
    public sealed class WfTasksClientGetExtension :
        CardGetExtension
    {
        #region Private Methods

        private static void SetResolutionFieldChanged(CardTask task)
        {
            Card taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection> taskSections;
            if (taskCard != null
                && (taskSections = taskCard.TryGetSections()) != null
                && taskSections.TryGetValue(WfHelper.ResolutionSection, out CardSection resolutionSection))
            {
                bool fieldsChangingInClosure = false;
                IDictionary<string, object> resolutionFields = resolutionSection.Fields;
                resolutionSection.FieldChanged += (s, e) =>
                {
                    if (fieldsChangingInClosure)
                    {
                        return;
                    }

                    switch (e.FieldName)
                    {
                        case WfHelper.ResolutionPlannedField:
                            fieldsChangingInClosure = true;
                            try
                            {
                                resolutionFields[WfHelper.ResolutionDurationInDaysField] = null;
                            }
                            finally
                            {
                                fieldsChangingInClosure = false;
                            }
                            break;

                        case WfHelper.ResolutionDurationInDaysField:
                            fieldsChangingInClosure = true;
                            try
                            {
                                resolutionFields[WfHelper.ResolutionPlannedField] = null;
                            }
                            finally
                            {
                                fieldsChangingInClosure = false;
                            }
                            break;

                        case WfHelper.ResolutionShowAdditionalField:
                            // нет рекурсивного изменения полей, поэтому не ставим fieldsChangingInClosure
                            if (!(bool)e.FieldValue)
                            {
                                resolutionFields[WfHelper.ResolutionKindIDField] = null;
                                resolutionFields[WfHelper.ResolutionKindCaptionField] = null;
                                resolutionFields[WfHelper.ResolutionAuthorIDField] = null;
                                resolutionFields[WfHelper.ResolutionAuthorNameField] = null;
                                resolutionFields[WfHelper.ResolutionSenderIDField] = null;
                                resolutionFields[WfHelper.ResolutionSenderNameField] = null;
                            }
                            break;

                        case WfHelper.ResolutionWithControlField:
                            // нет рекурсивного изменения полей, поэтому не ставим fieldsChangingInClosure
                            if (!(bool)e.FieldValue)
                            {
                                resolutionFields[WfHelper.ResolutionControllerIDField] = null;
                                resolutionFields[WfHelper.ResolutionControllerNameField] = null;
                            }
                            break;
                    }
                };
            }
        }

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || context.CardType == null
                || context.CardType.Flags.HasNot(CardTypeFlags.AllowTasks)
                || (card = context.Response.TryGetCard()) == null)
            {
                return Task.CompletedTask;
            }

            ListStorage<CardTask> tasks = card.TryGetTasks();
            if (tasks != null && tasks.Count > 0)
            {
                foreach (CardTask task in tasks)
                {
                    if (WfHelper.TaskTypeIsResolution(task.TypeID)
                        && !task.IsLocked)
                    {
                        SetResolutionFieldChanged(task);
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
