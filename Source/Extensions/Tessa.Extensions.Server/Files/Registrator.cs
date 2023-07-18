using Tessa.Cards.Extensions;
using Tessa.Cards;
using Tessa.Extensions.Shared.Info;
using Tessa.Cards.ComponentModel;
using Unity.Lifetime;
using Unity;
using Tessa.Platform.Data;
using System;

namespace Tessa.Extensions.Server.Files
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetFileContentExtension, LawVirtualFileGetContentExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithSingleton()
                    .WhenFileTypes(TypeInfo.LawFile.Alias))
                ;
        }

        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterFactory<CardSourceContentStrategy>(
                    c => new CardSourceContentStrategy(
                        c.Resolve<ICardFileSourceSettings>(),
                        settings => new CardDatabaseContentStrategy(c.Resolve<IDbScope>(), settings),
                        settings => settings.Name is not null && settings.Name.StartsWith("law-", StringComparison.Ordinal)
                            ? new LawContentStrategy(c.Resolve<IDbScope>(), settings)
                            : new CardFileSystemContentStrategy(settings)),
                    new ContainerControlledLifetimeManager())
                ;
        }
    }
}
