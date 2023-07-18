using System;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Элемент истории выполнения этапов.
    /// </summary>
    public sealed class KrProcessTraceItem
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessTraceItem"/>.
        /// </summary>
        /// <param name="stage">Этап, информация о котором записывается в историю.</param>
        /// <param name="result">Результат выполнения этапа или <see langword="null"/>, если он неизвестен или выполнение этапа было прервано.</param>
        /// <param name="cardID">Идентификатор карточки в рамках которой выполнялся этап или <see langword="null"/>, если он неизвестен.</param>
        /// <param name="processID">Идентификатор процесса, в рамках которого выполнялся этап или значение <see langword="null"/>, если процесс выполнялся в памяти и не имеет идентификатора.</param>
        public KrProcessTraceItem(
            Stage stage,
            StageHandlerResult? result,
            Guid? cardID,
            Guid? processID)
        {
            // Проверка stage на null выполняется в конструкторе копирования.
            this.Stage = new Stage(stage);
            this.Result = result;
            this.CardID = cardID;
            this.ProcessID = processID;
        }

        /// <summary>
        /// Возвращает копию выполненного этапа.
        /// </summary>
        public Stage Stage { get; }

        /// <summary>
        /// Возвращает результат выполнения этапа.
        /// </summary>
        public StageHandlerResult? Result { get; }

        /// <summary>
        /// Возвращает идентификатор карточки, в рамках которой выполнялся этап.
        /// </summary>
        public Guid? CardID { get; }

        /// <summary>
        /// Возвращает идентификатор процесса, в рамках которого выполнялся этап или значение <see langword="null"/>, если процесс выполнялся в памяти и не имеет идентификатора.
        /// </summary>
        public Guid? ProcessID { get; }

        /// <summary>
        /// Возвращает признак того, что этап был прерван.
        /// </summary>
        public bool Interrupted => !this.Result.HasValue;
    }
}