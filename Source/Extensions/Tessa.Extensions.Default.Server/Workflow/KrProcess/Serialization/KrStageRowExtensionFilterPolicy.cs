using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    public sealed class KrStageRowExtensionFilterPolicy : IFilterPolicy
    {
        public static readonly KrStageRowExtensionFilterPolicy Instance = new KrStageRowExtensionFilterPolicy();

        private KrStageRowExtensionFilterPolicy()
        {
        }

        /// <inheritdoc />
        public ValueTask<object> GetFilterContextAsync(
            ExtensionBuildKey buildKey,
            ExtensionExecutionKey executionKey,
            IExtensionPolicyContainer policies,
            object extensionContext) =>
            new ValueTask<object>(extensionContext);

        /// <inheritdoc />
        public ValueTask<bool> FilterAsync(
            ExtensionBuildKey buildKey,
            ExtensionResolveKey resolveKey,
            ExtensionExecutionKey executionKey,
            IExtensionPolicyContainer policies,
            IExtension extension,
            object extensionContext,
            object filterContext)
        {
            var cardTypePolicy = policies.TryResolve<ICardTypePolicy>();
            var routeCardTypePolicy = policies.TryResolve<IRouteCardTypeExtensionPolicy>();

            return new ValueTask<bool>(
                extensionContext is IKrStageRowExtensionContext ctx
                && cardTypePolicy?.IsAllowed(ctx.CardType) != false
                && routeCardTypePolicy?.IsAllowed(ctx.RouteCardType) != false);
        }
    }
}