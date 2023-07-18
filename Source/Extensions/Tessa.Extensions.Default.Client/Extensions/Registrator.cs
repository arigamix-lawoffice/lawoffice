using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Platform;
using Tessa.UI.Cards;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Extensions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<InitializeFilesViewExtensionType>(TypeLifetime.ContainerControlled)
                .RegisterType<OpenCardInViewExtensionType>(TypeLifetime.ContainerControlled)
                ;
        }

        /// <inheritdoc/>
        public override void FinalizeRegistration()
        {
            var typeExtensionTypeResolver = this.UnityContainer
                .TryResolve<ITypeExtensionTypeResolver>();
            typeExtensionTypeResolver
                ?.Register<InitializeFilesViewExtensionType>(DefaultCardTypeExtensionTypes.InitializeFilesView)
                ?.Register<OpenCardInViewExtensionType>(DefaultCardTypeExtensionTypes.OpenCardInView)
                ;

            DefaultCardTypeExtensionTypes.Register(CardTypeExtensionTypeRegistry.Instance.Register);
        }
    }
}
