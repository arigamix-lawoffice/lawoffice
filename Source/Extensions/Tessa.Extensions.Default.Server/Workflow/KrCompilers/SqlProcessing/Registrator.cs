using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrSqlPreprocessor, KrSqlPreprocessor>(new PerResolveLifetimeManager())
                .RegisterType<IKrSqlExecutor, KrSqlExecutor>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}