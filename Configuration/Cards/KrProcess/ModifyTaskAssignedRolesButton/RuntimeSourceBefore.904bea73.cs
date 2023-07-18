#script
var context = (CardStoreExtensionContext)this.CardContext;
var mainCard = context.Request.Card;

var dialogCard = await this.GetNewCardAsync();
dialogCard.Sections["KrCardTasksEditorDialogVirtual"].Fields["MainCardID"] = mainCard.ID;
dialogCard.Sections["KrCardTasksEditorDialogVirtual"].Fields["KrToken"] = KrToken.TryGet(mainCard.Info).ToTypedJson();