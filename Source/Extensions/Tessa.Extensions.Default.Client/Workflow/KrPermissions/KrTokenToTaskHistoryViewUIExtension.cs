using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    /// <summary>
    /// Устанавливает в параметре <see cref="CardTypeExtensionSettings.TokenParameterAlias"/> представления, указанного по ключу <see cref="CardTypeExtensionSettings.ViewControlAlias"/>, информацию о токене безопасности <see cref="KrToken"/>, выданного для карточки.
    /// </summary>
    public sealed class KrTokenToTaskHistoryViewUIExtension :
        CardUIExtension
    {
        #region Constructors

        public KrTokenToTaskHistoryViewUIExtension() { }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initializing(ICardUIExtensionContext context)
        {
            var result = await CardHelper
                .ExecuteTypeExtensionsAsync(
                    CardTypeExtensionTypes.MakeViewTaskHistory,
                    context.Card,
                    context.Model.CardMetadata,
                    ExecuteInitializingAsync,
                    context,
                    cancellationToken: context.CancellationToken);

            context.ValidationResult.Add(result);
        }

        #endregion

        #region Type extension methods

        private static Task ExecuteInitializingAsync(ITypeExtensionContext typeContext)
        {
            var context = (ICardUIExtensionContext) typeContext.ExternalContext;
            var settings = typeContext.Settings;
            var viewControlAlias = settings.TryGet<string>(CardTypeExtensionSettings.ViewControlAlias);

            context.Model.ControlInitializers.Add((control, m, r, ct) =>
            {
                if (control is CardViewControlViewModel viewControl)
                {
                    if (viewControl.Name == viewControlAlias)
                    {
                        var token = KrToken.TryGet(context.Card.Info);
                        var tokenParameterAlias = settings.TryGet<string>(CardTypeExtensionSettings.TokenParameterAlias);
                        if (token is not null && !string.IsNullOrWhiteSpace(tokenParameterAlias))
                        {
                            var tokenString = token.ToTypedJson();

                            var tokenMetadata = viewControl.ViewMetadata.Parameters.FindByName(tokenParameterAlias);
                            if (tokenMetadata is not null)
                            {
                                var tokenParameter = new RequestParameterBuilder()
                                    .WithMetadata(tokenMetadata)
                                    .AddCriteria(new EqualsCriteriaOperator(), tokenString, tokenString)
                                    .AsRequestParameter();
                                viewControl.Parameters.Add(tokenParameter);
                            }
                        }
                    }
                }

                return new ValueTask();
            });

            return Task.CompletedTask;
        }

        #endregion
    }
}
