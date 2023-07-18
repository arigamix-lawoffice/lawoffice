namespace Tessa.Extensions.Default.Console.User
{
    public class OperationContext
    {
        /// <summary>
        /// Идентификатор сотрудника или краткое имя.
        /// </summary>
        public string User { get; set; }

        public string Account { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        
        public bool Ldap { get; set; }

        public bool NoLogin { get; set; }
    }
}
