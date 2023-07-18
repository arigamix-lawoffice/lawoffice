import { CardHelpMode } from '../cardHelpMode';
export declare class DefaultFormSettings {
    constructor(params?: {
        blockInterval?: number;
        horizontalInterval?: number;
        filePreviewIsDisabled?: boolean;
        helpMode?: CardHelpMode;
        helpValue?: string;
        gridColumns?: (number | null)[];
        gridRows?: (number | null)[];
    });
    blockInterval: number;
    horizontalInterval: number;
    filePreviewIsDisabled: boolean;
    helpMode: CardHelpMode;
    helpValue: string;
    gridColumns: (number | null)[];
    gridRows: (number | null)[];
}
