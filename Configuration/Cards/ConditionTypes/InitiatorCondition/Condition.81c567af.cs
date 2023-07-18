if (!(context.Info.TryGetValue("InitiatorID", out var initiatorIDObj)
	&& initiatorIDObj is Guid initiatorID))
{
	var db = context.DbScope.Db;
	var builder = context.DbScope.BuilderFactory;
	
	initiatorID = await db.SetCommand(
		builder.Select().Top(1).C("AuthorID").From("KrApprovalCommonInfo").NoLock().Where().C("MainCardID").Equals().P("ID").Limit(1).Build(),
		db.Parameter("ID", context.CardID))
		.LogCommand()
		.ExecuteAsync<Guid?>(context.CancellationToken) ?? Guid.Empty;
	context.Info["InitiatorID"] = initiatorID;
}

return context.Settings["KrUsersCondition"].Rows.Any(row => 
{
	var settingsUserID = row.TryGet<Guid>("UserID");
	return settingsUserID == initiatorID;
});