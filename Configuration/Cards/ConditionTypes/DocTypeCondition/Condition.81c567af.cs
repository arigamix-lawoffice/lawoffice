var card = await context.GetCardAsync().ConfigureAwait(false);

if(card is null
	|| !card.Sections.TryGetValue("DocumentCommonInfo", out var dci))
{
	return false;
}

var docTypeID = dci.RawFields.TryGet<Guid?>("DocTypeID");
var typeID = card.TypeID;

return context.Settings["KrDocTypeCondition"].Rows.Any(row => 
{
	var settingTypeID = row.TryGet<Guid>("DocTypeID");
	return settingTypeID == typeID || settingTypeID == docTypeID;
});