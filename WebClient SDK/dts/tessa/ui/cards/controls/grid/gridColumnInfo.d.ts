import { Card, CardRow } from 'tessa/cards';
import { CardTypeColumn } from 'tessa/cards/types';
import { CardMetadataSection } from 'tessa/cards/metadata';
import { TypedField } from 'tessa/platform/typedField';
export declare class GridColumnInfo {
    constructor(typeColumn: CardTypeColumn, metadataSection: CardMetadataSection, ownedMetadataSection: CardMetadataSection | null);
    private readonly _type;
    private readonly _fieldNames;
    private readonly _displayFormat;
    private readonly _ignoreTimezone;
    private readonly _nonBreakableLine;
    private readonly _singleline;
    private readonly _trimSpaces;
    private readonly _applyDisplayFormatForEmptyValues;
    private readonly _ownedFieldNames;
    private readonly _ownedOrderFieldName;
    private readonly _ownedAggregationFormat;
    private readonly _ownedRowSeparator;
    private readonly _ownedReferenceFieldName;
    private readonly _ownedSectionName;
    private _previousCard;
    private _previousOwnedSectionRows;
    private _maxLength;
    get type(): CardTypeColumn;
    get ownedSectionName(): string;
    private replaceLineBreaks;
    private postprocessTextUsingCurrentSettings;
    private getFieldNames;
    formatValue(field: TypedField | null | undefined): any;
    clipValue(value: any): any;
    isClipValueNeed(value: any): boolean;
    private isEmptyValue;
    getValue(row: CardRow, card: Card): {
        formattedValue: any;
        value: any;
    };
}
