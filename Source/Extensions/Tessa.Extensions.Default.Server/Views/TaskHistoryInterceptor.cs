using System;
using System.Collections.Generic;
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
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Tessa.Views;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Перед возвратом данных представлением выполняем проверку или расчёт токена на карточку.
    /// </summary>
    public sealed class TaskHistoryInterceptor :
        ViewInterceptorBase
    {
        #region Constructor

        public TaskHistoryInterceptor(
            IKrTokenProvider krTokenProvider,
            ICardCache cardCache,
            ICardRepository cardRepository,
            ISession session,
            ICardMetadata cardMetadata,
            ICardGetStrategy getStrategy,
            IDbScope dbScope)
        {
            this.krTokenProvider = krTokenProvider;
            this.cardCache = cardCache;
            this.cardRepository = cardRepository;
            this.session = session;
            this.cardMetadata = cardMetadata;
            this.getStrategy = getStrategy;
            this.dbScope = dbScope;
        }

        #endregion

        #region Private Fields

        Dictionary<string, string> viewTokenParams;

        private readonly IKrTokenProvider krTokenProvider;

        private readonly ICardCache cardCache;

        private readonly ICardRepository cardRepository;

        private readonly ISession session;

        private readonly ICardMetadata cardMetadata;

        private readonly ICardGetStrategy getStrategy;

        private readonly IDbScope dbScope;

        #endregion

        #region Private Methods

        /// <summary>
        /// Ищет алиас преставления, используемого в контроле <c>Представление</c>
        /// по алиасу этого контрола в типе карточки.
        /// </summary>
        private static string FindViewAlias(CardType cardType, string viewControlAlias)
        {
            foreach(var form in cardType.Forms)
            {
                foreach (var block in form.Blocks)
                {
                    var viewControl = block.Controls.FirstOrDefault(x =>
                        x.Type.ID == CardControlTypes.ViewControl.ID &&
                        x.Name == viewControlAlias);

                    if (viewControl is null)
                    {
                        continue;
                    }
                    return viewControl.ControlSettings.TryGet<string>("ViewAlias");
                }
            }
            return null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Проходимся по всем типам карточек, в которых есть расширение на историю заданий.
        /// Собираем алиасы представление, которые будут показывать историю заданий в этих расширениях.
        /// Сохраняем пары ключ-значение "алиас представления"-"алиас параметра".
        /// Подразумевается, что у одного представления может быть только один парамер, в которой передается токен.
        /// У представления <c>Task History</c> алиас параметра всегда <c>Token</c>.
        /// </summary>
        public override async ValueTask<string[]> GetInterceptedViewAliasesAsync(IList<ITessaView> views, CancellationToken cancellationToken = default)
        {
            this.viewTokenParams = new Dictionary<string, string>();
            var cardTypes = await this.cardMetadata.GetCardTypesAsync(cancellationToken);
            foreach(var cardType in cardTypes)
            {
                foreach(var extensionType in cardType.Extensions.Where(x => x.Type.ID == CardTypeExtensionTypes.MakeViewTaskHistory.ID))
                {
                    var viewControlAlias = extensionType.ExtensionSettings.TryGet<string>(CardTypeExtensionSettings.ViewControlAlias);
                    if (string.IsNullOrWhiteSpace(viewControlAlias))
                    {
                        continue;
                    }
                    var viewAlias = FindViewAlias(cardType, viewControlAlias);
                    if (string.IsNullOrWhiteSpace(viewAlias))
                    {
                        continue;
                    }
                    var tokenParameterAlias = extensionType.ExtensionSettings.TryGet<string>(CardTypeExtensionSettings.TokenParameterAlias);

                    if (viewTokenParams.ContainsKey(viewAlias))
                    {
                        // Если в расширении типа для представления с viewAlias токен задан, а в данном расширении токен не задан, то мы не изменяем расширение.
                        // Получится, что если в расширении, где токен задан (и если задан корректно) проверка прав доступа будет использовать токен, а в расширении,
                        // где алиас параметра токена пустой, расчёт прав будет работать через загрузку карточки на сервере.
                        this.viewTokenParams[viewAlias] = string.IsNullOrWhiteSpace(tokenParameterAlias) ?
                            this.viewTokenParams[viewAlias] :
                            tokenParameterAlias;
                    }
                    else
                    {
                        // даже если алиас параметра с токином пустой, мы сохраняем алиас представления, чтобы его перехватить.
                        viewTokenParams[viewAlias] = tokenParameterAlias;
                    }
                }
            }

            this.viewTokenParams[SystemViewAliases.TaskHistory] = "Token";

            return this.viewTokenParams.Keys.ToArray();
        }

        /// <inheritdoc />
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
                if (request.TryGetParameter("CardID")?.CriteriaValues.FirstOrDefault()?.Values.FirstOrDefault()?.Value is Guid cardID)
                {
                    var validationResult = new ValidationResultBuilder();

                    CardGetContext getContext = await this.getStrategy.TryLoadCardInstanceAsync(
                        cardID, this.dbScope.Db, this.cardMetadata, validationResult, cancellationToken: cancellationToken);

                    if (getContext is null
                        || !validationResult.IsSuccessful()
                        || !(await this.cardMetadata.GetCardTypesAsync(cancellationToken)).TryGetValue(getContext.CardTypeID, out CardType cardType)
                        || !cardType.Flags.Has(CardTypeFlags.AllowTasks))
                    {
                        // или карточки с таким идентификатором нет в базе, или метаинформация по этому типу повреждена,
                        // или тип не поддерживает задания - дальнейшие проверки не имеют смысла
                        return TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
                    }

                    // административные карточки показываем только в случае, если текущий сотрудник - это администратор
                    if (cardType.Flags.Has(CardTypeFlags.Administrative))
                    {
                        return this.session.User.IsAdministrator()
                            ? await view.GetDataAsync(request, cancellationToken)
                            : TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
                    }

                    // карточки, входящие в типовое решение, должны подчиняться правилам доступа
                    if ((await KrComponentsHelper.GetKrComponentsAsync(cardType.ID, this.cardCache, cancellationToken)).Has(KrComponents.Base))
                    {
                        string tokenString = request.TryGetParameter(this.viewTokenParams[request.ViewAlias])?.CriteriaValues.FirstOrDefault()?.Values.FirstOrDefault()?.Value as string;
                        if (!string.IsNullOrEmpty(tokenString))
                        {
                            try
                            {
                                var tokenSerialized = StorageHelper.DeserializeFromTypedJson(tokenString);
                                var token = new KrToken(tokenSerialized);

                                if (token.IsValid()
                                    && token.CardID == cardID
                                    && token.CardVersion > 0
                                    && token.HasPermission(KrPermissionFlagDescriptors.ReadCard)
                                    && token.ExpiryDate.ToUniversalTime() > DateTime.UtcNow)
                                {
                                    var tokenResult = new ValidationResultBuilder();
                                    int cardVersion = getContext.Card.Version;

                                    if (await this.krTokenProvider.ValidateTokenAsync(
                                            new Card { ID = cardID, Version = cardVersion },
                                            token, tokenResult, cancellationToken) == KrTokenValidationResult.Success
                                        && tokenResult.IsSuccessful())
                                    {
                                        // токен валиден для актуальной версии карточки, в нём есть права на чтение карточки;
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

                        bool canReadCard = false;

                        try
                        {
                            var getRequest = new CardGetRequest
                            {
                                CardID = cardID,
                                CardTypeID = cardType.ID,
                                GetMode = CardGetMode.ReadOnly,
                                RestrictionFlags = CardGetRestrictionFlags.RestrictFiles
                                    | CardGetRestrictionFlags.RestrictTaskCalendar
                                    | CardGetRestrictionFlags.RestrictTaskHistory,
                                Info = { [nameof(TaskHistoryInterceptor)] = BooleanBoxes.True }
                            };

                            CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                            if (getResponse.ValidationResult.IsSuccessful())
                            {
                                canReadCard = true;
                            }
                        }
                        catch
                        {
                            // ignored
                        }

                        return canReadCard
                            ? await view.GetDataAsync(request, cancellationToken)
                            : TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
                    }

                    // карточка не связана с типовым решением и не является административной
                }

                // не указан идентификатор карточки
                return await view.GetDataAsync(request, cancellationToken);
            }
        }

        #endregion
    }
}
