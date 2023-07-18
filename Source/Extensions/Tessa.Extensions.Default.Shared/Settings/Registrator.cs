using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Settings;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Settings
{
    [Registrator]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterFactory<Func<CancellationToken, ValueTask<KrSettings>>>(
                    c => new Func<CancellationToken, ValueTask<KrSettings>>(async ct =>
                    {
                        ISettings settings = await c.Resolve<ISettingsProvider>().GetSettingsAsync(ct).ConfigureAwait(false);
                        return settings.Get<KrSettings>();
                    }),
                    new ContainerControlledLifetimeManager())
                .RegisterType<KrSettingsLazy>(new PerResolveLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // проектные расширения будут регистрироваться в AfterPlatform и резолвить/менять настройки;
                // поэтому добавление настроек будет выполнять в BeforePlatform
                .RegisterExtension<ISettingsExtension, KrSettingsExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())
                ;

            extensionContainer
                .RegisterExtension<ISettingsExtension, KrUserSettingsExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())
                ;

            extensionContainer
                .RegisterExtension<ISettingsExtension, TagsUserSettingsExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton())
                ;
        }
    }
}
