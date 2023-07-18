import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { KrTilesExtension } from './krTilesExtension';
import { KrEditModeTileExtension } from './krEditModeTileExtension';
import { KrShowHiddenStagesTileExtension } from './krShowHiddenStagesTileExtension';
import { AcquaintanceTileExtension } from './acquaintanceTileExtension';
import { StageSourceBuildTileExtension } from './stageSourceBuildTileExtension';
import { KrTypesAndCreateBasedOnTileExtension } from './krTypesAndCreateBasedOnTileExtension';
import { ExtendedDefaultTypeGroupCaptionsTileExtension } from './extendedDefaultTypeGroupCaptionsTileExtension';
import { TestProcessTileExtension } from './testProcessTileExtension';
import { CreateMultipleTemplateTileExtension } from './createMultipleTemplateTileExtension';
import { KrSettingsTileExtension } from './krSettingsTileExtension';
import { ProhibitTilesInViewsTileExtension } from './prohibitTilesInViewsTileExtension';
import { KrDocStateTileExtension } from './krDocStateTileExtension';
import { DocLoadPrintBarcodeTileExtension } from './docLoadPrintBarcodeTileExtension';
import { KrPermissionsTileExtension } from './krPermissionsTileExtension';
import { KrHideLanguageAndFormattingSelectionTileExtension } from './krHideLanguageAndFormattingSelectionTileExtension';
import { FilterViewDialogOverrideTileExtension } from './filterViewDialogOverrideTileExtension';

// Initialize
ExtensionContainer.instance.registerExtension({
  extension: ExtendedDefaultTypeGroupCaptionsTileExtension,
  stage: ExtensionStage.Initialize,
  singleton: true,
  order: 1
});

// AfterPlatform
ExtensionContainer.instance.registerExtension({
  extension: KrTilesExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 1
});
ExtensionContainer.instance.registerExtension({
  extension: KrEditModeTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 2
});
ExtensionContainer.instance.registerExtension({
  extension: KrShowHiddenStagesTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 3
});
ExtensionContainer.instance.registerExtension({
  extension: AcquaintanceTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 4
});
ExtensionContainer.instance.registerExtension({
  extension: StageSourceBuildTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 5
});
ExtensionContainer.instance.registerExtension({
  extension: KrTypesAndCreateBasedOnTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 6
});
ExtensionContainer.instance.registerExtension({
  extension: TestProcessTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 7
});
ExtensionContainer.instance.registerExtension({
  extension: CreateMultipleTemplateTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 8
});
ExtensionContainer.instance.registerExtension({
  extension: KrSettingsTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 9
});
ExtensionContainer.instance.registerExtension({
  extension: ProhibitTilesInViewsTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 10
});
ExtensionContainer.instance.registerExtension({
  extension: KrDocStateTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 11
});
ExtensionContainer.instance.registerExtension({
  extension: DocLoadPrintBarcodeTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 13
});
ExtensionContainer.instance.registerExtension({
  extension: KrPermissionsTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 14
});
ExtensionContainer.instance.registerExtension({
  extension: KrHideLanguageAndFormattingSelectionTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 15
});
ExtensionContainer.instance.registerExtension({
  extension: FilterViewDialogOverrideTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 16
});
