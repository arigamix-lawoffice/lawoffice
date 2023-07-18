
namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public enum KrDocNumberRegistrationAutoAssignmentID
    {
        /// <summary>
        /// Не выделять
        /// </summary>
        None = 0,
        /// <summary>
        /// Выделять
        /// </summary>
        Assign = 1,
    }

    public class KrDocNumberRegistrationAutoAssignmentName
    {
        /// <summary>
        /// Не выделять
        /// </summary>
        public const string None = "$Views_KrAutoAssigment_NotToAssign";
        /// <summary>
        /// Выделять
        /// </summary>
        public const string Assign = "$Views_KrAutoAssigment_Assign";
    }
}
