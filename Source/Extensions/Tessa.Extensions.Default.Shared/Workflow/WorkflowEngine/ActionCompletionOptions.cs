using System;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Варианты завершения действий.
    /// </summary>
    /// <remarks>
    /// В базе данных соответствующая информация хранится в таблице <see cref="WorkflowConstants.KrWeActionCompletionOptions.SectionName"/>.
    /// </remarks>
    public static class ActionCompletionOptions
    {
        /// <summary>
        /// Согласовано.
        /// </summary>
        public static readonly Guid Approved = new Guid(0x4339a03f, 0x234d, 0x4a9a, 0xa6, 0xe4, 0x58, 0xa8, 0x8a, 0x5a, 0x03, 0xce);

        /// <summary>
        /// Не согласовано.
        /// </summary>
        public static readonly Guid Disapproved = new Guid(0x6fbdd34b, 0xbe9a, 0x40bf, 0x90, 0xcb, 0x16, 0x40, 0xd4, 0xab, 0xb9, 0xf5);

        /// <summary>
        /// Подписано.
        /// </summary>
        public static readonly Guid Signed = new Guid(0xfa94b7bf, 0x7b99, 0x46d6, 0x9c, 0x65, 0xb2, 0x1a, 0x48, 0x3e, 0xbc, 0x45);

        /// <summary>
        /// Отказано.
        /// </summary>
        public static readonly Guid Declined = new Guid(0x4a1936c7, 0x1f94, 0x4897, 0x9d, 0xae, 0x93, 0x41, 0x63, 0xe2, 0xfe, 0x1c);
    }
}
