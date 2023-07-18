using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Views;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Перехватчик для представления CardTasks.
    /// В случае наличия параметра CardIDParam проверяет в параметрах представления валидный токен по указанному идентификатору карточки.
    /// Если токен валиден - разрешает дальнейшую обработку. Если нет - возвращает пустой результат.
    /// Если параметр - CardIDParam не указан, тоже разрешает дальнейшую обработку, т.к. в этом случае представление ничего не вернёт.
    /// </summary>
    public sealed class CardTasksInterceptor : ViewInterceptorBase
    {
        #region Private Fields

        private readonly IKrTokenProvider krTokenProvider;

        private readonly ICardCache cardCache;

        private readonly ICardRepository cardRepository;

        private readonly ICardMetadata cardMetadata;

        private readonly ICardGetStrategy getStrategy;

        private readonly IDbScope dbScope;

        #endregion

        #region Constructor

        public CardTasksInterceptor(
            IKrTokenProvider krTokenProvider,
            ICardCache cardCache,
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            ICardGetStrategy getStrategy,
            IDbScope dbScope) 
            : base(new[] { "CardTasks" })
        {
            this.krTokenProvider = krTokenProvider ?? throw new ArgumentNullException(nameof(krTokenProvider));
            this.cardCache = cardCache ?? throw new ArgumentNullException(nameof(cardCache));
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.cardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
            this.getStrategy = getStrategy ?? throw new ArgumentNullException(nameof(getStrategy));
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
        }

        #endregion

        #region Base Overrides

        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!this.InterceptedViews.TryGetValue(
                    request.ViewAlias ?? throw new InvalidOperationException("View alias isn't specified."),
                    out ITessaView view))
            {
                throw new InvalidOperationException($"Can't find view with alias:'{request.ViewAlias}'");
            }

            await using (this.dbScope.Create())
            {
                if (request.TryGetParameter("CardIDParam")?.CriteriaValues.FirstOrDefault()?.Values.FirstOrDefault()?.Value is Guid cardID)
                {
                    var validationResult = new ValidationResultBuilder();

                    CardGetContext getContext = 
                        await this.getStrategy.TryLoadCardInstanceAsync(
                            cardID, 
                            this.dbScope.Db, 
                            this.cardMetadata, 
                            validationResult,
                            cancellationToken: cancellationToken);

                    if (getContext is null
                        || !validationResult.IsSuccessful()
                        || !(await this.cardMetadata.GetCardTypesAsync(cancellationToken)).TryGetValue(getContext.CardTypeID, out CardType cardType)
                        || !cardType.Flags.Has(CardTypeFlags.AllowTasks))
                    {
                        // или карточки с таким идентификатором нет в базе, или метаинформация по этому типу повреждена,
                        // или тип не поддерживает задания - дальнейшие проверки не имеют смысла
                        return TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
                    }

                    // карточки, входящие в типовое решение, должны подчиняться правилам доступа
                    if ((await KrComponentsHelper.GetKrComponentsAsync(cardType.ID, this.cardCache, cancellationToken)).Has(KrComponents.Base))
                    {
                        string tokenString = 
                            request.TryGetParameter("Token")?.CriteriaValues.FirstOrDefault()?.Values.FirstOrDefault()?.Value as string;

                        if (!string.IsNullOrEmpty(tokenString))
                        {
                            try
                            {
                                var tokenSerialized = StorageHelper.DeserializeFromTypedJson(tokenString);
                                var token = new KrToken(tokenSerialized);

                                if (token.IsValid()
                                    && token.CardID == cardID
                                    && token.CardVersion > 0
                                    && token.HasPermission(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles)
                                    && token.ExpiryDate.ToUniversalTime() > DateTime.UtcNow)
                                {
                                    var tokenResult = new ValidationResultBuilder();
                                    int cardVersion = getContext.Card.Version;

                                    if (await this.krTokenProvider.ValidateTokenAsync(
                                            new Card { ID = cardID, Version = cardVersion },
                                            token, tokenResult, cancellationToken) == KrTokenValidationResult.Success
                                        && tokenResult.IsSuccessful())
                                    {
                                        // токен валиден для актуальной версии карточки, в нём есть права на изменение ФРЗ;
                                        // поэтому возвращаем данные представления
                                        return await view.GetDataAsync(request, cancellationToken);
                                    }
                                }
                            }
                            catch
                            {
                                // ignored
                            }
                        }

                        // либо токен не был задан, либо был, но не прошёл проверку, например,
                        // если версия карточки изменилась с момента отправки токена;
                        // рассчитываем токен заново, выполняя загрузку карточки с расширениями;
                        // если карточка успешно загрузится, то права присутствуют
                        bool canSeeAllTasks = false;

                        try
                        {
                            var getRequest = new CardGetRequest
                            {
                                CardID = cardID,
                                CardTypeID = cardType.ID,
                                GetMode = CardGetMode.Edit,
                                RestrictionFlags = CardGetRestrictionFlags.RestrictFiles
                                    | CardGetRestrictionFlags.RestrictTaskCalendar
                                    | CardGetRestrictionFlags.RestrictTaskHistory,
                                Info = { [nameof(CardTasksInterceptor)] = BooleanBoxes.True }
                            };

                            CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                            KrToken token;
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

                        return canSeeAllTasks
                            ? await view.GetDataAsync(request, cancellationToken)
                            : TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
                    }
                    // карточка не связана с типовым решением
                }

                // не указан идентификатор карточки, значит View и так ничего не вернёт
                return await view.GetDataAsync(request, cancellationToken);
            }
        }

        #endregion
    }
}
