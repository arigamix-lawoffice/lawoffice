using System.Threading;
using Tessa.Cards;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Notices
{
    /// <summary>
    /// Контекст операции обработки сообщения мобильного согласования <see cref="IMessageHandler"/>.
    /// </summary>
    public interface IMessageHandlerContext
    {
        /// <summary>
        /// Информация, разобранная из сообщения.
        /// </summary>
        IMessageInfo Info { get; }

        /// <summary>
        /// Обрабатываемое сообщение.
        /// </summary>
        NoticeMessage Message { get; }

        /// <summary>
        /// Сессия текущего пользователя.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Объект для взаимодействия с базой данных.
        /// </summary>
        DbManager Db { get; }

        /// <summary>
        /// Объект для генерации запросов к базе данных.
        /// </summary>
        IQueryBuilderFactory BuilderFactory { get; }

        /// <summary>
        /// Карточка, с которой производятся действия, или <c>null</c>, если карточка недоступна.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Задания, с которым производятся действия (обычно это завершаемое задание),
        /// или <c>null</c>, если задание недоступно.
        /// </summary>
        CardTask Task { get; }

        /// <summary>
        /// Признак того, что сохранение карточки <see cref="Card"/> будет отменено. По умолчанию <c>false</c>.
        /// </summary>
        bool Cancel { get; set; }
        
        /// <summary>
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}
