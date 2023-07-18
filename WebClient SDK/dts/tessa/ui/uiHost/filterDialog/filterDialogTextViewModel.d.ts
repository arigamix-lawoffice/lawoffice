import { IFilterDialogEditorViewModel } from './common';
import { ViewParameterMetadataSealed } from 'tessa/views/metadata';
import { CriteriaValue } from 'tessa/views/metadata/criteriaValue';
import { ControlButtonsContainer } from 'tessa/ui/cards/controls/controlButtonsContainer';
export declare class FilterDialogTextViewModel implements IFilterDialogEditorViewModel {
    constructor(meta: ViewParameterMetadataSealed);
    private _value;
    private readonly _buttonsContainer;
    private _reactComponentRef;
    readonly meta: ViewParameterMetadataSealed;
    get buttonsContainer(): ControlButtonsContainer;
    get value(): string;
    set value(value: string);
    getValue(): CriteriaValue;
    bindReactComponentRef: (ref: React.RefObject<HTMLInputElement>) => void;
    unbindReactComponentRef: () => void;
    focus(opt?: FocusOptions): void;
    blur(): void;
}
