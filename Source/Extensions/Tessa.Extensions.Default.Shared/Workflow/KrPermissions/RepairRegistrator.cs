using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    [Registrator(Tag = RegistratorTag.DefaultForRepair)]
    public sealed class RepairRegistrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardRepairExtension, KrPermissionsRulesRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrPermissionsTypeID))
                ;
        }
    }
}
