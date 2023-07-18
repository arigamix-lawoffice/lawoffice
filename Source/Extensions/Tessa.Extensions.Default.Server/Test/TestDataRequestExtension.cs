using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Sequences;

namespace Tessa.Extensions.Default.Server.Test
{
    public sealed class TestDataRequestExtension :
        CardRequestExtension
    {
        #region Constructors

        public TestDataRequestExtension(
            ICardRepository extendedRepository,
            ISequenceProvider sequenceProvider)
        {
            this.extendedRepository = extendedRepository;
            this.sequenceProvider = sequenceProvider;
        }

        #endregion

        #region Fields

        private readonly ICardRepository extendedRepository;

        private readonly ISequenceProvider sequenceProvider;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            if (!context.Session.User.IsAdministrator())
            {
                ValidationSequence
                     .Begin(context.ValidationResult)
                     .SetObjectName(this)
                     .Error(ValidationKeys.UserIsNotAdmin)
                     .End();

                return;
            }

            Dictionary<string, object> info = context.Request.Info;

            // создаём сотрудников
            int userCount = info.TryGet<int>("UserCount");
            if (userCount > 0)
            {
                var newRequest = new CardNewRequest { CardTypeID = RoleHelper.PersonalRoleTypeID };
                CardNewResponse newResponse = await this.extendedRepository.NewAsync(newRequest, context.CancellationToken);
                context.ValidationResult.Add(newResponse.ValidationResult);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                Card baseCard = newResponse.Card;

                Dictionary<string, object> baseFields = baseCard.Sections["PersonalRoles"].RawFields;
                baseFields["LoginTypeID"] = Int32Boxes.Zero;
                baseFields["LoginTypeName"] = "$Enum_LoginTypes_None";

                for (int i = 0; i < userCount; i++)
                {
                    long? number = await this.sequenceProvider.AcquireNumberAsync("Users", context.ValidationResult, context.CancellationToken);
                    if (!number.HasValue)
                    {
                        break;
                    }

                    Card card = baseCard.Clone();
                    card.ID = Guid.NewGuid();

                    string name = string.Format(LocalizationManager.GetString("KrMessages_UserNameFormat"), number);

                    Dictionary<string, object> fields = card.Sections["PersonalRoles"].RawFields;
                    fields["Name"] = name;
                    fields["FirstName"] = name;
                    fields["FullName"] = name;

                    card.RemoveAllButChanged(CardStoreMode.Insert);

                    var storeRequest = new CardStoreRequest { Card = card };
                    CardStoreResponse storeResponse = await this.extendedRepository.StoreAsync(storeRequest, context.CancellationToken);
                    context.ValidationResult.Add(storeResponse.ValidationResult);

                    if (!storeResponse.ValidationResult.IsSuccessful())
                    {
                        break;
                    }
                }
            }

            // создаём контрагентов
            int partnerCount = info.TryGet<int>("PartnerCount");
            if (partnerCount > 0)
            {
                var newRequest = new CardNewRequest { CardTypeID = DefaultCardTypes.PartnerTypeID };
                CardNewResponse newResponse = await this.extendedRepository.NewAsync(newRequest, context.CancellationToken);
                context.ValidationResult.Add(newResponse.ValidationResult);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                Card baseCard = newResponse.Card;
                baseCard.Sections["Partners"].RawFields["Phone"] = "+71234323232";

                for (int i = 0; i < partnerCount; i++)
                {
                    long? number = await this.sequenceProvider.AcquireNumberAsync("Partners", context.ValidationResult, context.CancellationToken);
                    if (!number.HasValue)
                    {
                        break;
                    }

                    Card card = baseCard.Clone();
                    card.ID = Guid.NewGuid();

                    string name = string.Format(LocalizationManager.GetString("KrMessages_PartnerNameFormat"), number);

                    Dictionary<string, object> fields = card.Sections["Partners"].RawFields;
                    fields["Name"] = name;
                    fields["FullName"] = name;

                    card.RemoveAllButChanged(CardStoreMode.Insert);

                    var storeRequest = new CardStoreRequest { Card = card };
                    CardStoreResponse storeResponse = await this.extendedRepository.StoreAsync(storeRequest, context.CancellationToken);
                    context.ValidationResult.Add(storeResponse.ValidationResult);

                    if (!storeResponse.ValidationResult.IsSuccessful())
                    {
                        break;
                    }
                }
            }

            // добавляем сообщение с результатами
            var resultText = new StringBuilder();
            if (userCount > 0)
            {
                resultText
                    .AppendFormat(LocalizationManager.GetString("KrMessages_UsersGenerated"), userCount);
            }

            if (partnerCount > 0)
            {
                if (userCount > 0)
                {
                    resultText
                        .AppendLine();
                }

                resultText
                    .AppendFormat(LocalizationManager.GetString("KrMessages_PartnersGenerated"), partnerCount);
            }

            if (resultText.Length > 0)
            {
                context.ValidationResult.AddInfo(this, resultText.ToString());
            }
        }

        #endregion
    }
}
