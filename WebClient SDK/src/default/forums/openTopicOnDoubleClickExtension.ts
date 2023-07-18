import { ForumHelper } from 'tessa/forums';
import { showLoadingOverlay } from 'tessa/ui';
import { CardEditorOpeningContext } from 'tessa/ui/cards';
import { openCard } from 'tessa/ui/uiHost';
import { DoubleClickInfo, openCardDoubleClickAction } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views/workplaceViewComponent';

export class OpenTopicOnDoubleClickExtension extends WorkplaceViewComponentExtension {
  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Forums.OpenTopicOnDoubleClickExtension';
  }

  public initializeSettings(model: IWorkplaceViewComponent): void {
    model.doubleClickAction = async (info: DoubleClickInfo) => {
      await openCardDoubleClickAction(info, async (cardId, displayValue, context) => {
        const selectedRow = info.context.viewContext?.selectedRow;
        if (selectedRow) {
          const topicId = selectedRow.get('TopicID');
          const typeId = selectedRow.get('TypeID');
          if (topicId && typeId) {
            await showLoadingOverlay(async () => {
              await openCard({
                cardId,
                displayValue,
                context,
                cardModifierAction: openingContext =>
                  OpenTopicOnDoubleClickExtension.cardModifierAction(
                    openingContext,
                    topicId,
                    typeId
                  )
              });
            });
          }
        }
      });
    };
  }

  private static async cardModifierAction(
    openingContext: CardEditorOpeningContext,
    topicId: string,
    typeId: string
  ) {
    openingContext.card.info[ForumHelper.TopicIDKey] = topicId;
    openingContext.card.info[ForumHelper.TopicTypeIDKey] = typeId;
  }
}
