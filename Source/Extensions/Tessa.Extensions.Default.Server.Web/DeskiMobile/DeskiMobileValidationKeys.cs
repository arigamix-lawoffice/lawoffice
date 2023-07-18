using System;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    /// <summary>
    /// Ключи валидации для DeskiMobile.
    /// </summary>
    public static class DeskiMobileValidationKeys
    {
        #region Public Fields

        /// <summary>
        /// Операция не найдена.
        /// </summary>
        public static readonly ValidationKey OperationNotFound =
            new(
                new Guid(0x48ccb7c5, 0xc39b, 0x432f, 0x9b, 0x09, 0xb0, 0x74, 0x59, 0x33, 0x5a, 0x17), // 48ccb7c5-c39b-432f-9b09-b07459335a17
                nameof(OperationNotFound));

        #endregion

        #region Register Method

        /// <summary>
        /// Регистрирует все стандартные ключи валидации посредством заданного метода.
        /// </summary>
        public static void Register()
        {
            var registry = (ValidationKeyRegistry)ValidationKeyRegistry.Instance;

            foreach (ValidationKey validationKey
                in new[]
                {
                    OperationNotFound
                })
            {
                registry.Register(validationKey);
            }
        }

        #endregion
    }
}
