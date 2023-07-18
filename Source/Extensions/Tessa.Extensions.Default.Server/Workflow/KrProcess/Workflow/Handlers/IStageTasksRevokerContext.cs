using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Описывает контекст <see cref="IStageTasksRevoker"/>.
    /// </summary>
    public interface IStageTasksRevokerContext
        : IExtensionContext
    {
        /// <summary>
        /// Контекст обработчика этапа.
        /// </summary>
        IStageTypeHandlerContext Context { get; }
        
        /// <summary>
        /// Идентификатор карточки.
        /// </summary>
        Guid CardID { get; set; }
        
        /// <summary>
        /// Идентификатор задания для отзыва, если отзывается только одно задание.
        /// </summary>
        Guid TaskID { get; set; }
        
        /// <summary>
        /// Список идентификаторов заданий для отзыва
        /// </summary>
        List<Guid> TaskIDs { get; set; }
        
        /// <summary>
        /// Опционально указывается вариант завершения.
        /// Если не указан, то все задания отзываются с вариантом завершения <see cref="DefaultCompletionOptions.Cancel"/>
        /// </summary>
        Guid? OptionID { get; set; }
        
        /// <summary>
        /// Удалить задание из активных.
        /// </summary>
        bool RemoveFromActive { get; set; }
        
        /// <summary>
        /// Действие перед завершением задания.
        /// </summary>
        Action<CardTask> TaskModificationAction { get; set; }
    }
}