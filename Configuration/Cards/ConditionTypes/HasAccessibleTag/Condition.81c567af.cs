#using Tessa.Tags

Card card = await context.GetCardAsync();
ITagManager tagManager = context.Container.Resolve<ITagManager>();
return await tagManager.HasAccessibleTagsAsync(card.ID, context.ValidationResult, context.CancellationToken);