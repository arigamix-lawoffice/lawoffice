export declare class KrPermissionFlagDescriptor {
    readonly id: guid;
    readonly name: string;
    order: number;
    readonly description: string | null;
    readonly controlCaption: string | null;
    readonly controlTooltip: string | null;
    readonly sqlName: string | null;
    readonly includedPermissions: KrPermissionFlagDescriptor[];
    constructor(id: guid, name: string, order?: number, description?: string | null, controlCaption?: string | null, controlTooltip?: string | null, sqlName?: string | null, includedPermissions?: KrPermissionFlagDescriptor[]);
    get isVirtual(): boolean;
    static getAllDescriptorsRaw: () => KrPermissionFlagDescriptor[];
    equals(other: KrPermissionFlagDescriptor): boolean;
}
