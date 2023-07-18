#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// User action type with the document
    /// </summary>
    public enum OnlyOfficeActionType
    {
        /// <summary>
        /// The user disconnects from the document co-editing,
        /// </summary>
        CoeditDisconnect = 0,
        /// <summary>
        /// The new user connects to the document co-editing,
        /// </summary>
        CoeditConnect = 1,
        /// <summary>
        /// The user clicks the forcesave button.
        /// </summary>
        ForcesaveClick = 2
    }
}
