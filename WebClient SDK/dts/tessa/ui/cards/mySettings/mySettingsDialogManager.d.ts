export declare class MySettingsDialogManager {
    static showSettingsDialog(userId?: guid, _dialogTitle?: string, resetSettingsConfirmation?: string): Promise<void>;
    static showChangePasswordDialog(): Promise<void>;
    private static updateWarningLabel;
    private static refreshSettingsAsync;
    static resetSettings(userId: guid, resetSettingsConfirmation?: string, completionFunc?: () => Promise<void>): Promise<void>;
    private static saveSettingsIfRequired;
    private static applySettingsToEmployeesAsync;
    static showApplySettingsToEmployeesDialogAndTryGetRoleIDList(): Promise<guid[] | null>;
}
