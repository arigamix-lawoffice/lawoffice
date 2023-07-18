import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ForumViewModel } from 'tessa/ui/cards/controls';
import { UIButton, showMessage, MenuAction } from 'tessa/ui';
import { Guid } from 'tessa/platform';
import { TestCardTypeID } from './common';

/**
 * Для выбранной карточки добавляет элементы управления:
 * - В верхней панели текущего обсуждения.
 * - В контекстное меню текущего обсуждения.
 *
 * Результат работы расширения:
 * В верхней панели, а также в контекстном меню текущего обсуждения добавлены
 * соответсвующие тестовые кнопка и пункт контекстного меню для тестовой карточки "Автомобиль".
 */
export class ForumUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти контрол "Обсуждения"
    const forumControl = this.tryGetForumControl(context);
    if (!forumControl) {
      return;
    }

    // добавляем контект в найденный контрол
    forumControl.addOnContentChangedAndInvoke(() => {
      const editor = forumControl.tryGetTopicEditor();
      if (!editor) {
        return;
      }

      // добавляем тестовую кнопку в верхнюю панель текущего обсуждения
      editor.rightButtons.push(
        UIButton.create({
          name: 'TestButton',
          icon: 'icon-thin-100',
          buttonAction: async () => {
            await showMessage('Hello from test button!');
          }
        })
      );

      // добавляем пункт тестовый пункт в контекстном меню текущего обсуждения
      editor.attachmentContextMenuGenerators.push(ctx => {
        ctx.menuActions.push(
          MenuAction.create({
            name: 'TestMenu',
            caption: 'TestMenu',
            icon: 'icon-thin-099',
            action: async () => {
              await showMessage('Hello from test menu!');
            }
          })
        );
      });
    });
  }

  private tryGetForumControl(context: ICardUIExtensionContext): ForumViewModel | null {
    // пытаемся найти вкладку "Обсуждения"
    const forumTab = context.model.forms.find(x => x.name === 'Forum');
    if (!forumTab) {
      return null;
    }

    // пытаемся найти блок "Обсуждения" в полученной вкладке
    const topicsBlock = forumTab.blocks.find(x => x.name === 'Topics');
    if (!topicsBlock) {
      return null;
    }

    // пытаемся найти соответствующий контрол
    const forumControl = topicsBlock.controls[0] as ForumViewModel;
    if (!forumControl || !(forumControl instanceof ForumViewModel)) {
      return null;
    }

    return forumControl;
  }
}
