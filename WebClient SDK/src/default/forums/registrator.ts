import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { HideForumTabUIExtension } from './hideForumTabUIExtension';
import { TopicsUIExtension } from './topicsUIExtension';
import { OpenForumContextMenuViewExtension } from './openForumContextMenuViewExtension';
import { OpenTopicOnDoubleClickExtension } from './openTopicOnDoubleClickExtension';
import { KrSettingsForumsSettingsUIExtension } from './krSettingsForumsSettingsUIExtension';
import { ForumControlUIExtension } from './forumControlUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: KrSettingsForumsSettingsUIExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 1
});
ExtensionContainer.instance.registerExtension({
  extension: ForumControlUIExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 2
});
ExtensionContainer.instance.registerExtension({
  extension: HideForumTabUIExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 3
});
ExtensionContainer.instance.registerExtension({
  extension: TopicsUIExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 4
});
ExtensionContainer.instance.registerExtension({
  extension: OpenForumContextMenuViewExtension,
  stage: ExtensionStage.Platform,
  order: 5
});
ExtensionContainer.instance.registerExtension({
  extension: OpenTopicOnDoubleClickExtension,
  stage: ExtensionStage.Platform,
  order: 6
});
