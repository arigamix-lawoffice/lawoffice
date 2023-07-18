using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public sealed class OperationData
    {
        #region Properties

        public bool Success { get; set; }
        
        public List<DepartmentInfo> Departments { get; } = new List<DepartmentInfo>();
        
        public List<UserInfo> Users { get; } = new List<UserInfo>();

        #endregion
    }
}
