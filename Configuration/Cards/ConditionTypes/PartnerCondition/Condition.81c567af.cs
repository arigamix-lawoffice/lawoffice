var card = await context.GetCardAsync();

if(card is null
	|| !card.Sections.TryGetValue("DocumentCommonInfo", out var dci))
{
	return false;
}

var partnerID = dci.RawFields.TryGet<Guid?>("PartnerID");

return context.Settings["KrPartnerCondition"].Rows.Any(row => 
{
	return partnerID == row.TryGet<Guid>("PartnerID");
});