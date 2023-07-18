import * as React from 'react';
import { TopicViewModel } from 'tessa/ui/cards/controls';
export interface ForumTopicsListItemProps {
    viewModel: TopicViewModel;
    mode: 'expanded' | 'normal';
}
export declare class ForumTopicsListItem extends React.Component<ForumTopicsListItemProps> {
    private _descriptionRef;
    render(): JSX.Element;
    private hasOverflow;
    private handleDescriptionRef;
    private renderMessages;
    private renderDescriptionToggleButton;
    private toggleDescription;
    private handleClick;
}
