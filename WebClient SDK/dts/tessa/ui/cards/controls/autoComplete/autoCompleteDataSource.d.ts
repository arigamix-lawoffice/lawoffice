import { IAutoCompleteItem } from './autoCompleteItem';
import { IAutoCompleteDataSourceContext } from './autoCompleteDataSourceContext';
import { IAutoCompletePopupItem } from './autoCompletePopupItem';
import { AutoCompleteColumnMap } from './autoCompleteColumnMap';
import { CardMetadataColumnSealed, CardMetadataSealed, CardMetadataSectionSealed } from 'tessa/cards/metadata';
import { ITessaView, ITessaViewResult } from 'tessa/views';
import { ViewMetadataSealed } from 'tessa/views/metadata';
export declare abstract class AutoCompleteDataSource {
    constructor(dataSourceContext: IAutoCompleteDataSourceContext);
    protected readonly _setItemDelegate: (item: IAutoCompleteItem, fields: ReadonlyMap<string, any>) => void;
    protected readonly _deleteItemDelegate: (item: IAutoCompleteItem) => void;
    protected readonly _view: ITessaView | null;
    protected readonly _viewAlias: string | null;
    protected readonly _parameterAlias: string | null;
    protected readonly _viewMapping: any[] | null;
    protected readonly _refSection: ReadonlyArray<string> | null;
    protected readonly _referenceSection: CardMetadataSectionSealed;
    protected readonly _referenceColumn: CardMetadataColumnSealed;
    protected readonly _viewReferencePrefix: string | null;
    protected readonly _physicalColumnsNames: ReadonlyArray<string>;
    protected readonly _physicalColumns: ReadonlyArray<CardMetadataColumnSealed>;
    protected readonly _physicalRefColumn: CardMetadataColumnSealed;
    protected readonly _physicalCaptionColumn: CardMetadataColumnSealed | null;
    protected readonly _complexColumnNames: ReadonlyArray<string>;
    protected readonly _complexColumnPref: string;
    protected _popupComplexColumnsDisplayIndexes: ReadonlyArray<number> | null;
    protected _complexColumnMap: AutoCompleteColumnMap;
    protected _manualInputItem: IAutoCompletePopupItem | null;
    get popupComplexColumnsDisplayIndexes(): ReadonlyArray<number> | null;
    get complexColumnMap(): AutoCompleteColumnMap;
    readonly maxResultsCount: number;
    readonly formatString: string | null;
    getDefaultCaption(popupItem: IAutoCompletePopupItem): string;
    getReference(popupItem: IAutoCompletePopupItem): any;
    getColumnValues(popupItem: IAutoCompletePopupItem): any[];
    protected getViewColPrefix(viewMetadata: ViewMetadataSealed): string;
    protected generatePopupItems(filter: string, dataSourceContext: IAutoCompleteDataSourceContext): Promise<IAutoCompletePopupItem[]>;
    protected mapComplexColumnsInput(result: ITessaViewResult, _cardMetadata: CardMetadataSealed, viewColPrefix: string, viewAlias: string): void;
    protected mapComplexColumnsBrowse(viewMetadata: ViewMetadataSealed, _cardMetadata: CardMetadataSealed, keys: string[], viewColPrefix: string): void;
    protected checkComplexColumnMap(): boolean;
    protected calculateDisplayIndexes(popupItems: IAutoCompletePopupItem[], result: ITessaViewResult, displayIndexes: ReadonlyArray<number> | null): ReadonlyArray<number>;
    protected createAutoCompletePopupItems(result: ITessaViewResult): IAutoCompletePopupItem[];
    asyncGuard: number;
    debouncedDelay: (delay: number) => Promise<void>;
}
