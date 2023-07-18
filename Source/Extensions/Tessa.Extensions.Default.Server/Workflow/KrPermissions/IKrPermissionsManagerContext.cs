using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Объект контекста проверки прав доступа в <see cref="IKrPermissionsManager"/>.
    /// </summary>
    public interface IKrPermissionsManagerContext : IExtensionContext
    {
        /// <summary>
        /// Контекст расширения, в котором была вызвана проверка прав доступа.
        /// Может быть равен <c>null</c>.
        /// </summary>
        ICardExtensionContext ExtensionContext { get; }
        
        /// <summary>
        /// Предыдущий токен прав доступа. Может быть не задан.
        /// </summary>
        KrToken PreviousToken { get; }

        /// <summary>
        /// Дополнительный токен прав доступа, рассчитанный на сервере.
        /// Его настройки приоритетнее, чем в <see cref="PreviousToken"/>, и он всегда считается валидным.
        /// Может быть не задан.
        /// </summary>
        KrToken ServerToken { get; }

        /// <summary>
        /// Дескриптор с результатами проверки правил доступа.
        /// </summary>
        KrPermissionsDescriptor Descriptor { get; set; }

        /// <summary>
        /// Режим проверки доступа к карточке.
        /// </summary>
        KrPermissionsCheckMode Mode { get; }

        /// <summary>
        /// Имя метода, который был вызван для проверки правил доступа.
        /// Может иметь значение <see cref="IKrPermissionsManager.CheckRequiredPermissionsAsync"/> или <see cref="IKrPermissionsManager.GetEffectivePermissionsAsync"/>.
        /// </summary>
        string Method { get; set; }

        /// <summary>
        /// Карточка, по которой идет проверка доступа.
        /// Ее наличие и содержимое зависит от <see cref="Mode"/>.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Идентификатор карточки или <c>null</c>, если проверка идет вне контекста карточки.
        /// </summary>
        Guid? CardID { get; }

        /// <summary>
        /// Тип карточки.
        /// </summary>
        CardType CardType { get; }

        /// <summary>
        /// Идентификатор типа документа, если используется тип документа, иначе <c>null</c>.
        /// </summary>
        Guid? DocTypeID { get; }

        /// <summary>
        /// Состояние карточки.
        /// </summary>
        KrState? DocState { get; }

        /// <summary>
        /// Идентификатор файла, если выполняется проверка доступа к конкретному файлу.
        /// </summary>
        Guid? FileID { get; }

        /// <summary>
        /// Идентификатор версии файла, если выполняется проверка доступа к конкретной версии файла.
        /// </summary>
        Guid? FileVersionID { get; }

        /// <summary>
        /// Флаг определяет, что нужно расчитывать и учитывать настройки правил доступа, помеченных как обязательные.
        /// </summary>
        bool WithRequiredPermissions { get; }

        /// <summary>
        /// Флаг определяет, что нужно рассчитать расширенные настройки прав доступа карточки.
        /// </summary>
        bool WithExtendedPermissions { get; }

        /// <summary>
        /// Список секций, по которым игнорируется проверка расширенных настроек прав доступа.
        /// </summary>
        ICollection<string> IgnoreSections { get; }

        /// <summary>
        /// Дополнительная информация, используемая при проверке прав доступа.
        /// </summary>
        IDictionary<string, object> Info { get; }

        /// <summary>
        /// Билдер результата валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Объект для взаимодействия с базой данных.
        /// </summary>
        IDbScope DbScope { get; }

        /// <summary>
        /// Сессия текущего сотрудника.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Версия кеша правил доступа, которая используется для получения данных о настройках правил доступа.
        /// Если не задано, то берется текущая версия правил доступа из <see cref="IKrPermissionsCacheContainer"/>.
        /// </summary>
        IKrPermissionsCache PermissionsCache { get; }

        /// <summary>
        /// Метаданные карточек.
        /// </summary>
        ICardMetadata CardMetadata { get; }

        /// <summary>
        /// Кеш типов документов.
        /// </summary>
        IKrTypesCache KrTypesCache { get; }

        /// <summary>
        /// Метод для добавления ошибки в <see cref="ValidationResult"/>, который пишет дополнительную информацию о контексте в деталях сообщения.
        /// </summary>
        /// <param name="callerObject">Объект, который вызвал добавление ошибки. Записывается в тип объекта результата валидации.</param>
        /// <param name="errorText">Текст ошибки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="args">Аргументы, добавлемые в текст ошибки.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask AddErrorAsync(
            object callerObject,
            string errorText,
            CancellationToken cancellationToken = default,
            params object[] args);
    }
}
