import { ValidationResult } from 'tessa/platform/validation';
import { UIButton } from 'tessa/ui/uiButton';
export declare class UIErrorPresenterViewModel {
    constructor(close: () => void);
    private _buttons;
    readonly close: () => void;
    result: ValidationResult;
    get buttons(): UIButton[];
}
