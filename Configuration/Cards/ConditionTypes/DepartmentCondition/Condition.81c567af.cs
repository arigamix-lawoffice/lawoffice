
Task<List<Guid>> GetUserDepartmentsAsync(Guid userID)
{
	var db = context.DbScope.Db;
	var builder = context.DbScope.BuilderFactory;
	
	return db.SetCommand(
		builder.Select().C("ID").From("RoleUsers").NoLock().Where().C("UserID").Equals().P("UserID").And().C("TypeID").Equals().V(2).Build(),
		db.Parameter("UserID", userID))
		.LogCommand()
		.ExecuteListAsync<Guid>(context.CancellationToken);
}

async ValueTask<IEnumerable<Guid>> GetAuthorDepartmentsAsync()
{
	if (context.Info.TryGetValue("AuthorDepartments", out var depsObj)
		&& depsObj is IEnumerable<Guid> deps)
	{
		return deps;
	}
	
	var card = await context.GetCardAsync();
	
	if(card is null
		|| !card.Sections.TryGetValue("DocumentCommonInfo", out var dci))
	{
		return Array.Empty<Guid>();
	}
	
	var authorID = dci.RawFields.TryGet<Guid?>("AuthorID");
	
	if (authorID.HasValue)
	{
		deps = await GetUserDepartmentsAsync(authorID.Value);
	}
	else
	{
		deps = Array.Empty<Guid>();
	}
	context.Info["AuthorDepartments"] = deps;
	return deps;
}

async ValueTask<IEnumerable<Guid>> GetInitiatorDepartmentsAsync()
{
	if (context.Info.TryGetValue("InitiatorDepartments", out var depsObj)
		&& depsObj is IEnumerable<Guid> deps)
	{
		return deps;
	}
	
	var db = context.DbScope.Db;
	var builder = context.DbScope.BuilderFactory;
	
	var initiatorID = await db.SetCommand(
		builder.Select().Top(1).C("AuthorID").From("KrApprovalCommonInfo").NoLock().Where().C("MainCardID").Equals().P("ID").Limit(1).Build(),
		db.Parameter("ID", context.CardID))
		.LogCommand()
		.ExecuteAsync<Guid?>(context.CancellationToken);
	
	if (initiatorID.HasValue)
	{
		deps = await GetUserDepartmentsAsync(initiatorID.Value);
	}
	else
	{
		deps = Array.Empty<Guid>();
	}
	context.Info["InitiatorDepartments"] = deps;
	return deps;
}

List<Guid> settingsDeps = null;
bool CheckDep(Guid? departmentID)
{
	if (!departmentID.HasValue)
	{
		return false;
	}

	if (settingsDeps == null)
	{
		settingsDeps = context.Settings["KrDepartmentCondition"].Rows.Select(x => x.TryGet<Guid>("DepartmentID")).ToList();
	}
	
	return settingsDeps.Contains(departmentID.Value);
}

var settings = context.Settings["KrDepartmentConditionSettings"];
bool checkAuthor = settings.RawFields.Get<bool>("CheckAuthor"),
	 checkInitiator = settings.RawFields.Get<bool>("CheckInitiator"),
	 checkCard = settings.RawFields.Get<bool>("CheckCard");

if (checkCard)
{
	var card = await context.GetCardAsync();
	if (card is not null
		&& card.Sections.TryGetValue("DocumentCommonInfo", out var dci))
	{
		var depID = dci.RawFields.TryGet<Guid?>("DepartmentID");
		if (CheckDep(depID))
		{
			return true;
		}
	}
}

if (checkAuthor)
{
	var deps = await GetAuthorDepartmentsAsync().ConfigureAwait(false);
	
	foreach(var depID in deps)
	{
		if (CheckDep(depID))
		{
			return true;
		}
	}
}

if (checkInitiator)
{
	var deps = await GetInitiatorDepartmentsAsync().ConfigureAwait(false);
	
	foreach(var depID in deps)
	{
		if (CheckDep(depID))
		{
			return true;
		}
	}
}

return false;