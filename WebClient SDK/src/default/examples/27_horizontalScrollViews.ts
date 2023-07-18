import {
  IWorkplaceViewComponent,
  StandardViewComponentContentItemFactory,
  WorkplaceViewComponentInitializationStrategy
} from 'tessa/ui/views';
import { TableGridViewModelBase } from 'tessa/ui/views/content';
import { DataNodeMetadataSealed } from 'tessa/views/workplaces';

/**
 * Данное расширение применимо для всех представлений. Позволяет активировать горизонтальный скролл вместо
 * добавления overflow-строк при уменьшении ширины представления.
 */

const initializeContentBase =
  WorkplaceViewComponentInitializationStrategy.prototype.initializeContent;
WorkplaceViewComponentInitializationStrategy.prototype.initializeContent =
  function initializeContent(component: IWorkplaceViewComponent, metadata: DataNodeMetadataSealed) {
    initializeContentBase(component, metadata);
    // пытаемся получить фабрику таблиц
    const tableFactory = component.contentFactories.get(
      StandardViewComponentContentItemFactory.Table
    );
    if (!tableFactory) {
      return;
    }

    // устанавливаем сгенерированную таблицу во вью компонент
    component.contentFactories.set(StandardViewComponentContentItemFactory.Table, c => {
      const vm = tableFactory(c);
      if (vm instanceof TableGridViewModelBase) {
        // активируем горизонтальный скролл
        vm.horizontalScroll = true;
      }
      return vm;
    });
  };
