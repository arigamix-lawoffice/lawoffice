using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Files;
using Tessa.UI.Files.Controls;
using Tessa.UI.Menu;

namespace Tessa.Extensions.Default.Client.Files
{
    public sealed class WfCardFileExtension :
        FileExtension
    {
        #region Private Methods

        private static async Task CreateCopyInMainCardAsync(IFileControl control, IFileCollection files)
        {
            IFile[] operationFiles = files.ToArray();
            
            ValueTask resetPreviewTask = await DispatcherHelper.InvokeInUIAsync(() => FileControlHelper.ResetIfInPreviewAsync(control.Manager, operationFiles));
            await resetPreviewTask;

            if (!await FileControlHelper.CheckCanDownloadFilesAndShowMessagesAsync(operationFiles))
            {
                return;
            }

            var totalValidationResult = new ValidationResultBuilder();

            var copiedFileList = new List<IFile>(operationFiles.Length);
            for (int i = 0; i < operationFiles.Length; i++)
            {
                IFile file = operationFiles[i];

                if (file.Category?.ID != WfHelper.MainCardCategoryID)
                {
                    (IFile copiedFile, ValidationResult result) = await file.CopyAsync();
                    totalValidationResult.Add(result);

                    if (!result.IsSuccessful)
                    {
                        break;
                    }

                    // мы создаём копию, но не сохраняем ссылку на файл, из которого он был скопирован
                    await copiedFile.SetOriginAsync(null);
                    await copiedFile.SetCategoryAsync(new FileCategory(WfHelper.MainCardCategoryID, WfHelper.MainCardCategoryCaption));

                    // поскольку файл новый - не пробрасываем PropertyChanged в поток UI
                    await copiedFile.Permissions.SetCanModifyCategoryAsync(false);
                    copiedFile.Info[WfHelper.CopiedToMainCardKey] = BooleanBoxes.True;

                    copiedFileList.Add(copiedFile);
                }
            }

            // если есть любое сообщение (ошибки или предупреждения) - показываем пользователю
            ValidationResult totalResult = totalValidationResult.Build();
            await TessaDialog.ShowNotEmptyAsync(totalResult);

            if (totalResult.IsSuccessful)
            {
                await control.Container.Files.AddWithNotificationAsync(copiedFileList);
            }
        }

        #endregion

        #region Base Overrides

        public override async Task OpeningMenu(IFileExtensionContext context)
        {
            if (UIContext.Current.CardEditor?.CardModel?.CardType.ID != DefaultCardTypes.WfTaskCardTypeID)
            {
                return;
            }

            IFileControl control = context.Control;
            IFileCollection files = context.Files;

            bool canCopyToMainCard = files.Any(x => x.Category?.ID != WfHelper.MainCardCategoryID);

            int index = context.Actions.IndexOf(x => x.Name == FileMenuActionNames.Separator2);
            if (index < 0)
            {
                index = 0;
            }

            context.Actions.Insert(
                index,
                new MenuSeparatorAction(
                    "WfSeparator1",
                    isCollapsed: !canCopyToMainCard));

            context.Actions.Insert(
                index + 1,
                new MenuAction(
                    FileMenuActionNames.CopyFromTaskToMainCard,
                    "$WfResolution_CopyFileFromTaskToMainCard",
                    context.Icons.Get("Thin119"),
                    new DelegateCommand(async x => await CreateCopyInMainCardAsync(control, files)),
                    isCollapsed: !canCopyToMainCard));
        }

        #endregion
    }
}
