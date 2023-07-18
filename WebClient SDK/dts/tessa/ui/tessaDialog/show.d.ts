import { MessageBoxButtons, MessageBoxIcon, MessageBoxOptions, MessageBoxResult } from './tessaDialogCommon';
export declare function show(args: {
    text: string | null | undefined;
    caption?: string;
    buttons?: MessageBoxButtons;
    icon?: MessageBoxIcon;
    iconColor?: string;
    options?: MessageBoxOptions;
}): Promise<MessageBoxResult>;
export declare function showError(text: string | null | undefined, caption?: string, options?: MessageBoxOptions): Promise<MessageBoxResult>;
export declare function showWarning(text: string | null | undefined, caption?: string, options?: MessageBoxOptions): Promise<MessageBoxResult>;
export declare function showMessage(text: string | null | undefined, caption?: string, options?: MessageBoxOptions): Promise<MessageBoxResult>;
export declare function showConfirm(text: string | null | undefined, caption?: string, options?: MessageBoxOptions): Promise<boolean>;
export declare function showConfirmWithCancel(text: string | null | undefined, caption?: string, options?: MessageBoxOptions): Promise<boolean | null>;
