import { CardMetadataExtension, ICardMetadataExtensionContext } from 'tessa/cards/extensions';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { OcrSliderControlType } from '../components/slider/ocrSliderType';
import { OcrRequestDialogTypeID } from '../misc/ocrConstants';

/**
 * Расширение на метаданные приложения, в котором происходит добавление
 * контрола ползунка в диалог создания запроса на распознавание файла.
 */
export class OcrRequestMetadataExtension extends CardMetadataExtension {
  initializing(context: ICardMetadataExtensionContext): void {
    const cardType = context.cardMetadata.getCardTypeById(OcrRequestDialogTypeID);
    if (!cardType) {
      return;
    }

    const block = cardType.forms[0]?.blocks[0];
    if (!block) {
      return;
    }

    const control = block.controls.find(c => c.name === 'Confidence');
    if (!control) {
      return;
    }

    const sources = control.getSourceInfo();

    const sliderType = new CardTypeEntryControl();
    sliderType.type = OcrSliderControlType;
    sliderType.name = control.name;
    sliderType.caption = control.caption;
    sliderType.toolTip = control.toolTip;
    sliderType.blockSettings = control.blockSettings;
    sliderType.controlSettings = control.controlSettings;
    sliderType.controlSettings['Step'] = 0.1;
    sliderType.sectionId = sources.sectionId;
    sliderType.physicalColumnIdList = sources.columnIds;

    block.controls.splice(1, 0, sliderType);
  }
}
