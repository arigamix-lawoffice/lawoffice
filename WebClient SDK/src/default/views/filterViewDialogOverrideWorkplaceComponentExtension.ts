import { FilteringBlockViewModel, QuickSearchViewModel } from 'tessa/ui/views/content';
import { IWorkplaceViewComponent, StandardViewComponentContentItemFactory } from 'tessa/ui/views';

import { AdvancedFilterViewDialogManager } from './advancedFilterViewDialogManager';
import { CustomFilterButtonViewModel } from './customFilterButtonViewModel';
import { FilterViewDialogDescriptorRegistry } from './filterViewDialogDescriptorRegistry';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';

/**
 * Расширение, переопределяющее диалог фильтрации представления.
 */
export class FilterViewDialogOverrideWorkplaceComponentExtension extends WorkplaceViewComponentExtension {
  //#region base overrides

  getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.FilterViewDialogOverrideWorkplaceComponentExtension';
  }

  initialize(model: IWorkplaceViewComponent): void {
    const descriptor = FilterViewDialogDescriptorRegistry.instance.tryGet(model.id);

    if (!descriptor) {
      return;
    }

    model.contentFactories.set(
      StandardViewComponentContentItemFactory.FilteringBlock,
      c =>
        new FilteringBlockViewModel(
          c,
          new QuickSearchViewModel(c),
          new CustomFilterButtonViewModel(
            async cc =>
              await AdvancedFilterViewDialogManager.instance.open(descriptor, cc.parameters),
            c
          )
        )
    );
  }

  //#endregion
}
