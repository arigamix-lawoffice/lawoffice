using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Построитель процесса KrProcess.
    /// </summary>
    public sealed class KrProcessBuilder
    {
        #region Fields

        private Guid processID;

        private Guid? cardID;
        
        private IDictionary<string, object> processInfo;
        
        private Guid? processHolderID;

        private string parentProcessTypeName;
        
        private Guid? parentProcessID;
        
        private Guid? parentStageRowID;
        
        private int? nestedOrder;

        #endregion

        #region Constructors

        private KrProcessBuilder()
        {

        }

        private KrProcessBuilder(KrProcessInstance instance)
        {
            Check.ArgumentNotNull(instance, nameof(instance));

            this.processID = instance.ProcessID;
            this.cardID = instance.CardID;
            this.processInfo = StorageHelper.Clone(instance.ProcessInfo);
            this.processHolderID = instance.ProcessHolderID;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Устанавливает идентификатор процесса.
        /// </summary>
        /// <param name="pID">Идентификатор процесса.</param>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public KrProcessBuilder SetProcess(
            Guid pID)
        {
            this.processID = pID;
            return this;
        }

        /// <summary>
        /// Устанавливает идентифкатор карточки в которой должен быть запусщен процесс.
        /// </summary>
        /// <param name="id">Идентификатор карточки.</param>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public KrProcessBuilder SetCard(
            Guid id)
        {
            this.cardID = id;
            return this;
        }

        /// <summary>
        /// Устанавливает дополнительную информацию по процессу.
        /// </summary>
        /// <param name="info">Дополнительная информация по процессу.</param>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public KrProcessBuilder SetProcessInfo(
            IDictionary<string, object> info)
        {
            this.processInfo = info;
            return this;
        }

        /// <summary>
        /// Устанавливает информацию по вложенному процессу.
        /// </summary>
        /// <param name="processHolder">Идентификатор карточки держателя процесса.</param>
        /// <param name="parentProcessTypeName">Имя типа родитеского процесса.</param>
        /// <param name="parentProcessID">Идентификатор родительского процесса.</param>
        /// <param name="parentStageRow">Идентификатор строки этапа создавшего этот вложенный процесс.</param>
        /// <param name="nestedOrder">Порядковый номер вложенного опроцесса.</param>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public KrProcessBuilder SetNestedProcess(
            Guid processHolder,
            string parentProcessTypeName,
            Guid? parentProcessID,
            Guid parentStageRow,
            int nestedOrder)
        {
            this.processHolderID = processHolder;
            this.parentProcessTypeName = parentProcessTypeName;
            this.parentProcessID = parentProcessID;
            this.parentStageRowID = parentStageRow;
            this.nestedOrder = nestedOrder;
            return this;
        }

        /// <summary>
        /// Завершает построение процесса.
        /// </summary>
        /// <returns>Информация об экземпляре процесса.</returns>
        public KrProcessInstance Build()
        {
            return new KrProcessInstance(
                this.processID,
                this.cardID,
                this.processInfo,
                this.parentStageRowID,
                this.parentProcessTypeName,
                this.parentProcessID,
                this.processHolderID,
                this.nestedOrder);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Создаёт построитель процесса.
        /// </summary>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public static KrProcessBuilder CreateProcess() => new KrProcessBuilder();

        /// <summary>
        /// Создаёт построитель процесса на основе указанного экземпляра процесса.
        /// </summary>
        /// <param name="instance">Экземпляр процесса.</param>
        /// <returns>Объект <see cref="KrProcessBuilder"/> для создания цепочки.</returns>
        public static KrProcessBuilder ModifyProcess(KrProcessInstance instance) => new KrProcessBuilder(instance);

        #endregion

    }
}