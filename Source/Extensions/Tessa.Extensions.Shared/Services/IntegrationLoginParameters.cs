using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Shared.Services
{
    /// <summary>
    /// Account login parameters for integration services.
    /// </summary>
    public class IntegrationLoginParameters :
        StorageSerializable
    {
        #region Properties

        /// <summary>
        /// Login to the account.
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// The password to the account.
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
