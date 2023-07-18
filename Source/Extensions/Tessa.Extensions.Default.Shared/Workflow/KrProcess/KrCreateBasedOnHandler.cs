using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <inheritdoc />
    public sealed class KrCreateBasedOnHandler :
        IKrCreateBasedOnHandler
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="unityContainer">Контейнер Unity, используемый для копирования файлов.</param>
        public KrCreateBasedOnHandler(IUnityContainer unityContainer) =>
            this.unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));

        #endregion

        #region Fields

        private readonly IUnityContainer unityContainer;

        #endregion

        #region IKrCreateBasedOnHandler Members

        /// <inheritdoc />
        public ValueTask<ValidationResult> CopyInfoAsync(Card baseCard, Card newCard, CancellationToken cancellationToken = default)
        {
            StringDictionaryStorage<CardSection> baseSections = baseCard.Sections;
            StringDictionaryStorage<CardSection> newSections = newCard.Sections;

            if (!baseSections.TryGetValue("DocumentCommonInfo", out CardSection baseSection)
                || !newSections.TryGetValue("DocumentCommonInfo", out CardSection newSection))
            {
                return new ValueTask<ValidationResult>(ValidationResult.Empty);
            }

            IDictionary<string, object> newFields = newSection.Fields;
            Dictionary<string, object> baseFields = baseSection.RawFields;

            if (newFields.ContainsKey("Subject") && baseFields.ContainsKey("Subject"))
            {
                newFields["Subject"] = baseFields["Subject"];
            }

            if (baseFields.ContainsKey("PartnerID") && newFields.ContainsKey("PartnerID"))
            {
                newFields["PartnerID"] = baseFields["PartnerID"];
            }

            if (baseFields.ContainsKey("PartnerName") && newFields.ContainsKey("PartnerName"))
            {
                newFields["PartnerName"] = baseFields["PartnerName"];
            }

            if (baseFields.ContainsKey("Amount") && newFields.ContainsKey("Amount"))
            {
                newFields["Amount"] = baseFields["Amount"];
            }

            if (baseFields.ContainsKey("CurrencyID") && newFields.ContainsKey("CurrencyID"))
            {
                newFields["CurrencyID"] = baseFields["CurrencyID"];
            }

            if (baseFields.ContainsKey("CurrencyName") && newFields.ContainsKey("CurrencyName"))
            {
                newFields["CurrencyName"] = baseFields["CurrencyName"];
            }

            if (newSections.TryGetValue("OutgoingRefDocs", out CardSection newOutgoingRefDocs))
            {
                CardRow row = newOutgoingRefDocs.Rows.Add();
                row.RowID = Guid.NewGuid();

                row["DocID"] = baseCard.ID;
                row["DocDescription"] = KrCreateBasedOnHelper.TryGetDocDescription(baseCard);
                row["Order"] = Int32Boxes.Zero;

                row.State = CardRowState.Inserted;
            }

            return new ValueTask<ValidationResult>(ValidationResult.Empty);
        }


        /// <inheritdoc />
        public ValueTask<ValidationResult> CopyFilesAsync(Card baseCard, Card newCard, CancellationToken cancellationToken = default) =>
            CardHelper.CopyFilesAsync(baseCard, newCard, this.unityContainer, cancellationToken: cancellationToken);

        #endregion
    }
}