import { ICardEditorModel } from './interfaces';
import { CardHelpMode } from './cardHelpMode';
import { Card } from 'tessa/cards';
export declare function onWorkspaceClosing(editor: ICardEditorModel, e: {
    cancel: boolean;
}, activateAction?: () => Promise<void>): Promise<boolean>;
export declare const handleHelpSection: (helpMode: CardHelpMode, helpValue: string) => void | Promise<void>;
/**
 * Проверяет, можно ли показывать плитку с заданным именем в зависимости от данных карточки.
 * @param card карточка
 * @param tileName имя плитки
 * @returns истина - можно показывать плитку, ложь - нельзя
 */
export declare function tileIsVisible(card: Card | null | undefined, tileName: string): boolean;
