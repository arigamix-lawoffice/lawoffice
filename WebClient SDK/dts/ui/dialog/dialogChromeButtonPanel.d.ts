import React from 'react';
import { UIButton } from 'tessa/ui';
export interface IDialogChromeButtonPanelProps {
    onClose: () => void;
    fullscreen?: boolean;
    onExpand?: () => void;
    buttons?: UIButton[];
    title?: string;
}
export declare const DialogChromeButtonPanel: React.FC<IDialogChromeButtonPanelProps>;
