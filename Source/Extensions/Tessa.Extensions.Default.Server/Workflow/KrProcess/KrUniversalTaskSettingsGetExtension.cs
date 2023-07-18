using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение необходимо для заполнения поля Order для старых этапов до добавления этого поля.
    /// </summary>
    public sealed class KrUniversalTaskSettingsGetExtension : CardGetExtension
    {
        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            var card = context.Response.Card;

            if (card.Sections.TryGetValue(KrConstants.KrUniversalTaskOptionsSettingsVirtual.Synthetic, out var optionsSection))
            {
                var orderCounters = new Dictionary<Guid, int>();
                foreach (var row in optionsSection.Rows)
                {
                    var order = row.TryGet<int?>(KrConstants.KrUniversalTaskOptionsSettingsVirtual.Order);
                    var stageRowID = row.TryGet<Guid>(KrConstants.StageRowIDReferenceToOwner);
                    if (!order.HasValue)
                    {
                        orderCounters.TryGetValue(stageRowID, out var counter);
                        row.Fields[KrConstants.KrUniversalTaskOptionsSettingsVirtual.Order] = Int32Boxes.Box(counter++);
                        orderCounters[stageRowID] = counter;
                        row.State = CardRowState.Modified;
                    }
                }
            }

            // Часть расширения, которая переводит сотрудника из поля Исполнитель в секцию Исполнители для этапов Настраиваемое задание.
            if (card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var stagesSection)
                && card.Sections.TryGetValue(KrConstants.KrPerformersVirtual.Synthetic, out var performersSection))
            {
                var isDirty = false;
                foreach (var stageRow in stagesSection.Rows)
                {
                    if (stageRow.TryGet<Guid?>(KrConstants.KrStages.StageTypeID) == StageTypeDescriptors.UniversalTaskDescriptor.ID)
                    {
                        var performerID = stageRow.TryGet<Guid?>(KrConstants.KrSinglePerformerVirtual.PerformerID);
                        if (performerID.HasValue)
                        {
                            var newRow = performersSection.Rows.Add();
                            newRow.Fields[KrConstants.KrPerformersVirtual.PerformerID] = performerID;
                            newRow.Fields[KrConstants.KrPerformersVirtual.PerformerName] = stageRow.TryGet<string>(KrConstants.KrSinglePerformerVirtual.PerformerName);
                            newRow.Fields[KrConstants.KrPerformersVirtual.SQLApprover] = BooleanBoxes.False;
                            newRow.Fields[KrConstants.KrPerformersVirtual.Order] = Int32Boxes.Zero;
                            newRow.Fields[KrConstants.StageRowIDReferenceToOwner] = stageRow.RowID;
                            newRow.RowID = Guid.NewGuid();
                            newRow.State = CardRowState.Inserted;

                            stageRow.Fields[KrConstants.KrSinglePerformerVirtual.PerformerID] = null;
                            stageRow.Fields[KrConstants.KrSinglePerformerVirtual.PerformerName] = null;
                            stageRow.State = CardRowState.Modified;
                            isDirty = true;
                        }
                    }
                }
                // Этот костыль нужен, чтобы передать с сервера на клиент .state и .changed для всех строк, иначе
                // ListStorageCompressor упадет на методе компресии, если UniversalTaskStage первый этап, но не единственный,
                // или обновление строки секции KrStagesVirtual не передасться на клиент, т.к. ListStorageCompressor использует список полей для компресии от первой строки
                if (isDirty)
                {
                    foreach (var row in stagesSection.Rows)
                    {
                        if (row.State == CardRowState.None)
                        {
                            row.State = CardRowState.Modified;
                            row.SetChanged(CardRow.RowIDKey, true);
                            row.SetChanged(CardRow.RowIDKey, false);
                            row.State = CardRowState.None;
                        }
                    }
                    // Аналогично для секции Исполнители
                    foreach (var row in performersSection.Rows)
                    {
                        if (row.State == CardRowState.None)
                        {
                            row.State = CardRowState.Modified;
                            row.SetChanged(CardRow.RowIDKey, true);
                            row.SetChanged(CardRow.RowIDKey, false);
                            row.State = CardRowState.None;
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
