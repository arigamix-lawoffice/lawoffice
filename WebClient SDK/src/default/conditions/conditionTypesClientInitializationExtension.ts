import { ApplicationExtension } from 'tessa/applicationExtension';
import { IApplicationExtensionMetadataContext } from 'tessa/applicationExtensionContext';
import { tryGetFromInfo } from 'tessa/ui';
import { ClientConditionTypesProvider, IConditionType } from 'tessa/platform/conditions';

export class ConditionTypesClientInitializationExtension extends ApplicationExtension {
  async afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void> {
    const conditionTypes = tryGetFromInfo<IConditionType[]>(
      _context.response?.info,
      '.ConditionTypes'
    );

    if (!!conditionTypes) {
      ClientConditionTypesProvider.instance.initialize(conditionTypes);
    }
  }
}
