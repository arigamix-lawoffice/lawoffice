import { TabControlViewModel } from 'tessa/ui/cards/controls';
import { IStorage } from 'tessa/platform/storage';
import { CardFieldChangedEventArgs } from 'tessa/cards';
export declare class TabContentIndicator {
    constructor(tabControl: TabControlViewModel, fieldsStorage: IStorage, fieldIds: [guid, string][], updateBlockHeader?: boolean);
    private _fieldsStorage;
    private _blockViewModel;
    private _originalBlockName;
    private _hasContent;
    private _originalTabNames;
    private _tabs;
    private _tabFieldsMapping;
    private _fieldTabMapping;
    update(): void;
    private updateTabName;
    fieldChangedAction: (e: CardFieldChangedEventArgs) => void;
}
