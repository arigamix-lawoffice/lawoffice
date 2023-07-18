import { ILoginExtensionContext, LoginExtension } from 'tessa/ui/login';
/**
 * Позволяет добавлять дополнительные параметры и элементы управления в форму логинизации.
 *
 * Результат работы расширения:
 * В форму логинизации добавлен параметр безопасности в виде капчи и тестовая кнопка, при нажатии на которую
 * появляется сообщение в диалоговом окне.
 */
export declare class ExampleLoginExtension extends LoginExtension {
    initializing(context: ILoginExtensionContext): void;
    initialized(context: ILoginExtensionContext): void;
}
