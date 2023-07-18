using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение проверяет воможность создания карточки текущим пользователем по создаваемому
    /// шаблону и, если прав для создание карточки по этому шаблону недостаточно, предупреждает
    /// пользователя об этом.
    /// </summary>
    public sealed class KrWarnCannotCreateWhenCreatingTemplate : CardNewExtension
    {
        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Constructors

        public KrWarnCannotCreateWhenCreatingTemplate(IKrPermissionsManager permissionsManager)
        {
            Check.ArgumentNotNull(permissionsManager, nameof(permissionsManager));

            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(ICardNewExtensionContext context)
        {
            Card template;
            if (context.CardType is null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || (template = context.Request.TryGetTemplateCard()) is null)
            {
                return;
            }

            var permContext = await permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardTypeID = template.TypeID,
                    DocTypeID = KrProcessSharedHelper.GetDocTypeID(template),
                    AdditionalInfo = context.Info,
                    PrevToken = KrToken.TryGet(context.Request.Info),
                    ExtensionContext = context,
                    ServerToken = context.Info.TryGetServerToken(),
                },
                cancellationToken: context.CancellationToken);

            if (permContext is not null)
            {
                var result = await permissionsManager.CheckRequiredPermissionsAsync(
                    permContext,
                    KrPermissionFlagDescriptors.CreateCard);

                if (!result)
                {
                    context.ValidationResult.AddWarning(this, "$KrMessages_WarnCantCreateCardBasedOnTemplate");
                }
            }
        }

        #endregion
    }
}
