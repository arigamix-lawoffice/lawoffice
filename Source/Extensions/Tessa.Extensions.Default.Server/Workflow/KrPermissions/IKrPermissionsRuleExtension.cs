using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение проверки прав по карточке правил доступа.
    /// </summary>
    public interface IKrPermissionsRuleExtension : IExtension
    {
        /// <summary>
        /// Метод для расширения проверки доступа по правилу доступа.
        /// В методе можно проверить дополнительные поля, добавленные через карточки расширения правил доступа.
        /// Если данное правило не подходит, следует установить свойство <see cref="IKrPermissionsRuleExtensionContext.Cancel"/>.
        /// </summary>
        /// <param name="context">Контекст проверки прав доступа в правиле.</param>
        /// <returns>Асинхронная задача.</returns>
        Task CheckRuleAsync(IKrPermissionsRuleExtensionContext context);
    }
}
