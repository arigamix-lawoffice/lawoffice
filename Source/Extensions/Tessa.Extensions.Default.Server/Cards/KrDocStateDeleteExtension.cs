using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение на удаление виртуальной карточки состояния документа.
    /// </summary>
    public sealed class KrDocStateDeleteExtension :
        CardDeleteExtension
    {
        #region Constructors

        public KrDocStateDeleteExtension(
            IConfigurationInfoProvider configurationInfoProvider,
            [OptionalDependency] ISchemeService schemeService = null)
        {
            this.configurationInfoProvider = configurationInfoProvider ?? throw new ArgumentNullException(nameof(configurationInfoProvider));
            this.schemeService = schemeService;
        }

        #endregion

        #region Fields

        private readonly IConfigurationInfoProvider configurationInfoProvider;

        private readonly ISchemeService schemeService;

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardDeleteExtensionContext context)
        {
            if (!context.Session.User.IsAdministrator())
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(ValidationKeys.UserIsNotAdmin)
                    .End();

                return;
            }

            int? stateID = context.Request.TryGetInfo()?.TryGet<int?>(DefaultExtensionHelper.StateIDKey);
            if (!stateID.HasValue)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(CardValidationKeys.UnspecifiedCardID)
                    .End();

                return;
            }

            this.configurationInfoProvider.CheckSealed();

            if (this.schemeService is null)
            {
                // не зарегистрирован сервис схемы, остальное API карточек на месте,
                // но в этом расширении мы ничего не можем сделать
                return;
            }

            SchemeTable table = await this.schemeService.GetTableAsync("KrDocState", context.CancellationToken);
            if (table is null)
            {
                return;
            }

            SchemeRecord record = table.Records.FirstOrDefault(x => (short) x["ID"] == stateID.Value);
            if (record != null && table.Records.Remove(record))
            {
                await this.schemeService.SaveTableAsync(table, context.CancellationToken);
                // кэш схемы, и за ним кэш карточек сбрасываются сами, явный вызов InvalidateCache не нужен
            }

            context.Response = new CardDeleteResponse
            {
                CardTypeID = DefaultCardTypes.KrDocStateTypeID,
                CardTypeName = DefaultCardTypes.KrDocStateTypeName,
            };
        }

        #endregion
    }
}
