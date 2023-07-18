#using Tessa.Workflow.Helpful;
#using Tessa.Workflow;
#using Tessa.Extensions.Default.Shared;
#using Tessa.Cards.Extensions.Templates;

// Получаем состояние из скоупа карточек Workflow Engine, если сателлит там был уже загружен
async ValueTask<int?> CalcStateFromWECardsScope()
{
	var weContext = context.GetWorkflowContext();
	if (weContext != null)
	{
		var scope = context.Container.Resolve<IWorkflowEngineCardsScope>();
		var satelliteCardID = await CardSatelliteHelper.TryGetUniversalSatelliteIDAsync(
			context.DbScope,
			context.CardID,
			null,
			DefaultCardTypes.KrSatelliteTypeID,
			context.CancellationToken);
		if (satelliteCardID.HasValue && scope.CardIsLoaded(satelliteCardID.Value))
		{
			var satelliteCard = await scope.GetCardAsync(satelliteCardID.Value, context.ValidationResult, context.CancellationToken);
			var stateID = satelliteCard.Sections["KrApprovalCommonInfo"].RawFields.TryGet<int>("StateID");
			return stateID;
		}
	}
	return null;
}

// Получаем состояние из базы
Task<int?> CalcStateFromDb()
{
	var db = context.DbScope.Db;
	var builder = context.DbScope.BuilderFactory;
	
	return db.SetCommand(
		builder.Select().Top(1).C("StateID").From("KrApprovalCommonInfo").NoLock().Where().C("MainCardID").Equals().P("ID").Limit(1).Build(),
		db.Parameter("ID", context.CardID))
		.LogCommand()
		.ExecuteAsync<int?>(context.CancellationToken);
}

if (!(context.Info.TryGetValue("StateID", out var stateIDObj)
	&& stateIDObj is int stateID))
{
	stateID = (await CalcStateFromWECardsScope())
		?? (await CalcStateFromDb())
		?? 0;
	context.Info["StateID"] = stateID;
}

return context.Settings["KrDocStateCondition"].Rows.Any(row => 
{
	var settingsStateID = row.TryGet<int>("StateID");
	return settingsStateID == stateID;
});