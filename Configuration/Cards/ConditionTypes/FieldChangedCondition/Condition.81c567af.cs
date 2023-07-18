var callerCard = context.StoreCard;
var cancellationToken = context.CancellationToken;

if (callerCard is null)
{
	return false;
}

var settings = context.Settings["SectionChangedCondition"];
var fields = context.Settings["FieldChangedCondition"].TryGetRows()?.Select(x => x.TryGet<Guid>("FieldID")).ToArray();
var sectionID = settings.RawFields.TryGet<Guid?>("SectionID") ?? default;
if (sectionID == default)
{
	return false;
}

var cardMetadata = await context.Container.Resolve<ICardMetadata>().GetMetadataForTypeAsync(callerCard.TypeID, cancellationToken);

var sections = await cardMetadata.GetSectionsAsync(context.CancellationToken);
if (!sections.TryGetValue(sectionID, out var sectionMeta)
    || !callerCard.Sections.TryGetValue(sectionMeta.Name, out var section))
{
    // В типе карточки нет такой секции или в изменениях карточки нет этой секции, то тригер не выполняем.
    return false;
}

bool isEntry = section.Type == CardSectionType.Entry;
if (fields != null
    && fields.Length > 0)
{
    if (isEntry)
    {
        foreach (var field in fields)
        {
            if (sectionMeta.Columns.TryGetValue(field, out var fieldMeta)
                && section.RawFields.ContainsKey(fieldMeta.Name))
            {
                return true;
            }
        }
    }
    else
    {
        foreach (var row in section.Rows)
        {
            if (row.State == CardRowState.Deleted
                || row.State == CardRowState.None)
            {
                continue;
            }

            foreach (var field in fields)
            {
                if (sectionMeta.Columns.TryGetValue(field, out var fieldMeta)
                    && row.ContainsKey(fieldMeta.Name))
                {
                    return true;
                }
            }
        }
    }
}
else
{
    return isEntry
        ? section.RawFields.Count > 0
        : section.Rows.Any(x => x.State != CardRowState.None && x.State != CardRowState.Deleted);
}

return false;