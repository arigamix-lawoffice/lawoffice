import { observe, Lambda } from 'mobx';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { GridViewModel, LabelViewModel, AutoCompleteTableViewModel } from 'tessa/ui/cards/controls';
import { Card, CardRow, CardRowState } from 'tessa/cards';
import { LocalizationManager } from 'tessa/localization';
import { Visibility, DotNetType, Guid } from 'tessa/platform';
import { ValidationResultType } from 'tessa/platform/validation';

export class KrStageTemplateUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    const cardModel = context.model;

    if (cardModel.card.typeId !== '2fa85bb3-bba4-4ab6-ba97-652106db96de') { // KrStageTemplateTypeID
      return;
    }

    const grid = cardModel.controls.get('ApprovalStagesTable') as GridViewModel;
    if (!grid) {
      return;
    }

    let approversControlDisposer: Lambda | null = null;
    grid.rowInitializing.add(e => {
      const hyperlink = e.rowModel!.controls.get('AddComputedRoleLink') as LabelViewModel;
      if (!hyperlink) {
        return;
      }

      hyperlink.controlVisibility = KrStageTemplateUIExtension.hasComputedRole(
        e.cardModel.card,
        e.row.rowId
      )
        ? Visibility.Collapsed
        : Visibility.Visible;

      const approversControl = e.rowModel!.controls.get(
        'MultiplePerformersTableAC'
      ) as AutoCompleteTableViewModel;
      if (approversControl) {
        const approversControlChanged = () => {
          hyperlink.controlVisibility = KrStageTemplateUIExtension.hasComputedRole(
            e.cardModel.card,
            e.row.rowId
          )
            ? Visibility.Collapsed
            : Visibility.Visible;
        };

        approversControlDisposer = observe(approversControl, 'items', approversControlChanged);
      }

      const card = e.cardModel.card;
      const rows = card.sections.get('KrPerformersVirtual_Synthetic')!.rows;
      const rowId = e.row.rowId;

      hyperlink.onClick = () => {
        if (!KrStageTemplateUIExtension.hasComputedRole(card, rowId)) {
          const row = new CardRow();
          row.rowId = Guid.newGuid();
          row.state = CardRowState.Inserted;

          row.set('PerformerID', 'cd4d4a0d-414f-478d-a226-319aa8417f88', DotNetType.Guid); // SqlApproverRoleID
          row.set('PerformerName', '$KrProcess_SqlPerformersRole', DotNetType.String);
          row.set('StageRowID', rowId, DotNetType.Guid);
          const order = rows.length !== 0 ? Math.max(...rows.map(x => x.get('Order')), -1) + 1 : 0;
          row.set('Order', order, DotNetType.Int32);
          rows.push(row);
        }
      };
    });

    grid.rowEditorClosed.add(_ => {
      if (approversControlDisposer) {
        approversControlDisposer();
      }
    });

    grid.rowValidating.add(e => {
      const multiplePerformerControl = e.rowModel!.controls.get('MultiplePerformersTableAC');
      if (
        !multiplePerformerControl ||
        multiplePerformerControl.controlVisibility !== Visibility.Visible
      ) {
        return;
      }

      if (
        KrStageTemplateUIExtension.hasSqlQuery(e.row) &&
        e.row.isChanged('SqlApproverRole') &&
        !KrStageTemplateUIExtension.hasComputedRole(e.cardModel.card, e.row.rowId)
      ) {
        e.validationResult.add(
          ValidationResultType.Warning,
          LocalizationManager.instance.localize('$CardTypes_Validators_SQLApproversRoleMissed')
        );
      }
    });
  }

  private static hasSqlQuery(row: CardRow): boolean {
    return !!row.get('SqlApproverRole');
  }

  private static hasComputedRole(card: Card, stageRowId: guid): boolean {
    const sectionAlias =
      card.typeId === '4a377758-2366-47e9-98ac-c5f553974236'
        ? 'KrPerformersVirtual'
        : 'KrPerformersVirtual_Synthetic';

    const section = card.sections.tryGet(sectionAlias);
    if (!section) {
      return false;
    }

    return section.rows.some(row =>
      KrStageTemplateUIExtension.hasComputedRoleInRow(row, stageRowId)
    );
  }

  private static hasComputedRoleInRow(row: CardRow, stageRowId: guid): boolean {
    const srid = row.get('StageRowID');
    if (srid !== stageRowId) {
      return false;
    }

    const approverId = row.get('PerformerID');
    if (approverId !== 'cd4d4a0d-414f-478d-a226-319aa8417f88') {
      // SqlApproverRoleID
      return false;
    }

    return row.state !== CardRowState.Deleted;
  }
}
