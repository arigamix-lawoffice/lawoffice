using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [Registrator(Tag = RegistratorTag.DefaultForRepair)]
    public sealed class RepairRegistrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardRepairExtension, KrSatelliteTransferRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())
                .RegisterExtension<ICardRepairExtension, KrDialogSatelliteTransferRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithSingleton())
                .RegisterExtension<ICardRepairExtension, KrSecondarySatelliteTransferRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 3)
                    .WithSingleton())
                .RegisterExtension<ICardRepairExtension, KrSecondaryProcessRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 4)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID))
                .RegisterExtension<ICardRepairExtension, KrProcessRepairExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton())
                ;
        }
    }
}
