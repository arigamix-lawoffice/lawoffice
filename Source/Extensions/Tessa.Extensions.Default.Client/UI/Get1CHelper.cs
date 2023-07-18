using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Platform.Client.Tiles;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Forms;
using Tessa.UI.Files;
using Tessa.UI.Notifications;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.UI
{
    public static class Get1CHelper
    {
        public static async Task RequestAndAddFileAsync(
            IAdvancedCardDialogManager cardDialogManager,
            ICardRepository cardRepository,
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            INotificationUIManager notificationUIManager)
        {
            ICardEditorModel mainEditor = UIContext.Current.CardEditor;
            if (mainEditor is null)
            {
                return;
            }

            ICardModel mainModel = mainEditor.CardModel;
            if (mainModel is null)
            {
                return;
            }

            (_, ICardModel model) = await createDialogFormFuncAsync("Car1CDialog", formCreationOptions: FormCreationOptions.AlwaysCreateTabbedForm);

            await cardDialogManager.ShowCardAsync(
                model,
                prepareEditorActionAsync: (editor, ct) =>
                {
                    editor.StatusBarIsVisible = false;

                    editor.Toolbar.Actions.AddRange(
                        new CardToolbarAction(
                            "Ok",
                            "$UI_Common_OK",
                            editor.Toolbar.CreateIcon("Int426"),
                            new DelegateCommand(
                                async _ =>
                                {
                                    var request = new CardRequest { RequestType = DefaultRequestTypes.GetFake1C };
                                    request.DynamicInfo.Name = model.Card.DynamicEntries.TEST_CarMainInfoDialog.Name;
                                    request.DynamicInfo.Driver = model.Card.DynamicEntries.TEST_CarMainInfoDialog.DriverName;

                                    // показываем сплэш и задаём блокирующую операцию для карточки:
                                    // пользователь не сможет закрыть вкладку или отрефрешить карточку, пока она не закончится

                                    CardResponse response;
                                    using (TessaSplash.Create("$KrTest_Splash_LoadingFrom1C"))
                                    using (editor.SetOperationInProgress(blocking: true))
                                    {
                                        await Task.Delay(2000, ct); // для примера добавим задержки
                                        response = await cardRepository.RequestAsync(request, ct);
                                    }

                                    ValidationResult result = response.ValidationResult.Build();
                                    await TessaDialog.ShowNotEmptyAsync(result);

                                    if (!result.IsSuccessful)
                                    {
                                        return;
                                    }

                                    string content = response.Info.Get<string>("Xml");

                                    const string fileName = "1C.xml";

                                    IFile file = mainModel.FileContainer.Files.FirstOrDefault(x => x.Name == fileName);
                                    if (file != null)
                                    {
                                        await mainModel.FileControlManager.ResetIfInPreviewAsync(file, cancellationToken: ct);
                                        await file.ReplaceTextAsync(content, cancellationToken: ct);
                                    }
                                    else
                                    {
                                        await mainModel.FileContainer
                                            .BuildFile(fileName)
                                            .SetContentText(content, isLocal: true)
                                            .AddWithNotificationAsync(cancellationToken: ct);
                                    }

                                    await notificationUIManager.ShowTextOrMessageBoxAsync("$KrTest_LoadedFrom1C");
                                    await editor.CloseAsync(cancellationToken: ct);
                                }),
                            tooltip: TileHelper.GetToolTip("$KrTiles_SaveAndClose_Tooltip",
                                TileKeys.SaveAndCloseCard),
                            order: -2,
                            gestures: new[] { TileKeys.SaveAndCloseCard }),
                        new CardToolbarAction(
                            TileNames.Cancel,
                            "$UI_Common_Cancel",
                            editor.Toolbar.CreateIcon("Int626"),
                            new DelegateCommand(async _ => await editor.CloseAsync(cancellationToken: ct)),
                            tooltip: "$UI_Common_Cancel",
                            order: 40)
                    );

                    editor.Context.SetDialogClosingAction((dialogContext, args) => Task.FromResult(false));
                    
                    return ValueTask.FromResult(true);
                },
                options: new ShowCardOptions
                {
                    DisplayValue = "$CardTypes_Tabs_Dialog1C",
                    WithTabControlBackground = true
                }
            );
        }
    }
}
