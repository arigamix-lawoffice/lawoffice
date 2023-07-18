using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на сохранение карточки выполняющее запуск процесса в соответствии с объектом типа <see cref="KrProcessInstance"/> содержащемся в запросе на сохранение.
    /// </summary>
    public sealed class KrLaunchProcessStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IKrProcessLauncher processLauncher;

        private readonly IKrProcessCache processCache;

        private IKrProcessLaunchResult result;

        private KrProcessInstance krProcess;

        private bool startingProcessNameSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrLaunchProcessStoreExtension"/>.
        /// </summary>
        /// <param name="processLauncher">Объект выполняющий запуск процессов.</param>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        public KrLaunchProcessStoreExtension(
            IKrProcessLauncher processLauncher,
            IKrProcessCache processCache)
        {
            this.processLauncher = processLauncher ?? throw new System.ArgumentNullException(nameof(processLauncher));
            this.processCache = processCache ?? throw new System.ArgumentNullException(nameof(processCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !context.Request.TryGetKrProcessInstance(out this.krProcess))
            {
                this.krProcess = null;
                return;
            }

            if (this.krProcess.CardID != context.Request.Card.ID)
            {
                context.ValidationResult.AddError(this, KrErrorHelper.ProcessStartingForDifferentCardID());
                return;
            }

            context.Request.ForceTransaction = true;
            var process = await this.processCache.GetSecondaryProcessAsync(this.krProcess.ProcessID, context.CancellationToken);

            if (process.Async)
            {
                // В качестве аванса BeforeRequest KrProcessWorkflowStoreExtension запланирует запуск
                context.Request.SetStartingProcessName(KrConstants.KrSecondaryProcessName);
                this.startingProcessNameSet = true;
            }
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(
            ICardStoreExtensionContext context)
        {
            if (this.startingProcessNameSet
                && context.Request.TryGetStartingProcessName() == KrConstants.KrSecondaryProcessName)
            {
                // Неважно, что раньше мы попросили запустить процесс.
                // Перепроверим права и перепроверим флаг уже в расширении на старт процесса.
                context.Request.Info.Remove(CardHelper.SystemKeyPrefix + "startProcess");
            }

            if (context.ValidationResult.IsSuccessful()
                && this.krProcess is not null)
            {
                var specificParameters = new KrProcessServerLauncher.SpecificParameters()
                {
                    RaiseErrorWhenExecutionIsForbidden = context.Request.Info.TryGet<bool>(KrConstants.RaiseErrorWhenExecutionIsForbidden)
                };

                this.result = await this.processLauncher.LaunchAsync(this.krProcess, context, specificParameters);
                context.ValidationResult.Add(this.result.ValidationResult);
            }
        }

        /// <inheritdoc/>
        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (this.result is not null
                && this.result is KrProcessLaunchResult res)
            {
                context.Response.SetKrProcessLaunchResult(res);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
