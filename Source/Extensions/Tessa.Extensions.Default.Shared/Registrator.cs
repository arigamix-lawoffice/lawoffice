using Tessa.Forums;

namespace Tessa.Extensions.Default.Shared
{
    [Registrator]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void InitializeRegistration()
        {
            DefaultValidationKeys.Register();
            ForumValidationKeys.Register();
        }
    }
}