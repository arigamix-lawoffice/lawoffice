using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    public static class CycleGroupingUIHelper
    {
        #region Methods

        /// <summary>
        /// Переключает видимости файлов в зависимости от текущего режима группировки по циклам.
        /// </summary>
        /// <param name="control">IFileControl</param>
        /// <param name="currentCycle">Текущий цикл</param>
        /// <param name="mode">Режим отображенгия файлов <see cref="CycleFilesMode"/></param>
        public static async Task SwitchFilesVisibilityAsync(IFileControl control, Card card, int? currentCycle, CycleFilesMode mode)
        {
            var task = await DispatcherHelper.InvokeInUIAsync(async () =>
            {
                switch (mode)
                {
                    case CycleFilesMode.ShowAllCycleFiles:
                        ReturnAllFiles(control);
                        break;

                    case CycleFilesMode.ShowCurrentCycleFilesOnly:
                        if (currentCycle.HasValue)
                        {
                            // Добавить в контейнер все файлы относящиеся к последнему циклу
                            foreach (var containerFile in control.Container.Files)
                            {
                                if (containerFile.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                                    cycleObj != null &&
                                    (int) cycleObj == currentCycle &&
                                    control.Files.All(p => p.ID != containerFile.ID))
                                {
                                    control.Files.Add(containerFile);
                                }
                            }
                            // Удалить все файлы, что не относятся к последнему циклу
                            for (int i = control.Files.Count - 1; i > -1; i--)
                            {
                                var file = control.Files[i];
                                if (file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey,
                                        out object cycleObj) &&
                                    cycleObj != null &&
                                    (int)cycleObj != currentCycle)
                                {
                                    control.Files.RemoveAt(i);
                                }
                            }
                        }
                        break;

                    case CycleFilesMode.ShowCurrentAndLastCycleFilesOnly:
                        if (currentCycle.HasValue)
                        {
                            // Добавить в контейнер все файлы, относящиеся к последнему и предпоследнему циклу
                            foreach (var containerFile in control.Container.Files)
                            {
                                var cardFile = card.Files.FirstOrDefault(x => x.RowID == containerFile.ID);
                                if (cardFile?.IsVirtual != false &&
                                    containerFile.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                                    cycleObj != null &&
                                    ((int) cycleObj == currentCycle ||
                                        (currentCycle > 1 && (int) cycleObj == currentCycle - 1)) &&
                                    control.Files.All(p => p.ID != containerFile.ID))
                                {
                                    control.Files.Add(containerFile);
                                }
                            }
                            // Удалить все файлы, что не относятся к последнему и предпоследнему циклу
                            for (int i = control.Files.Count - 1; i > -1; i--)
                            {
                                var file = control.Files[i];
                                if (file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey,
                                        out object cycleObj) &&
                                    cycleObj != null &&
                                    (int)cycleObj != currentCycle &&
                                    (currentCycle > 1 && (int)cycleObj != currentCycle - 1))
                                {
                                    control.Files.RemoveAt(i);
                                }
                            }
                        }
                        break;
                }
            });

            await task;
        }
        

        public static async Task ModifyFilesListAsync(
            IFileControl control,
            Card card,
            CycleFilesMode currentMode,
            CycleFilesMode mode)
        {
            var task = await DispatcherHelper.InvokeInUIAsync(async () =>
            {
                // Какой сейчас последний цикл?
                int? currentCycle = card.Sections["KrApprovalHistoryVirtual"]?.Rows.Max(p => p.Fields.Get<int>("Cycle"));

                if (currentMode != CycleFilesMode.ShowAllCycleFiles &&
                    mode != CycleFilesMode.ShowAllCycleFiles)
                {
                    // Добавить все файлы назад
                    ReturnAllFiles(control);
                }

                switch (mode)
                {
                    case CycleFilesMode.ShowAllCycleFiles:
                        // Добавить все файлы назад
                        ReturnAllFiles(control);
                        break;
                    case CycleFilesMode.ShowCurrentCycleFilesOnly:
                        if (currentCycle.HasValue)
                        {
                            // Удалить все файлы, что не относятся к последнему циклу
                            for (int i = control.Files.Count - 1; i > -1; i--)
                            {
                                var file = control.Files[i];
                                if (file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey,
                                        out object cycleObj) &&
                                    cycleObj != null &&
                                    (int)cycleObj != currentCycle)
                                {
                                    control.Files.RemoveAt(i);
                                }
                            }
                        }
                        break;
                    case CycleFilesMode.ShowCurrentAndLastCycleFilesOnly:
                        if (currentCycle.HasValue)
                        {
                            // Удалить все файлы, что не относятся к последнему и предпоследнему циклу
                            for (int i = control.Files.Count - 1; i > -1; i--)
                            {
                                var file = control.Files[i];
                                if (file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey,
                                        out object cycleObj) &&
                                    cycleObj != null &&
                                    (int)cycleObj != currentCycle &&
                                    (currentCycle > 1 && (int)cycleObj != currentCycle - 1))
                                {
                                    control.Files.RemoveAt(i);
                                }
                            }
                        }
                        break;
                }

                await control.ExecuteInContextAsync(
                    async (context, token) =>
                    {
                        // Записываем в инфо UIContext`а, только если есть имя (алиас) контрола
                        if (control.Name != null)
                        {
                            var controlsModes =
                                context.Info.TryGet<Dictionary<string, CycleFilesMode>>(CycleGroupingInfoKeys
                                    .CycleGroupingModeKey) ??
                                new Dictionary<string, CycleFilesMode>();

                            // Не делаем лишнего присваивания
                            if (!context.Info.ContainsKey(CycleGroupingInfoKeys.CycleGroupingModeKey))
                            {
                                context.Info[CycleGroupingInfoKeys.CycleGroupingModeKey] = controlsModes;
                            }

                            controlsModes[control.Name] = mode;
                        }

                        // так же запишем в инфо контрола, чтобы работало локальное сохранение, до обновления краточки
                        // даже если у контролла нет алиаса
                        control.Info[CycleGroupingInfoKeys.CycleGroupingModeKey] = mode;
                    });
            });

            await task;
        }

        /// <summary>
        /// Восстанавливает наполненик контрола, которое должно быть без включенной группировки.
        /// </summary>
        /// <param name="control">IFileControl</param>
        public static async Task RestoreFilesListAsync(IFileControl control, Card card)
        {
            await DispatcherHelper.InvokeInUIAsync(async () =>
            {
                // Удалим виртуальные файлы "Версий"
                for (int i = control.Files.Count - 1; i > -1; i--)
                {
                    var file = control.Files[i];
                    var cardFile = card.Files.FirstOrDefault(x => x.RowID == file.ID);
                    if (cardFile?.IsVirtual != false &&
                        file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                        cycleObj != null)
                    {
                        control.Files.RemoveAt(i);
                    }
                }

                // Вернём все "копии"
                foreach (var containerFile in control.Container.Files)
                {
                    var cardFile = card.Files.FirstOrDefault(x => x.RowID == containerFile.ID);
                    if (cardFile?.IsVirtual == false &&
                        containerFile.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                        cycleObj != null &&
                        control.Files.All(p => p.ID != containerFile.ID))
                    {
                        control.Files.Add(containerFile);
                    }
                }
            });
        }

        #endregion

        #region Private Methods

        private static void ReturnAllFiles(IFileControl control)
        {
            foreach (var containerFile in control.Container.Files)
            {
                if (containerFile.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                    cycleObj != null &&
                    control.Files.All(p => p.ID != containerFile.ID))
                {
                    control.Files.Add(containerFile);
                }
            }
        }

        #endregion
    }
}
