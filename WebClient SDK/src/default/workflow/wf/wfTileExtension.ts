import { UIContext } from 'tessa/ui';
import {
  TileExtension,
  ITileGlobalExtensionContext,
  Tile,
  TileGroups,
  enableWhenVisibleInCardHandler,
  TileHotkey,
  ITileLocalExtensionContext,
  TileEvaluationEventArgs
} from 'tessa/ui/tiles';
import { CardStoreMode } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';

/**
 * Плитки для бизнес-процессов Workflow.
 */
export class WfTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext): void {
    const panel = context.workspace.leftPanel;
    const contextSource = panel.contextSource;

    panel.tiles.push(
      new Tile({
        name: 'WfCreateResolution',
        caption: '$WfTiles_CreateResolution',
        icon: 'ta icon-thin-091',
        contextSource,
        command: WfTileExtension.createWfResolutionAction,
        group: TileGroups.Cards,
        order: 50,
        evaluating: enableWhenVisibleInCardHandler,
        toolTip: '$WfTiles_CreateResolution_ToolTip'
      })
    );
  }

  public initializingLocal(context: ITileLocalExtensionContext): void {
    const panel = context.workspace.leftPanel;
    const panelContext = panel.context;
    if (!panelContext.cardEditor || !panelContext.cardEditor.cardModel) {
      return;
    }

    const hotkeyStorage = panel.contextSource.hotkeyStorage;

    const createResolution = panel.tryGetTile('WfCreateResolution');
    if (createResolution) {
      hotkeyStorage.addTileHotkey(
        new TileHotkey(createResolution, 'Ctrl+Alt+R', 'KeyR', { ctrl: true, alt: true })
      );
    }

    const notificationSubscriptions = panel.tryGetTile('NotificationSubscriptions');
    if (notificationSubscriptions) {
      notificationSubscriptions.evaluating.add(WfTileExtension.enableOnCardIsNotTaskCard);
    }

    const createFileTemplate = panel.tryGetTile('CreateFileFromTemplate');
    if (createFileTemplate) {
      createFileTemplate.evaluating.add(WfTileExtension.enableOnCardIsNotTaskCard);
    }

    // const copyLink = panel.tryGetTile('CopyCardLink');
    // if (copyLink) {
    //   copyLink.evaluating.add(e => {
    //     const editor = e.currentTile.context.cardEditor;
    //     e.setIsEnabledWithCollapsing(e.currentTile,
    //       !!editor
    //       && !!editor.cardModel
    //       && (editor.cardModel.cardType.id !== 'de75a343-8164-472d-a20e-4937819760ac' // WfTaskCard
    //         || tryGetFromInfo(editor.cardModel.card.tryGetInfo()!, 'VirtualMainCardID', true)));
    //   });
    // }
  }

  private static async createWfResolutionAction() {
    const context = UIContext.current;
    const editor = context.cardEditor;

    if (!editor || !editor.cardModel) {
      return;
    }

    const cardIsNew = editor.cardModel.card.storeMode === CardStoreMode.Insert;
    if (cardIsNew) {
      const saved = await editor.saveCard(context);
      if (!saved) {
        return;
      }
    }

    editor.saveCard(context, {
      '.startProcess': createTypedField('WfResolution', DotNetType.String)
    });
  }

  private static enableOnCardIsNotTaskCard(e: TileEvaluationEventArgs) {
    const editor = e.currentTile.context.cardEditor;
    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor &&
        !!editor.cardModel &&
        editor.cardModel.cardType.id !== 'de75a343-8164-472d-a20e-4937819760ac'
    ); // WfTaskCard
  }
}
