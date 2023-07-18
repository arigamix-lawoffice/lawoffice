using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение обрабатывающее запрос <see cref="KrConstants.LaunchProcessRequestType"/> выполняющий запуск процесса в соответствии с объектом типа <see cref="KrProcessInstance"/> содержащемся в запросе.
    /// </summary>
    public sealed class KrLaunchProcessCustomExtension :
        CardRequestExtension
    {
        #region Fields

        private readonly IKrProcessLauncher processLauncher;
        private readonly ITransactionScope transactionScope;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrLaunchProcessCustomExtension"/>.
        /// </summary>
        /// <param name="processLauncher">Объект выполняющий запуск процессов.</param>
        /// <param name="transactionScope">Объект контролирующий область выполнения транзакции.</param>
        public KrLaunchProcessCustomExtension(
            IKrProcessLauncher processLauncher,
            ITransactionScope transactionScope)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.transactionScope = transactionScope ?? throw new ArgumentNullException(nameof(transactionScope));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !context.Request.TryGetKrProcessInstance(out var processInstance))
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                using var transact = await db.BeginTransactionAsync(context.CancellationToken);
                await using var _ = this.transactionScope.Create();
                var isSuccessful = false;
                try
                {
                    var specificParameters = new KrProcessServerLauncher.SpecificParameters()
                    {
                        RaiseErrorWhenExecutionIsForbidden = context.Request.Info.TryGet<bool>(KrConstants.RaiseErrorWhenExecutionIsForbidden)
                    };

                    var result = await this.processLauncher.LaunchAsync(
                        processInstance,
                        context,
                        specificParameters);

                    context.ValidationResult.Add(result.ValidationResult);

                    if (result is KrProcessLaunchResult typedResult)
                    {
                        context.Response.SetKrProcessLaunchResult(typedResult);
                    }

                    if (result.ValidationResult.IsSuccessful())
                    {
                        isSuccessful = true;
                        await transact.CommitAsync(context.CancellationToken);
                    }
                    else
                    {
                        isSuccessful = false;
                        await transact.RollbackAsync(context.CancellationToken);
                    }
                }
                finally
                {
                    var validationResult = new ValidationResultBuilder();
                    await this.transactionScope.PerformHandlersAsync(
                        isSuccessful,
                        validationResult,
                        context.CancellationToken);
                }
            }
        }

        #endregion
    }
}