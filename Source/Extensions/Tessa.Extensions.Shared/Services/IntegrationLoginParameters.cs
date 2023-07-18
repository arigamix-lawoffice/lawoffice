using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Shared.Services
{
    /// <summary>
    /// Параметры входа в учётную запись для интеграционных сервисов.
    /// </summary>
    public class IntegrationLoginParameters :
        StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Логин к учётной записи.
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// Пароль к учётной записи.
        /// </summary>
        public string? Password { get; set; }

        #endregion

        #region Base Overrides

        protected override void SerializeCore(Dictionary<string, object?> storage)
        {
            storage[nameof(this.Login)] = this.Login;
            storage[nameof(this.Password)] = this.Password;
        }

        protected override void DeserializeCore(Dictionary<string, object?> storage)
        {
            this.Login = TryGetString(storage, nameof(this.Login));
            this.Password = TryGetString(storage, nameof(this.Password));
        }

        #endregion
    }
}
