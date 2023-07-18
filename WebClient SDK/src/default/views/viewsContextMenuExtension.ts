import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent, IViewContextMenuContext } from 'tessa/ui/views';
import { MenuAction, SeparatorMenuAction, LoadingOverlay } from 'tessa/ui';
import { ViewReferenceMetadataSealed } from 'tessa/views/metadata';
import { getValueId, getFirstStringValueByPrefix } from 'tessa/views';
import { LocalizationManager } from 'tessa/localization';
import { openCard } from 'tessa/ui/uiHost';

export class ViewsContextMenuExtension extends WorkplaceViewComponentExtension {

  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.ViewsContextMenuExtension';
  }

  public initialize(model: IWorkplaceViewComponent) {
    model.contextMenuGenerators.push(ctx => {
      ViewsContextMenuExtension.createRefreshMenuAction(ctx);
      ViewsContextMenuExtension.createFilterMenuAction(ctx);
      ViewsContextMenuExtension.createClearFilterMenuAction(ctx);
      ViewsContextMenuExtension.createOpenCardMenuAction(ctx);
    });
  }

  private static createRefreshMenuAction(ctx: IViewContextMenuContext) {
    const viewContext = ctx.viewContext;

    ctx.menuActions.push(new MenuAction(
      'Refresh',
      '$UI_Tiles_Refresh',
      'ta icon-thin-412',
      () => { viewContext.refreshView(); },
      null,
      !viewContext.canRefreshView()
    ));
  }

  private static createFilterMenuAction(ctx: IViewContextMenuContext) {
    const viewContext = ctx.viewContext;

    if (viewContext.parameters.metadata.every(p => p.hidden)) {
      return;
    }

    ctx.menuActions.push(new SeparatorMenuAction());
    ctx.menuActions.push(new MenuAction(
      'Filter',
      '$UI_Tiles_Filter',
      'ta icon-thin-100',
      () => { viewContext.filterView(); },
      null,
      !viewContext.canFilterView()
    ));
  }

  private static createClearFilterMenuAction(ctx: IViewContextMenuContext) {
    const viewContext = ctx.viewContext;

    if (viewContext.parameters.parameters.every(p => p.readOnly)) {
      return;
    }

    ctx.menuActions.push(new MenuAction(
      'ClearFilter',
      '$Views_Table_ClearFilter_ToolTip',
      'ta icon-thin-065',
      () => { viewContext.clearFilterView(); },
      null,
      !viewContext.canClearFilterView()
    ));
  }

  private static createOpenCardMenuAction(ctx: IViewContextMenuContext) {
    const viewContext = ctx.viewContext;

    if (viewContext.refSection) {
      return;
    }

    const view = viewContext.view;
    const selectedObject = viewContext.selectedRow;
    if (!selectedObject || !view) {
      return;
    }

    const metadata = view.metadata;
    if (!metadata) {
      return;
    }

    const cardRefs: ViewReferenceMetadataSealed[] = [];
    metadata.references.forEach(ref => {
      if (ref.isCard) {
        cardRefs.push(ref);
      }
    });
    if (cardRefs.length === 0) {
      return;
    }

    let separatorAddided = false;
    const addedIDs: Set<guid> = new Set();
    for (let cardRef of cardRefs) {
      const cardId = getValueId(selectedObject, cardRef.colPrefix!);
      if (!cardId) {
        return;
      }

      const displayValueObj = cardRef.displayValueColumn
        ? selectedObject.get(cardRef.displayValueColumn)
        : getFirstStringValueByPrefix(selectedObject, cardRef.colPrefix!, viewContext.columns);

      const displayValue = displayValueObj || '';

      if (!addedIDs.has(cardId)) {
        addedIDs.add(cardId);
        if (!separatorAddided) {
          ctx.menuActions.push(new SeparatorMenuAction());
          separatorAddided = true;
        }

        const uiContextExecutor = ctx.uiContextExecutor;
        ctx.menuActions.push(new MenuAction(
          `OpenCard_${cardId}`,
          `${LocalizationManager.instance.localize('$UI_Tiles_OpenCard')}: ${LocalizationManager.instance.localize(displayValue)}`,
          'ta icon-thin-013',
          () => {
            LoadingOverlay.instance.show(async (splashResolve) => {
              await uiContextExecutor(async context => {
                await openCard({
                  cardId,
                  displayValue,
                  context,
                  splashResolve
                });
              });
            });
          },
          null,
          false
        ));
      }
    }
  }

}