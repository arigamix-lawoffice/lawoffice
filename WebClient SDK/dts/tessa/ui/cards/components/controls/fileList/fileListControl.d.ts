import * as React from 'react';
import { ControlProps } from '../controlProps';
import { FileListViewModel } from 'tessa/ui/cards/controls';
export declare class FileListControl extends React.Component<ControlProps<FileListViewModel>> {
    private _controlRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element | null;
    private renderFilterPlank;
    private renderStubCaption;
    private renderItems;
    private renderFiles;
    private renderGroups;
    private handleDrop;
    private handleGroupClick;
    private _handleEmptyClick;
    get handleEmptyClick(): () => Promise<void>;
    set handleEmptyClick(value: () => Promise<void>);
    private handleOnContextMenu;
    private handleControlClick;
}
