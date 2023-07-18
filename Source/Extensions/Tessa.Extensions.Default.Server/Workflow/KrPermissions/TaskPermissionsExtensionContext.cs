using Tessa.Cards;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="ITaskPermissionsExtensionContext" />
    public sealed class TaskPermissionsExtensionContext : KrPermissionsManagerContext, ITaskPermissionsExtensionContext
    {
        #region Constructors

        public TaskPermissionsExtensionContext(IKrPermissionsManagerContext managerContext)
            : base(managerContext)
        {
        }

        #endregion

        #region ITaskPermissionsExtensionContext Implementation

        /// <inheritdoc />
        public CardTask Task { get; set; }

        /// <inheritdoc />
        public CardType TaskType { get; set; }

        #endregion
    }
}
