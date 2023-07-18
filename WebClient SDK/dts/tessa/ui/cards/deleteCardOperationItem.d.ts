import { CardOperationItem } from './cardOperationItem';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { CardDeletionMode } from 'tessa/cards';
export declare class DeleteCardOperationItem<T = guid> extends CardOperationItem<T> {
    constructor(cardId: T, displayValue: string);
    private _withoutBackupOnly;
    get withoutBackupOnly(): boolean;
    private _deletionMode;
    get deletionMode(): CardDeletionMode;
    setupType(typeId: guid | null, withoutBackupOnly: boolean, cardMetadata: CardMetadataSealed): void;
}
