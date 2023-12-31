import { CardMetadataSealed } from 'tessa/cards/metadata';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { IItemProperties, ItemPropertiesSealed } from 'tessa/views/workplaces/properties';
import { CommonMetadata } from 'tessa/platform';
import { ValidationResult } from 'tessa/platform/validation';
import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed, WorkplaceMetadataSealed } from './views/workplaces';
export declare class MetadataStorage {
    private constructor();
    private static _instance;
    static get instance(): MetadataStorage;
    private _cardMetadata;
    get isCardMetadataInitialized(): boolean;
    get cardMetadata(): CardMetadataSealed;
    setCardMetadata(cardMetadata: CardMetadataSealed): void;
    private _workplacesMetadata;
    get isWorkplacesMetadataInitialized(): boolean;
    get workplacesMetadata(): WorkplaceMetadataSealed[];
    setWorkplacesMetadata(workplacesMetadata: WorkplaceMetadataSealed[]): void;
    addUserMetadata(metadata: IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed): Promise<ValidationResult | null>;
    changeUserMetadata(metadata: IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed): Promise<ValidationResult | null>;
    deleteUserMetadata(compositionID: guid): Promise<ValidationResult | null>;
    saveUserMetadataProperties(properties: (IItemProperties | ItemPropertiesSealed)[]): Promise<ValidationResult | null>;
    private _viewsMetadata;
    get isViewsMetadataInitialized(): boolean;
    get viewsMetadata(): ViewMetadataSealed[];
    setViewsMetadata(viewsMetadata: ViewMetadataSealed[]): void;
    private _commonMetadata;
    private _pendingServiceWorkerError;
    get isCommonMetadataInitialized(): boolean;
    get commonMetadata(): CommonMetadata;
    setCommonMetadata(commonMetadata: CommonMetadata): void;
    setPendingServiceWorkerError(error: string): void;
    readonly info: Map<string, any>;
}
