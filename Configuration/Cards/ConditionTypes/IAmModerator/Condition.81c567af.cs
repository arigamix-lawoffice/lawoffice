#using Tessa.Extensions.Default.Server.Workflow.KrPermissions; 

if(context.Info.TryGetValue("IKrPermissionsManagerContext", out var permissionsManagerContext))
{
	if ((permissionsManagerContext as KrPermissionsManagerContext).Info.TryGetValue("KrForumPermissionsProvider", out var forumPermissionsInfo))
       {
      		if((forumPermissionsInfo as Dictionary<string, object>).TryGetValue("topicID", out var topicID))
             {
             		var db = context.DbScope.Db;
			var builder = context.DbScope.BuilderFactory;
			
			var isModerator = await db.SetCommand(
                    		builder.Select().Top(1)
                           	.C("TypeID").From("FmTopicParticipants").NoLock()
                            	.Where().C("UserID").Equals().P("userID")
                            	.And().C("TopicRowID").Equals().P("topicID").Limit(1).Build(),
                        db.Parameter("userID", context.Session.User.ID),
                        db.Parameter("topicID", topicID))
                        .LogCommand()
                        .ExecuteAsync<int>(context.CancellationToken) == 1 ? true: false;   //1 - Moderator

                        return isModerator;
              }
	}
}
context.ValidationResult.Add(
	Tessa.Forums.ForumValidationKeys.PermissionWarning,
       Tessa.Platform.Validation.ValidationResultType.Warning,
       Tessa.Localization.LocalizationManager.Localize("$Forum_ValidationKey_PermissionWarning"));
return false;