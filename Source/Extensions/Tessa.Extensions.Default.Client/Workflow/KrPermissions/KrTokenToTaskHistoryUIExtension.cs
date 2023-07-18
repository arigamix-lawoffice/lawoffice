using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Tasks;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    /// <summary>
    /// Устанавливает в параметре <c>Token</c> представления <see cref="SystemViewAliases.TaskHistory"/> информацию о токене безопасности <see cref="KrToken"/>, выданного для карточки.
    /// </summary>
    public sealed class KrTokenToTaskHistoryUIExtension :
        CardUIExtension
    {
        #region Constructors

        public KrTokenToTaskHistoryUIExtension(IKrTypesCache typesCache) =>
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            TaskHistoryViewModel taskHistory;
            if (context.Model.Flags.InSpecialMode()
                || (taskHistory = context.Model.TryGetTaskHistory()) is null
                || (await KrComponentsHelper.GetKrComponentsAsync(context.Card, this.typesCache, context.CancellationToken)).HasNot(KrComponents.Base))
            {
                return;
            }

            var card = context.Model.Card;
            taskHistory.ModifyOpenViewRequestAction += request =>
            {
                var token = KrToken.TryGet(card.Info);
                if (token is not null)
                {
                    var tokenString = token.ToTypedJson();

                    var tokenMetadata = request.TaskHistoryViewMetadata.Parameters.FindByName("Token");
                    if (tokenMetadata is not null)
                    {
                        var tokenParameter = new RequestParameterBuilder()
                            .WithMetadata(tokenMetadata)
                            .AddCriteria(new EqualsCriteriaOperator(), tokenString, tokenString)
                            .AsRequestParameter();

                        request.Parameters.Add(tokenParameter);
                    }
                }
            };
        }

        #endregion
    }
}
