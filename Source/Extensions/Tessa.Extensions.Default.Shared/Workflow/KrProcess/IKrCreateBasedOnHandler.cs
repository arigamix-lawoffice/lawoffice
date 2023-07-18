using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Объект, выполняющий копирование информации при создании карточки на основании другой карточки
    /// (например, используя плитку "Создать на основании").
    /// </summary>
    public interface IKrCreateBasedOnHandler
    {
        /// <summary>
        /// Выполняет копирование информации, такой как поля из секции <c>DocumentCommonInfo</c>
        /// из карточки <paramref name="baseCard"/> в карточку <paramref name="newCard"/>.
        /// </summary>
        /// <param name="baseCard">Карточка, из которой копируется информация, например, данные из секции <c>DocumentCommonInfo</c>.</param>
        /// <param name="newCard">Карточка, в которую копируется информация из <paramref name="baseCard"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<ValidationResult> CopyInfoAsync(Card baseCard, Card newCard, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет копирование невиртуальных файлов из карточки <paramref name="baseCard"/> в карточку <paramref name="newCard"/>.
        /// </summary>
        /// <param name="baseCard">Карточка, из которой копируются файлы.</param>
        /// <param name="newCard">Карточка, в которую копируются файлы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<ValidationResult> CopyFilesAsync(Card baseCard, Card newCard, CancellationToken cancellationToken = default);
    }
}