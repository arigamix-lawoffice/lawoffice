import { IAutoCompleteItem } from './autoCompleteItem';
import { ICardModel } from '../../interfaces';
import { CardMetadataSectionSealed, CardMetadataColumnSealed } from 'tessa/cards/metadata';
import { ITessaView } from 'tessa/views';
import { CardAutoCompleteSearchMode } from 'tessa/cards/cardAutoCompleteSearchMode';
export interface IAutoCompleteDataSourceContext {
    readonly setItemDelegate: (item: IAutoCompleteItem, fields: ReadonlyMap<string, any>) => void;
    readonly deleteItemDelegate: (item: IAutoCompleteItem) => void;
    readonly cardModel: ICardModel;
    readonly referenceSection: CardMetadataSectionSealed;
    readonly referenceColumn: CardMetadataColumnSealed;
    readonly refSection: ReadonlyArray<string> | null;
    readonly physicalColumns: ReadonlyArray<CardMetadataColumnSealed>;
    readonly view: ITessaView | null;
    readonly viewAlias: string | null;
    readonly viewParameterAlias: string | null;
    readonly viewMapping: any[] | null;
    readonly viewReferencePrefix: string | null;
    readonly formatString: string | null;
    readonly maxResultsCount: number;
    readonly complexColumnPref: string;
    readonly complexColumnNames: string[];
    readonly popupComplexColumnsDisplayIndexes: ReadonlyArray<number> | null;
    readonly popupScreenLengthPercent: number;
    readonly popupColumnLengths: ReadonlyArray<number> | null;
    readonly manualInput: boolean;
    readonly manualInputColumnId: guid | null;
    readonly manualInputColumnName: string | null;
    readonly searchDelay: number;
    readonly extendendLocalization: boolean;
    readonly searchMode: CardAutoCompleteSearchMode;
}