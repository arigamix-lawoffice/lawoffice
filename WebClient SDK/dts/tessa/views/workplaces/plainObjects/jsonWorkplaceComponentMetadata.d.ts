import { NodeClientVisibility } from 'tessa/views/metadata';
import { WorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed, WorkplaceMetadataComponent, WorkplaceMetadataComponentSealed } from 'tessa/views/workplaces';
import { CardStorageObject } from 'tessa/cards';
import { ArrayStorage, IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { JsonExtensionMetadata, JsonExtensionMetadataFactory } from './jsonExtensionMetadata';
export declare class JsonWorkplaceComponentMetadataFactory implements IStorageValueFactory<JsonWorkplaceComponentMetadata> {
    getValue(storage: IStorage): JsonWorkplaceComponentMetadata;
    getValueAndStorage(): {
        value: JsonWorkplaceComponentMetadata;
        storage: IStorage;
    };
}
export declare class JsonWorkplaceComponentMetadata extends CardStorageObject {
    constructor(metadata?: WorkplaceCompositeMetadata | WorkplaceCompositeMetadataSealed | WorkplaceMetadataComponent | WorkplaceMetadataComponentSealed, storage?: IStorage);
    protected static readonly _jsonExtensionMetadataFactory: JsonExtensionMetadataFactory;
    static readonly expandedIconNameKey: string;
    static readonly iconNameKey: string;
    static readonly compositionIdKey: string;
    static readonly nodeClientVisibilityKey: string;
    static readonly aliasKey: string;
    static readonly orderPosKey: string;
    static readonly ownerIdKey: string;
    static readonly parentCompositionIdKey: string;
    static readonly extensionsKey: string;
    static readonly typeNameKey: string;
    get expandedIconName(): string | null;
    set expandedIconName(value: string | null);
    get iconName(): string | null;
    set iconName(value: string | null);
    get compositionId(): guid;
    set compositionId(value: guid);
    get nodeClientVisibility(): NodeClientVisibility;
    set nodeClientVisibility(value: NodeClientVisibility);
    get orderPos(): number;
    set orderPos(value: number);
    get ownerId(): guid;
    set ownerId(value: guid);
    get parentCompositionId(): guid;
    set parentCompositionId(value: guid);
    get alias(): string | null;
    set alias(value: string | null);
    get extensions(): ArrayStorage<JsonExtensionMetadata> | null;
    set extensions(value: ArrayStorage<JsonExtensionMetadata> | null);
    get typeName(): string | null;
    set typeName(value: string | null);
}
