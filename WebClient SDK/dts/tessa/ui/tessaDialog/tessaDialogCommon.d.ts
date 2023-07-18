/// <reference types="react" />
export declare enum MessageBoxButtons {
    OK = 1,
    OKCancel = 2,
    YesNo = 4,
    YesNoCancel = 8
}
export declare enum MessageBoxIcon {
    Error = 0,
    Warning = 1,
    Information = 2,
    Question = 3
}
export declare namespace MessageBoxIcon {
    function getIconName(icon: MessageBoxIcon): string;
}
export interface MessageBoxOptions {
    className?: string;
    dialogStyle?: React.CSSProperties;
    captionStyle?: React.CSSProperties;
    textStyle?: React.CSSProperties;
    buttonsStyle?: React.CSSProperties;
    OKButtonText?: string;
    YesButtonText?: string;
    NoButtonText?: string;
    CancelButtonText?: string;
    OKButtonIcon?: string;
    YesButtonIcon?: string;
    NoButtonIcon?: string;
    CancelButtonIcon?: string;
}
export declare enum MessageBoxResult {
    None = 0,
    OK = 1,
    Cancel = 2,
    Yes = 3,
    No = 4
}
