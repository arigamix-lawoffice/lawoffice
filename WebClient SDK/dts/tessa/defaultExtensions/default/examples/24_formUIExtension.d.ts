import { FormUIExtension } from 'tessa/ui';
import { IFormUIExtensionContext } from 'tessa/ui';
/**
 * Добавляет дополнительные элементы управления
 * и позволяет управлять соответствующими настройками форм диалога.
 *
 * Результат работы расширения:
 * В форму диалога (например, диалоговое окно загрузки Deski) добавляет тестовую кнопку,
 * при нажатии на которую отображается сообщение в диалоговом окне о нажатии кнопки.
 * Также, при закрытии формы диалога, появляется диалоговое окно с сообщением о закрытии формы.
 */
export declare class ExampleFormUIExtension extends FormUIExtension {
    initialized(context: IFormUIExtensionContext): void;
    shouldExecute(_context: IFormUIExtensionContext): boolean;
}
