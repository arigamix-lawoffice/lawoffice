
namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public enum KrDocNumberRegularAutoAssignmentID
    {
        /// <summary>
        /// Не выделять
        /// </summary>
        None = 0,
        /// <summary>
        /// При создании
        /// </summary>
        WhenCreating = 1,
        /// <summary>
        /// При сохранении
        /// </summary>
        WhenSaving = 2,
    }

    public class KrDocNumberRegularAutoAssignmentName
    {
        /// <summary>
        /// Не выделять
        /// </summary>
        public const string None = "$Views_KrAutoAssigment_NotToAssign";
        /// <summary>
        /// При создании
        /// </summary>
        public const string WhenCreating = "$Views_KrAutoAssigment_WhenCreating";
        /// <summary>
        /// При сохранении
        /// </summary>
        public const string WhenSaving = "$Views_KrAutoAssigment_WhenSaving";
    }
}
