using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsRuleExtensionContext" />
    public sealed class KrPermissionsRuleExtensionContext : KrPermissionsManagerContext, IKrPermissionsRuleExtensionContext
    {
        #region Constructors

        public KrPermissionsRuleExtensionContext(
            IKrPermissionsManagerContext context,
            Guid ruleID)
            :base(context)
        {
            this.RuleID = ruleID;
        }

        #endregion

        #region IKrPermissionsRuleExtensionContext Implementation

        /// <inheritdoc />
        public Guid RuleID { get; }

        /// <inheritdoc />
        public bool Cancel { get; set; }

        #endregion
    }
}
