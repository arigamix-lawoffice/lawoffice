import { IBlockViewModel } from 'tessa/ui/cards';
import { Visibility } from 'tessa/platform';

export class WfUiHelper {
  public static setControlVisibility(block: IBlockViewModel, suffix: string, isVisible: boolean) {
    for (let control of block.controls) {
      const controlName = control.name;
      if (controlName && controlName.toLocaleLowerCase().endsWith(suffix.toLocaleLowerCase())) {
        const controlIsVisible = control.controlVisibility === Visibility.Visible;
        if (controlIsVisible !== isVisible) {
          control.controlVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
      }
    }
  }
}
