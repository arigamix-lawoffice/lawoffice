/**
 * includes next props:
 *
 * **strictGreater**: describes which operator should be used, > or >=, by default false.
 *
 * **suggestUpt**: if false then func won't suggest to download a new version, by default true.
 *
 * **forceCheckUpdateAgain**: if true then every call func will suggest to download a new version, by default false,
 * except cases when current version less than required.
 *
 * **showMes**: if true and current version less than required and version on the server then
 * func will show message box about it, by default true.
 */
interface ComparingOptions {
    strictGreater?: boolean;
    suggestUpt?: boolean;
    forceCheckUpdateAgain?: boolean;
    showMes?: boolean;
}
/**
 * Compares current deski version with provided min version.
 * Also compares current version with version published on server
 * if version on server greater than current suggest to download it.
 *
 * @param minRequiredVer minimal required version, e.g. '2.1.0'.
 * @param options options for using during the comparing.
 * @returns true if current version greater or equal required and user refused update, otherwise false.
 */
export declare function versionCheck(minRequiredVer: string, options?: ComparingOptions): Promise<boolean>;
export {};
