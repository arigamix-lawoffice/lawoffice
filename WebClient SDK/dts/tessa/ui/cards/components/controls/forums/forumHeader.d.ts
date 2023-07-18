import * as React from 'react';
import { UIButton } from 'tessa/ui/uiButton';
import { ForumPager, SearchBoxViewModel } from 'tessa/ui/cards/controls';
export interface ForumHeaderProps {
    title: string;
    description?: string;
    authorName?: string;
    created?: string;
    leftButtons?: UIButton[];
    rightButtons?: UIButton[];
    pager?: ForumPager;
    mode: 'normal' | 'compact';
    hideOverlays?: boolean;
    showDescriptionOverlay?: boolean;
    showDescriptionPopover?: boolean;
    onShowDescriptionOverlayChange?: (value: boolean) => void;
    onShowDescriptionPopoverChange?: (value: boolean) => void;
    searchBoxViewModel?: SearchBoxViewModel;
    isArchived?: boolean;
}
export declare class ForumHeader extends React.Component<ForumHeaderProps> {
    private _headerWrapperRef;
    render(): JSX.Element;
    private renderCompactHeader;
    private renderNormalHeader;
    private renderHeaderCaption;
    private renderDescriptionOverlay;
    private renderPopoverButton;
    private renderDescriptionPopover;
    private openDescriptionOverlay;
    private closeDescriptionOverlay;
    private toggleDescriptionPopover;
    private closeDescriptionPopover;
    private renderButtons;
    private renderPager;
    private renderDescription;
    private handleCurrentPageChanged;
}
