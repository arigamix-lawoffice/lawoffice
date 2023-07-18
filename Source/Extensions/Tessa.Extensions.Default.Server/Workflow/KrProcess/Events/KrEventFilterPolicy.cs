using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    public sealed class KrEventFilterPolicy : IFilterPolicy
    {
        public static readonly KrEventFilterPolicy Instance = new KrEventFilterPolicy();

        private KrEventFilterPolicy()
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
            var policy = policies.TryResolve<IKrEventPolicy>();
            if (policy is null)
            {
                return new ValueTask<bool>(true);
            }

            return new ValueTask<bool>(
                extensionContext is IKrEventExtensionContext ctx
                && policy.IsAllowed(ctx.EventType));
        }
    }
}