import { CardRowPermissionInfo } from './cardRowPermissionInfo';
import { CardStorageObject } from './cardStorageObject';
import { CardSectionType } from './cardSectionType';
import { IStorage } from 'tessa/platform/storage';
import { CardPermissionFlags, CardPermissionFlagsTypedField } from './cardPermissionFlags';
import { MapStorage, IKeyedStorageValueFactory } from 'tessa/platform/storage';
export declare class CardSectionPermissionInfo extends CardStorageObject {
    constructor(name: string, storage?: IStorage);
    static readonly sectionPermissionsKey = "SectionPermissions";
    static readonly fieldPermissionsKey = "FieldPermissions";
    static readonly rowsKey = "Rows";
    private _type;
    readonly name: string;
    get type(): CardSectionType;
    set type(value: CardSectionType);
    get sectionPermissions(): CardPermissionFlags;
    set sectionPermissions(value: CardPermissionFlags);
    get fieldPermissions(): MapStorage<CardPermissionFlagsTypedField>;
    set fieldPermissions(value: MapStorage<CardPermissionFlagsTypedField>);
    get rows(): MapStorage<CardRowPermissionInfo>;
    set rows(value: MapStorage<CardRowPermissionInfo>);
    private static readonly _rowFactory;
    tryGetFieldPermissions(): MapStorage<CardPermissionFlagsTypedField> | null | undefined;
    tryGetRows(): MapStorage<CardRowPermissionInfo> | null | undefined;
    setSectionPermissions(flags: CardPermissionFlags, overwrite?: boolean): CardSectionPermissionInfo;
    setFieldPermissions(fieldName: string, flags: CardPermissionFlags, overwrite?: boolean): CardSectionPermissionInfo;
}
export declare class CardSectionPermissionInfoFactory implements IKeyedStorageValueFactory<string, CardSectionPermissionInfo> {
    getValue(key: string, storage: IStorage): CardSectionPermissionInfo;
    getValueAndStorage(key: string): {
        value: CardSectionPermissionInfo;
        storage: IStorage;
    };
}
