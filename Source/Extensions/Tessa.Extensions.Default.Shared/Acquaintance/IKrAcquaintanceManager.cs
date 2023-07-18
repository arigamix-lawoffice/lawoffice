using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Acquaintance
{
    /// <summary>
    /// Интерфейс менеджера для отправки массового ознакомления.
    /// Доступен как на сервеной, так на клиентской стороне.
    /// </summary>
    public interface IKrAcquaintanceManager
    {
        /// <summary>
        /// Производит отправку массового ознакомления карточки
        /// </summary>
        /// <param name="mainCardID">ID карточки</param>
        /// <param name="roleIDList">Список ID ролей для ознакомления</param>
        /// <param name="excludeDeputies">Определяет, нужно ли отправлять ознакомление на заместителей</param>
        /// <param name="comment">Комментарий</param>
        /// <param name="placeholderAliases">Алиасы плейсхолдеров</param>
        /// <param name="info">Дополнительная информация, которая передается в info методов замены плейсхолдеров</param>
        /// <param name="notificationCardID">
        /// Карточка уведомления, которая используется для отправки ознакомления.
        /// По умолчанию используется <see cref="DefaultNotifications.AcquaintanceID"/>.
        /// Данный параметр не передается с клиента на сервер и его использование допускается только на серверной стороне.
        /// </param>
        /// <param name="senderID">
        /// Сотрудник, от имени которого производится отправка ознакомления. Если не задан или некорректен, отправка производится от текущего сотрудника.
        /// Данный параметр не передается с клиента на сервер и его использование допускается только на серверной стороне.
        /// </param>
        /// <param name="addSuccessMessage">
        /// Признак того, что при успешной отправке на ознакомление требуется вывести информационное сообщение в <c>ValidationResult</c>
        /// о количестве сотрудников, которым было направлено ознакомление.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Возвращает результат валидации отправки массового ознакомления</returns>
        Task<ValidationResult> SendAsync(
            Guid mainCardID,
            IReadOnlyList<Guid> roleIDList,
            bool excludeDeputies = false,
            string comment = null,
            string placeholderAliases = null,
            Dictionary<string, object> info = null,
            Guid? notificationCardID = null,
            Guid? senderID = null,
            bool addSuccessMessage = false,
            CancellationToken cancellationToken = default);
    }
}
