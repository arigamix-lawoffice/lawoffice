using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Formatting;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class CreateMultipleTemplateTileExtension :
        TileExtension
    {
        #region Constructors

        public CreateMultipleTemplateTileExtension(
            ICardRepository cardRepository,
            ICardManager cardManager,
            ICardFileManager cardFileManager,
            ICardDialogManager dialogManager,
            CreateCardModelFuncAsync createCardModelFuncAsync,
            ICardMetadata cardMetadata,
            ISession session,
            IUIHost uiHost,
            IViewService viewService)
        {
            this.cardRepository = cardRepository;
            this.cardManager = cardManager;
            this.cardFileManager = cardFileManager;
            this.dialogManager = dialogManager;
            this.createCardModelFuncAsync = createCardModelFuncAsync;
            this.cardMetadata = cardMetadata;
            this.session = session;
            this.uiHost = uiHost;
            this.viewService = viewService;
        }

        #endregion

        #region DirectoryEntry Private Class

        private sealed class DirectoryEntry :
            NamedEntry
        {
        }

        #endregion

        #region DirectoryIterator Private Class

        private sealed class DirectoryIterator
        {
            #region Constructors

            public DirectoryIterator(List<DirectoryEntry> entries)
            {
                if (entries == null)
                {
                    throw new ArgumentNullException("entries");
                }
                if (entries.Count == 0)
                {
                    throw new ArgumentOutOfRangeException("entries");
                }

                this.entries = entries;
            }

            #endregion

            #region Fields

            private readonly List<DirectoryEntry> entries;

            private int index;  // = 0

            #endregion

            #region Methods

            public DirectoryEntry GetNext()
            {
                var result = this.entries[this.index++];

                if (this.index == this.entries.Count)
                {
                    this.index = 0;
                }

                return result;
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        private readonly ICardManager cardManager;

        private readonly ICardFileManager cardFileManager;

        private readonly ICardDialogManager dialogManager;

        private readonly CreateCardModelFuncAsync createCardModelFuncAsync;

        private readonly ICardMetadata cardMetadata;

        private readonly ISession session;

        private readonly IUIHost uiHost;

        private readonly IViewService viewService;

        #endregion

        #region Private Constants

        private const string PartnersViewAlias = "Partners";

        private const string PartnersViewPrefix = "Partner";

        private const string UsersViewAlias = "Users";

        private const string UsersViewPrefix = "User";

        #endregion

        #region Private Methods

        private static bool TypeIsAllowedForMultipleCreation(CardType cardType)
        {
            return cardType.Flags.HasNot(CardTypeFlags.Hidden)
                   && cardType.Flags.HasNot(CardTypeFlags.Singleton);
        }

        private static void GetTypeInfoForMultipleCreation(
            CardType cardType,
            out bool hasPartner,
            out bool hasAuthor)
        {
            CardTypeSchemeItem documentCommonInfo = cardType.SchemeItems
                .FirstOrDefault(x => x.SectionID == DefaultSchemeHelper.DocumentCommonInfoSectionID);

            if (documentCommonInfo != null)
            {
                hasPartner = documentCommonInfo.ColumnIDList.Contains(DefaultSchemeHelper.PartnerComplexColumnID);
                hasAuthor = documentCommonInfo.ColumnIDList.Contains(DefaultSchemeHelper.AuthorComplexColumnID);
            }
            else
            {
                hasPartner = false;
                hasAuthor = false;
            }
        }

        private static bool CheckResponseAndSetID(CardNewResponse response)
        {
            response.Card.ID = Guid.NewGuid();
            return CheckResponse(response);
        }

        private static bool CheckResponse(CardResponseBase response)
        {
            ValidationResult responseResult = response.Validate();
            TessaDialog.ShowNotEmpty(responseResult);
            if (!responseResult.IsSuccessful)
            {
                return false;
            }

            ValidationResult result = response.ValidationResult.Build();
            TessaDialog.ShowNotEmpty(result);
            return result.IsSuccessful;
        }

        private static bool CheckResponseAndSetIDSilent(
            CardNewResponse response,
            IValidationResultBuilder validationResult)
        {
            if (response.Card.ID == Guid.Empty)
            {
                response.Card.ID = Guid.NewGuid();
            }
            
            return CheckResponseSilent(response, validationResult);
        }

        private static bool CheckResponseSilent(
            CardResponseBase response,
            IValidationResultBuilder validationResult)
        {
            ValidationResult responseResult = response.Validate();
            validationResult.Add(responseResult);
            if (!responseResult.IsSuccessful)
            {
                return false;
            }

            ValidationResult result = response.ValidationResult.Build();
            validationResult.Add(result);
            return result.IsSuccessful;
        }

        private async void CreateMultipleCardsActionAsync(object parameter)
        {
            ICardEditorModel editor = UIContext.Current.CardEditor;

            ICardModel modelInEditor;
            CardType cardInTemplateType;
            CardTypeNamedForm dialogForm;
            if (editor == null
                || (modelInEditor = editor.CardModel) == null
                || (cardInTemplateType = modelInEditor.TryGetCardInTemplateType()) == null
                || !(await this.cardMetadata.GetCardTypesAsync()).TryGetValue("Dialogs", out CardType dialogType)
                || (dialogForm = dialogType.Forms.FirstOrDefault(x => x.Name == "CreateMultipleCards")) == null)
            {
                return;
            }

            CardNewRequest request = new CardNewRequest { CardTypeID = dialogType.ID };
            CardNewResponse response = await this.cardRepository.NewAsync(request);
            if (!CheckResponseAndSetID(response))
            {
                return;
            }

            ICardModel model = await this.createCardModelFuncAsync(
                response.Card,
                response.SectionRows,
                this.dialogManager.ShowRowAsync);

            Dictionary<string, object> mainFields = model.Card.Sections["Dialogs"].RawFields;
            GetTypeInfoForMultipleCreation(cardInTemplateType, out bool hasPartner, out bool hasAuthor);

            await this.uiHost.ShowFormDialogAsync(
                LocalizationManager.Localize(dialogForm.TabCaption),
                dialogForm,
                model,
                async (form, ct) =>
                {
                    IBlockViewModel block = (form as IFormWithBlocksViewModel)?.Blocks.FirstOrDefault();
                    if (block != null)
                    {
                        IControlViewModel changePartnerControl = null;
                        if (!hasPartner && (changePartnerControl =
                            block.Controls.FirstOrDefault(x => x.Name == "ChangePartner")) != null)
                        {
                            changePartnerControl.ControlVisibility = Visibility.Collapsed;
                        }

                        IControlViewModel changeAuthorControl = null;
                        if (!hasAuthor && (changeAuthorControl =
                            block.Controls.FirstOrDefault(x => x.Name == "ChangeAuthor")) != null)
                        {
                            changeAuthorControl.ControlVisibility = Visibility.Collapsed;
                        }

                        if (changePartnerControl != null || changeAuthorControl != null)
                        {
                            ((IFormWithBlocksViewModel) form).Rearrange();
                        }
                    }
                },
                buttons: new[]
                {
                    new UIButton(
                        "$UI_Cards_CreateMultipleTemplate_CreateCards",
                        async btn =>
                        {
                            int? cardCount = mainFields.Get<int?>("CardCount");

                            if (cardCount > 0
                                && TessaDialog.Confirm(
                                    string.Format(
                                        LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_Confirm"),
                                        cardCount)))
                            {
                                bool changePartner = hasPartner && mainFields.Get<bool>("ChangePartner");
                                bool changeAuthor = hasAuthor && mainFields.Get<bool>("ChangeAuthor");

                                // карточка шаблона может быть асинхронно изменена, поэтому сначала клонируем её,
                                // а потом запускаем поток с операцией массового создания
                                Card templateCard = modelInEditor.Card.Clone();

                                var tuple = await this.TryCreateMultipleCardsAsync(cardCount.Value, changePartner, changeAuthor, templateCard);
                                await btn.CloseAsync();

                                if (tuple != null)
                                {
                                    TessaDialog.ShowNotEmpty(tuple.Item1);
                                    TessaDialog.ShowMessage(tuple.Item2);
                                }
                            }
                        },
                        isDefault: true,
                        isEnabledFunc: () => mainFields.Get<int?>("CardCount") > 0),
                    new UIButton("$UI_Common_Cancel", isCancel: true),
                });
        }

        private async Task<List<DirectoryEntry>> TryLoadDirectoryAsync(
            string viewAlias,
            string entryPrefix,
            CancellationToken cancellationToken = default)
        {
            ITessaView view = await this.viewService.GetByNameAsync(viewAlias, cancellationToken).ConfigureAwait(false);
            if (view == null)
            {
                TessaDialog.ShowError(string.Format(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_NoAccessToDictionary"), viewAlias));
                return null;
            }

            var viewMetadata = await view.GetMetadataAsync(cancellationToken);
            var viewRequest = new TessaViewRequest(viewMetadata) { CalculateRowCounting = false };

            var viewSpecialParameters = new ViewSpecialParameters(
                new ViewCurrentUserParameters(this.session),
                new ViewPagingParameters(),
                new ViewCardParameters());

            int currentPage = 1;
            int pageLimit = viewMetadata.ExportDataPageLimit;
            if (pageLimit <= 0)
            {
                pageLimit = ViewMetadata.DefaultExportDataPageLimit;
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            viewSpecialParameters.ProvideCurrentUserIdParameter(viewRequest.Values);
            viewSpecialParameters.ProvidePageLimitParameter(
                viewRequest.Values,
                Paging.Always,
                pageLimit,
                false);

            var result = new List<DirectoryEntry>();
            int entryIDIndex = -1;
            int entryNameIndex = -1;

            while (true)
            {
                viewSpecialParameters.ProvidePageOffsetParameter(
                    viewRequest.Values,
                    Paging.Always,
                    currentPage++,
                    pageLimit,
                    false);

                ITessaViewResult viewResult = await view.GetDataAsync(viewRequest, cancellationToken).ConfigureAwait(false);
                if (viewResult.Rows is null || viewResult.Rows.Count == 0)
                {
                    break;
                }

                if (entryIDIndex < 0)
                {
                    entryIDIndex = (viewResult.Columns ?? Array.Empty<object>())
                        .Cast<string>()
                        .IndexOf(entryPrefix + "ID", StringComparer.OrdinalIgnoreCase);

                    if (entryIDIndex < 0)
                    {
                        return null;
                    }
                }

                if (entryNameIndex < 0)
                {
                    entryNameIndex = (viewResult.Columns ?? Array.Empty<object>())
                        .Cast<string>()
                        .IndexOf(entryPrefix + "Name", StringComparer.OrdinalIgnoreCase);

                    if (entryNameIndex < 0)
                    {
                        return null;
                    }
                }

                foreach (object rowValue in viewResult.Rows)
                {
                    var row = (IList<object>)rowValue;

                    string name;
                    if (row.Count <= entryIDIndex
                        || !(row[entryIDIndex] is Guid)
                        || row.Count <= entryNameIndex
                        || (name = row[entryNameIndex] as string) == null)
                    {
                        return null;
                    }

                    Guid id = (Guid)row[entryIDIndex];
                    var entry = new DirectoryEntry { ID = id, Name = name };
                    result.Add(entry);
                }

                if (viewResult.Rows.Count < pageLimit)
                {
                    break;
                }
            }

            return result;
        }

        private async Task<Tuple<ValidationResult, string>> TryCreateMultipleCardsAsync(
            int cardCount,
            bool changePartner,
            bool changeAuthor,
            Card templateCard,
            CancellationToken cancellationToken = default)
        {
            int successCount = 0;
            TimeSpan elapsed;
            ValidationResult result;

            using (ISplash splash = TessaSplash.CreateLazy())
            {
                DirectoryIterator partnerIterator = null;
                if (changePartner)
                {
                    splash.Text = "$UI_Cards_CreateMultipleTemplate_LoadingPartnersSplash";

                    List<DirectoryEntry> partners = await this.TryLoadDirectoryAsync(PartnersViewAlias, PartnersViewPrefix, cancellationToken).ConfigureAwait(false);
                    if (partners != null && partners.Count > 0)
                    {
                        partnerIterator = new DirectoryIterator(partners);
                    }
                }

                DirectoryIterator userIterator = null;
                if (changeAuthor)
                {
                    splash.Text = "$UI_Cards_CreateMultipleTemplate_LoadingEmployeesSplash";

                    List<DirectoryEntry> users = await this.TryLoadDirectoryAsync(UsersViewAlias, UsersViewPrefix, cancellationToken).ConfigureAwait(false);
                    if (users != null)
                    {
                        int systemIndex = users.IndexOf(x => x.ID == Session.SystemID);
                        if (systemIndex >= 0)
                        {
                            users.RemoveAt(systemIndex);
                        }

                        if (users.Count > 0)
                        {
                            userIterator = new DirectoryIterator(users);
                        }
                    }
                }

                string splashTemplate = LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_CreateCardSplash");
                int prevPercentage = -1;

                IValidationResultBuilder validationResult = new ValidationResultBuilder();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                try
                {
                    for (int i = 0; i < cardCount; i++)
                    {
                        // i - количество уже созданных карточек, именно это количество надо отобразить пользователю
                        // поэтому выводим в сплэше i, а не i + 1
                        int percentage = (int)Math.Round(i * 100.0 / cardCount);
                        if (percentage != prevPercentage || (i % 100) == 0)
                        {
                            // изменяем текст при изменении процента или для каждой сотой записи
                            splash.Text = string.Format(splashTemplate, i, cardCount, percentage);
                            prevPercentage = percentage;
                        }

                        (_, CardNewResponse newResponse) = await CardHelper.CreateFromTemplateAsync(
                            templateCard, this.cardManager, cancellationToken: cancellationToken).ConfigureAwait(false);

                        Card card = newResponse.TryGetCard();
                        if (card != null)
                        {
                            // удаляем Permissions, которые могли быть неактуальными и вызывать предупреждения при валидации
                            card.Permissions = null;
                        }

                        if (!CheckResponseAndSetIDSilent(newResponse, validationResult)
                            || newResponse.CancelOpening)
                        {
                            // CancelOpening проверяем после того, как выведем сообщения об ошибках, если они были
                            break;
                        }

                        if ((partnerIterator != null || userIterator != null)
                            // ReSharper disable once PossibleNullReferenceException
                            // здесь card != null, т.к. он мог быть равен null только в случае ошибок запроса,
                            // тогда метод CheckResponseAndSetIDSilent вернул бы false
                            && card.Sections.TryGetValue("DocumentCommonInfo", out CardSection section))
                        {
                            Dictionary<string, object> fields = section.RawFields;

                            if (partnerIterator != null)
                            {
                                DirectoryEntry partner = partnerIterator.GetNext();
                                fields["PartnerID"] = partner.ID;
                                fields["PartnerName"] = partner.Name;
                            }

                            if (userIterator != null)
                            {
                                DirectoryEntry author = userIterator.GetNext();
                                fields["AuthorID"] = author.ID;
                                fields["AuthorName"] = author.Name;
                            }
                        }

                        CardStoreResponse response;
                        await using (ICardFileContainer container = await this.cardFileManager.CreateContainerAsync(card, cancellationToken: cancellationToken).ConfigureAwait(false))
                        {
                            response = await container.StoreAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                        }

                        if (!CheckResponseSilent(response, validationResult))
                        {
                            break;
                        }

                        successCount++;
                    }
                }
                finally
                {
                    stopwatch.Stop();
                }

                elapsed = stopwatch.Elapsed;
                result = validationResult.Build();
            }

            var finalMessage = new StringBuilder();

            finalMessage
                .Append(successCount == cardCount
                    ? string.Format(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_StatisticsSucceeded"), cardCount)
                    : string.Format(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_StatisticsFailed"), successCount, cardCount));

            if (elapsed > TimeSpan.Zero)
            {
                finalMessage
                    .AppendLine()
                    .AppendFormat(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_TimeElapsed"), FormattingHelper.FormatTime(elapsed));
            }

            TimeSpan elapsedPerCard = successCount > 0
                ? TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds / successCount)
                : TimeSpan.Zero;

            if (elapsedPerCard > TimeSpan.Zero)
            {
                finalMessage
                    .AppendLine()
                    .AppendFormat(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_AverageTimeToCreateCard"), elapsedPerCard.TotalMilliseconds);
            }

            return Tuple.Create(result, finalMessage.ToString());
        }

        #endregion

        #region Base Overrides

        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            if (this.session.User.AccessLevel != UserAccessLevel.Administrator)
            {
                return Task.CompletedTask;
            }

            ITilePanel panel = context.Workspace.LeftPanel;
            ICardEditorModel editor = panel.Context.CardEditor;
            ICardModel model;
            CardType cardInTemplateType;
            ITile cardTools;
            if (editor != null
                && (model = editor.CardModel) != null
                && model.CardType.ID == CardHelper.TemplateTypeID
                && (cardInTemplateType = model.TryGetCardInTemplateType()) != null
                && TypeIsAllowedForMultipleCreation(cardInTemplateType)
                && (cardTools = panel.Tiles.TryGet(TileNames.CardTools)) != null)
            {
                cardTools.Tiles.Add(
                    new Tile(
                        TileNames.CreateMultipleCards,
                        "$UI_Tiles_CreateMultipleCards",
                        context.Icons.Get("Thin64"),
                        panel,
                        new DelegateCommand(this.CreateMultipleCardsActionAsync),
                        TileGroups.Cards,
                        order: 100,
                        evaluating: (s, e) =>
                            e.SetIsEnabledWithCollapsing(
                                e.CurrentTile,
                                editor.CardModel != null
                                && !editor.CardModel.InSpecialMode()
                                && editor.CardModel.Card.StoreMode == CardStoreMode.Update)));
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
