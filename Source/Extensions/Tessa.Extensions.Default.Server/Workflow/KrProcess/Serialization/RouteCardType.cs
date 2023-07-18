using System;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Перечисление типов карточек маршрута.
    /// </summary>
    /// <see cref="KrProcessSharedHelper.DesignTimeCard(Guid)"/>
    /// <see cref="KrProcessSharedHelper.RuntimeCard(Guid)"/>
    public enum RouteCardType
    {
        /// <summary>
        /// Карточка шаблона маршрута.
        /// </summary>
        Template = 0,

        /// <summary>
        /// Карточка документа.
        /// </summary>
        Document = 1,
    }
}