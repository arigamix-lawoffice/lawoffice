using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public sealed class UserInfo :
        TimeZoneObject
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string ShortName { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Hide { get; set; }

        public UserLoginType LoginType { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ExternalID { get; set; }

        #endregion
    }
}