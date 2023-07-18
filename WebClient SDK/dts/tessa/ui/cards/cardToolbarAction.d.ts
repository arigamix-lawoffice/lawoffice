import { CardToolbarItem, CardToolbarItemCommand } from './cardToolbarItem';
export declare class CardToolbarAction extends CardToolbarItem {
    constructor(args: {
        name: string;
        caption: string;
        icon: string;
        toolTip?: string;
        order?: number;
        command: CardToolbarItemCommand;
    });
}
