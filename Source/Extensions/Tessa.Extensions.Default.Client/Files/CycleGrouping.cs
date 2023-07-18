using System;
using System.Linq;
using Tessa.Extensions.Default.Shared;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Группировка по циклу согласования
    /// </summary>
    /// <remarks>
    /// Класс-наследник может переопределить поведение класса, например,
    /// установить сортировку по умолчанию для данной группировки.
    /// </remarks>
    public class CycleGrouping : FileGrouping
    {
        #region Constructors

        public CycleGrouping(string name, string caption, bool isCollapsed = false)
            : base(name, caption, isCollapsed, new FileModelPropertyListener(nameof(IFile.Category), nameof(IFile.Origin)))
        {
        }

        #endregion

        #region Private Methods

        private static readonly FileDelegate<FileCaptionFunc> captionDelegate =
            new FileDelegate<FileCaptionFunc>(GetCaption, new[] { nameof(IFileObject.Name), nameof(IFile.Category), nameof(IFile.Origin) });

        private static string GetCaption(IFileViewModel viewModel)
        {
            IFile file = viewModel.Model;
            IFile origin = file.Origin;
            var category = file.Category;

            if (origin == null &&
                category != null &&
                viewModel.Collection
                    .Any(p => p.Model.Category != null &&
                              p.Model.Category.Equals(category)))
            {
                return
                    LocalizationManager.Format(
                        "$UI_Controls_FilesControl_Category", 
                        file.Name,
                        category.Caption);
            }

            if (origin == null &&
                file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                cycleObj != null &&
                file.Info.TryGetValue(CycleGroupingInfoKeys.CreatedByNameKey, out object createtByNameObj) &&
                createtByNameObj != null &&
                file.Info.TryGetValue(CycleGroupingInfoKeys.CreatedKey, out object createdObj) &&
                createdObj != null)
            {
                return 
                    LocalizationManager.Format(
                        "$UI_Controls_FilesControl_Version",
                        file.Name,
                        file.Versions.Last.Number, 
                        (string)createtByNameObj);
            }

            if (origin != null)
            {
                return string.Format(
                    LocalizationManager.GetString("UI_Controls_FilesControl_ByCopy_Format"),
                    file.Name,
                    LocalizationManager.GetString("UI_Controls_FilesControl_ByCopy_Copy"),
                    file.Versions.Last.CreatedByName,
                    FormattingHelper.FormatDateTime(file.Versions.Last.Created));
            }

            return file.Name;
        }

        #endregion

        #region Constants

        /// <summary>
        /// Группа "Согласуемые документы".
        /// </summary>
        public const string DocumentsOnApprovalGroupName = "DocumentsOnApprovalGroup";

        public const string CycleGroupName = "CycleGroup";

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IFileGrouping" and @item="GetGroupInfo"]'/>
        protected override FileGroupInfo GetGroupInfoCore(IFileViewModel viewModel)
        {
            if (viewModel.Model.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleNumberObj) &&
                cycleNumberObj != null &&
                viewModel.Model.Info.TryGetValue(CycleGroupingInfoKeys.CycleOrderKey, out object cycleOrderObj) &&
                cycleOrderObj != null &&
                viewModel.Model.Info.TryGetValue(CycleGroupingInfoKeys.MaxCycleNumberKey, out object maxCycleNumberObj) &&
                maxCycleNumberObj != null)
            {
                int length = Math.Max(1, maxCycleNumberObj.ToString()?.Length ?? 1);
                string cycleNumberString = cycleNumberObj.ToString();
                
                return new 
                    FileGroupInfo(
                        CycleGroupName + cycleNumberString,
                        LocalizationManager.Format("$UI_Controls_FilesControl_CycleGroup", cycleNumberString), 
                        ((int)cycleOrderObj + 1).ToString("D" + length));
            }

            return new FileGroupInfo(DocumentsOnApprovalGroupName, LocalizationManager.GetString("UI_Controls_FilesControl_DocumentsOnApprovalGroup"), "0");
        }

        
        /// <doc path='info[@type="FileControlObject" and @item="Attach"]'/>
        protected override void Attach(IFileViewModel viewModel)
        {
            base.Attach(viewModel);

            viewModel.CaptionDelegateManager.Set(captionDelegate);
        }


        /// <doc path='info[@type="FileControlObject" and @item="Detach"]'/>
        protected override void Detach(IFileViewModel viewModel)
        {
            FileDelegate<FileCaptionFunc> restoredDelegate = viewModel.CaptionDelegateManager.Restore();
            if (restoredDelegate != captionDelegate)
            {
                throw new InvalidOperationException($"{nameof(IFileViewModel)}.{nameof(IFileViewModel.CaptionDelegateManager)} stack is damaged.");
            }

            base.Detach(viewModel);
        }
        
        #endregion
    }
}