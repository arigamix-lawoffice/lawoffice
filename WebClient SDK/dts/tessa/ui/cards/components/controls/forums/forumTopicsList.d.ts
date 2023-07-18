import * as React from 'react';
import { TopicListViewModel } from 'tessa/ui/cards/controls';
export interface ForumTopicsListProps {
    viewModel: TopicListViewModel;
}
export declare class ForumTopicsList extends React.Component<ForumTopicsListProps> {
    private _containerRef;
    private _wrapperRef;
    private _listItemsMode;
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private handleKeyDown;
    private resizeLeft;
    private resizeRight;
    private handleContainerRef;
    private handleTransitionEnd;
    private updateListItemsMode;
}
