using Tessa.Extensions.Platform.Shared.Conditions;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Shared.Conditions
{
    /// <summary>
    /// Расширение на починку условий при импорте карточек типового решения.
    /// </summary>
    public sealed class DefaultConditionsCardRepairExtension : ConditionsCardRepairExtensionBase
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="conditionRepairManager"><inheritdoc cref="IConditionRepairManager" path="/summary"/></param>
        public DefaultConditionsCardRepairExtension(
            IConditionRepairManager conditionRepairManager)
            : base(conditionRepairManager)
        {
        }

        #endregion
    }
}
