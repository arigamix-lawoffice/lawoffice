using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Forums
{
    /// <summary>
    /// <inheritdoc cref="IForumPermissionsDependencies" path="/summary"/>
    /// </summary>
    /// <param name="KrPermissionsManager"><inheritdoc cref="IKrPermissionsManager" path="/summary"/></param>
    /// <param name="KrTokenProvider"><inheritdoc cref="IKrTokenProvider" path="/summary"/></param>
    /// <param name="ForumParticipantProvider"><inheritdoc cref="IForumParticipantProvider" path="/summary"/></param>
    /// <param name="KrPermissionsCacheContainer"><inheritdoc cref="IKrPermissionsCacheContainer" path="/summary"/></param>
    /// <param name="Session"><inheritdoc cref="ISession" path="/summary"/></param>
    /// <param name="DbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
    public record ForumPermissionsDependencies(
        IKrPermissionsManager KrPermissionsManager,
        IKrTokenProvider KrTokenProvider,
        IForumParticipantProvider ForumParticipantProvider,
        IKrPermissionsCacheContainer KrPermissionsCacheContainer,
        ISession Session,
        IDbScope DbScope) : IForumPermissionsDependencies;
}
