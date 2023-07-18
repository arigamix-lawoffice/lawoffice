import React from 'react';
import { CardTableViewControlViewModel } from './cardTableViewControlViewModel';
import { UIButton, UIButtonComponent } from 'tessa/ui';
import { BaseViewControlItem, ViewControlPagingViewModel } from 'tessa/ui/cards/controls';
import { GridInputFilter } from 'tessa/ui/cards/components/controls';
import { PagingView } from 'tessa/ui/views/components';
import { Toolbar } from 'ui/toolbar';
import { end, filler, group, stretch } from 'ui/toolbar/helpers';
import { IToolbarGroup } from 'ui/toolbar/interfaces';
import { groupSplitter } from 'ui/toolbar/common';

export class CardTableViewPanelViewModel extends BaseViewControlItem {
  constructor(viewComponent: CardTableViewControlViewModel) {
    super(viewComponent);

    this.paging = new ViewControlPagingViewModel(viewComponent);
  }

  leftButtons: UIButton[] = [];

  rightButtons: UIButton[] = [];

  paging: ViewControlPagingViewModel;
}

export interface ICardTableViewPanelProps {
  viewModel: CardTableViewPanelViewModel;
}

interface ICardTableViewPanelState {
  expandSearch: boolean;
}

export class CardTableViewPanel extends React.Component<
  ICardTableViewPanelProps,
  ICardTableViewPanelState
> {
  constructor(props: ICardTableViewPanelProps) {
    super(props);

    this.state = { expandSearch: false };
  }

  render(): JSX.Element {
    const { viewModel } = this.props;
    const { leftButtons, rightButtons } = viewModel;
    const viewComponent = viewModel.viewComponent as CardTableViewControlViewModel;

    const items = [
      ...leftButtons.map(b => <UIButtonComponent key={b.name} viewModel={b} />),
      ...rightButtons.map(b => <UIButtonComponent key={b.name} viewModel={b} />),
      <PagingView key="paging" viewModel={viewModel.paging} />,
      <GridInputFilter
        key="filter"
        filterableGrid={viewComponent}
        visibility={viewComponent.getSearchBoxVisibility()}
        onFocusChange={this.handleFocusChange}
        expand={this.state.expandSearch}
      />
    ];

    const groups: IToolbarGroup[] = [];

    const left = leftButtons.length
      ? stretch(group('left', [...leftButtons.map(b => b.name), groupSplitter]))
      : filler('filler-paging-left');

    const paging = group('paging', ['paging']);

    const filter = end(
      stretch(group('filter', ['filter'], { isOverlay: this.state.expandSearch }))
    );

    if (!this.state.expandSearch) {
      filter.items.unshift(groupSplitter);
    }

    const right = group('right', []);
    for (const b of rightButtons) {
      right.items.push(groupSplitter, b.name);
    }

    groups.push(left, paging, filter, right);

    return (
      <div className="tableControlButtons">
        <Toolbar items={items} groups={groups} />
      </div>
    );
  }

  private handleFocusChange = (isFocused: boolean): void => {
    this.setState({ expandSearch: isFocused });
  };
}
