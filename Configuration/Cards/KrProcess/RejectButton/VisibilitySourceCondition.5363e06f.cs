#script

if(this.HasKrComponents(KrComponents.Routes))
{
	var card = await this.GetCardObjectAsync();
	
	return !(await KrProcessSharedHelper.TryGetKrTypeAsync(
		this.Resolve<IKrTypesCache>(),
		card,
		card.TypeID,
		validationResult: this.ValidationResult,
		validationObject: this,
		cancellationToken: this.CancellationToken)).UseRoutesInWorkflowEngine;
}

return default;