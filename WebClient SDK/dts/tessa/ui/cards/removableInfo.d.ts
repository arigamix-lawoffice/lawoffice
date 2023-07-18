import { ICardEditorModel } from 'tessa/ui/cards/interfaces';
import { CreateCardArg } from 'tessa/ui/uiHost/common';
/**
 * Предоставляет информацию, которая не должна передаваться на сервер.
 */
export declare class RemovableInfo {
    /**
     * Параметры создания карточки по шаблону или значение null, если используются параметры по умолчанию.
     *
     * Игнорируется значение свойства CreateCardArg.alwaysNewTab.
     */
    args: CreateCardArg | null;
    /**
     * {@link ICardEditorModel} карточки, созданной по шаблону, или null, если произошла ошибка.
     */
    editor: ICardEditorModel | null;
}
/**
 * Ключ в CardNewResponse.info, который содержит информацию для создания по шаблону ({@link RemovableInfo}).
 */
export declare const removableCreateFromTemplateKey: string;
