using Tessa.Compilation;
using Tessa.Platform;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IExtraSourceSerializer, ExtraSourceStorageSerializer>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrPreprocessorProvider, KrPreprocessorProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrCompiler, KrCompiler>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrProcessCache, KrProcessCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrStageTemplateCompilationCache, KrStageTemplateCompilationCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrStageGroupCompilationCache, KrStageGroupCompilationCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrSecondaryProcessCompilationCache, KrSecondaryProcessCompilationCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrCommonMethodCompilationCache, KrCommonMethodCompilationCache>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrExecutor, KrStageExecutor>(KrExecutorNames.StageExecutor, new PerResolveLifetimeManager())
                .RegisterType<IKrExecutor, KrGroupExecutor>(KrExecutorNames.GroupExecutor, new PerResolveLifetimeManager())
                .RegisterType<IKrExecutor, KrCacheExecutor>(KrExecutorNames.CacheExecutor, new ContainerControlledLifetimeManager())
            ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<ICompilationCacheContainer>()
                ?
                .Register(this.UnityContainer.Resolve<IKrStageTemplateCompilationCache>())
                .Register(this.UnityContainer.Resolve<IKrStageGroupCompilationCache>())
                .Register(this.UnityContainer.Resolve<IKrSecondaryProcessCompilationCache>())
                .Register(this.UnityContainer.Resolve<IKrCommonMethodCompilationCache>())
                ;
        }
    }
}
