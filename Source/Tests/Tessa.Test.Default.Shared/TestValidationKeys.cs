using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет ключи валидации используемые в тестах.
    /// </summary>
    public static class TestValidationKeys
    {
        #region Fields

        /// <summary>
        /// Массив ключей сообщений валидации добавляемых в результат валидации при обработке отложенных действий.
        /// </summary>
        private static readonly ValidationKey[] pendingActionValidationKeys;

        #endregion

        #region ValidationKey Fields

        /// <summary>
        /// При выполнении подготовительного действия "{0}" отложенного действия "{1}" возникли сообщения. Дополнительная информация расположена в последующих сообщениях.<para/>
        /// {0} - Имя подготовительного действия.<para/>
        /// {1} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionPreparationActionMessages =
            new ValidationKey(
                new Guid(0x2697c6d4, 0x50ac, 0x4484, 0xad, 0xd2, 0xe1, 0x99, 0x76, 0xba, 0x1, 0xa2),
                nameof(PendingActionPreparationActionMessages),
                "Messages occurred while executing preparation action \"{0}\". Pending action \"{1}\". Additional information can be found in the following messages.");

        /// <summary>
        /// При выполнении подготовительного действия "{0}" отложенного действия "{1}" возникло исключение.<para/>
        /// {0} - Имя подготовительного действия.<para/>
        /// {1} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionPreparationActionException =
            new ValidationKey(
                new Guid(0xe4b2532b, 0x31c1, 0x4648, 0xb1, 0xf5, 0xe3, 0x6e, 0x2c, 0x63, 0x60, 0x8b),
                nameof(PendingActionPreparationActionException),
                "An exception was thrown while executing preparation action \"{0}\". Pending action \"{1}\".");
        
        /// <summary>
        /// При выполнении действия "{0}", выполняющегося после отложенного действия "{1}", возникли сообщения. Дополнительная информация расположена в последующих сообщениях.<para/>
        /// {0} - Имя текущего действия.<para/>
        /// {1} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionAfterActionMessages =
            new ValidationKey(
                new Guid(0x2697c6d4, 0x50ac, 0x4484, 0xad, 0xd2, 0xe1, 0x99, 0x76, 0xba, 0x1, 0xa2),
                nameof(PendingActionPreparationActionMessages),
                "Messages occurred while executing after action \"{0}\". Pending action \"{1}\". Additional information can be found in the following messages.");

        /// <summary>
        /// При выполнении действия "{0}", выполняющегося после отложенного действия "{1}", возникло исключение.<para/>
        /// {0} - Имя подготовительного действия.<para/>
        /// {1} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionAfterActionException =
            new ValidationKey(
                new Guid(0xe4b2532b, 0x31c1, 0x4648, 0xb1, 0xf5, 0xe3, 0x6e, 0x2c, 0x63, 0x60, 0x8b),
                nameof(PendingActionPreparationActionException),
                "An exception was thrown while executing after action \"{0}\". Pending action \"{1}\".");

        /// <summary>
        /// При выполнении отложенного действия \"{0}\" возникли сообщения. Дополнительная информация расположена в последующих сообщениях.<para/>
        /// {0} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionMessages =
            new ValidationKey(
                new Guid(0xb339be5d, 0x8c47, 0x47f0, 0xb5, 0xb9, 0x9, 0x9a, 0xff, 0xe3, 0x11, 0xa7),
                nameof(PendingActionMessages),
                "Messages occurred while executing pending action \"{0}\". Additional information can be found in the following messages.");

        /// <summary>
        /// При выполнении отложенного действия "{0}" возникло исключение.<para/>
        /// {0} - Имя действия.
        /// </summary>
        public static ValidationKey PendingActionException =
            new ValidationKey(
                new Guid(0x75d22c77, 0x992b, 0x4114, 0xba, 0xf6, 0x8d, 0xea, 0x14, 0xd8, 0xa0, 0x66),
                nameof(PendingActionException),
                "An exception was thrown while executing pending action \"{0}\".");

        /// <summary>
        /// Трассировка выполнения отложенных действий:{<see cref="Environment.NewLine"/>}{0}.<para/>
        /// {0} - Строковое представление трассировки отложенных действий.
        /// </summary>
        public static ValidationKey PendingActionTrace =
            new ValidationKey(
                new Guid(0x89de37e0, 0xe5d5, 0x43ac, 0xb9, 0x54, 0xc2, 0xb7, 0x73, 0x67, 0x1e, 0x92),
                nameof(PendingActionTrace),
                $"Tracing for performing pending actions:{Environment.NewLine}{{0}}");

        #endregion

        #region Constructor

        /// <summary>
        /// Регистрирует все ключи валидации, используемые в тестах.
        /// </summary>
        static TestValidationKeys()
        {
            pendingActionValidationKeys = new ValidationKey[]
            {
                PendingActionPreparationActionMessages,
                PendingActionPreparationActionException,
                PendingActionMessages,
                PendingActionException,
                PendingActionTrace,
            };

            var registry = ValidationKeyRegistry.Instance;

            foreach (var validationKey in pendingActionValidationKeys)
            {
                registry.Register(validationKey);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Исключает, из заданной коллекции сообщений валидации, информационные сообщения добавленные при обработке отложенных действий.
        /// </summary>
        /// <param name="validationResult">Коллекция сообщений валидации.</param>
        /// <returns>Коллекция сообщений валидаций из которой исключены информационные сообщения добавленные при обработке отложенных действий.</returns>
        public static IReadOnlyCollection<IValidationResultItem> ExceptPendingActionValidationResult(
            IReadOnlyCollection<IValidationResultItem> validationResult)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            return validationResult.Count > 0
                ? validationResult.Where(i => !pendingActionValidationKeys.Contains(i.Key)).ToArray()
                : validationResult;
        }

        /// <summary>
        /// Исключает, из заданной коллекции сообщений валидации, информационные сообщения добавленные при обработке отложенных действий.
        /// </summary>
        /// <param name="validationResult">Результат валидации.</param>
        /// <returns>Результат валидации из которой исключены информационные сообщения добавленные при обработке отложенных действий.</returns>
        public static ValidationResult ExceptPendingActionValidationResult(
            ValidationResult validationResult)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            return validationResult.Items.Count > 0
                ? new ValidationResult(validationResult.Items.Where(i => !pendingActionValidationKeys.Contains(i.Key)))
                : validationResult;
        }

        #endregion
    }
}
