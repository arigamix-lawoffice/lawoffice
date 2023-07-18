import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { CreateCardExtension, CreateCardInitializeExtension } from './createCardExtension';
import {
  CustomFolderViewExtension,
  CustomFolderInitializeExtension
} from './customFolderViewExtension';
import { FilterViewDialogDescriptorRegistry } from './filterViewDialogDescriptorRegistry';
import { FilterViewDialogDescriptors } from './filterViewDialogDescriptors';
import { FilterViewDialogOverrideWorkplaceComponentExtension } from './filterViewDialogOverrideWorkplaceComponentExtension';
import { TreeViewItemTestExtension } from './treeViewItemTestExtension';
import { ViewsContextMenuExtension } from './viewsContextMenuExtension';
import { TagsWorkplaceViewDemoActionExtension } from './tagsWorkplaceViewDemoActionExtension';

ExtensionContainer.instance.registerExtension({
  extension: CreateCardExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: CreateCardInitializeExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: FilterViewDialogOverrideWorkplaceComponentExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: CustomFolderInitializeExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: CustomFolderViewExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: TreeViewItemTestExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ViewsContextMenuExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: TagsWorkplaceViewDemoActionExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

FilterViewDialogDescriptorRegistry.instance.register(
  '23d03a10-e610-442d-9e8d-714ff05829f4',
  FilterViewDialogDescriptors.cars
);
