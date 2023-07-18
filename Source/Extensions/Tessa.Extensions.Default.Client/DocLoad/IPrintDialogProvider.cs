using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.DocLoad
{
    /// <summary>
    /// Объект, управляющий отображением диалога печати для страницы со штрих-кодом.
    /// </summary>
    public interface IPrintDialogProvider
    {
        /// <summary>
        /// Отображать ли кнопку выбора принтера
        /// </summary>
        /// <returns></returns>
        bool IsPrinterSelectionEnabled();

        /// <summary>
        /// Отображает диалог для печати. Возвращает признак того, что пользователь подтвердил печать.
        /// </summary>
        /// <param name="forceShow">Диалог будет отображён, даже если пользователь уже выбирал принтер.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если пользователь подтвердил печать;
        /// <c>false</c>, если пользователь отменил печать кнопкой "Отмена";
        /// <c>null</c>, если пользователь отменил печать, закрыв окно.
        /// </returns>
        ValueTask<bool?> SelectPrinterDialogAsync(bool forceShow, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет печать указанного документа с параметрами печати, которые ранее были выбраны в диалоге <see cref="SelectPrinterDialogAsync"/>.
        /// </summary>
        /// <param name="digest">Дайджест карточки.</param>
        /// <param name="barcodeBytes">Штрих-код.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task PrintDocumentAsync(string digest, byte[] barcodeBytes, CancellationToken cancellationToken = default);
    }
}