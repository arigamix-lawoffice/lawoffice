using Tessa.Cards.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    [Registrator]
    public sealed class Registrator :
        RegistratorBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, KrUpdateDialogTypeMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 0)
                    .WithSingleton())
                    ;
        }

        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IActionCompletionOptionsProvider, ActionCompletionOptionsProvider>(
                    new ContainerControlledLifetimeManager());
        }

        #endregion
    }
}