using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Описывает объект, позволяющий определить группу в истории заданий.
    /// </summary>
    public interface IKrTaskHistoryResolver
    {
        /// <summary>
        /// Текущий используемый менеджер истории заданий.
        /// </summary>
        ICardTaskHistoryManager TaskHistoryManager { get; }

        /// <summary>
        /// Возвращает группу в истории заданий, вычисленную для заданных параметров. При необходимости группа будет создана.
        /// </summary>
        /// <param name="groupTypeID">Идентификатор типа группы, которую требуется найти или добавить.</param>
        /// <param name="parentGroupTypeID">
        /// <para>
        /// Идентификатор типа родительской группы.
        /// </para>
        /// <para>
        /// Если родительская группа указана, то будет выбрана родительская группа заданного типа с наибольшей итерацией.
        /// </para>
        /// <para>
        /// Если родительская группа отсутствует, то она будет создана.
        /// </para>
        /// </param>
        /// <param name="newIteration">
        /// <para>
        /// Признак того, что метод всегда добавляет итерацию для группы типа <paramref name="groupTypeID" />.
        /// </para>
        /// <para>
        /// Если указано <see langword="true"/>, то метод создаёт новый экземпляр группы как при её существовании (тогда увеличивается номер итерации),
        /// так и при её отсутствии (тогда указывается итерация номер <c>1</c>).
        /// </para>
        /// <para>
        /// Если указано <see langword="false"/>, то метод возвращает экземпляр группы без его создания, если группа заданного типа была найдена
        /// (возвращается группа с наибольшей итерацией); если же группа не найдена, то также создаётся экземпляр группы с итерацией номер <c>1</c>.
        /// </para>
        /// </param>
        /// <param name="overrideValidationResult">
        /// <para>
        /// Результат валидации, содержащий информацию по проблемам, возникшим при загрузке истории заданий (если она ещё не была загружена)
        /// и при вычислении названия группы <c>Caption</c> (при замене плейсхолдеров).
        /// Вычисление названия группы выполняется при добавлении группы, а также при добавлении родительской группы.
        /// </para>
        /// <para>
        /// Если указано значение <see langword="null"/>, то используется стандартный объект <c>Manager.ValidationResult</c>,
        /// который влияет на успешность процесса сохранения и на сообщения, которые будут возвращены в результате сохранения.
        /// </para>
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>
        /// Созданная или найденная строка с информацией по группе в истории заданий, которая соответствует переданным параметрами,
        /// или <see langword="null"/>, если не удалось создать группу (например, ошибки в плейсхолдерах в карточке типа группы).
        /// </returns>
        ValueTask<CardTaskHistoryGroup> ResolveTaskHistoryGroupAsync(
            Guid groupTypeID,
            Guid? parentGroupTypeID = null,
            bool newIteration = false,
            IValidationResultBuilder overrideValidationResult = null,
            CancellationToken cancellationToken = default);
    }
}