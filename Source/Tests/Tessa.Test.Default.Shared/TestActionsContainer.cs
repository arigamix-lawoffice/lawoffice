using System.Collections.Generic;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="ITestActionsContainer"/>
    public sealed class TestActionsContainer :
        ITestActionsContainer
    {
        #region Fields

        private readonly Dictionary<ActionStage, IList<ITestAction>> actionsByStages = new();

        private readonly object actionsByStagesLock = new();

        #endregion

        #region ITestActionsContainer Members

        /// <inheritdoc/>
        public IList<ITestAction> GetTestActions(ActionStage stage)
        {
            if (this.actionsByStages.TryGetValue(stage, out var actions))
            {
                return actions;
            }

            lock (this.actionsByStagesLock)
            {
                if (this.actionsByStages.TryGetValue(stage, out actions))
                {
                    return actions;
                }

                actions = new List<ITestAction>();
                this.actionsByStages.Add(stage, actions);
                return actions;
            }
        }

        #endregion
    }
}
