import * as React from 'react';
import { ControlProps } from '../controlProps';
import { ForumViewModel } from 'tessa/ui/cards/controls';
export declare class ForumControl extends React.Component<ControlProps<ForumViewModel>> {
    render(): JSX.Element | null;
    private renderStubMessage;
    private renderEmptyControl;
    private renderTopicsList;
    private renderTopicEditor;
    private handleAddTopic;
    private handleOpenStubMenu;
    private handleContextMenu;
}
