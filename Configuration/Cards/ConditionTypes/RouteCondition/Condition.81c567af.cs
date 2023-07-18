#using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
#using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

var card = await context.GetCardAsync();

if(card is null)
{
	return false;
}

var krTypesCache = context.Container.Resolve<IKrTypesCache>();
var krComponents = await KrComponentsHelper.GetKrComponentsAsync(card.TypeID, krTypesCache, context.CancellationToken);
var settingsFields = context.Settings["KrRouteSettings"].Fields;

IKrType krType = default;

async ValueTask<IKrType> GetKrTypeAsync()
{
	return krType ?? (krType = await KrProcessSharedHelper.TryGetKrTypeAsync(
		krTypesCache,
		card,
		card.TypeID,
		cancellationToken: context.CancellationToken));
}

// Возвращает значение, показывающее, используются ли маршруты.
async ValueTask<bool> IsRoutesUsedAsync(
	KrComponents krComponents)
{
	if(krComponents.Has(KrComponents.Routes))
	{
		return true;
	}
	
	return (await GetKrTypeAsync()).UseApproving;
}

// Возвращает значение, показывающее, активен ли процесс.
async ValueTask<bool> ProcessInactiveAsync(
	IKrScope krScope,
	Guid cardId)
{
	return
		(await krScope.GetKrSatelliteAsync(
			cardId,
			validationResult: context.ValidationResult,
			cancellationToken: context.CancellationToken)).GetStagesSection()
				.Rows
				.All(p => (p.TryGet<int?>(KrConstants.KrStages.StateID) ?? KrStageState.Inactive.ID) == KrStageState.Inactive);
}

if((bool)settingsFields["AllowedRegistration"] == (krComponents.Has(KrComponents.Registration)
	|| (await GetKrTypeAsync()).UseRegistration))
{
	return true;
}

const short routesNotUsed				= 0; // Маршруты не используются.
const short routesUsed					= 1; // Маршруты используются.
const short routesUsedProcessActive		= 2; // Маршруты используются и процесс активен.
const short routesUsedProcessInactive	= 3; // Маршруты используются и процесс не активен.

switch((short?)(int?)settingsFields["RouteModeID"])
{
	case null:
		return true;
	case routesNotUsed:
 		return await IsRoutesUsedAsync(krComponents) == false;
	case routesUsed:
		return await IsRoutesUsedAsync(krComponents);
	case routesUsedProcessActive:
		return await IsRoutesUsedAsync(krComponents)
			&& !await ProcessInactiveAsync(context.Container.Resolve<IKrScope>(), card.ID);
	case routesUsedProcessInactive:
		return await IsRoutesUsedAsync(krComponents)
			&& await ProcessInactiveAsync(context.Container.Resolve<IKrScope>(), card.ID);
}

return false;