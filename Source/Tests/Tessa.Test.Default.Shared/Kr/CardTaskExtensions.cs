using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="CardTask"/>.
    /// </summary>
    public static class CardTaskExtensions
    {
        #region CardTask Extensions

        /// <summary>
        /// Устанавливает указанный текст в заданном поле секции задания.
        /// </summary>
        /// <param name="task">Задание, в поле карточки которого необходимо установить значение.</param>
        /// <param name="text">Устанавливаемый текст.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="field">Имя поля.</param>
        public static void SetTaskComment(
            this CardTask task,
            string text = ".",
            string section = KrConstants.KrTask.Name,
            string field = KrConstants.KrTask.Comment) =>
            task.Card.Sections[section].Fields[field] = text;

        #endregion
    }
}
