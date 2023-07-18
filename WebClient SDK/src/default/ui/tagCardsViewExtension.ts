import { ApplicationExtension } from 'tessa/applicationExtension';
import { ViewComponentRegistry } from 'tessa/ui/views/componentRegistry';
import { IBaseContentItem } from 'tessa/ui/views/content/baseContentItem';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions/workplaceViewComponentExtension';
import { StandardViewComponentContentItemFactory } from 'tessa/ui/views/standardViewComponentContentItemFactory';
import { IWorkplaceViewComponent } from 'tessa/ui/views/workplaceViewComponent';
import { FullSizeTextBlockView, FullSizeTextBlockViewModel } from './fullSizeTextBlockViewModel';

/**
 * Расширение для вывода надписи "Выберите тег в дереве слева", вместо таблицы, при открытии представления "TagCards".
 */
export class TagCardsViewExtension extends WorkplaceViewComponentExtension {
  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.TagCardsViewExtension';
  }

  public initialize(model: IWorkplaceViewComponent): void {
    const tagParam = 'Tag';
    const tableFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.Table
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.Table, c =>
      c.parameters.parameters.find(x => x.name === tagParam)
        ? tableFactory(c)
        : new FullSizeTextBlockViewModel(c, '$Tags_SelectTag_Text')
    );
    const refreshButtonFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.RefreshButton
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.RefreshButton, c =>
      c.parameters.parameters.find(x => x.name === tagParam) ? refreshButtonFactory(c) : null
    );
    const filteringBlockFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.FilteringBlock
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.FilteringBlock, c =>
      c.parameters.parameters.find(x => x.name === tagParam) ? filteringBlockFactory(c) : null
    );
    const multiSelectFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.MultiSelect
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.MultiSelect, c =>
      c.parameters.parameters.find(x => x.name === tagParam) ? multiSelectFactory(c) : null
    );
    const pagingFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.Paging
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.Paging, c =>
      c.parameters.parameters.find(x => x.name === tagParam) ? pagingFactory(c) : null
    );
    const sortButtonFactory = model.contentFactories.get(
      StandardViewComponentContentItemFactory.SortButton
    ) as (c: IWorkplaceViewComponent) => IBaseContentItem;
    model.contentFactories.set(StandardViewComponentContentItemFactory.SortButton, c =>
      c.parameters.parameters.find(x => x.name === tagParam) ? sortButtonFactory(c) : null
    );
  }
}

export class TagCardsViewInitializeExtension extends ApplicationExtension {
  public initialize() {
    ViewComponentRegistry.instance.register(FullSizeTextBlockViewModel, () => {
      return FullSizeTextBlockView;
    });
  }
}
