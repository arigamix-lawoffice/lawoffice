using System;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Идентификаторы типов групп истории заданий, задействованные в типовом решении.
    /// </summary>
    public static class DefaultTaskHistoryGroupTypes
    {
        #region Static Fields
        
        /// <summary>
        /// Тип группы истории заданий "Согласование" - родительская группа.
        /// </summary>
        public static readonly Guid KrApproval =                    // 3344515F-B7AF-4E89-8684-EB94DBC9B1E6
            new Guid(0x3344515f, 0xb7af, 0x4e89, 0x86, 0x84, 0xeb, 0x94, 0xdb, 0xc9, 0xb1, 0xe6);
                
        /// <summary>
        /// Тип группы истории заданий "Согласование" - дочерняя группа с указанием цикла согласования.
        /// </summary>
        public static readonly Guid KrApprovalCycle =               // 3B61470E-AAF8-4CC6-BA37-4B243FB295AB
            new Guid(0x3b61470e, 0xaaf8, 0x4cc6, 0xba, 0x37, 0x4b, 0x24, 0x3f, 0xb2, 0x95, 0xab);
                
        /// <summary>
        /// Тип группы истории заданий "Типовой процесс отправки задач" - родительская группа для всех задач, не принадлежащих маршруту.
        /// </summary>
        public static readonly Guid WfResolutions =                 // 915A0961-2B76-4D4C-BA2F-E0346AC8FFE9
            new Guid(0x915a0961, 0x2b76, 0x4d4c, 0xba, 0x2f, 0xe0, 0x34, 0x6a, 0xc8, 0xff, 0xe9);

        #endregion
    }
}
