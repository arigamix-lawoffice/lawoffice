using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Conditions;
using Unity;

namespace Tessa.Extensions.Default.Shared.Conditions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterSingleton<KrPermissionsConditionSourceHandler>()
                .RegisterSingleton<KrVirtualFileConditionSourceHandler>()
                .RegisterSingleton<KrSecondaryProcessConditionSourceHandler>()
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardRepairExtension, DefaultConditionsCardRepairExtension>(x => x
                    .WithOrder(ExtensionStage.Platform, 1001)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(
                        DefaultCardTypes.KrPermissionsTypeID,
                        DefaultCardTypes.KrVirtualFileTypeID,
                        DefaultCardTypes.KrSecondaryProcessTypeID))
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IConditionSourceHandlerResolver>()?
                .Register<KrPermissionsConditionSourceHandler>(DefaultCardTypes.KrPermissionsTypeID)
                .Register<KrVirtualFileConditionSourceHandler>(DefaultCardTypes.KrVirtualFileTypeID)
                .Register<KrSecondaryProcessConditionSourceHandler>(DefaultCardTypes.KrSecondaryProcessTypeID)
                ;
        }
    }
}
