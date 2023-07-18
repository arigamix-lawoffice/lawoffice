namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    public interface IRouteCardTypeExtensionPolicy: IExtensionPolicy
    {
        bool IsAllowed(RouteCardType routeCardType);
    }
}