import * as React from 'react';
import { TopicEditorViewModel } from 'tessa/ui/cards/controls';
export interface ForumTopicEditorProps {
    viewModel: TopicEditorViewModel;
}
export declare class ForumTopicEditor extends React.Component<ForumTopicEditorProps> {
    private _containerRef;
    private _bodyRef;
    componentDidMount(): void;
    componentDidUpdate(prevProps: ForumTopicEditorProps): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private handleKeyDown;
    private resizeLeft;
    private resizeRight;
    private scrollToMessage;
    private renderMessagesFiller;
    private renderMessages;
    private renderSeparator;
    private shouldScrollBodyToEnd;
    private scrollBodyToEnd;
}
