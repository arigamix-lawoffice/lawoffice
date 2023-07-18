import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';
import { LawCreateCaseViewExtension } from './viewsExtensions/lawCreateCaseViewExtension';
import { LawCaseUIExtension } from './ui/lawCaseUIExtension';
import { LawApplicationExtension } from './application/lawApplicationExtension';
import { LawCaseTileExtension } from './tiles/lawCaseTileExtension';

//#region cardsUIExtensions

ExtensionContainer.instance.registerExtension({
  extension: LawCaseUIExtension,
  stage: ExtensionStage.AfterPlatform
});

//#endregion

//#region fileExtensions

//#endregion

//#region viewsExtensions

ExtensionContainer.instance.registerExtension({
  extension: LawCreateCaseViewExtension,
  stage: ExtensionStage.AfterPlatform
});

//#endregion

//#region taskUIExtensions

//#endregion

//#region tilesExtensions

ExtensionContainer.instance.registerExtension({
  extension: LawCaseTileExtension,
  stage: ExtensionStage.AfterPlatform
});

//#endregion

//#region applicationExtension

ExtensionContainer.instance.registerExtension({
  extension: LawApplicationExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 112
});


//#endregion
