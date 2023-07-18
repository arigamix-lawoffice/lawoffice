#nullable enable

using Tessa.Extensions.Default.Shared.Tags;
using Tessa.Tags;
using Tessa.UI.Cards;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Tags
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ITagPermissionsTokenProvider, KrTagPermissionsTokenProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<EnableTagsUIExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardUIExtension, EnableTagsUIExtension>(x => x
                    .WithOrder(ExtensionStage.Platform, 1)
                    .WithUnity(this.UnityContainer))
                ;
        }
    }
}
