using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Files;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Forms;
using Tessa.UI.Cards.Tasks;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Пример расширения UI, которое позволяет добавлять файлы в задачи карточки.
    /// </summary>
    public sealed class TaskEnableAttachFilesExampleUIExtension : CardUIExtension
    {
        #region Base Overrides

        ///<inheritdoc/>
        public override async Task Saving(ICardUIExtensionContext context)
        {
            // Если в форме с карточкой нет задач, то ничего не делаем.
            if (context.Model.MainForm is not DefaultFormTabWithTasksViewModel mainForm || !mainForm.Tasks.Any())
            {
                return;
            }

            // Перебираем файлы в карточках задач главной формы.
            foreach (var taskViewModel in mainForm.Tasks.OfType<TaskViewModel>())
            {
                await taskViewModel.TaskModel.FileContainer.Files.EnsureAllContentModifiedAsync(context.CancellationToken).ConfigureAwait(false);
                foreach (var fileCard in taskViewModel.TaskModel.CardTask.Card.Files)
                {
                    
                    // Отправляем в контейнер только измененные файлы, либо те, у которых была добавлена или снята электронная подпись.
                    if (fileCard.State != CardFileState.None ||
                        fileCard.Flags != CardFileFlags.None ||
                        (fileCard.Card.Sections.TryGetValue(CardSignatureHelper.SectionName, out CardSection signatures) &&
                        signatures.Rows.Any(x => x.State != CardRowState.None))) 
                    {
                        if (taskViewModel.TaskModel.FileContainer.TryGetFile(fileCard.RowID) is { } file)
                        {
                            // Добавляем файл в контейнер главной карточки, если он есть.
                            context.FileContainer.Files.Add(file);
                        }

                        // Добавляем файл в главную карточку.
                        var newFileCard = context.Card.Files.Add(fileCard);
                        
                        // Записываем в поле TaskID идентификатор текущей задачи.
                        newFileCard.TaskID = taskViewModel.TaskModel.CardTask.Card.ID;
                    }
                }
            }
        }

        #endregion
    }
}
