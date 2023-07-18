import { ICloneable } from 'tessa/platform';
import { DeltaKind } from 'tessa/platform/data';
export declare class RoleLink implements ICloneable<RoleLink> {
    deltaKind: DeltaKind;
    objectId: guid;
    roleId: guid;
    roleName: string;
    clone(): RoleLink;
}
