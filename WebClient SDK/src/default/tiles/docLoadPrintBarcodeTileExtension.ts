import {
  TileExtension,
  ITileGlobalExtensionContext,
  Tile,
  TileGroups,
  ITileLocalExtensionContext,
  TileHotkey,
  TileEvaluationEventArgs
} from 'tessa/ui/tiles';
import { UIContext, LoadingOverlay, showNotEmpty, showConfirmWithCancel } from 'tessa/ui';
import { Guid, DotNetType, createTypedField } from 'tessa/platform';
import { CardSingletonCache, CardStoreMode } from 'tessa/cards';
import { ICardModel } from 'tessa/ui/cards';
import {
  CardGetFileContentRequest,
  CardGetFileContentResponse,
  CardService
} from 'tessa/cards/service';
import { LocalizationManager } from 'tessa/localization';

const Name = 'PrintBarcode';

export class DocLoadPrintBarcodeTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const contextSource = panel.contextSource;

    // const cardOthers = panel.tryGetTile('CardOthers');
    // if (!cardOthers) {
    //   return;
    // }

    const tile = new Tile({
      name: Name,
      caption: '$CardTypes_Controls_DocLoad_PrintBarcode',
      icon: 'ta icon-thin-181',
      contextSource,
      command: this.selectPrinterAndPrintAsync.bind(this),
      group: TileGroups.Cards,
      order: 1000,
      evaluating: this.evaluatingPrintBarcode,
      toolTip: '$CardTypes_Controls_DocLoad_PrintBarcode'
    });

    panel.tiles.push(tile);
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const leftPanel = context.workspace.leftPanel;

    const tile = leftPanel.tryGetTile(Name);
    if (!tile) {
      return;
    }

    const hotkeyStorage = leftPanel.contextSource.hotkeyStorage;
    hotkeyStorage.addTileHotkey(new TileHotkey(tile, 'Alt+P', 'KeyP', { alt: true }));
  }

  private evaluatingPrintBarcode(e: TileEvaluationEventArgs) {
    const editor = e.currentTile.context.cardEditor;
    let model: ICardModel | null;

    const settingsCard = CardSingletonCache.instance.cards.get('DocLoad');
    if (!settingsCard) {
      return;
    }
    const fields = settingsCard.sections.tryGet('DocLoadSettings')!.fields;
    const isEnabled = fields.tryGet('IsEnabled');
    const tableName = fields.tryGet('DefaultBarcodeTableName');
    const fieldName = fields.tryGet('DefaultBarcodeFieldName');

    e.setIsEnabledWithCollapsing(
      e.currentTile,
      isEnabled &&
        !!editor &&
        !!(model = editor.cardModel) &&
        model.card.storeMode === CardStoreMode.Update &&
        model.card.sections.has(tableName) &&
        model.card.sections.tryGet(tableName)!.fields.has(fieldName)
    );
  }

  private async selectPrinterAndPrintAsync() {
    const context = UIContext.current;
    const editor = context.cardEditor;
    const model = editor && editor.cardModel;
    if (model == null) {
      return;
    }

    const settingsCard = CardSingletonCache.instance.cards.get('DocLoad');
    if (!settingsCard) {
      return;
    }

    const card = model.card;

    const barcodeBytes = await this.downloadBarcode(card.id);
    if (barcodeBytes == null) {
      return;
    }

    const imageUrl = URL.createObjectURL(barcodeBytes);

    const fields = settingsCard.sections.tryGet('DocLoadSettings')!.fields;
    const showHeader = fields.tryGet('ShowHeader');
    const offsetWidth = fields.tryGet('OffsetWidth');
    const offsetHeight = fields.tryGet('OffsetHeight');

    this.printImage(imageUrl, model.digest, showHeader, offsetHeight, offsetWidth);
  }

  private async downloadBarcode(cardId: string) {
    const context = UIContext.current;
    const editor = context.cardEditor;
    const model = editor && editor.cardModel;

    if (model == null) {
      return null;
    }

    const fileName = 'Barcode.bmp';

    const request = new CardGetFileContentRequest();
    request.cardId = cardId;
    request.fileId = Guid.empty;
    request.fileName = fileName;
    request.versionRowId = Guid.empty;
    request.fileTypeName = 'Barcode';
    if (model.digest) {
      request.info['.digest'] = createTypedField(model.digest, DotNetType.String);
    }

    let response!: CardGetFileContentResponse;
    await LoadingOverlay.instance.show(async () => {
      response = await CardService.instance.getFileContent(request);
    });

    const validationResult = response.validationResult.build();
    await showNotEmpty(validationResult);
    if (!validationResult.isSuccessful || !response.content) {
      return;
    }

    const content = response.content;

    // TODO: временный коммент: пока вместе с файлом не приходит инфа
    // if (response.info['RefreshCard']) {
    if (model.card.storeMode === CardStoreMode.Insert || (await model.hasChanges())) {
      if (!(await showConfirmWithCancel('$UI_Common_ConfirmSave'))) {
        return null;
      }
    }

    if (!(await (editor && editor.saveCard(context)))) {
      return null;
    }
    // }

    return response.hasContent ? content : null;
  }

  printImage(
    imagePath: string,
    digest: string,
    showHeader: boolean,
    offsetHeight: number = 0,
    offsetWidth: number = 0
  ) {
    const width = window.innerWidth;
    const height = window.innerHeight;
    const header =
      (showHeader &&
        `<div style="font-size: 27px;position: absolute;left:270px;top:135px">${LocalizationManager.instance.localize(
          '$CardTypes_TypesNames_DocLoad'
        )}</div><div style="font-size: 20px;position: absolute;left:270px;top:165px">${LocalizationManager.instance.format(
          '$CardTypes_Controls_DocLoad_DocNumber',
          digest
        )}</div><img src="${launcherIcon64}" style="position: absolute;left:50px;top:50px" />`) ||
      '';

    const content = `<!DOCTYPE html><html><body onload="window.focus(); window.print(); window.close();">${header}<img src="${imagePath}" style="position: absolute;left:${250 +
      offsetWidth}px;top:${250 + offsetHeight}px"/></body></html>`;
    const options =
      'toolbar=no,location=no,directories=no,menubar=no,scrollbars=yes,width=' +
      width +
      ',height=' +
      height;
    const printWindow = window.open('', 'print', options);
    if (!printWindow) {
      return;
    }
    printWindow.document.open();
    printWindow.document.write(content);
    printWindow.document.close();
    printWindow.focus();
  }
}

const launcherIcon64 =
  'data:image/png;base64,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';
