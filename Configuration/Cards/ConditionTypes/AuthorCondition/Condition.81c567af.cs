var card = await context.GetCardAsync();

if(card is null
	|| !card.Sections.TryGetValue("DocumentCommonInfo", out var dci))
{
	return false;
}

var authorID = dci.RawFields.TryGet<Guid?>("AuthorID");

return context.Settings["KrUsersCondition"].Rows.Any(row => 
{
	return row.TryGet<Guid>("UserID") == authorID;
});