using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Объект с параметрами для создания контекста в <see cref="IKrPermissionsManager"/>.
    /// </summary>
    public sealed class KrPermissionsCreateContextParams
    {
        /// <summary>
        /// Карточка. Может быть не передана, если проверка прав доступа выполняется вне контекста карточки.
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Определяет, что переданная карточка сохраняется.
        /// </summary>
        public bool IsStore { get; set; }

        /// <summary>
        /// Идентификатор карточки. Может быть не передан, если проверка прав доступа выполняется вне контекста карточки.
        /// </summary>
        public Guid? CardID { get; set; }

        /// <summary>
        /// Тип карточки. Должен быть передан, если не передан <see cref="Card"/> или <see cref="CardID"/>.
        /// </summary>
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// Тип документа. Должен быть передан, если не передан <see cref="Card"/> или <see cref="CardID"/>.
        /// </summary>
        public Guid? DocTypeID { get; set; }

        /// <summary>
        /// Состояние документа.
        /// </summary>
        public KrState? KrState { get; set; }

        /// <summary>
        /// Идентификатор проверяемого файла, если идет проверка доступа к файлу карточки.
        /// </summary>
        public Guid? FileID { get; set; }

        /// <summary>
        /// Идентификатор версии проверяемого файла, если идет проверка доступа к версии файла.
        /// </summary>
        public Guid? FileVersionID { get; set; }

        /// <summary>
        /// Флаг определяет, что нужно рассчитать расширенные настройки обязательности полей.
        /// </summary>
        public bool WithRequiredPermissions { get; set; }

        /// <summary>
        /// Флаг определяет, что нужно рассчитать расширенные настройки прав доступа карточки.
        /// </summary>
        public bool WithExtendedPermissions { get; set; }

        /// <summary>
        /// Список секций, по которым игнорируется проверка расширенных настроек прав доступа.
        /// </summary>
        public ICollection<string> IgnoreSections { get; set; }

        /// <summary>
        /// Билдер результата валидации. Может быть не передан, если не требуется обработка результата валидации.
        /// </summary>
        public IValidationResultBuilder ValidationResult { get; set; }

        /// <summary>
        /// Дополнительная информация, используемая при проверке прав доступа.
        /// </summary>
        public IDictionary<string, object> AdditionalInfo { get; set; }

        /// <summary>
        /// Предыдущий токен.
        /// </summary>
        public KrToken PrevToken { get; set; }

        /// <summary>
        /// Дополнительный токен прав доступа, рассчитанный на сервере.
        /// Его настройки приоритетнее, чем в <see cref="PrevToken"/> и он всегда считается валидным.
        /// </summary>
        public KrToken ServerToken { get; set; }

        /// <summary>
        /// Контекст расширения, в котором была вызвана данная проверка прав доступа. Может быть равен null.
        /// </summary>
        public ICardExtensionContext ExtensionContext { get; set; }

        /// <summary>
        /// Версия кеша правил доступа, которая используется для получения данных о настройках правил доступа.
        /// Если не задано, то берется текущая версия правил доступа из <see cref="IKrPermissionsCacheContainer"/>.
        /// </summary>
        public IKrPermissionsCache PermissionsCache { get; set; }
    }
}
