Card card = await context.GetCardAsync();
if (!card.Sections.TryGetValue("KrApprovalHistoryVirtual", out CardSection approvalHistorySection)
    || approvalHistorySection.Rows.Count == 0)
{
    var db = context.DbScope.Db;
    var builder = context.DbScope.BuilderFactory;
    
    return await db.SetCommand(
        builder
            .Select().Top(1).V(true)
            .From("KrApprovalCommonInfo", "kr").NoLock()
            .InnerJoin("KrApprovalHistory", "krh").NoLock().On().C("kr", "ID").Equals().C("krh", "ID")
            .Where().C("kr", "MainCardID").Equals().P("CardID")
            .Limit(1)
            .Build(),
        db.Parameter("CardID", card.ID))
        .LogCommand()
        .ExecuteAsync<bool?>(context.CancellationToken) ?? false;
}

return true;