using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет информацию об отложенном действии.
    /// </summary>
    public interface IPendingAction :
        ISealable
    {
        #region Properties

        /// <summary>
        /// Возвращает название отложенного действия.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Возвращает список действий, выполняющихся перед выполнением отложенного действия.
        /// </summary>
        IReadOnlyCollection<IPendingAction> PreparationActions { get; }

        /// <summary>
        /// Возвращает список действий, выполняющихся после выполнения отложенного действия.
        /// </summary>
        IReadOnlyCollection<IPendingAction> AfterActions { get; }

        /// <summary>
        /// Возвращает дополнительную информацию. Возвращаемое значение всегда не равно <see langword="null"/>.
        /// </summary>
        Dictionary<string, object> Info { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Выполняет отложенное действие и подготовительные действия передавая текущий экземпляр в качестве первого параметра метода реализующего действие.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения.</returns>
        /// <remarks>
        /// При возникновении ошибки, при обработке подготовительных действий, дальнейшее выполнение прекращается, основное действие не выполняется.<para/>
        /// Результаты выполнения предварительных действий и основного действия, если они есть, предваряются информационными сообщениями. Для их исключения, на итоговом результате выполнения, необходимо вызвать метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>.
        /// </remarks>
        ValueTask<ValidationResult> ExecuteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавляет указанное действие в список действий выполняющихся перед выполнением отложенного действия.
        /// </summary>
        /// <param name="pendingAction">Отложенное действие добавляемое в список действий выполняющихся перед отложенным действием.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pendingAction"/> имеет значение <see langword="null"/>.</exception>
        /// <exception cref="ObjectSealedException">Нельзя изменять список действий выполняющихся перед выполнением отложенного действия при выполнении действия.</exception>
        /// <exception cref="ArgumentException">Can't add the action into its own actions list, which is used to execute before the pending action.</exception>
        void AddPreparationAction(IPendingAction pendingAction);

        /// <summary>
        /// Добавляет указанное действие в список действий, выполняющихся после выполнения отложенного действия.
        /// </summary>
        /// <param name="pendingAction">Отложенное действие добавляемое в список действий выполняющихся после отложенного действия.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="pendingAction"/> имеет значение <see langword="null"/>.</exception>
        /// <exception cref="ObjectSealedException">Нельзя изменять список действий выполняющихся после выполнения отложенного действия при выполнении действия.</exception>
        /// <exception cref="ArgumentException">Can't add the action into its own actions list, which is used to execute after the pending action.</exception>
        void AddAfterAction(IPendingAction pendingAction);

        /// <summary>
        /// Устанавливает значение свойства <see cref="Info"/>.
        /// </summary>
        /// <param name="dict">Словарь, содержащий дополнительную информацию.</param>
        /// <param name="isReplaceInfo">Значение <see langword="true"/>, если текущее значение <see cref="Info"/> должно быть заменено на значение <paramref name="dict"/>, иначе текущее значение <see cref="Info"/> будет объединено с переданным словарём по правилам <see cref="StorageHelper.Merge(IDictionary{string, object}, IDictionary{string, object})"/>.</param>
        void SetInfo(Dictionary<string, object> dict, bool isReplaceInfo = false);

        #endregion
    }
}