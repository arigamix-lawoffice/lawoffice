using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Server.Cards;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение, проставляющее информацию о виде задания из TaskCommonInfo.Kind в task.Info.
    /// Это нужно для того, чтобы платформенное расширение <see cref="TaskKindGetExtension"/> по этому Info проставило
    /// заголовок задания. По умолчанию заголовок ставится по таск хистори, но если задание не пишется в историю, то
    /// это расширение позволяет выставить заголовок.
    /// </summary>
    public sealed class KrTaskKindGetExtension : CardGetExtension
    {
        #region Base Overrides

        public override Task AfterRequest(
            ICardGetExtensionContext context)
        {
            Card card;
            ListStorage<CardTask> tasks;
            if (!context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null
                || (tasks = card.TryGetTasks()) == null
                || tasks.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (var task in tasks)
            {
                Card taskCard;
                StringDictionaryStorage<CardSection> sections;

                if ((taskCard = task.TryGetCard()) != null
                    && (sections = taskCard.TryGetSections()) != null
                    && sections.TryGetValue(KrConstants.TaskCommonInfo.Name, out var taskCommonInfo)
                    && taskCommonInfo.RawFields.TryGetValue(KrConstants.TaskCommonInfo.KindID, out var idObj)
                    && taskCommonInfo.RawFields.TryGetValue(KrConstants.TaskCommonInfo.KindCaption, out var captionObj)
                    && idObj is Guid id
                    && captionObj is string caption
                    && !string.IsNullOrWhiteSpace(caption))
                {
                    task.Info[CardHelper.TaskKindIDKey] = id;
                    task.Info[CardHelper.TaskKindCaptionKey] = caption;
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}