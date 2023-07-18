using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Actions
{
    /// <summary>
    /// Расширение, выполняющее запуск вторичных процессов в соответствии с произошедшим событием.
    /// </summary>
    public sealed class EventCollector : KrEventExtension
    {
        #region Fields

        private readonly IKrProcessLauncher processLauncher;

        private readonly IKrProcessCache processCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventCollector"/>.
        /// </summary>
        /// <param name="processLauncher">Объект выполняющий запуск процессов.</param>
        /// <param name="processCache">Кэш для данных из карточек шаблонов этапов.</param>
        public EventCollector(
            IKrProcessLauncher processLauncher,
            IKrProcessCache processCache)
        {
            this.processLauncher = processLauncher ?? throw new ArgumentNullException(nameof(processLauncher));
            this.processCache = processCache ?? throw new ArgumentNullException(nameof(processCache));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task HandleEvent(
            IKrEventExtensionContext context)
        {
            var actions = await this.processCache.GetActionsByTypeAsync(
                context.EventType,
                context.CancellationToken);

            if (actions.Count == 0)
            {
                return;
            }

            var pb = KrProcessBuilder.CreateProcess();
            if (context.MainCardID.HasValue)
            {
                pb.SetCard(context.MainCardID.Value);
            }

            if (context.Info is Dictionary<string, object> info)
            {
                pb.SetProcessInfo(info);
            }

            var baseProcess = pb.Build();
            var specifiedParameters = new KrProcessServerLauncher.SpecificParameters
            {
                MainCardAccessStrategy = context.MainCardAccessStrategy,
                RaiseErrorWhenExecutionIsForbidden = false
            };

            foreach (var action in actions)
            {
                var process = KrProcessBuilder
                    .ModifyProcess(baseProcess)
                    .SetProcess(action.ID)
                    .Build();

                var result = await this.processLauncher.LaunchAsync(
                    process,
                    context.CardExtensionContext,
                    specifiedParameters,
                    context.CancellationToken);
                context.ValidationResult.Add(result.ValidationResult);
            }
        }

        #endregion
    }
}