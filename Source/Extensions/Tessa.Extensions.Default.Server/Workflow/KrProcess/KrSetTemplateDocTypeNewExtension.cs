using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Использует тип документа вместо типа карточки при создании шаблона.
    /// </summary>
    public sealed class KrSetTemplateDocTypeNewExtension :
        CardNewExtension
    {
        #region Constructors

        public KrSetTemplateDocTypeNewExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            CardNewResponse response;
            Card card;
            StringDictionaryStorage<CardSection> sections;
            Dictionary<string, object> templatesFields;
            if (!context.RequestIsSuccessful
                || (response = context.Response) == null
                || (card = response.TryGetCard()) == null
                || (sections = card.TryGetSections()) == null
                || !sections.TryGetValue("Templates", out CardSection templates)
                || !(await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))
                    .TryGetValue((templatesFields = templates.RawFields).Get<Guid>("TypeID"), out CardType typeInTemplate)
                || (await KrComponentsHelper.GetKrComponentsAsync(typeInTemplate.ID, this.typesCache, context.CancellationToken)).HasNot(KrComponents.DocTypes))
            {
                return;
            }

            Card cardInTemplate = new Card(StorageHelper.DeserializeFromTypedJson(templatesFields.Get<string>("Card")));
            Dictionary<string, object> commonInfo = cardInTemplate.Sections["DocumentCommonInfo"].RawFields;

            Guid? docTypeID = commonInfo.Get<Guid?>("DocTypeID");
            string docTypeTitle;
            if (docTypeID.HasValue
                && !string.IsNullOrWhiteSpace(docTypeTitle = commonInfo.Get<string>("DocTypeTitle")))
            {
                templatesFields["TypeCaption"] = docTypeTitle;
            }
        }

        #endregion
    }
}
