#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Roles.Queries;
using Tessa.Roles.SmartRoles;
using Tessa.Roles.Triggers;

namespace Tessa.Extensions.Default.Server.Roles
{

    /// <summary>
    /// Объект данных для генератора умных ролей по данным карточки "Генератор умных ролей",
    /// которые учитывают обработку триггеров по типам документов.
    /// </summary>
    public class KrSmartRoleGeneratorData : SmartRoleGeneratorData
    {
        #region Constructors

        public KrSmartRoleGeneratorData(
            SmartRoleGeneratorDataSource source,
            IDbScope dbScope,
            IComplexQueryBuilderFactory getItemsQueryBuilderFactory)
            : base(source, dbScope, getItemsQueryBuilderFactory)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask InitializeTriggersAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeTriggersAsync(cancellationToken);

            foreach (var trigger in this.Triggers)
            {
                trigger.CheckTriggerCardAsyncFunc = this.CheckTriggerAsync;
            }
        }

        #endregion


        #region Private Methods

        private async ValueTask<bool> CheckTriggerAsync(UpdateTrigger trigger, Card triggerCard, CancellationToken cancellationToken)
        {
            if (trigger.CardTypes.Contains(triggerCard.TypeID))
            {
                return true;
            }

            var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(triggerCard, this.DbScope, cancellationToken);
            if (docTypeID is not null
                && trigger.CardTypes.Contains(docTypeID.Value))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
