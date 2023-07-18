#nullable enable

using System;
using System.Collections.Generic;
using Tessa.Platform.Maintenance;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Maintenance
{
    [Registrator(Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                // Регистрация сервиса получения строк локализации для режима техобслуживания.
                .RegisterFactory<Func<IEnumerable<IMaintenanceLocalizationStrategy>>>(
                    c => new Func<IEnumerable<IMaintenanceLocalizationStrategy>>(
                        () => c.ResolveAll<IMaintenanceLocalizationStrategy>()), 
                    new ContainerControlledLifetimeManager())
                
                .RegisterType<IMaintenanceLocalizationStrategy, ConfigurationMaintenanceLocalizationStrategy>(nameof(ConfigurationMaintenanceLocalizationStrategy), new ContainerControlledLifetimeManager())
                .RegisterType<IMaintenanceLocalizationStrategy, LocalizationServiceMaintenanceLocalizationStrategy>(nameof(LocalizationServiceMaintenanceLocalizationStrategy), new ContainerControlledLifetimeManager())
                .RegisterType<IMaintenanceLocalizationStrategy, AggregateMaintenanceLocalizationStrategy>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
