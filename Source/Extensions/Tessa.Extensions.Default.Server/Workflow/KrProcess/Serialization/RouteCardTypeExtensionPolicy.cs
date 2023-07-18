using System.Linq;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    public sealed class RouteCardTypeExtensionPolicy: IRouteCardTypeExtensionPolicy
    {
        private readonly RouteCardType[] allowedRouteCardTypes;

        public RouteCardTypeExtensionPolicy(
            RouteCardType[] allowedRouteCardTypes)
        {
            this.allowedRouteCardTypes = allowedRouteCardTypes;
        }


        /// <inheritdoc />
        public bool IsAllowed(
            RouteCardType routeCardType)
        {
            return this.allowedRouteCardTypes.Contains(routeCardType);
        }
    }
}