using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    [Registrator(Tag = RegistratorTag.DefaultForRepair)]
    public sealed class RepairRegistrator : RegistratorBase
    {
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardRepairExtension, WfTaskSatelliteTransferRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton())
                .RegisterExtension<ICardRepairExtension, WfSatelliteTransferRepairExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithSingleton())
                ;
        }
    }
}
