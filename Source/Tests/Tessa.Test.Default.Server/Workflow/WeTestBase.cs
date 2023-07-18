using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Workflow;
using Unity;

namespace Tessa.Test.Default.Server.Workflow
{
    /// <summary>
    /// Абстрактный базовый класс для тестов WorkflowEngine.
    /// </summary>
    public abstract class WeTestBase :
        KrServerTestBase
    {
        #region Consts

        /// <summary>
        /// Идентификатор типа карточки шаблона бизнес-процесса.
        /// </summary>
        protected static readonly Guid ProcessTemplateType = CardHelper.BusinessProcessTemplateTypeID;

        /// <summary>
        /// Имя типа карточки шаблона бизнес-процесса.
        /// </summary>
        protected const string ProcessTemplateTypeName = CardHelper.BusinessProcessTemplateTypeName;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает сервис для управления шаблонами, экземплярами и подписками Бизнес-процесса.
        /// </summary>
        protected IWorkflowService WorkflowService { get; private set; }

        #endregion

        #region SetUp Methods

        /// <inheritdoc/>
        protected override async Task InitializeCoreAsync()
        {
            await base.InitializeCoreAsync();

            await this.TestConfigurationBuilder
                .GetPermissionsConfigurator()
                .GetPermissionsCard(Guid.NewGuid())
                .AddFlags(KrPermissionFlagDescriptors.Full)
                .AddRole(this.Session.User.ID)
                .ModifyStates(static _ => KrState.DefaultStates)
                .AddType(this.TestDocTypeID)
                .Complete()
                .GoAsync()
                ;

            this.WorkflowService = this.UnityContainer.Resolve<IWorkflowService>();
        }

        #endregion
    }
}
