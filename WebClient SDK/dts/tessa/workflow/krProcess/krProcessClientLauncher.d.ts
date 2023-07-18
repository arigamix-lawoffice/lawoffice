import { KrProcessInstance } from './krProcessInstance';
import { KrProcessLaunchResult } from './krProcessLaunchResult';
import { IStorage } from 'tessa/platform/storage';
import { ICardEditorModel } from 'tessa/ui/cards';
export declare function launchProcess(krProcess: KrProcessInstance, specificParams?: {
    useCurrentCardEditor?: boolean;
    cardEditor?: ICardEditorModel;
    requestInfo?: IStorage;
    raiseErrorWhenExecutionIsForbidden?: boolean;
}): Promise<KrProcessLaunchResult | null>;
export declare function launchProcessWithCardEditor(krProcess: KrProcessInstance, cardEditor: ICardEditorModel, raiseErrorWhenExecutionIsForbidden: boolean): Promise<KrProcessLaunchResult | null>;
