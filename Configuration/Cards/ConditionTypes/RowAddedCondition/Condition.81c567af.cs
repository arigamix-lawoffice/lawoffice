var callerCard = context.StoreCard;
var cancellationToken = context.CancellationToken;

if (callerCard is null)
{
	return false;
}

var settings = context.Settings["SectionChangedCondition"];
var sectionID = settings.RawFields.TryGet<Guid?>("SectionID") ?? default;
if (sectionID == default)
{
	return false;
}

var cardMetadata = await context.Container.Resolve<ICardMetadata>().GetMetadataForTypeAsync(callerCard.TypeID, cancellationToken);

var sections = await cardMetadata.GetSectionsAsync(context.CancellationToken);
if (!sections.TryGetValue(sectionID, out var sectionMeta)
    || sectionMeta.SectionType == CardSectionType.Entry
    || !callerCard.Sections.TryGetValue(sectionMeta.Name, out var section))
{
     // В типе карточки нет такой секции или в изменениях карточки нет этой секции или секция не коллекционная, то триггер не выполняем.
    return false;
}

return section.Rows.Any(x => x.State == CardRowState.Inserted);