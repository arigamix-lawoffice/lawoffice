using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public sealed class OperationSettings :
        IAsyncInitializable
    {
        #region Fields

        private List<string> yes;

        private List<string> windowsUser;

        private List<string> tessaUser;

        #endregion

        #region Methods

        public bool GetBool(string value) => this.yes.Contains(value);

        public UserLoginType GetLoginType(string value) =>
            this.tessaUser.FirstOrDefault(x => x.Equals(value, StringComparison.OrdinalIgnoreCase)) != null
                ? UserLoginType.Tessa
                : this.windowsUser.FirstOrDefault(x => x.Equals(value, StringComparison.OrdinalIgnoreCase)) != null
                    ? UserLoginType.Windows
                    : UserLoginType.None;

        #endregion

        #region IAsyncInitializable Members

        public async ValueTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            this.yes = await LocalizationManager.GetAllStringsAsync("UI_Common_Yes", cancellationToken);
            this.windowsUser = await LocalizationManager.GetAllStringsAsync("Enum_LoginTypes_Windows", cancellationToken);
            this.tessaUser = await LocalizationManager.GetAllStringsAsync("Enum_LoginTypes_Tessa", cancellationToken);
        }

        #endregion
    }
}