import { DotNetType, createTypedField } from 'tessa/platform';
import { showLoadingOverlay } from 'tessa/ui';
import { openCard } from 'tessa/ui/uiHost';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { TypeInfo } from '../info/typesInfo';
import { SchemeInfo } from '../info/schemeInfo';
import { ViewInfo } from '../info/viewInfo';

/**
 * The extension responsible for double-clicking on the LawCases view.
 */
export class LawCreateCaseViewExtension extends WorkplaceViewComponentExtension {
  /**
   * The full name of the extension.
   */
  private static nameExtension = 'Tessa.Extensions.Client.Views.LawCreateCaseViewExtension';

  getExtensionName(): string {
    return LawCreateCaseViewExtension.nameExtension;
  }

  initialized(model: IWorkplaceViewComponent) {
    if (model.inSelectionMode()) {
      return;
    }

    model.doubleClickAction = async () => {
      let row = model.selectedRow;
      let caseID = row?.get(ViewInfo.LawCases.ColumnID.Alias);
      if (!caseID) {
        return;
      }

      const info = {};
      info[SchemeInfo.LawCase.ID] = createTypedField(caseID, DotNetType.Guid);
      await showLoadingOverlay(async splashResolve => {
        await openCard({
          cardId: caseID.toString(),
          cardTypeId: TypeInfo.LawCase.ID,
          alwaysNewTab: true,
          info: info,
          splashResolve: splashResolve
        });
      });
    };
  }
}

