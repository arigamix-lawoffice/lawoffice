import { CardTypeExtensionTypes, executeExtensions, TypeExtensionContext } from 'tessa/cards';
import { tryGetFromSettings } from 'tessa/ui';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ViewControlViewModel } from 'tessa/ui/cards/controls';
import { KrToken } from 'tessa/workflow';

export class KrTokenToTaskHistoryViewUIExtension extends CardUIExtension {
  public async initializing(context: ICardUIExtensionContext) {
    const result = await executeExtensions(
      CardTypeExtensionTypes.makeViewTaskHistory,
      context.card,
      context.model.generalMetadata,
      this.executeInitializingAction,
      context
    );

    context.validationResult.add(result);
  }

  executeInitializingAction = async (typeContext: TypeExtensionContext) => {
    const context = typeContext.externalContext as ICardUIExtensionContext;
    const settings = typeContext.settings;
    const viewControlAlias = tryGetFromSettings<string>(settings, 'ViewControlAlias');

    context.model.controlInitializers.push(control => {
      if (control instanceof ViewControlViewModel) {
        if (control.name === viewControlAlias) {
          const token = KrToken.tryGet(context.card.info);
          const tokenParameterAlias = tryGetFromSettings<string>(settings, 'TokenParamterAlias');
          if (token && tokenParameterAlias) {
            // ...
          }
          // KrToken token = KrToken.TryGet(context.Card.Info);
          // var tokenParameterAlias = settings.TryGet<string>(CardTypeExtensionSettings.TokenParameterAlias);
          // if (token != null && !string.IsNullOrWhiteSpace(tokenParameterAlias))
          // {
          //     string tokenString = token.GetStorage().ToSerializable().ToBase64String();
          //     IViewParameterMetadata tokenMetadata = viewControl.ViewMetadata.Parameters.FindByName(tokenParameterAlias);
          //     if (tokenMetadata != null)
          //     {
          //         RequestParameter tokenParameter = new RequestParameterBuilder()
          //             .WithMetadata(tokenMetadata)
          //             .AddCriteria(new EqualsCriteriaOperator(), tokenString, tokenString)
          //             .AsRequestParameter();
          //         viewControl.Parameters.Add(tokenParameter);
          //     }
          // }
        }
      }
    });
  };
}
