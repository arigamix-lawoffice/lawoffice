using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Operations;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Controls.AutoComplete;
using Tessa.UI.Notifications;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class CalendarUIExtension :
        CardUIExtension
    {
        #region Fields

        private readonly IBusinessCalendarService businessCalendarService;

        private readonly IOperationRepository operationRepository;

        private readonly INotificationUIManager notificationUIManager;

        private readonly IAdvancedCardDialogManager dialogManager;

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public CalendarUIExtension(
            IBusinessCalendarService businessCalendarService,
            IOperationRepository operationRepository,
            INotificationUIManager notificationUIManager,
            IAdvancedCardDialogManager dialogManager,
            ICardRepository cardRepository)
        {
            this.businessCalendarService = businessCalendarService;
            this.operationRepository = operationRepository;
            this.notificationUIManager = notificationUIManager;
            this.dialogManager = dialogManager;
            this.cardRepository = cardRepository;
        }

        #endregion

        #region Command Actions

        private async void ValidateCalendarButtonActionAsync(object parameter)
        {
            var context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;
            var cardID = editor.CardModel.Card.ID;

            var calendarID = editor.CardModel.Card.Sections[BusinessCalendarHelper.CalendarSettingsSection]
                .RawFields.Get<int?>("CalendarID");
            if (!calendarID.HasValue)
            {
                await TessaDialog.ShowErrorAsync("$UI_Common_Messages_CantValidateCalendarWithEmptyID");
                return;
            }

            ValidationResult result = await this.businessCalendarService.ValidateCalendarAsync(cardID);
            TessaDialog.ShowNotEmpty(result, "$UI_BusinessCalendar_ValidatedTitle");
        }

        private async void RebuildCalendarButtonActionAsync(object parameter)
        {
            var context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;

            if (editor != null && !editor.OperationInProgress)
            {
                var calendarID = editor.CardModel.Card.Sections[BusinessCalendarHelper.CalendarSettingsSection]
                    .RawFields.Get<int?>("CalendarID");
                if (!calendarID.HasValue)
                {
                    await TessaDialog.ShowErrorAsync("$UI_Common_Messages_CantCalcCalendarWithEmptyID");
                    return;
                }

                var cardID = editor.CardModel.Card.ID;
                string cardName = null;
                if (editor.CardModel.Card.Sections["CalendarSettings"].RawFields.TryGetValue("Name", out object cardNameObj))
                {
                    cardName = (string)cardNameObj;
                }
                
                var isAlive = await this.operationRepository.IsAliveAsync(OperationTypes.CalendarRebuild).ConfigureAwait(false);
                if (isAlive)
                {
                    await TessaDialog.ShowErrorAsync("$UI_Common_Messages_CalendarRebuildOperationAlreadyRunning");
                    return;
                }

                using (editor.SetOperationInProgress())
                {
                    var storeRequest = new CardStoreRequest
                    {
                        Card = editor.CardModel.Card.Clone(),
                        Info = { [BusinessCalendarHelper.RebuildMarkKey] = BooleanBoxes.True }
                    };

                    var storeResponse = await this.cardRepository.StoreAsync(storeRequest).ConfigureAwait(false);
                    var storeResult = storeResponse.ValidationResult.Build();

                    Guid? operationID;
                    if (storeResult.IsSuccessful
                        && (operationID = storeResponse.Info.TryGet<Guid?>(BusinessCalendarHelper.RebuildOperationIDKey)).HasValue)
                    {
                        using (TessaSplash.Create(TessaSplashMessage.CalculatingCalendar))
                        {
                            do
                            {
                                await Task.Delay(500).ConfigureAwait(false);
                            }
                            while (await this.operationRepository.IsAliveAsync(operationID.Value).ConfigureAwait(false));
                        }
                    }

                    await TessaDialog.ShowNotEmptyAsync(storeResult);
                }

                await context.CardEditor.RefreshCardAsync(context);
                await this.notificationUIManager.ShowTextOrMessageBoxAsync("$UI_BusinessCalendar_CalendarIsRebuiltNotification").ConfigureAwait(false);
            }
        }

        #endregion

        #region Private Methods

        private static void AttachCommandToButton(ICardUIExtensionContext context, string buttonAlias, Action<object> action)
        {
            if (!context.Model.Controls.TryGet(buttonAlias, out IControlViewModel control))
            {
                return;
            }

            var button = (ButtonViewModel)control;
            if (!context.Model.Card.Permissions.Resolver.GetCardPermissions().Has(CardPermissionFlags.AllowModify))
            {
                button.IsReadOnly = true;
                return;
            }

            button.CommandClosure.Execute = action;
        }

        private async Task OpenCalendarTypeInDialogAsync(ICardUIExtensionContext context, CancellationToken cancellationToken)
        {
            Guid? calendarTypeID;
            if (!context.Card.Sections.TryGetValue("CalendarSettings", out var settingsSection) ||
                !settingsSection.RawFields.TryGetValue("CalendarTypeID", out var calendarTypeIDObject) ||
                (calendarTypeID = calendarTypeIDObject as Guid?) is null)
            {
                return;
            }

            var calendarTypeName = settingsSection.RawFields.Get<string>("CalendarTypeCaption");

            using ISplash splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);
            await this.dialogManager.OpenCardAsync(
                calendarTypeID, 
                null,
                options: new OpenCardOptions
                {
                    DisplayValue = calendarTypeName,
                    UIContext = context.UIContext,
                    Splash = splash,
                    CardEditorModifierActionAsync = async openingContext=>
                    {
                        var uiContext = UIContext.Current;
                        
                        uiContext.CardEditor.Closed += async (sender, args) =>
                        {
                            var typeCard = uiContext.CardEditor.CardModel.Card;
                            if (uiContext.CardEditor.IsUpdatedServer)
                            {
                                var typeSection = typeCard.Sections["CalendarTypes"];
                                
                                var task = 
                                    DispatcherHelper.InvokeInUIAsync(async () =>
                                    {
                                        settingsSection.Fields["CalendarTypeCaption"] = typeSection.RawFields["Caption"];
                                        settingsSection.Fields["CalendarTypeID"] = calendarTypeID;
                                    });
                                await task;
                            }
                        };
                    }
                }, 
                cancellationToken: cancellationToken);
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            AttachCommandToButton(context, "ValidateCalendar", this.ValidateCalendarButtonActionAsync);
            AttachCommandToButton(context, "RebuildCalendar", this.RebuildCalendarButtonActionAsync);

            if (!context.Model.Controls.TryGet("CalendarType", out IControlViewModel control))
            {
                return;
            }

            if (control is AutoCompleteEntryViewModel calendarTypeControl)
            {
                calendarTypeControl.ChangeFieldCommandClosure.Execute = async (o) =>
                    await this.OpenCalendarTypeInDialogAsync(context, context.CancellationToken);
            }

            // Для всех новых записей в именованных интервалах, которые добавляются с клиента - ставим, что они добавлены вручную. 
            if (!context.Model.Controls.TryGet("NamedRanges", out IControlViewModel namedRangesControl))
            {
                return;
            }

            ((GridViewModel)namedRangesControl).RowInvoked += 
                (sender, args) =>
                {
                    if (args.Action == GridRowAction.Inserted)
                    {
                        args.Row.Fields["IsManual"] = BooleanBoxes.True;
                    }
                };
        }

        #endregion
    }
}
