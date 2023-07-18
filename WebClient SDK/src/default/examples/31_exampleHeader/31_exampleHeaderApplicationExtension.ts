import { ApplicationExtension } from 'tessa/applicationExtension';
import { IApplicationExtensionContext } from 'tessa/applicationExtensionContext';
import { ComponentRegistry } from 'tessa/ui/cards';
import { ExampleHeader } from './31_exampleHeader';
import { ExampleHeaderViewModel } from './31_exampleHeaderViewModel';

export class ExampleHeaderApplicationExtension extends ApplicationExtension {
  // регистрируем компонент для хэдера и соответствующую ему вью-модель
  public initialize(_context: IApplicationExtensionContext): void {
    ComponentRegistry.instance.register(
      ExampleHeaderViewModel.type,
      (_viewModel: ExampleHeaderViewModel) => {
        return ExampleHeader;
      }
    );
  }
}
