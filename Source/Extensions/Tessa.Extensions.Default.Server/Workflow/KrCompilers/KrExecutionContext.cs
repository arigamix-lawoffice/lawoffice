using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrExecutionContext"/>
    public sealed class KrExecutionContext :
        IKrExecutionContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrExecutionContext"/>.
        /// </summary>
        private KrExecutionContext()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrExecutionContext"/>.
        /// </summary>
        /// <param name="cardContext">Контекст расширения карточки содержащейся в контексте выполнения.</param>
        /// <param name="mainCardAccessStrategy">Стратегия загрузки основной карточки.</param>
        /// <param name="cardID">Идентификатор типа карточки.</param>
        /// <param name="cardType">Тип карточки.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="krComponents">Включённые компоненты типового решения для текущей карточки.</param>
        /// <param name="workflowProcess">Объектная модель процесса.</param>
        /// <param name="secondaryProcess">Информацию по вторичному процессу для которого выполняется пересчёт или значение по умолчанию для типа, если пересчёт выполняется для основного процесса.</param>
        /// <param name="executionUnits">Список идентификаторов единиц выполнения, которые необходимо выполнить или значение по умолчанию для типа, если необходимо выполнить все доступные единицы выполнения.</param>
        /// <param name="groupID">Идентификатор группы единиц выполнения.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public KrExecutionContext(
            ICardExtensionContext cardContext,
            IMainCardAccessStrategy mainCardAccessStrategy,
            Guid? cardID,
            CardType cardType,
            Guid? docTypeID,
            KrComponents? krComponents,
            WorkflowProcess workflowProcess,
            IKrSecondaryProcess secondaryProcess = null,
            IEnumerable<Guid> executionUnits = null,
            Guid? groupID = null,
            CancellationToken cancellationToken = default)
        {
            this.CardContext = cardContext;
            this.MainCardAccessStrategy = mainCardAccessStrategy;
            this.CardID = cardID;
            this.CardType = cardType;
            this.DocTypeID = docTypeID;
            this.KrComponents = krComponents;
            this.TypeID = docTypeID ?? cardType?.ID;
            this.WorkflowProcess = workflowProcess;
            this.GroupID = groupID;
            this.SecondaryProcess = secondaryProcess;

            this.ExecutionUnitIDs = executionUnits is null
                ? null
                : new HashSet<Guid>(executionUnits);

            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IKrExecutionContext Members

        /// <inheritdoc />
        public bool ExecuteAll => this.ExecutionUnitIDs is null;

        /// <inheritdoc />
        public HashSet<Guid> ExecutionUnitIDs { get; }

        /// <inheritdoc />
        public IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <inheritdoc />
        public Guid? CardID { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public Guid? TypeID { get; }

        /// <inheritdoc />
        public KrComponents? KrComponents { get; }

        /// <inheritdoc />
        public WorkflowProcess WorkflowProcess { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardContext { get; }

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess { get; }

        /// <inheritdoc />
        public Guid? GroupID { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        /// <inheritdoc />
        public IKrExecutionContext Copy(
            IEnumerable<Guid> executionUnits = null)
        {
            return new KrExecutionContext(
                this.CardContext,
                this.MainCardAccessStrategy,
                this.CardID,
                this.CardType,
                this.DocTypeID,
                this.KrComponents,
                this.WorkflowProcess,
                this.SecondaryProcess,
                executionUnits is null
                ? null
                : new HashSet<Guid>(executionUnits),
                this.GroupID,
                this.CancellationToken);
        }

        /// <inheritdoc />
        public IKrExecutionContext Copy(
            Guid? groupID,
            IEnumerable<Guid> executionUnits = null)
        {
            return new KrExecutionContext(
                this.CardContext,
                this.MainCardAccessStrategy,
                this.CardID,
                this.CardType,
                this.DocTypeID,
                this.KrComponents,
                this.WorkflowProcess,
                this.SecondaryProcess,
                executionUnits is null
                ? null
                : new HashSet<Guid>(executionUnits),
                groupID,
                this.CancellationToken);
        }

        #endregion
    }
}
