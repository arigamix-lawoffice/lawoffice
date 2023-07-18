using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Controls;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Расширение, которое прикрепляет открытие редактора ФРЗ по двойному клику в диалоге редактирования ФРЗ,
    /// вызываемом из левого меню по кнопке вторичного процесса.
    /// </summary>
    public sealed class KrCardTasksEditorUIExtension : CardUIExtension
    {
        #region Records 

        /// <summary>
        /// Маппинг колонок в представлении.
        /// </summary>
        /// <param name="TaskIDName">Название колонки с идентификатором задания.</param>
        /// <param name="CardIDName">Название колонки с идентификатором карточки.</param>
        private readonly record struct ViewMapping(string TaskIDName, string CardIDName);

        #endregion

        #region Constants

        private const string ViewControlName = "CardTasks";
        private const string TaskIDPrefixReference = "Task";
        private const string CardIDColumnName = "ID";
        private const string TokenParamName = "Token";

        #endregion

        #region Fields

        private readonly ISession session;
        private readonly IUIHost uiHost;
        private readonly ICardRepository cardRepository;
        private readonly IAdvancedCardDialogManager advancedCardDialogManager;
        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;

        #endregion

        #region Constructors

        public KrCardTasksEditorUIExtension(
            ISession session,
            IUIHost uiHost,
            ICardRepository cardRepository,
            IAdvancedCardDialogManager advancedCardDialogManager,
            CreateDialogFormFuncAsync createDialogFormFuncAsync)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            this.uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.advancedCardDialogManager = advancedCardDialogManager ?? throw new ArgumentNullException(nameof(advancedCardDialogManager));
            this.createDialogFormFuncAsync = createDialogFormFuncAsync ?? throw new ArgumentNullException(nameof(createDialogFormFuncAsync));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Подключает функционал открытия карточки по двойному клику к <see chef="ViewControlName"/>.
        /// </summary>
        /// <param name="uiContext">Контекст операции с пользовательским интерфейсом.</param>
        /// <param name="cardModel">Модель карточки.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task AttachDoubleClickHandlerAsync(IUIContext uiContext, ICardModel cardModel)
        {
            // пытаемся получить контрол по имени
            var viewModel = cardModel.Controls.TryGet<CardViewControlViewModel>(ViewControlName);
            if (viewModel is null)
            {
                return;
            }

            // определяем колонки с идентификатором карточки и задания
            // Если не получилось - дальше не идём
            if (!TryMappView(viewModel, out var mapping))
            {
                return;
            }

            // присоединяем наш обработчик
            viewModel.DoubleClickCommand = new DelegateCommand(async p =>
            {
                await this.DoubleClickHandlerAsync(
                    uiContext,
                    viewModel,
                    (IViewClickInfo) p,
                    mapping);
            });
        }

        /// <summary>
        /// Обработчик двойного клика мыши.
        /// </summary>
        /// <param name="uiContext">Контекст операции с пользовательским интерфейсом.</param>
        /// <param name="viewControl">Контрол представления.</param>
        /// <param name="clickInfo">Информация о текущей выделенной в представлении строке.</param>
        /// <param name="mapping">Названия колонок с идентификатором и именем.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task DoubleClickHandlerAsync(
            IUIContext uiContext,
            CardViewControlViewModel viewControl,
            IViewClickInfo clickInfo,
            ViewMapping mapping)
        {
            var row = clickInfo.Row;
            RequestParameter param;
            if (row.Data[mapping.TaskIDName] is not Guid taskRowID ||
                taskRowID == Guid.Empty ||
                row.Data[mapping.CardIDName] is not Guid cardID ||
                cardID == Guid.Empty ||
                viewControl?.Parameters is null ||
                (param = viewControl.Parameters.FirstOrDefault(p => p.Name == TokenParamName)) is null ||
                param.CriteriaValues.Count == 0 ||
                param.CriteriaValues[0].Values.Count == 0 ||
                param.CriteriaValues[0].Values[0].Value is not string tokenString)
            {
                return;
            }

            using var splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);

            // Нам нужен UIContext для карточки редактирования связанных с заданием ролей
            await using var _ = UIContext.Create(uiContext);
            try
            {
                var tokenSerialized = StorageHelper.DeserializeFromTypedJson(tokenString);
                var token = new KrToken(tokenSerialized);
                Card card = null;
                await TaskAssignedRolesDialogHelper.ShowTaskAssignedRolesEditorDialogAsync(
                    this.advancedCardDialogManager,
                    this.createDialogFormFuncAsync,
                    this.uiHost,
                    uiContext.CardEditor.CardModel.GeneralMetadata,
                    taskRowID,
                    this.session.User.ID,
                    // Проверки на права нет, т.к. если пользователь увидел этот диалог, то он точно имеет права на редактирование любых ФРЗ.
                    checkAccessFunctionAsync: async (cardTask, cardModel, refreshFunc) => (true, cardTask),
                    getCardTaskFunctionAsync: async () =>
                    {
                        // Создадим info для запроса задания
                        var info =
                            new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                { TaskAssignedRolesHelper.EspecialTaskRowIDKey, taskRowID }
                            };
                        // Добавим в info токен с правами доступа
                        token.Set(info);

                        var getResponse =
                            await this.cardRepository.GetAsync(
                                new CardGetRequest
                                {
                                    CardID = cardID,
                                    RestrictionFlags =
                                        CardGetRestrictionFlags.RestrictFileSections |
                                        CardGetRestrictionFlags.RestrictFiles |
                                        CardGetRestrictionFlags.RestrictSections |
                                        CardGetRestrictionFlags.RestrictTaskCalendar |
                                        CardGetRestrictionFlags.RestrictTaskHistory |
                                        CardGetRestrictionFlags.RestrictTaskSections |
                                        CardGetRestrictionFlags.RestrictTasks,
                                    Info = info
                                });

                        splash?.Dispose();

                        if (!getResponse.ValidationResult.IsSuccessful())
                        {
                            await TessaDialog.ShowNotEmptyAsync(getResponse.ValidationResult);
                            return null;
                        }

                        // Запоминаем карточку, чтобы потом её сохранить.
                        card = getResponse.Card;
                        return card.Tasks.FirstOrDefault(p => p.RowID == taskRowID);
                    },
                    trySaveChangesFunctionAsync: async () =>
                    {
                        var storeResponse =
                            await this.cardRepository.StoreAsync(
                                new CardStoreRequest
                                {
                                    Card = card
                                });

                        await TessaDialog.ShowNotEmptyAsync(storeResponse.ValidationResult);
                        if (!storeResponse.ValidationResult.IsSuccessful())
                        {
                            return false;
                        }

                        var mainCardContext = uiContext.Parent;
                        if (mainCardContext.CardEditor is not null)
                        {
                            await mainCardContext.CardEditor.RefreshCardAsync(mainCardContext);
                        }

                        return true;
                    });

                await viewControl.RefreshAsync();
            }
            catch (NotSupportedException)
            {
                // используется FakeUIHost, например, мы открыты в TessaAdmin, игнорируем ошибку
            }
        }

        /// <summary>
        /// Помощник получения маппинга колонок представления.
        /// </summary>
        /// <param name="viewModel">Модель контрола представления.</param>
        /// <param name="mapping">Маппинг.</param>
        /// <returns>Возвращает маппинг или <c>null</c> - если маппинг не найден.</returns>
        private static bool TryMappView(CardViewControlViewModel viewModel, out ViewMapping mapping)
        {
            mapping = new ViewMapping
            {
                TaskIDName = null,
                CardIDName = null
            };

            var viewMetadata = viewModel.ViewMetadata;
            if (viewMetadata is null)
            {
                return false;
            }

            // пытаемся найти нужную ссылку
            var reference = viewMetadata.References.FindByName(TaskIDPrefixReference);
            if (reference is null)
            {
                return false;
            }
            var taskIdColumnAlias = TryGetColumnAlias(viewMetadata, prefixReference: reference.ColPrefix);
            var cardIdColumnAlias = TryGetColumnAlias(viewMetadata, columnName: CardIDColumnName);
            if (!string.IsNullOrWhiteSpace(taskIdColumnAlias) &&
                !string.IsNullOrWhiteSpace(cardIdColumnAlias))
            {
                mapping = new ViewMapping
                {
                    TaskIDName = taskIdColumnAlias,
                    CardIDName = cardIdColumnAlias
                };
                return true;
            }
            return false;
        }

        /// <summary>
        /// Помощник получения маппинга колонок представления.
        /// </summary>
        /// <param name="viewMetadata">Метаданные представления.</param>
        /// <param name="prefixReference">Префикс целевых колонок представления.</param>
        /// <param name="columnName">Название целевой колонки.</param>
        /// <returns>Возвращает маппинг или <c>null</c> - если маппинг не найден.</returns>
        private static string TryGetColumnAlias(
            IViewMetadata viewMetadata,
            string prefixReference = null,
            string columnName = null)
        {
            if (viewMetadata is null)
            {
                return null;
            }

            IViewColumnMetadata column = null;
            if (!string.IsNullOrWhiteSpace(prefixReference))
            {
                column =
                    viewMetadata.Columns.FindByName(prefixReference + "ID") ??
                    viewMetadata.Columns.FindByName(prefixReference + "RowID");
            }
            else if (!string.IsNullOrWhiteSpace(columnName))
            {
                column =
                    viewMetadata.Columns.FindByName(columnName);
            }

            return column?.Alias;
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            context.Model.MainFormWithTabs.TabsAreCollapsed = true;

            await this.AttachDoubleClickHandlerAsync(
                context.UIContext,
                context.Model);
        }

        #endregion
    }
}
