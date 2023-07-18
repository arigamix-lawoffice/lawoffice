import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { IViewColumnMetadata, ViewMetadataSealed } from 'tessa/views/metadata';
import { ViewControlViewModel } from 'tessa/ui/cards/controls';
import { IUIContext, LoadingOverlay, showNotEmpty, UIContext } from 'tessa/ui';
import { DoubleClickInfo } from 'tessa/ui/views';
import { createTypedField, DotNetType, Guid } from 'tessa/platform';
import { KrToken } from 'tessa/workflow';
import { deserializeFromTypedToPlain } from 'tessa/platform/serialization';
import { Card, CardGetRestrictionFlags, CardTasksEditorDialogTypeID } from 'tessa/cards';
import { userSession } from 'common/utility';
import { CardGetRequest, CardService, CardStoreRequest } from 'tessa/cards/service';
import { ScopeContextInstance } from 'tessa/platform/scopes';
import { TaskAssignedRolesDialogHelper } from 'tessa/ui/cards/tasks';

export class KrCardTasksEditorUIExtension extends CardUIExtension {
  private static readonly viewControlName = 'CardTasks';
  private static readonly taskIDPrefixReference = 'Task';
  private static readonly cardIDColumnName = 'ID';
  private static readonly tokenParamName = 'Token';

  private async attachDoubleClickHandlerAsync(
    uiContext: IUIContext,
    cardModel: ICardModel
  ): Promise<void> {
    // пытаемся получить контрол по имени
    const viewModel = cardModel.controls.get(
      KrCardTasksEditorUIExtension.viewControlName
    ) as ViewControlViewModel;
    if (!viewModel) {
      return;
    }

    // определяем колонки с идентификатором карточки и задания
    // Если не получилось - дальше не идём
    const mapping = KrCardTasksEditorUIExtension.tryMapView(viewModel);
    if (!mapping) {
      return;
    }

    // присоединяем наш обработчик
    viewModel.doubleClickAction = async doubleClick => {
      await this.doubleClickHandler(uiContext, viewModel, doubleClick, mapping);
    };
  }

  private async doubleClickHandler(
    uiContext: IUIContext,
    viewControl: ViewControlViewModel,
    clickInfo: DoubleClickInfo,
    mapping: IViewMapping
  ): Promise<void> {
    const row = clickInfo.selectedObject;
    if (!row || !clickInfo.view) {
      return;
    }

    const taskRowId = mapping.taskIdName && row.get(mapping.taskIdName);
    const cardId = mapping.cardIdName && row.get(mapping.cardIdName);
    if (
      !taskRowId ||
      !cardId ||
      Guid.equals(taskRowId, Guid.empty) ||
      Guid.equals(cardId, Guid.empty)
    ) {
      return;
    }

    const param = viewControl?.parameters.parameters.find(
      p => p.name === KrCardTasksEditorUIExtension.tokenParamName
    );
    if (!param || param.criteriaValues.length === 0) {
      return;
    }

    const token = param.criteriaValues[0].values[0]?.value;
    if (!token) {
      return;
    }

    await LoadingOverlay.instance.show(async splashResolve => {
      let uiContextScope: ScopeContextInstance<IUIContext> | null = null;
      try {
        uiContextScope = UIContext.create(uiContext);
        const serializedToken = new KrToken(deserializeFromTypedToPlain(token));
        let card: Card | null = null;

        await TaskAssignedRolesDialogHelper.showTaskAssignedRolesEditorDialog(
          uiContext.cardEditor?.cardModel?.generalMetadata ?? null,
          taskRowId,
          userSession.UserID,
          cardTask => Promise.resolve([true, cardTask]),
          async () => {
            const info = { '.TaskRowID': createTypedField(taskRowId, DotNetType.Guid) };
            serializedToken.setInfo(info);

            const getRequest = new CardGetRequest();
            getRequest.cardId = cardId;
            getRequest.restrictionFlags =
              CardGetRestrictionFlags.RestrictFileSections |
              CardGetRestrictionFlags.RestrictFiles |
              CardGetRestrictionFlags.RestrictSections |
              CardGetRestrictionFlags.RestrictTaskCalendar |
              CardGetRestrictionFlags.RestrictTaskHistory |
              CardGetRestrictionFlags.RestrictTaskSections |
              CardGetRestrictionFlags.RestrictTasks;
            getRequest.info = info;

            const getResponse = await CardService.instance.get(getRequest);
            splashResolve();

            await showNotEmpty(getResponse.validationResult.build());
            if (!getResponse.validationResult.isSuccessful) {
              return null;
            }

            card = getResponse.card;

            return card.tasks.find(t => t.rowId === taskRowId) ?? null;
          },
          async () => {
            const storeRequest = new CardStoreRequest();
            storeRequest.card = card!;

            const storeResponse = await CardService.instance.store(storeRequest);

            await showNotEmpty(storeResponse.validationResult.build());
            if (!storeResponse.validationResult.isSuccessful) {
              return false;
            }

            const mainCardContext = uiContext.parent;
            if (mainCardContext?.cardEditor) {
              await mainCardContext.cardEditor.refreshCard(mainCardContext);
            }

            return true;
          }
        );

        await viewControl.refresh();
      } catch (e) {
        console.error(e);
      } finally {
        uiContextScope?.dispose();
      }
    });
  }

  private static tryMapView(viewModel: ViewControlViewModel): IViewMapping | null {
    const viewMetadata = viewModel.viewMetadata;
    if (!viewMetadata) {
      return null;
    }

    // пытаемся найти нужную ссылку
    const reference = viewMetadata.references.get(
      KrCardTasksEditorUIExtension.taskIDPrefixReference
    );
    if (!reference) {
      return null;
    }

    const taskIdColumnAlias = KrCardTasksEditorUIExtension.tryGetColumnAlias(
      viewMetadata,
      reference.colPrefix
    );
    const cardIdColumnAlias = KrCardTasksEditorUIExtension.tryGetColumnAlias(
      viewMetadata,
      null,
      KrCardTasksEditorUIExtension.cardIDColumnName
    );

    if (taskIdColumnAlias && cardIdColumnAlias) {
      return {
        taskIdName: taskIdColumnAlias,
        cardIdName: cardIdColumnAlias
      };
    }
    return null;
  }

  private static tryGetColumnAlias(
    viewMetadata: ViewMetadataSealed,
    prefixReference: string | null = null,
    columnName: string | null = null
  ): string | null {
    if (viewMetadata == null) {
      return null;
    }

    let column: IViewColumnMetadata | undefined = undefined;
    if (prefixReference) {
      column =
        viewMetadata.columns.get(prefixReference + 'ID') ??
        viewMetadata.columns.get(prefixReference + 'RowID');
    } else if (columnName) {
      column = viewMetadata.columns.get(columnName);
    }

    return column?.alias ?? null;
  }

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return context.card.typeId === CardTasksEditorDialogTypeID;
  }

  public initialized(context: ICardUIExtensionContext): Promise<void> {
    context.model.mainFormWithTabs!.tabsAreCollapsed = true;

    return this.attachDoubleClickHandlerAsync(context.uiContext, context.model);
  }
}

interface IViewMapping {
  taskIdName: string | null;
  cardIdName: string | null;
}
