import * as React from 'react';
import { ForumItemViewModel } from 'tessa/ui/cards/controls';
export interface ForumAttachmentProps {
    viewModel: ForumItemViewModel;
}
export declare class ForumAttachment extends React.Component<ForumAttachmentProps> {
    render(): JSX.Element;
    private handleClick;
}
