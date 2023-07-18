import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
/**
 * Предупреждение, добавляемое в случае, если при сохранении карточки версия карточки поменялась,
 * но после этого возникли любые ошибки. Тогда карточка не будет загружена после сохранения,
 * а пользователю надо сказать, что сейчас у него открыта старая версия карточки,
 * и возможны конфликты сохранения.
 */
export declare class WarnWhenErrorAfterVersionIncrementedStoreExtension extends CardStoreExtension {
    afterRequest(context: ICardStoreExtensionContext): void;
}
