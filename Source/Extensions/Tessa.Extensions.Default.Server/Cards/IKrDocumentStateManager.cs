using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект, управляющий состоянием карточки документа.
    /// </summary>
    public interface IKrDocumentStateManager
    {
        /// <summary>
        /// Устанавливает указанное состояние в карточке документа и карточке основного сателлита.
        /// </summary>
        /// <param name="card">Карточка документа.</param>
        /// <param name="mainSatelliteCard">Карточка основного сателлита (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>).</param>
        /// <param name="state">Устанавливаемое состояние.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Кортеж, содержащий:<para/>
        /// Значение <see langword="true"/>, если в карточке документа были изменены поля, содержащие состояние, иначе - <see langword="false"/>.<para/>
        /// Значение <see langword="true"/>, если в карточке основного сателлита были изменены поля, содержащие состояние, иначе - <see langword="false"/>.<para/>
        /// Предыдущее состояние или значение <see langword="null"/>, если в карточке основного сателлита не содержалась информация о состоянии.
        /// </returns>
        ValueTask<(bool HasCardChanges, bool HasMainSatelliteChanges, KrState? OldState)> SetStateAsync(
            Card card,
            Card mainSatelliteCard,
            KrState state,
            CancellationToken cancellationToken = default);
    }
}
