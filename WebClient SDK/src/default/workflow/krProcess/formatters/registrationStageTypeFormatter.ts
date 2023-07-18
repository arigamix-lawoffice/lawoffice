import {
  KrStageTypeFormatter,
  IKrStageTypeFormatterContext,
  StageTypeHandlerDescriptor,
  registrationDescriptor
} from 'tessa/workflow/krProcess';

export class RegistrationStageTypeFormater extends KrStageTypeFormatter {
  public descriptors(): StageTypeHandlerDescriptor[] {
    return [registrationDescriptor];
  }

  public format(context: IKrStageTypeFormatterContext) {
    super.format(context);
    context.displaySettings =
      context.stageRow.tryGet('KrRegistrationStageSettingsVirtual__Comment') || '';
  }
}
