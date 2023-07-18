import * as React from 'react';
export interface TabPanelScrollerProps {
    tabPanelId: guid;
    onScrollUpdate: (show: boolean) => void;
    tabsButtonVisible: boolean;
}
export declare class TabPanelScroller extends React.Component<TabPanelScrollerProps> {
    shadowLeft: any;
    shadowRight: any;
    handleUpdate: (values: any) => void;
    renderView: ({ style, ...props }: {
        [x: string]: any;
        style: any;
    }) => JSX.Element;
    render(): JSX.Element;
}
