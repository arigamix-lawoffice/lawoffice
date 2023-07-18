import { CardStorageObject } from './cardStorageObject';
import { IStorage } from 'tessa/platform/storage';
import { CardPermissionFlags, CardPermissionFlagsTypedField } from './cardPermissionFlags';
import { MapStorage, IKeyedStorageValueFactory } from 'tessa/platform/storage';
export declare class CardRowPermissionInfo extends CardStorageObject {
    constructor(rowId: guid, storage?: IStorage);
    static readonly rowPermissionsKey = "RowPermissions";
    static readonly fieldPermissionsKey = "FieldPermissions";
    readonly rowId: guid;
    get rowPermissions(): CardPermissionFlags;
    set rowPermissions(value: CardPermissionFlags);
    get fieldPermissions(): MapStorage<CardPermissionFlagsTypedField>;
    set fieldPermissions(value: MapStorage<CardPermissionFlagsTypedField>);
    tryGetFieldPermissions(): MapStorage<CardPermissionFlagsTypedField> | null | undefined;
    setRowPermissions(flags: CardPermissionFlags, overwrite?: boolean): CardRowPermissionInfo;
    setFieldPermissions(fieldName: string, flags: CardPermissionFlags, overwrite?: boolean): CardRowPermissionInfo;
}
export declare class CardRowPermissionInfoFactory implements IKeyedStorageValueFactory<string, CardRowPermissionInfo> {
    getValue(key: string, storage: IStorage): CardRowPermissionInfo;
    getValueAndStorage(key: string): {
        value: CardRowPermissionInfo;
        storage: IStorage;
    };
}
