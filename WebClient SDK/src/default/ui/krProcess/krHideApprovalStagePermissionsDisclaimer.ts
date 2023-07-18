import { reaction } from 'mobx';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { hasNotFlag, hasFlag, Visibility } from 'tessa/platform';
import { CardTypeFlags } from 'tessa/cards/types';
import { GridViewModel, GridRowEventArgs, GridRowAction, CheckBoxViewModel } from 'tessa/ui/cards/controls';
import { CardPermissionFlags } from 'tessa/cards';

export class KrHideApprovalStagePermissionsDisclaimer extends CardUIExtension {

  private _disposes: (Function | null)[] = [];

  public initialized(context: ICardUIExtensionContext) {
    const model = context.model;

    if (hasNotFlag(model.cardType.flags, CardTypeFlags.AllowTasks)) {
      return;
    }

    const approvalTab = model.forms.find(x => x.name === 'ApprovalProcess');

    if (!approvalTab) {
      return;
    }

    for (let block of approvalTab.blocks) {
      if (block.name === 'ApprovalStagesBlock') {
        for (let control of block.controls) {
          if (control instanceof GridViewModel) {
            control.rowInvoked.add(this.gridRowInvoked);
          }
        }
      }
    }
  }

  public finalized() {
    for (let dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  private gridRowInvoked = (e: GridRowEventArgs) => {
    if (e.action !== GridRowAction.Inserted && e.action !== GridRowAction.Opening) {
      return;
    }

    const label = e.rowModel!.controls.get('DisclaimerControl');
    if (!label) {
      return;
    }

    const readOnly = hasFlag(e.rowModel!.card.permissions.resolver
      .getRowPermissions('KrStagesVirtual', e.row.rowId), CardPermissionFlags.ProhibitModify);

    if (readOnly) {
      const fieldName = 'KrApprovalSettingsVirtual__IsParallel';
      if (!e.row.get(fieldName)) {
        label.controlVisibility = Visibility.Collapsed;
      }
    } else {
      const isParallelControl = e.rowModel!.controls.get('IsParallelFlag');
      if (isParallelControl) {
        this._disposes.push(reaction(
          () => (isParallelControl as CheckBoxViewModel).isChecked,
          () => {
            label.controlVisibility = label.controlVisibility === Visibility.Collapsed
              ? Visibility.Visible
              : Visibility.Collapsed;
          }
        ));
      }
    }
  }

}