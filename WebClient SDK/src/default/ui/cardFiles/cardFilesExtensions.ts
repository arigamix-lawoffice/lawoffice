import { TypeExtensionContext } from 'tessa/cards';
import { tryGetFromInfo } from 'tessa/ui';
import { IViewControlInitializationStrategy } from 'tessa/ui/cards/controls/viewControl/viewControlInitializationStrategy';
import { ICardModel } from 'tessa/ui/cards/interfaces';

type TryGetControlInitializationStrategy = (
  context: TypeExtensionContext,
  cardModel: ICardModel
) => IViewControlInitializationStrategy | null;

export function getFileViewExtensionInitializationStrategyHandlers(
  cardModel: ICardModel
): TryGetControlInitializationStrategy[] {
  return getFileViewExtensionInitializationStrategyHandlersCore(cardModel, false);
}

export function tryGetFileViewExtensionInitializationStrategyHandlers(
  cardModel: ICardModel
): TryGetControlInitializationStrategy[] {
  return getFileViewExtensionInitializationStrategyHandlersCore(cardModel, true);
}

function getFileViewExtensionInitializationStrategyHandlersCore(
  cardModel: ICardModel,
  tryGet: boolean
): TryGetControlInitializationStrategy[] {
  const keyName = 'InitializeFilesView';
  let list = tryGetFromInfo<Array<TryGetControlInitializationStrategy>>(cardModel.info, keyName);
  if (!list && !tryGet) {
    list = [];
    cardModel.info[keyName] = list;
  }
  return list;
}
