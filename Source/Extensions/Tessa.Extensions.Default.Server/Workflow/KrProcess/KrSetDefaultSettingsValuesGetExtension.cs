using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrSetDefaultSettingsValuesGetExtension : CardGetExtension
    {
        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful || context.CardType == null)
            {
                return Task.CompletedTask;
            }

            //Заполняем поля для нормальной работы
            StringDictionaryStorage<CardRow> sectionRows = context.Response.TryGetSectionRows();

            if (sectionRows != null
                && sectionRows.TryGetValue(KrConstants.KrSettingsCardTypes.Name, out CardRow typeRow))
            {
                if (typeRow.ContainsKey("DocNumberRegularAutoAssignmentID")
                    && typeRow.ContainsKey("DocNumberRegularAutoAssignmentDescription"))
                {
                    typeRow["DocNumberRegularAutoAssignmentID"] = KrDocNumberRegularAutoAssignmentID.None;
                    typeRow["DocNumberRegularAutoAssignmentDescription"] = KrDocNumberRegularAutoAssignmentName.None;
                }

                if (typeRow.ContainsKey("DocNumberRegistrationAutoAssignmentID")
                    && typeRow.ContainsKey("DocNumberRegistrationAutoAssignmentDescription"))
                {
                    typeRow["DocNumberRegistrationAutoAssignmentID"] = KrDocNumberRegistrationAutoAssignmentID.None;
                    typeRow["DocNumberRegistrationAutoAssignmentDescription"] = KrDocNumberRegistrationAutoAssignmentName.None;
                }
            }

            return Task.CompletedTask;
        }
    }
}