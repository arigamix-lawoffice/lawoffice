








using System;
using Tessa.Files;

namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region TemplateInfo

    public sealed class TemplateInfo
    {
        public readonly Guid ID;
        public readonly string Name;

        public TemplateInfo(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #region Template

    public sealed class TemplateFileInfo
    {
         /// <summary>
         ///     File templates for "$ApprovalHistory_Name_Default": {9cf3a99f-3818-4176-b1d5-df44a2217160}.
         /// </summary>
         public readonly TemplateInfo ApprovalHistoryNameDefault = new TemplateInfo(new Guid(0x9cf3a99f,0x3818,0x4176,0xb1,0xd5,0xdf,0x44,0xa2,0x21,0x71,0x60), "$ApprovalHistory_Name_Default");

         /// <summary>
         ///     File templates for "$FileTemplate_MyTasks_Excel": {4b437916-7c46-4f90-bf78-fbabe65b7392}.
         /// </summary>
         public readonly TemplateInfo FileTemplateMyTasksExcel = new TemplateInfo(new Guid(0x4b437916,0x7c46,0x4f90,0xbf,0x78,0xfb,0xab,0xe6,0x5b,0x73,0x92), "$FileTemplate_MyTasks_Excel");

         /// <summary>
         ///     File templates for "$FileTemplate_MyTasks_Html": {166a2dcf-5594-49ae-973f-586386151a8f}.
         /// </summary>
         public readonly TemplateInfo FileTemplateMyTasksHtml = new TemplateInfo(new Guid(0x166a2dcf,0x5594,0x49ae,0x97,0x3f,0x58,0x63,0x86,0x15,0x1a,0x8f), "$FileTemplate_MyTasks_Html");

         /// <summary>
         ///     File templates for "$ApprovalHistory_Name_Printable": {e13a8b06-4d9c-4163-906f-94800cc3608f}.
         /// </summary>
         public readonly TemplateInfo ApprovalHistoryNamePrintable = new TemplateInfo(new Guid(0xe13a8b06,0x4d9c,0x4163,0x90,0x6f,0x94,0x80,0x0c,0xc3,0x60,0x8f), "$ApprovalHistory_Name_Printable");

         /// <summary>
         ///     File templates for "$KrProtocols_ProtocolOfMeetingExcelTitle": {88ca8652-9e8e-4d3a-bf8a-45db6957530f}.
         /// </summary>
         public readonly TemplateInfo KrProtocolsProtocolOfMeetingExcelTitle = new TemplateInfo(new Guid(0x88ca8652,0x9e8e,0x4d3a,0xbf,0x8a,0x45,0xdb,0x69,0x57,0x53,0x0f), "$KrProtocols_ProtocolOfMeetingExcelTitle");

         /// <summary>
         ///     File templates for "$KrProtocols_ProtocolOfMeetingTitle": {49e340f6-c478-4a11-8b03-999aa22aa9fd}.
         /// </summary>
         public readonly TemplateInfo KrProtocolsProtocolOfMeetingTitle = new TemplateInfo(new Guid(0x49e340f6,0xc478,0x4a11,0x8b,0x03,0x99,0x9a,0xa2,0x2a,0xa9,0xfd), "$KrProtocols_ProtocolOfMeetingTitle");

    }

    #endregion

    #region VirtualInfo

    public sealed class VirtualInfo
    {
        public readonly Guid ID;
        public readonly string Name;

        public VirtualInfo(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #region Virtual

    public sealed class VirtualFileInfo
    {
         /// <summary>
         ///     VirtualFiles for "$ApprovalHistory_Name_Default": {6e69fc8d-8c1d-4ca4-bf59-0dbae4e21420}.
         /// </summary>
         public readonly VirtualInfo ApprovalHistoryNameDefault = new VirtualInfo(new Guid(0x6e69fc8d,0x8c1d,0x4ca4,0xbf,0x59,0x0d,0xba,0xe4,0xe2,0x14,0x20), "$ApprovalHistory_Name_Default");

    }

    #endregion

    public static class FilesInfo
    {
        #region FileInfo

        public static readonly TemplateFileInfo Template = new TemplateFileInfo();
        public static readonly VirtualFileInfo Virtual = new VirtualFileInfo();

        #endregion
    }
}