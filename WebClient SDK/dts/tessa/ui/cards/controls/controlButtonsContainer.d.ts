import { UIButton } from 'tessa/ui/uiButton';
import { ButtonsContainerType } from './buttonsContainerType';
export declare class ControlButtonsContainer {
    constructor();
    protected _defaultButtons: UIButton[];
    protected _dialogButtons: UIButton[];
    getButtons(type?: ButtonsContainerType): UIButton[];
    addButton(btn: UIButton, type?: ButtonsContainerType): void;
    removeButton(btn: UIButton, type?: ButtonsContainerType): boolean;
    removeButtonByName(name: string, type?: ButtonsContainerType): boolean;
}
