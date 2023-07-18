import { IStorage } from 'tessa/platform/storage';
import { SelectedValue } from 'tessa/views';
import { DoubleClickInfo } from 'tessa/ui/views';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { AutoCompleteEntryViewModel, ViewControlViewModel, ViewMappingSettings } from '../controls';
import { SchemeDbType } from 'tessa/platform';
export declare class AutoCompleteDialogProvider {
    readonly autocomplete: AutoCompleteEntryViewModel;
    protected modifyViewFuncAsync?: ((viewControl: ViewControlViewModel) => void) | undefined;
    protected _viewMetadata: ViewMetadataSealed | null;
    protected _closeFunc: (() => void) | null;
    constructor(autocomplete: AutoCompleteEntryViewModel, modifyViewFuncAsync?: ((viewControl: ViewControlViewModel) => void) | undefined);
    showDialog: () => Promise<void>;
    protected showDialogCore(): Promise<void>;
    protected createForm(): Promise<import("../interfaces").IFormWithBlocksViewModel | null>;
    protected getViewMapping(settings: IStorage): ViewMappingSettings[] | null;
    protected selectValue: (info: DoubleClickInfo) => Promise<void>;
    protected selectValueCore(data: ReadonlyMap<string, any> | null, dataTypes: ReadonlyMap<string, SchemeDbType> | null): void;
    protected getSelectedValues(data: ReadonlyMap<string, any> | null, dataTypes: ReadonlyMap<string, SchemeDbType> | null): SelectedValue[];
}
