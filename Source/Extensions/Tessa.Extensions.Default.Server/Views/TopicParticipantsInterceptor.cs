using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Platform.Validation;
using Tessa.Views;

namespace Tessa.Extensions.Default.Server.Views
{
    public sealed class TopicParticipantsInterceptor : ViewInterceptorBase
    {
        #region Private Fields

        private readonly IForumPermissionsProvider forumPermissionsProvider;

        #endregion

        #region Constructor

        public TopicParticipantsInterceptor(IForumPermissionsProvider forumPermissionsProvider)
            : base(new[] { ForumHelper.TopicParticipantsView })
            => this.forumPermissionsProvider = forumPermissionsProvider;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!this.InterceptedViews.TryGetValue(
                    request.ViewAlias ?? throw new InvalidOperationException("View alias isn't specified."),
                    out ITessaView view))
            {
                throw new InvalidOperationException($"Can't find view with alias:'{request.ViewAlias}'");
            }

            if (request.TryGetParameter(ForumHelper.TopicIDParam)?.CriteriaValues.FirstOrDefault()?.Values.FirstOrDefault()?.Value is not Guid topicId)
            {
                return TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
            }

            (ParticipantModel participant, ValidationResult validationResult) = await this.forumPermissionsProvider.ResolveUserPermissionsAsync(
                topicId,
                checkSuperModeratorMode: true,
                cancellationToken: cancellationToken);

            if (participant is null || !validationResult.IsSuccessful)
            {
                return TessaViewResult.CreateEmpty(await view.GetMetadataAsync(cancellationToken));
            }

            return await view.GetDataAsync(request, cancellationToken);
        }

        #endregion
    }
}
