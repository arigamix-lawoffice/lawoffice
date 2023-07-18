import * as React from 'react';
import { MessageViewModelBase } from 'tessa/ui/cards/controls';
export interface ForumMessageProps {
    viewModel: MessageViewModelBase;
}
export declare class ForumMessage extends React.Component<ForumMessageProps> {
    private _defaultType;
    constructor(props: ForumMessageProps);
    render(): JSX.Element | null;
}
