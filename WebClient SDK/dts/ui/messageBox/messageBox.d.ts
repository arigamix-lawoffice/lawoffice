import React from 'react';
import { MessageBoxButtons, MessageBoxIcon, MessageBoxResult, MessageBoxOptions } from 'tessa/ui/tessaDialog';
declare class MessageBox extends React.Component<MessageBoxProps> {
    private closeRequest;
    private getMessageResultOnClosing;
    private renderHeader;
    private renderContent;
    private handleCloseRequest;
    private renderFooter;
    render(): JSX.Element;
    private handleKeyDown;
}
export interface MessageBoxProps {
    text?: string | null;
    noPortal?: boolean;
    caption?: string;
    buttons?: MessageBoxButtons;
    icon?: MessageBoxIcon;
    iconColor?: string;
    options?: MessageBoxOptions;
    onClose: (value: MessageBoxResult) => void;
}
export { MessageBox };
