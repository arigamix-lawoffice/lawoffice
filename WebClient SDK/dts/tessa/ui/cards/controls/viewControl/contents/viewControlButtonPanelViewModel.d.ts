import React from 'react';
import { UIButton } from 'tessa/ui/uiButton';
import { BaseViewControlItem } from './baseViewControlItem';
import type { ViewControlViewModel } from '../viewControlViewModel';
export declare class ViewControlButtonPanelViewModel extends BaseViewControlItem {
    private _buttons;
    constructor(viewComponent: ViewControlViewModel);
    get buttons(): UIButton[];
    get visibleButtons(): readonly UIButton[];
}
export interface ViewControlButtonPanelProps {
    viewModel: ViewControlButtonPanelViewModel;
}
export declare class ViewControlButtonPanel extends React.Component<ViewControlButtonPanelProps> {
    render(): JSX.Element;
}
