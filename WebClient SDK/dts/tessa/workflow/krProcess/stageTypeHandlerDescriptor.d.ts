import { PerformerUsageMode } from '../performerUsageMode';
import { KrProcessRunnerMode } from '../krProcessRunnerMode';
export declare class StageTypeHandlerDescriptor {
    constructor(id: guid, caption: string | null, defaultStageName: string | null, settingsCardTypeId: guid | null, performerUsageMode: PerformerUsageMode, performerIsRequired: boolean, performerCaption: string | null, canOverrideAuthor: boolean, useTimeLimit: boolean, usePlanned: boolean, canBeHidden: boolean, canOverrideTaskHistoryGroup: boolean, supportedModes: ReadonlyArray<KrProcessRunnerMode>, canBeSkipped: boolean);
    readonly id: guid;
    readonly caption: string | null;
    readonly defaultStageName: string | null;
    readonly settingsCardTypeId: guid | null;
    readonly performerUsageMode: PerformerUsageMode;
    readonly performerIsRequired: boolean;
    readonly performerCaption: string | null;
    readonly canOverrideAuthor: boolean;
    readonly useTimeLimit: boolean;
    readonly usePlanned: boolean;
    readonly canBeHidden: boolean;
    readonly canOverrideTaskHistoryGroup: boolean;
    readonly supportedModes: ReadonlyArray<KrProcessRunnerMode>;
    readonly canBeSkipped: boolean;
    static create(args: {
        id?: guid;
        caption?: string | null;
        defaultStageName?: string | null;
        settingsCardTypeId?: guid | null;
        performerUsageMode?: PerformerUsageMode;
        performerIsRequired?: boolean;
        performerCaption?: string | null;
        canOverrideAuthor?: boolean;
        useTimeLimit?: boolean;
        usePlanned?: boolean;
        canBeHidden?: boolean;
        canOverrideTaskHistoryGroup?: boolean;
        supportedModes?: ReadonlyArray<KrProcessRunnerMode>;
        canBeSkipped?: boolean;
    }): StageTypeHandlerDescriptor;
}
