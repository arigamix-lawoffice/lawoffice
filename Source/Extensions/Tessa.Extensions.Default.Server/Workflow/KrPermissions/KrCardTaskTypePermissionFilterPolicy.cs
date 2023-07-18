using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Политика фильтрации расширений, использующая политику <see cref="ICardTaskTypePolicy"/> для того,
    /// чтобы не выполнять методы расширений, для которых в контексте <see cref="ITaskPermissionsExtensionContext"/>
    /// использован тип задания, запрещённый указанной политикой, или тип задания неизвестен.
    /// 
    /// Если политика <see cref="ICardTaskTypePolicy"/> не зарегистрирована, то метод расширения выполняется.
    /// </summary>
    public sealed class KrCardTaskTypePermissionFilterPolicy : IFilterPolicy
    {
        public static KrCardTaskTypePermissionFilterPolicy Instance { get; } = new KrCardTaskTypePermissionFilterPolicy();

        private KrCardTaskTypePermissionFilterPolicy()
        {
        }

        /// <inheritdoc />
        public ValueTask<object> GetFilterContextAsync(
            ExtensionBuildKey buildKey,
            ExtensionExecutionKey executionKey,
            IExtensionPolicyContainer policies,
            object extensionContext) =>
            new ValueTask<object>(extensionContext is ITaskPermissionsExtensionContext context ? context.TaskType : null);

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
            var policy = policies.TryResolve<ICardTaskTypePolicy>();
            if (policy is null || policy.IsAllowAny)
            {
                return new ValueTask<bool>(true);
            }

            if (filterContext is CardType cardType)
            {
                return new ValueTask<bool>(policy.IsAllowed(cardType));
            }

            return new ValueTask<bool>(false);
        }
    }
}
