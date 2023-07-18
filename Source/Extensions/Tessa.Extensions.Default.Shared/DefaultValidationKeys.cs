using System;
using System.Globalization;
using Tessa.Localization;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Ключи валидации, используемые в типовом решении.
    /// </summary>
    public static class DefaultValidationKeys
    {
        #region Public Fields

        /// <summary>
        /// Пересчет маршрута недоступен, т.к. основной процесс запущен.
        /// </summary>
        public static readonly ValidationKey MainProcessStarted = new ValidationKey(
            new Guid(0x0865C2E9, 0x556C, 0x4F01, 0xB8, 0x01, 0xDE, 0xDE, 0x6A, 0xC0, 0xAC, 0x8E),
            nameof(MainProcessStarted),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Маршрут не изменен.
        /// </summary>
        public static readonly ValidationKey RecalcWithoutChanges = new ValidationKey(
            new Guid(0x1D632146, 0xBE8A, 0x46B2, 0x93, 0x7B, 0xF9, 0xE1, 0x6C, 0xB5, 0x88, 0x54),
            nameof(RecalcWithoutChanges),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Маршрут обновлен.
        /// </summary>
        public static readonly ValidationKey RecalcWithChanges = new ValidationKey(
            new Guid(0xCF2420A8, 0xE3D7, 0x42F8, 0x99, 0x5A, 0x82, 0x7C, 0x1D, 0x53, 0x58, 0x71),
            nameof(RecalcWithChanges),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Этап {0} был добавлен{1}.<para/>
        /// {0} - Название этапа.<para/>
        /// {1} - Строка " (скрытый)", если этап является скрытым, иначе - <see cref="string.Empty"/>.
        /// </summary>
        public static readonly ValidationKey StageAdded = new ValidationKey(
            new Guid(0xD88AB6C7, 0x36AB, 0x4BF7, 0x97, 0x1D, 0x5A, 0x9C, 0xF0, 0x4B, 0x15, 0x50),
            nameof(StageAdded),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Этап {0} {1} был изменен{2}.<para/>
        /// {0} - Актуальное название этапа.<para/>
        /// {1} - Строка "(переименован из "&lt;Старое название этапа&gt;")", если название этапа было изменено, иначе <see cref="string.Empty"/>.<para/>
        /// {1} - Строка " (скрытый)", если этап является скрытым, иначе - <see cref="string.Empty"/>.
        /// </summary>
        public static readonly ValidationKey StageModified = new ValidationKey(
            new Guid(0xCB03D28C, 0xFE42, 0x46BC, 0x92, 0x9B, 0xA8, 0x60, 0x0F, 0xE4, 0x89, 0x65),
            nameof(StageModified),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Этап {0} был удален{1}.<para/>
        /// {0} - Название этапа.<para/>
        /// {1} - Строка " (скрытый)", если этап является скрытым, иначе - <see cref="string.Empty"/>.
        /// </summary>
        public static readonly ValidationKey StageDeleted = new ValidationKey(
            new Guid(0x68EB9D6D, 0x3262, 0x4E24, 0xB1, 0x4E, 0x0A, 0x62, 0x17, 0x78, 0xEF, 0x73),
            nameof(StageDeleted),
            messageFunc: LocalizeKrCompilationValidationKey);

        /// <summary>
        /// Служебный ключ валидации, используемый для передачи флага прерывающего обработку диалога.
        /// </summary>
        public static readonly ValidationKey CancelDialog = new ValidationKey(
            new Guid(0x0CDFB070, 0x7D03, 0x47B2, 0x85, 0x54, 0xAF, 0x61, 0xA0, 0x62, 0x25, 0xA6),
            nameof(CancelDialog),
            message: nameof(CancelDialog));

        #endregion

        #region Register Method

        /// <summary>
        /// Регистрирует все стандартные ключи валидации посредством заданного метода.
        /// </summary>
        public static void Register()
        {
            var registry = (ValidationKeyRegistry) ValidationKeyRegistry.Instance;
            foreach (var validationKey
                in
                new[]
                {
                    MainProcessStarted,
                    RecalcWithoutChanges,
                    RecalcWithChanges,
                    StageAdded,
                    StageModified,
                    StageDeleted,
                    CancelDialog,
                })
            {
                registry.Register(validationKey);
            }
        }

        #endregion

        #region Private Methods

        private static string LocalizeKrCompilationValidationKey(
            ValidationKey validationKey,
            CultureInfo culture) =>
            LocalizationManager.GetString("KrCompilation_ValidationKey_" + validationKey.Name, culture);

        #endregion
    }
}
