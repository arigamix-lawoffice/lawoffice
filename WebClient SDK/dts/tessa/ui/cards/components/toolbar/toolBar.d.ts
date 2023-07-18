import * as React from 'react';
import { ICardToolbarViewModel } from '../../cardToolbarViewModel';
export interface ToolBarProps {
    toolbar: ICardToolbarViewModel | null;
    bottom?: boolean;
    inCard?: boolean;
    className?: string;
}
export declare class ToolBar extends React.Component<ToolBarProps> {
    render(): JSX.Element | null;
    private renderItems;
    private renderTags;
}
export declare const StyledToolBar: import("styled-components").StyledComponent<typeof ToolBar, any, ToolBarProps, never>;
