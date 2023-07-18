using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение, которое обеспечивает получение конкретного задания в запросе,
    /// если в Request.Info есть <see cref="TaskAssignedRolesHelper.EspecialTaskRowIDKey"/>.
    /// Для выполнения, также, требуется валидный <see cref="KrToken"/> в Request.Info с правом <see cref="KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles"/>.
    /// </summary>
    public sealed class KrGetTaskForTasksEditorGetExtension : CardGetExtension
    {
        #region Fields

        private readonly IKrTokenProvider krTokenProvider;
        private readonly ICardGetStrategy cardGetStrategy;
        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public KrGetTaskForTasksEditorGetExtension(
            IKrTokenProvider krTokenProvider,
            ICardGetStrategy cardGetStrategy,
            ICardRepository cardRepository)
        {
            this.krTokenProvider = krTokenProvider ?? throw new ArgumentNullException(nameof(krTokenProvider));
            this.cardGetStrategy = cardGetStrategy ?? throw new ArgumentNullException(nameof(cardGetStrategy));
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Guid taskRowID;
            KrToken token;
            if (!context.Request.CardID.HasValue ||
                (taskRowID = context.Request.Info.TryGet(TaskAssignedRolesHelper.EspecialTaskRowIDKey, Guid.Empty)) == Guid.Empty ||
                context.Response.Card.Tasks.FirstOrDefault(p => p.RowID == taskRowID) is not null ||
                (token = KrToken.TryGet(context.Request.Info)) is null)
            {
                return;
            }

            var cardID = context.Request.CardID.Value;
            bool canSeeAllTasks = false;

            if (token.IsValid()
                && token.CardID == cardID
                && token.CardVersion > 0
                && token.HasPermission(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles)
                && token.ExpiryDate.ToUniversalTime() > DateTime.UtcNow)
            {
                var tokenResult = new ValidationResultBuilder();
                int cardVersion = context.Response.Card.Version;

                if (await this.krTokenProvider.ValidateTokenAsync(
                        new Card { ID = cardID, Version = cardVersion },
                        token, tokenResult, context.CancellationToken) == KrTokenValidationResult.Success
                    && tokenResult.IsSuccessful())
                {
                    canSeeAllTasks = true;
                }
            }
            // либо токен не был задан, либо был, но не прошёл проверку, например,
            // если версия карточки изменилась с момента отправки токена;
            // рассчитываем токен заново, выполняя загрузку карточки с расширениями;
            // если карточка успешно загрузится, то права присутствуют
            else
            {
                try
                {
                    var getRequest = new CardGetRequest
                    {
                        CardID = cardID,
                        CardTypeID = context.CardType.ID,
                        GetMode = CardGetMode.Edit,
                        RestrictionFlags = CardGetRestrictionFlags.RestrictFiles
                            | CardGetRestrictionFlags.RestrictTaskCalendar
                            | CardGetRestrictionFlags.RestrictTaskHistory,
                        Info = { [nameof(KrGetTaskForTasksEditorGetExtension)] = BooleanBoxes.True }
                    };

                    CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, context.CancellationToken);
                    if (getResponse.ValidationResult.IsSuccessful() &&
                        (token = KrToken.TryGet(getResponse.Info)) is not null &&
                        token.IsValid() &&
                        token.HasPermission(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles))
                    {
                        canSeeAllTasks = true;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            if (canSeeAllTasks)
            {
                await using (context.DbScope.Create())
                {
                    var db = context.DbScope.Db;
                    await this.cardGetStrategy
                        .TryLoadTaskInstancesAsync(
                            cardID,
                            context.Response.Card,
                            db,
                            context.CardMetadata,
                            context.ValidationResult,
                            context.Session,
                            getTaskMode: CardGetTaskMode.All,
                            taskRowIDList: new[] { taskRowID },
                            cancellationToken: context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
