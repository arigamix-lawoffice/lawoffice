import * as React from 'react';
import { observer } from 'mobx-react';
import styled from 'styled-components';
import { StageGroup } from './stageGroup';
import { StageType } from './stageType';
import { StageSelectorViewModel } from './stageSelectorViewModel';
import { LocalizationManager } from 'tessa/localization';
import {
  Dialog,
  DialogContainer,
  DialogContent,
  DialogHeader,
  DialogFooter,
  RaisedButton
} from 'ui';
import { Grid } from 'components/cardElements';
import { StageDialogRowAdapter } from './stageDialogRowAdapter';
import {
  defaultLayout,
  GridColumnDisplayType,
  IGridColumnViewModel,
  IGridLayout
} from 'components/cardElements/grid';
import { Visibility } from 'tessa/platform';
import { computed } from 'mobx';

export const stageSelectorColumnName = 'name';

const StageDialog = styled(Dialog)`
  background: ${props => props.theme.card.cardSelectorBackground};
`;

export interface StageSelectorDialogProps {
  viewModel: StageSelectorViewModel;
  onClose: (args: { cancel: boolean; group: StageGroup | null; type: StageType | null }) => void;
}

@observer
// tslint:disable-next-line:max-line-length
export class StageSelectorDialog extends React.Component<StageSelectorDialogProps> {
  //#region ctor

  constructor(props: StageSelectorDialogProps) {
    super(props);
    this._layouts = [{ name: defaultLayout.name, start: 1 }];

    this._groupColumns = [
      {
        id: stageSelectorColumnName,
        caption: LocalizationManager.instance.localize('$Views_KrStageTemplates_StageGroup'),
        visibility: Visibility.Visible,
        displayType: GridColumnDisplayType.normal
      }
    ];

    this._typeColumns = [
      {
        id: stageSelectorColumnName,
        caption: LocalizationManager.instance.localize('$Views_KrStageTemplates_Types'),
        visibility: Visibility.Visible,
        displayType: GridColumnDisplayType.normal
      }
    ];
  }

  //#endregion

  //#region fields

  private _layouts: IGridLayout[];
  private _groupColumns: IGridColumnViewModel[];
  private _typeColumns: IGridColumnViewModel[];

  //#endregion

  //#region props

  @computed
  private get groupRows() {
    const { viewModel } = this.props;
    return viewModel.groups.map(g => new StageDialogRowAdapter(g, viewModel, g.id));
  }

  @computed
  private get typeRows() {
    const { viewModel } = this.props;
    return viewModel.types.map(g => new StageDialogRowAdapter(g, viewModel, g.id));
  }

  //#endregion

  //#region react

  public render() {
    return (
      <StageDialog
        isOpened={true}
        noPortal={true}
        isAutoSize={true}
        onCloseRequest={this.handleCloseForm}
        className="kr-stages-modal"
      >
        <DialogContainer>
          <DialogHeader>
            {LocalizationManager.instance.localize('$UI_Cards_SelectGroupAndType')}
          </DialogHeader>
          <DialogContent>
            <Grid
              columns={this._groupColumns}
              rows={this.groupRows}
              canSelectMultipleItems={false}
              layouts={this._layouts}
            />
            <Grid
              columns={this._typeColumns}
              rows={this.typeRows}
              canSelectMultipleItems={false}
              noDataText={LocalizationManager.instance.localize('$UI_Error_NoAvailableStages')}
              layouts={this._layouts}
            />
          </DialogContent>
          <DialogFooter className="default-footer">
            <RaisedButton
              icon="icon-thin-254"
              onClick={this.handleCloseFormWithResult}
              label={LocalizationManager.instance.localize('$UI_Common_OK')}
            />
            <RaisedButton
              icon="icon-thin-253"
              onClick={this.handleCloseForm}
              label={LocalizationManager.instance.localize('$UI_Common_Cancel')}
            />
          </DialogFooter>
        </DialogContainer>
      </StageDialog>
    );
  }

  //#endregion

  //#region handlers

  private handleCloseForm = () => {
    this.props.onClose({
      cancel: true,
      group: null,
      type: null
    });
  };

  private handleCloseFormWithResult = () => {
    const { viewModel } = this.props;

    this.props.onClose({
      cancel: false,
      group: viewModel.selectedGroup,
      type: viewModel.selectedType
    });
  };

  //#endregion
}
