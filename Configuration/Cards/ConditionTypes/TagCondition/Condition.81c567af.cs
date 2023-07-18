#using Tessa.Tags;

IReadOnlyCollection<Guid> cardTagGuids = null;
if (context.Info.TryGetValue("TagConditionIDList", out var tagConditionIDListObj)
    && tagConditionIDListObj is IReadOnlyCollection<Guid> tagConditionIDList)
{
	cardTagGuids = tagConditionIDList;
}
else 
{
	var tagManager = context.Container.Resolve<ITagManager>();
	cardTagGuids = (await tagManager.GetTagsAsync(context.CardID, context.ValidationResult, context.CancellationToken)).Select(x => x.ID).ToList();
	context.Info["TagConditionIDList"] = cardTagGuids;
}

foreach (var row in context.Settings["TagCondition"].Rows) {
	var tagID = row.TryGet<Guid?>("TagID");
	if (!tagID.HasValue || tagID == Guid.Empty) {
		continue;
	}
	if (cardTagGuids.Contains(tagID.Value)) {
		return true;
	}
}

return false;