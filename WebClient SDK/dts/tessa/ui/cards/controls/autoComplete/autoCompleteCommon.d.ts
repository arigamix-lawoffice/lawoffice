import { IAutoCompleteItem } from './autoCompleteItem';
export declare class AutoCompleteValueEventArgs<T> {
    constructor(item: IAutoCompleteItem, autocomplete: T, fields?: ReadonlyMap<string, any>);
    readonly item: IAutoCompleteItem;
    readonly autocomplete: T;
    readonly fields?: ReadonlyMap<string, any>;
}
