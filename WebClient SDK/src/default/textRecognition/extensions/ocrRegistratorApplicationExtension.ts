import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { CardControlTypeRegistry } from 'tessa/cards';
import { ComponentRegistry, ControlTypeRegistry } from 'tessa/ui/cards';
import { OcrInput } from '../components/input/ocrInput';
import { OcrInputControlType, OcrInputType } from '../components/input/ocrInputType';
import { OcrSlider } from '../components/slider/ocrSlider';
import { OcrSliderControlType, OcrSliderType } from '../components/slider/ocrSliderType';

/** Расширение для регистрации контролов по решению OCR в приложении. */
export class OcrRegistratorApplicationExtension extends ApplicationExtension {
  public initialize(_context: IApplicationExtensionContext): void {
    CardControlTypeRegistry.instance.register(OcrSliderControlType);
    ControlTypeRegistry.instance.register(OcrSliderControlType, () => new OcrSliderType());
    ComponentRegistry.instance.register(OcrSliderControlType.id, (_: unknown) => OcrSlider);

    CardControlTypeRegistry.instance.register(OcrInputControlType);
    ControlTypeRegistry.instance.register(OcrInputControlType, () => new OcrInputType());
    ComponentRegistry.instance.register(OcrInputControlType.id, (_: unknown) => OcrInput);
  }
}
