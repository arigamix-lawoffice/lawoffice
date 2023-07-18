using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Объект, который выполняет проверку прав доступа.
    /// </summary>
    public interface IKrPermissionsManager
    {
        /// <summary>
        /// Список секций, проверка которых игнорируется при проверке прав доступа.
        /// </summary>
        ICollection<string> IgnoreSections { get; }

        /// <summary>
        /// Метод для создания контекста проверки прав доступа.
        /// Метод формирует контекст прав доступа с учетом полученных данных.
        /// Если данных для создания контекста недостаточно, то метод выбросит исключение.
        /// </summary>
        /// <param name="param">
        /// Параметры создания контекста. Обязательно должны быть указаны или <see cref="KrPermissionsCreateContextParams.Card"/>
        /// или <see cref="KrPermissionsCreateContextParams.CardID"/>
        /// или <see cref="KrPermissionsCreateContextParams.CardTypeID"/> с <see cref="KrPermissionsCreateContextParams.DocTypeID"/> (при наличии типа документа)
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает контекст проверки прав доступа или <c>null</c>, если для данного типа карточки проверка не предусмотрена.</returns>
        Task<IKrPermissionsManagerContext> TryCreateContextAsync(
            KrPermissionsCreateContextParams param,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// Метод для расчёта прав доступа.
        /// </summary>
        /// <param name="context">Контекст проверки прав доступа.</param>
        /// <param name="requiredPermissions">Набор проверяемых расширений прав доступа.</param>
        /// <returns>Результат расчёта прав доступа.</returns>
        Task<IKrPermissionsManagerResult> GetEffectivePermissionsAsync(
            IKrPermissionsManagerContext context,
            params KrPermissionFlagDescriptor[] requiredPermissions);

        /// <summary>
        /// Метод для проверки прав доступа.
        /// Если при проверке прав доступа будут обнаружены ошибки, они будут записаны в <paramref name="context"/> 
        /// в свойство <see cref="IKrPermissionsManagerContext.ValidationResult"/>.
        /// </summary>
        /// <param name="context">Контекст проверки прав доступа.</param>
        /// <param name="requiredPermissions">Набор проверяемых расширений прав доступа.</param>
        /// <returns>Возвращает результат проверки, который может содержать дополнительную информацию.</returns>
        Task<KrPermissionsManagerCheckResult> CheckRequiredPermissionsAsync(
            IKrPermissionsManagerContext context,
            params KrPermissionFlagDescriptor[] requiredPermissions);
    }
}
