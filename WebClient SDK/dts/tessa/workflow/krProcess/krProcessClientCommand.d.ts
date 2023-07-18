import { StorageObject, IStorage } from 'tessa/platform/storage';
export declare class KrProcessClientCommand extends StorageObject {
    constructor(storage?: IStorage);
    static readonly commandTypeKey = "CommandType";
    static readonly parametersKey = "Parameters";
    get commandType(): string;
    get parameters(): IStorage;
}
