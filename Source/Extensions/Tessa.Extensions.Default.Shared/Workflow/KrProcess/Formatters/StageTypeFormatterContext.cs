using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Предоставляет контекст форматтера этап.
    /// </summary>
    public class StageTypeFormatterContext : IStageTypeFormatterContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StageTypeFormatterContext"/>.
        /// </summary>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="info">Дополнительная информация.</param>
        /// <param name="card">Карточка содержащая этап.</param>
        /// <param name="stageRow">Строка содержащая этап.</param>
        /// <param name="settings">Словарь содержащий настройки этапа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public StageTypeFormatterContext(
            ISession session,
            Dictionary<string, object> info,
            Card card,
            CardRow stageRow,
            IDictionary<string, object> settings,
            CancellationToken cancellationToken = default)
        {
            this.Session = session;
            this.Info = info;
            this.Card = card;
            this.StageRow = stageRow;
            this.Settings = settings;
            this.CancellationToken = cancellationToken;
        }

        /// <inheritdoc />
        public ISession Session { get; }

        /// <inheritdoc />
        public Dictionary<string, object> Info { get; }

        /// <inheritdoc />
        public Card Card { get; }

        /// <inheritdoc />
        public CardRow StageRow { get; }

        /// <inheritdoc />
        public IDictionary<string, object> Settings { get; }

        /// <inheritdoc />
        public string DisplayTimeLimit { get; set; }

        /// <inheritdoc />
        public string DisplayParticipants { get; set; }

        /// <inheritdoc />
        public string DisplaySettings { get; set; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }
    }
}