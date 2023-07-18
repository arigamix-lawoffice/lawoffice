using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tessa.Platform;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Views.CardEditor
{
    /// <summary>
    /// Модель представления закрытой карточки на клиенте.
    /// </summary>
    public sealed class ClosedCardViewModel : ViewModel<EmptyModel>
    {

        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса.
        /// </summary>
        public ClosedCardViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Команда переоткрытия карточки или null, если редактор не поддерживает переоткрытие карточки.
        /// </summary>
        public ICommand ReopenCommand { get; init; }

        #endregion

    }
}
