import { LicenseModule } from './licenseModule';
import { IStorage } from '../storage';
export declare class License {
    constructor(storage: IStorage);
    readonly version: number;
    readonly id: guid;
    readonly companyName: string;
    readonly companyAddress: string;
    readonly issuedBy: string;
    readonly validFrom: string;
    readonly validTo: string;
    readonly receiveUpdatesTo: string;
    readonly concurrentCount: number;
    readonly personalCount: number;
    readonly modules: ReadonlyArray<LicenseModule>;
    readonly mobileApprovalCount: number;
    private getMobileCount;
}
