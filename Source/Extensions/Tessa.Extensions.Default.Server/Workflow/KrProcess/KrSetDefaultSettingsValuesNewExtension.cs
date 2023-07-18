using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrSetDefaultSettingsValuesNewExtension : CardNewExtension
    {
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || context.CardType is null)
            {
                return;
            }

            Guid cardTypeID = context.CardType.ID;
            if (cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
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
            }
            else if (cardTypeID == DefaultCardTypes.KrDocTypeTypeID)
            {
                StringDictionaryStorage<CardSection> sections = context.Response.Card.Sections;
                CardSection docTypeSection = sections[KrConstants.KrDocType.Name];
                Dictionary<string, object> docTypeFields = docTypeSection.RawFields;

                docTypeFields["DocNumberRegularAutoAssignmentID"] = KrDocNumberRegularAutoAssignmentID.None;
                docTypeFields["DocNumberRegularAutoAssignmentDescription"] = KrDocNumberRegularAutoAssignmentName.None;
                docTypeFields["DocNumberRegistrationAutoAssignmentID"] = KrDocNumberRegistrationAutoAssignmentID.None;
                docTypeFields["DocNumberRegistrationAutoAssignmentDescription"] = KrDocNumberRegistrationAutoAssignmentName.None;
            }
        }
    }
}
