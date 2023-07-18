import React from 'react';
import { observer } from 'mobx-react';
import { TaskHistoryViewSearchViewModel } from './taskHistoryViewSearchViewModel';
import { GridInputFilter } from 'tessa/ui/cards/components/controls';

export interface ITaskHistoryViewSearchProps {
  viewModel: TaskHistoryViewSearchViewModel;
}

interface ITaskHistoryViewSearchState {
  expandSearch: boolean;
}

@observer
export class TaskHistoryViewSearch extends React.Component<
  ITaskHistoryViewSearchProps,
  ITaskHistoryViewSearchState
> {
  constructor(props: ITaskHistoryViewSearchProps) {
    super(props);

    this.state = { expandSearch: false };
  }

  render(): JSX.Element {
    const { viewModel } = this.props;
    return (
      <GridInputFilter
        filterableGrid={viewModel}
        visibility={viewModel.visibility}
        expand={this.state.expandSearch}
        onFocusChange={this.handleFocusChange}
      />
    );
  }

  private handleFocusChange = (isFocused: boolean) => {
    this.props.viewModel.expand = isFocused;
    this.setState({ expandSearch: isFocused });
  };
}
