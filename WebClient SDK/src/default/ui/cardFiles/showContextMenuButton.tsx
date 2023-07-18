import * as React from 'react';
import { observer } from 'mobx-react';
import { ShowContextMenuButtonViewModel } from './showContextMenuButtonViewModel';
import { IconButton } from 'ui';
import { MenuActionComponent } from 'tessa/ui/components';
import { MenuAction } from 'tessa/ui';

export interface ShowContextMenuButtonProps {
  viewModel: ShowContextMenuButtonViewModel;
}

export interface ShowContextMenuButtonState {
  isDropDownOpen: boolean;
}

@observer
export class ShowContextMenuButton extends React.Component<
  ShowContextMenuButtonProps,
  ShowContextMenuButtonState
> {
  constructor(props: ShowContextMenuButtonProps) {
    super(props);

    this.state = {
      isDropDownOpen: false
    };
  }

  private _dropDownRef = React.createRef<IconButton>();

  public render() {
    const { isDropDownOpen } = this.state;
    return (
      <>
        <IconButton
          className="button-plain"
          icon={'icon-grid-spread'}
          ref={this._dropDownRef}
          onClick={this.handleOpenDropDown}
        />
        <MenuActionComponent
          isOpened={isDropDownOpen}
          rootElement={this._dropDownRef.current}
          className="files-control-dropdown"
          actions={this.getActions()}
          onClose={this.handleCloseDropDown}
        />
      </>
    );
  }

  private getActions(): MenuAction[] {
    const { viewModel } = this.props;
    const { isDropDownOpen } = this.state;
    if (!isDropDownOpen) {
      return [];
    }

    return viewModel.getMenuActions() as MenuAction[];
  }

  private handleOpenDropDown = () => {
    this.setState({
      isDropDownOpen: true
    });
  };

  private handleCloseDropDown = () => {
    this.setState({
      isDropDownOpen: false
    });
  };
}
