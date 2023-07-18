using System;
using System.Threading;
using Tessa.Cards;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Notices
{
    public class MessageHandlerContext :
        IMessageHandlerContext
    {
        #region Constructors

        public MessageHandlerContext(
            IMessageInfo info,
            NoticeMessage message,
            ISession session,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Card card = null,
            CardTask task = null,
            CancellationToken cancellationToken = default)
        {
            this.Info = info ?? throw new ArgumentNullException(nameof(info));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Session = session ?? throw new ArgumentNullException(nameof(session));
            this.Db = db ?? throw new ArgumentNullException(nameof(db));
            this.BuilderFactory = builderFactory ?? throw new ArgumentNullException(nameof(builderFactory));
            this.Card = card;
            this.Task = task;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IMessageHandlerContext Members

        public IMessageInfo Info { get; }

        public NoticeMessage Message { get; }

        public ISession Session { get; }

        public DbManager Db { get; }

        public IQueryBuilderFactory BuilderFactory { get; }

        public Card Card { get; }

        public CardTask Task { get; }

        public bool Cancel { get; set; }    // = false

        /// <summary>
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </summary>
        public CancellationToken CancellationToken { get; }

        #endregion
    }
}
