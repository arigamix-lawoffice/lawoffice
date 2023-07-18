using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Forums
{
    /// <summary>
    /// Объект, содержащий в себе зависимости для <see cref="IForumPermissionsProvider"/>
    /// </summary>
    public interface IForumPermissionsDependencies
    {
        /// <inheritdoc cref="IKrPermissionsManager"/>
        IKrPermissionsManager KrPermissionsManager { get; }
        
        /// <inheritdoc cref="IKrTokenProvider"/>
        IKrTokenProvider KrTokenProvider { get; }
        
        /// <inheritdoc cref="IForumParticipantProvider"/>
        IForumParticipantProvider ForumParticipantProvider { get; }
        
        /// <inheritdoc cref="IKrPermissionsCacheContainer"/>
        IKrPermissionsCacheContainer KrPermissionsCacheContainer { get; }
        
        /// <inheritdoc cref="ISession"/>
        ISession Session { get; }
        
        /// <inheritdoc cref="IDbScope"/>
        IDbScope DbScope { get; }
    }
}
