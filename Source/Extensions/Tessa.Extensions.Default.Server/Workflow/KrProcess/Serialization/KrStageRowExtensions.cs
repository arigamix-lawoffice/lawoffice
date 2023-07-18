using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    public static class KrStageRowExtensions
    {
        public static IExtensionContainer RegisterKrStageRowExtensionTypes(
            this IExtensionContainer extensionContainer)
        {
            return extensionContainer
                .RegisterType<IKrStageRowExtension>(x => x
                        .MethodAsync<IKrStageRowExtensionContext>(y => y.BeforeSerialization)
                        .MethodAsync<IKrStageRowExtensionContext>(y => y.DeserializationBeforeRepair)
                        .MethodAsync<IKrStageRowExtensionContext>(y => y.DeserializationAfterRepair),
                    x => x.Register(KrStageRowExtensionFilterPolicy.Instance));
        }

        public static IExtensionPolicyContainer WhenRouteCardTypes(
            this IExtensionPolicyContainer policyContainer,
            params RouteCardType[] routeCardTypes)
        {
            if (policyContainer is null)
            {
                throw new ArgumentNullException(nameof(policyContainer));
            }

            return policyContainer
                .Register(new RouteCardTypeExtensionPolicy(routeCardTypes));
        }
    }
}