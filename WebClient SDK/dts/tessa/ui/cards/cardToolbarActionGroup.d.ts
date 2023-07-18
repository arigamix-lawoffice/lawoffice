import { CardToolbarItem } from './cardToolbarItem';
import { CardToolbarAction } from './cardToolbarAction';
import { ICardToolbarViewModel } from './cardToolbarViewModel';
import { KeyboardModifiers } from '../tiles';
export declare class CardToolbarActionGroup extends CardToolbarItem {
    constructor(args: {
        name: string;
        caption: string;
        icon: string;
        toolTip?: string;
        order?: number;
        innerItems: CardToolbarAction[];
    });
    private _actions;
    get actions(): ReadonlyArray<CardToolbarAction>;
    get hasChildren(): boolean;
    attach(toolbar: ICardToolbarViewModel): void;
    addAction(action: CardToolbarAction): void;
    removeAction(action: CardToolbarAction): void;
    addActionIfNotExists(action: CardToolbarAction, hotkey?: {
        name: string;
        key: string;
        modifiers?: KeyboardModifiers;
    }): void;
    removeActionIfExists(name: string): void;
    clearActions(): void;
}
