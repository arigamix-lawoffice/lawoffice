import { License } from './license';
import { IStorage } from '../storage';
export declare class LicenseManager {
    private constructor();
    private static _instance;
    static get instance(): LicenseManager;
    private _license;
    get license(): License;
    init(storage: IStorage): void;
}
