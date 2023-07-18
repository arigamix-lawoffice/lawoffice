import { SliderControlType } from './29_sliderControlType';
import { SliderType } from './29_sliderType';
import { SliderControl } from './29_sliderControl';
import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { CardControlTypeRegistry } from 'tessa/cards';
import { ControlTypeRegistry, ComponentRegistry, IControlViewModel } from 'tessa/ui/cards';

export class SliderControlApplicationExtension extends ApplicationExtension {
  public initialize(_context: IApplicationExtensionContext): void {
    CardControlTypeRegistry.instance.register(SliderControlType);
    ControlTypeRegistry.instance.register(SliderControlType, () => new SliderType());
    ComponentRegistry.instance.register(SliderControlType.id, (_viewModel: IControlViewModel) => {
      return SliderControl;
    });
  }
}
