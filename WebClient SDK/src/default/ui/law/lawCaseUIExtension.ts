import { Guid } from 'tessa/platform/guid';
import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Lambda } from 'mobx';
import { getFileViewExtensionInitializationStrategyHandlers } from 'src/default/ui/cardFiles/cardFilesExtensions';
import { TypeExtensionContext } from 'tessa/cards';
import { tryGetFromSettings } from 'tessa/ui';
import { FilesViewControlContentItemsFactory } from 'src/default/ui/cardFiles/filesViewControlContentItemsFactory';
import { TypeInfo } from 'src/law/info/typesInfo';
import { LawClientFilesViewCardControlInitializationStrategy } from './files/lawClientFilesViewCardControlInitializationStrategy';

/**
 * Расширение на карточку "LawCase".
 */
export class LawCaseUIExtension extends CardUIExtension {
  /**
   * Массив диспозеров.
   */
  readonly _dispose: Array<Function | Lambda | null> = [];

  public initializing(context: ICardUIExtensionContext): void {
    if (!Guid.equals(TypeInfo.LawCase.ID, context.card.typeId)) {
      return;
    }

    getFileViewExtensionInitializationStrategyHandlers(context.model)?.push((ctx: TypeExtensionContext, _m: ICardModel) => {
      const settings = ctx.settings;
      const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');
      return filesViewAlias === TypeInfo.LawCase.FileList
        ? new LawClientFilesViewCardControlInitializationStrategy(new FilesViewControlContentItemsFactory())
        : null;
    });
  }
}
