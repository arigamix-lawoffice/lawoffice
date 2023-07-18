using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Политика фильтрации расширений, использующая политику <see cref="ICardTypePolicy"/> для того,
    /// чтобы не выполнять методы расширений, для которых в контексте <see cref="IKrPermissionsManagerContext"/>
    /// использовано имя типа карточки, запрещённое указанной политикой, или тип карточки неизвестен.
    /// 
    /// Если политика <see cref="ICardTypePolicy"/> не зарегистрирована, то метод расширения выполняется.
    /// </summary>
    public sealed class KrCardTypePermissionFilterPolicy : IFilterPolicy
    {
        public static KrCardTypePermissionFilterPolicy Instance { get; } = new KrCardTypePermissionFilterPolicy();

        private KrCardTypePermissionFilterPolicy()
        {
        }

        /// <inheritdoc />
        public ValueTask<object> GetFilterContextAsync(
            ExtensionBuildKey buildKey,
            ExtensionExecutionKey executionKey,
            IExtensionPolicyContainer policies,
            object extensionContext) =>
            new ValueTask<object>(extensionContext is IKrPermissionsManagerContext context ? context.CardType : null);

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
            var policy = policies.TryResolve<ICardTypePolicy>();
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
