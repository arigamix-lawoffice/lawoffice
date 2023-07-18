#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Defines the status of the document.
    /// </summary>
    public enum OnlyOfficeCallbackDocumentStatus
    {
        /// <summary>
        /// document is being edited,
        /// </summary>
        BeingEdited = 1,
        /// <summary>
        /// document is ready for saving,
        /// </summary>
        ReadyForSaving = 2,
        /// <summary>
        /// document saving error has occurred,
        /// </summary>
        SavingError = 3,
        /// <summary>
        /// document is closed with no changes,
        /// </summary>
        ClosedWithNoChanges = 4,
        /// <summary>
        /// document is being edited, but the current document state is saved,
        /// </summary>
        BeingEditedButStateIsSaved = 6,
        /// <summary>
        /// error has occurred while force saving the document.
        /// </summary>
        ForceSavingError = 7
    }
}
