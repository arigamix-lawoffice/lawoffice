using System;

namespace Tessa.Extensions.Default.Shared.VirtualFiles
{
    public static class KrVirtualFilesHelper
    {
        /// <summary>
        /// ID виртуального файла листа согласования
        /// </summary>
        public static Guid ApprovalListFileID = new Guid(0x6e69fc8d, 0x8c1d, 0x4ca4, 0xbf, 0x59, 0x0d, 0xba, 0xe4, 0xe2, 0x14, 0x20); // 6e69fc8d-8c1d-4ca4-bf59-0dbae4e21420

        /// <summary>
        /// ID основной версии виртуального файла листа согласования
        /// </summary>
        public static Guid ApprovalListDefaultVersionID = new Guid(0x9b299233, 0x1e28, 0x490d, 0xa0, 0x09, 0x71, 0x4f, 0x8b, 0x0b, 0xa7, 0x4f); // 9b299233-1e28-490d-a009-714f8b0ba74f

        /// <summary>
        /// ID печатной версии виртуального файла листа согласования
        /// </summary>
        public static Guid ApprovalListPrintableVersionID = new Guid(0xf6f565cc, 0x4f6f, 0x4d3b, 0x98, 0xc3, 0xec, 0x59, 0xbf, 0x0d, 0x84, 0x6f); // f6f565cc-4f6f-4d3b-98c3-ec59bf0d846f
    }
}
