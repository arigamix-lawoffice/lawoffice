export declare type ToolbarItemsAlignment = 'start' | 'end' | 'center';
export declare type ToolbarViewOrientation = 'horizontal' | 'vertical';
export declare type ToolbarOverflowHandling = 'paging' | 'wrap' | 'scroll' | 'spread' | 'spread-end' | 'mono' | 'none';
export declare const ToolbarDefaults: {
    spacing: number;
    padding: number;
    pagingControls: string;
};
export declare const unusedGroupId = "__toolbar_internal_group_unused_items";
export declare const PagingPrevId = "__toolbar_internal_item_navigation_prev";
export declare const PagingNextId = "__toolbar_internal_item_navigation_next";
export declare const groupSplitter = "__toolbar_splitter";
export declare type ToolbarGroupDisplayType = 'normal' | 'hidden' | 'overlay';
export declare type ToolbarGroupSpreadMode = 'always' | 'never' | 'overflow';
export declare type ToolbarPagingControls = 'all' | 'pages' | 'arrows' | 'none';
export declare const shadowSuffix = "__toolbar_group_shadow";
