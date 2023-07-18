import { TestCardTypeID } from '../common';
import { Guid } from 'tessa/platform';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { IFileControlManager } from 'tessa/ui/files';
import { ExamplePreviewerViewModel } from './30_examplePreviewerViewModel';
import { FilePreviewViewModel } from 'tessa/ui/cards/controls';

/**
 * Позволяет создавать кастомный превьюер и использовать его для определенного
 * типа данных в выбранной карточке.
 *
 * Результат работы расширения:
 * Для тестовой карточки "Автомобиль" создает кастомный превьюер и отображает его
 * для типа данных с расширением ".txt".
 */
export class ExamplePreviewerCardUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // устанавливаем кастомный превьюер для основного окна превью карточки
    ExamplePreviewerCardUIExtension.setExamplePreviewerFactory(context.model.previewManager);

    // устанавливаем кастомный превьюер для всех контролов "Предпросмотр" в карточке
    for (const control of context.model.controls.values()) {
      if (control instanceof FilePreviewViewModel) {
        if (control.fileControlManager !== context.model.previewManager) {
          ExamplePreviewerCardUIExtension.setExamplePreviewerFactory(control.fileControlManager);
        }
      }
    }
  }

  private static setExamplePreviewerFactory(manager: IFileControlManager): void {
    const existingFactory = manager.previewToolFactory;
    manager.previewToolFactory = version => {
      // добавляем условие отображения кастомного превьюера:
      // в данном случае, отображаем кастомный превьюер для файлов с расширением .txt
      if (version.getExtension() === 'txt') {
        return {
          type: ExamplePreviewerViewModel.type,
          createViewModelFunc: () => new ExamplePreviewerViewModel()
        };
      }

      // в противоположном случае, вызываем уже существующую логику
      return existingFactory(version);
    };
  }
}
