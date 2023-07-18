import { ControlButtonsContainer } from 'tessa/ui/cards/controls/controlButtonsContainer';
import { ViewParameterMetadataSealed } from 'tessa/views/metadata';
import { CriteriaValue } from 'tessa/views/metadata/criteriaValue';
import { IFilterDialogEditorViewModel } from './common';
export declare class FilterDialogDecimalViewModel implements IFilterDialogEditorViewModel {
    constructor(meta: ViewParameterMetadataSealed);
    private readonly _buttonsContainer;
    private _isFocused;
    private _value;
    private _reactComponentRef;
    readonly meta: ViewParameterMetadataSealed;
    get buttonsContainer(): ControlButtonsContainer;
    get isFocused(): boolean;
    set isFocused(value: boolean);
    get value(): string;
    set value(value: string);
    private decimalFormat;
    getValue(): CriteriaValue;
    bindReactComponentRef: (ref: React.RefObject<HTMLInputElement>) => void;
    unbindReactComponentRef: () => void;
    focus(opt?: FocusOptions): void;
    blur(): void;
}
