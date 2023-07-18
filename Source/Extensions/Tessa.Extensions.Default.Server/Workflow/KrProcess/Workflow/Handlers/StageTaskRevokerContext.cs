using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Контекст <see cref="IStageTasksRevoker"/>.
    /// </summary>
    public sealed class StageTaskRevokerContext : IStageTasksRevokerContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StageTaskRevokerContext"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public StageTaskRevokerContext(
            IStageTypeHandlerContext context,
            CancellationToken cancellationToken = default)
        {
            this.Context = context;
            this.CancellationToken = cancellationToken;
        }

        /// <inheritdoc />
        public IStageTypeHandlerContext Context { get; }

        /// <inheritdoc />
        public Guid CardID { get; set; }

        /// <inheritdoc />
        public Guid TaskID { get; set; }

        /// <inheritdoc />
        public List<Guid> TaskIDs { get; set; }

        /// <inheritdoc />
        public Guid? OptionID { get; set; }

        /// <inheritdoc />
        public bool RemoveFromActive { get; set; }

        /// <inheritdoc />
        public Action<CardTask> TaskModificationAction { get; set; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }
    }
}