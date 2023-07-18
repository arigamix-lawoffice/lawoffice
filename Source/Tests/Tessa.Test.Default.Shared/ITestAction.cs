using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Представляет действие.
    /// </summary>
    public interface ITestAction
    {
        #region Properties

        /// <summary>
        /// Возвращает источник действия.
        /// </summary>
        object Sender { get; }

        /// <summary>
        /// Возвращает или задаёт параметры действия.
        /// </summary>
        TestActionOptions Options { get; set; }

        /// <summary>
        /// Возвращает или задаёт состояние действия.
        /// </summary>
        TestActionState State { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Выполняет заданное действие.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        ValueTask ExecuteAsync();

        #endregion
    }
}
