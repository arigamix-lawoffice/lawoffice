using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public sealed class DepartmentInfo :
        TimeZoneObject
    {
        #region Properties

        public int DepartmentID { get; set; }

        public int ParentDepartmentID { get; set; } = -1;

        public string Name { get; set; }

        public Guid? TessaID { get; set; }

        public string ExternalID { get; set; }

        public List<UserInfo> Users { get; } = new List<UserInfo>();

        #endregion
    }
}