using System.Collections.Generic;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, предоставляющий списки действий.
    /// </summary>
    public interface ITestActionsContainer
    {
        /// <summary>
        /// Возвращает список действий, соответствующий заданному этапу.
        /// </summary>
        /// <param name="stage">Этап выполнения действия.</param>
        /// <returns>Список действий.</returns>
        IList<ITestAction> GetTestActions(ActionStage stage);
    }
}
