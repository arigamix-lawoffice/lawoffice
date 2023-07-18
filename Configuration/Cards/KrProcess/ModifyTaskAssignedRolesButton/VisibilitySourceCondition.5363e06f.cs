#using Tessa.Extensions.Default.Shared.Workflow.KrPermissions

#script
if (this.CardContext is CardGetExtensionContext context &&
	context.CardType.Flags.HasFlag(CardTypeFlags.AllowTasks) &&
	await this.GetVersionAsync() > 0)
{
	var token = KrToken.TryGet(context.Response.Card.Info);

	return token?.HasPermission(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles) == true;
}

return false;