using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform.Plugins;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class KrAutoApprovePlugin : PluginExtension
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        public override async Task EntryPoint(IPluginExtensionContext context)
        {
            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var builderFactory = context.DbScope.BuilderFactory;

                List<KrAutoApproveTaskRecord> tasksToApprove = await KrAutoApprovePluginHelper.GetTasksToAutoApproveAsync(db, builderFactory, context.CancellationToken)
                    ;

                if (tasksToApprove.Count == 0)
                {
                    return;
                }

                const string defaultComment = "{$UI_Tasks_DefaultAutoApprovedMessage}";
                foreach (KrAutoApproveTaskRecord taskRecord in tasksToApprove)
                {
                    if (string.IsNullOrEmpty(taskRecord.ApprovalComment))
                    {
                        taskRecord.ApprovalComment = defaultComment;
                    }
                }

                // Обрабатываем в цикле задания согласования
                await KrAutoApprovePluginHelper.CompleteApproveTasksAsync(
                    tasksToApprove,
                    db,
                    builderFactory,
                    context.Resolve<ICardRepository>(CardRepositoryNames.ExtendedWithoutTransactionAndLocking),
                    context.Resolve<ICardMetadata>(),
                    context.Session,
                    context.Resolve<ICardGetStrategy>(),
                    context.Resolve<ICardServerPermissionsProvider>(),
                    context.Resolve<ICardTransactionStrategy>(),
                    logger,
                    () => context.StopRequested,
                    context.CancellationToken);
            }
        }

        #endregion
    }
}
