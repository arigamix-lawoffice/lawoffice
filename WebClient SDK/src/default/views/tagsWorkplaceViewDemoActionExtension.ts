import { userSession } from 'common/utility';
import moment from 'moment';
import { LocalizationManager } from 'tessa/localization';
import { Tag } from 'tessa/tags';
import { MenuAction, getBasedRgbaFromDecimal, getRgbaFromDecimal, showNotEmpty } from 'tessa/ui';
import { TagManager, selectOrCreateTag } from 'tessa/ui/tags';
import { TagClickMode } from 'tessa/ui/tags/tagClickMode';
import { openTagCards } from 'tessa/ui/tags/tagMenuHelper';
import {
  IViewContextMenuContext,
  IWorkplaceViewComponent,
  StandardViewComponentContentItemFactory
} from 'tessa/ui/views';
import {
  BaseButtonViewModel,
  ContentPlaceArea,
  ContentPlaceOrder,
  TableGridViewModelBase
} from 'tessa/ui/views/content';
import { GridRowTagViewModel } from 'tessa/ui/views/content/gridRowTagViewModel';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { ViewColumnMetadataSealed, ViewReferenceMetadataSealed } from 'tessa/views/metadata';

export class TagsWorkplaceViewDemoActionExtension extends WorkplaceViewComponentExtension {
  getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.TagsWorkplaceViewDemoActionExtension';
  }

  private table: TableGridViewModelBase | null;

  initialize(model: IWorkplaceViewComponent): void {
    model.contentFactories.set(
      'TagsWorkplaceViewDemoActionExtension',
      c => new TagsWorkplaceViewDemoActionExtensionViewModel(c, this.table, this.addTag)
    );

    const tableFactory = model.contentFactories.get(StandardViewComponentContentItemFactory.Table);
    if (!tableFactory) {
      return;
    }
    model.contentFactories.set(StandardViewComponentContentItemFactory.Table, c => {
      const table = tableFactory(c);
      if (table instanceof TableGridViewModelBase) {
        this.table = table;
      }
      return table;
    });

    model.contextMenuGenerators.push(ctx => {
      this.createAddTagMenuAction(ctx, model);
    });
  }

  //#region private methods

  private createAddTagMenuAction(ctx: IViewContextMenuContext, model: IWorkplaceViewComponent) {
    ctx.menuActions.push(
      new MenuAction(
        `AddTag`,
        `${LocalizationManager.instance.localize('$Tags_ContextMenu_Add')}`,
        'ta icon-thin-020',
        () => {
          this.addTag(model, this.table);
        },
        null,
        false
      )
    );
  }

  public async addTag(
    viewComponent: IWorkplaceViewComponent,
    table: TableGridViewModelBase | null
  ): Promise<void> {
    const viewMeta = viewComponent.viewMetadata!;
    let cardRef: ViewReferenceMetadataSealed;
    let refColumn: ViewColumnMetadataSealed;
    if (
      (cardRef = Array.from(viewMeta.references.values()).find(x => x.isCard)!) &&
      (refColumn = viewMeta.columns.get(cardRef.colPrefix + 'ID')!)
    ) {
      let cardId = viewComponent.selectedRows![0]?.get(refColumn.alias);
      const tagInfo = await selectOrCreateTag(cardId);

      if (!tagInfo) {
        return;
      }

      for (const row of viewComponent.selectedRows!) {
        cardId = row.get(refColumn.alias);
        if (!cardId) {
          continue;
        }

        const tag = new Tag();
        tag.cardId = cardId;
        tag.tagId = tagInfo.id;
        tag.userId = userSession.UserID;
        tag.setAt = moment.utc().format();
        const result = await TagManager.instance.storeTag(tag);
        let cardTags = viewComponent.tags.get(cardId);
        if (!cardTags) {
          cardTags = [];
          viewComponent.tags.set(cardId, cardTags);
        }

        // Возможно, тег был удален раньше, и теперь присутствует в виде удаленного с возможностью восстановления.
        // Если это так, то удаляем из контрола и добавляем заново.
        const existingTagIndex = cardTags.findIndex(x => x.id == tag.tagId);
        if (existingTagIndex >= 0) {
          cardTags.splice(existingTagIndex, 1);
        }

        const model = new GridRowTagViewModel({
          id: tagInfo.id,
          isDeleted: false,
          cardId: cardId,
          tooltip: tagInfo.isCommon
            ? LocalizationManager.instance.format('$Tags_Common_Tooltip', tagInfo.name)
            : tagInfo.name,
          name: tagInfo.name,
          icon: tagInfo.isCommon ? 'icon-thin-195' : undefined,
          clickMode: tagInfo.clickMode,
          onClick:
            tagInfo.clickMode === TagClickMode.GoToTag
              ? async e => {
                  if (!e.ctrlKey) {
                    e.stopPropagation();
                    e.preventDefault();
                    openTagCards(tagInfo.name, tagInfo.id);
                  }
                }
              : null,
          style: {
            background: getRgbaFromDecimal(tagInfo.background),
            color:
              tagInfo.foreground != null
                ? getRgbaFromDecimal(tagInfo.foreground)
                : getBasedRgbaFromDecimal(tagInfo.background)
          }
        });
        cardTags.push(model);
        await showNotEmpty(result);
      }
      table?.rebuild();
    }
  }

  //#endregion
}

class TagsWorkplaceViewDemoActionExtensionViewModel extends BaseButtonViewModel {
  //#region ctor

  constructor(
    viewComponent: IWorkplaceViewComponent,
    table: TableGridViewModelBase | null,
    addTag: (
      viewComponent: IWorkplaceViewComponent,
      table: TableGridViewModelBase | null
    ) => Promise<void>,
    area: ContentPlaceArea = ContentPlaceArea.ToolBarPanel,
    order: number = ContentPlaceOrder.Middle
  ) {
    super(viewComponent, area, order);

    this.icon = 'icon-thin-020';
    this.toolTip = LocalizationManager.instance.localize('$Tags_UI_AddTagButton_Tooltip');
    this.onClick = async () => {
      if (!this.viewComponent.selectedRow || !this.viewComponent.view) {
        return;
      }
      await addTag(viewComponent, table);
    };
  }

  //#endregion
}
