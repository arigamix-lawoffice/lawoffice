import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { HideBlockByRefValueUIExtension } from './1_hideBlockByRefValueUIExtension';
import { ChangeFieldOrRowUIExtension } from './2_changeFieldOrRowUIExtension';
import { TableSectionChangedUIExtension } from './3_tableSectionChangedUIExtension';
import { HideFormUIExtension } from './4_hideFormUIExtension';
import { HideTaskBlockUIExtension } from './5_hideTaskBlockUIExtension';
import { SimpleCardTileExtension } from './6_simpleCardTileExtension';
import { SimpleViewTileExtension } from './7_simpleViewTileExtension';
import { RequestTileExtension } from './8_requestTileExtension';
import { AdditionalTableButtonUIExtension } from './9_additionalTableButtonUIExtension';
import { DialogModeAutocompleteUIExtension } from './10_dialogModeAutocompleteUIExtension';
import { CustomBPTileExtension } from './11_customBPTileExtension';
import { FileControlUIExtension } from './12_fileControlUIExtension';
import { HideTileExtension } from './13_hideTileExtension';
import { TableControlDoubleClickUIExtension } from './14_tableControlDoubleClickUIExtension';
import { ShowFormDialogUIExtension } from './15_showFormDialogUIExtension';
import { ShowCustomDialogUIExtension } from './16_showCustomDialogUIExtension';
import {
  CloseCardOnCompleteTaskStoreExtension,
  CloseCardOnCompleteTaskUIExtension
} from './17_closeCardOnCompleteTaskExtension';
import {
  AdditionalMetaInitializationExtension,
  AdditionalMetaTileExtension
} from './18_additionalMetaExtension';
import {
  CustomThemePropApplicationExtension,
  CustomThemePropUIExtension
} from './19_customThemeProp';
import { CustomTabPanelButtonUIExtension } from './20_tabPanelButtonUIExtension';
import { ForumUIExtension } from './21_forumUIExtension';
import './22_serviceClient';
import { ExampleLoginExtension } from './23_loginExtension';
import { ExampleFormUIExtension } from './24_formUIExtension';
import { GridBasicStylingExtension } from './25_gridBasicStylingExtension';
import { GridLayoutStylingExtension } from './26_gridLayoutStylingExtension';
import './27_horizontalScrollViews';
import { TaskEnableAttachFilesExampleUIExtension } from './28_taskFiles/28_taskEnableAttachFilesExampleUIExtension';
import { SliderControlApplicationExtension } from './29_sliderControl/29_sliderControlApplicationExtension';
import { SliderMetadataExtension } from './29_sliderControl/29_sliderMetadataExtension';
import { ExamplePreviewerInitialization } from './30_examplePreviewer/30_examplePreviewerInitialization';
import { ExamplePreviewerCardUIExtension } from './30_examplePreviewer/30_examplePreviewerCardUIExtension';
import { ExampleHeaderApplicationExtension } from './31_exampleHeader/31_exampleHeaderApplicationExtension';
import { ExampleHeaderUIExtension } from './31_exampleHeader/31_exampleHeaderUIExtension';
import { OcrPreviewerUIExtension } from './32_ocrPreviewerUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: HideBlockByRefValueUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ChangeFieldOrRowUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TableSectionChangedUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: HideFormUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: HideTaskBlockUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: SimpleCardTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: SimpleViewTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: RequestTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: AdditionalTableButtonUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DialogModeAutocompleteUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CustomBPTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: FileControlUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: HideTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TableControlDoubleClickUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ShowFormDialogUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ShowCustomDialogUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CloseCardOnCompleteTaskStoreExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CloseCardOnCompleteTaskUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: AdditionalMetaInitializationExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: AdditionalMetaTileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CustomThemePropApplicationExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CustomThemePropUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CustomTabPanelButtonUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ForumUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExampleLoginExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExampleFormUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: SliderControlApplicationExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: SliderMetadataExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: OcrPreviewerUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExamplePreviewerInitialization,
  singleton: true,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExamplePreviewerCardUIExtension,
  singleton: true,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: GridBasicStylingExtension,
  singleton: true,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: GridLayoutStylingExtension,
  singleton: true,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TaskEnableAttachFilesExampleUIExtension,
  singleton: true,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExampleHeaderApplicationExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ExampleHeaderUIExtension,
  stage: ExtensionStage.AfterPlatform
});
