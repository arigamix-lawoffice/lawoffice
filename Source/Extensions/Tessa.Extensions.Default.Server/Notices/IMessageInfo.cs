using System;

namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Информация, разобранная из сообщения.
    /// </summary>
    public interface IMessageInfo
    {
        /// <summary>
        /// Идентификатор задания, с которым производятся действия,
        /// или <c>null</c>, если не выполняется стандартная обработка действия, связанного с заданием.
        /// </summary>
        Guid? TaskRowID { get; }
    }
}
