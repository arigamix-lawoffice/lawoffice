using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Абстрактный базовый класс для обработчиков действий взаимодействующих с вложенными процессами.
    /// </summary>
    public abstract class ForkStageTypeHandlerBase : StageTypeHandlerBase
    {
        #region Fields

        /// <summary>
        /// Фабрика объектов <see cref="BranchAdditionInfo"/>.
        /// </summary>
        protected static readonly IStorageValueFactory<int, BranchAdditionInfo> BranchAdditionInfoFactory =
            new DictionaryStorageValueFactory<int, BranchAdditionInfo>(
                (key, storage) => new BranchAdditionInfo(storage));

        /// <summary>
        /// Фабрика объектов <see cref="BranchRemovalInfo"/>.
        /// </summary>
        protected static readonly IStorageValueFactory<int, BranchRemovalInfo> BranchRemovalInfoFactory =
            new DictionaryStorageValueFactory<int, BranchRemovalInfo>(
                (key, storage) => new BranchRemovalInfo(storage));

        /// <summary>
        /// Формат представления идентификатора строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.
        /// </summary>
        public static readonly string ForkSecondaryProcessesRowIDFormat = "D";

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task BeforeInitializationAsync(
            IStageTypeHandlerContext context)
        {
            await base.BeforeInitializationAsync(context);

            var infos = new Dictionary<string, object>(StringComparer.Ordinal);

            foreach (var secondaryProcessRow in EnumerateSecondaryProcessRows(context))
            {
                var rowID = secondaryProcessRow.TryGet<Guid>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.RowID);
                SetProcessInfo(infos, rowID, new Dictionary<string, object>());
            }

            context.Stage.InfoStorage[KrConstants.Keys.ForkNestedProcessInfo] = infos;
        }

        /// <inheritdoc />
        public override async Task AfterPostprocessingAsync(
            IStageTypeHandlerContext context)
        {
            await base.AfterPostprocessingAsync(context);

            context.Stage.InfoStorage.Remove(KrConstants.Keys.ForkNestedProcessInfo);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Возвращает коллекцию ключ-значение: ключ - идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/> представленный в строковом представлении по формату <see cref="ForkSecondaryProcessesRowIDFormat"/>; значение - коллекцию ключ-значение содержащая информацию по вложенному запускаемому процессу, тип значения <see cref="IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>.
        /// </summary>
        /// <param name="stage">Этап, содержащий получаемую информацию.</param>
        /// <returns>Коллекция ключ-значение: ключ - идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/> представленный в строковом представлении по формату <see cref="ForkSecondaryProcessesRowIDFormat"/>; значение - коллекцию ключ-значение содержащая информацию по вложенному запускаемому процессу, тип значения <see cref="IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>.</returns>
        protected static IDictionary<string, object> GetProcessInfos(Stage stage)
        {
            if (stage.InfoStorage.TryGetValue(KrConstants.Keys.ForkNestedProcessInfo, out var processInfosObj)
                && processInfosObj is IDictionary<string, object> processInfos)
            {
                return processInfos;
            }
            return new Dictionary<string, object>(StringComparer.Ordinal);
        }

        /// <summary>
        /// Возвращает коллекцию ключ-значение содержащую информацию по вложенному запускаемому процессу.
        /// </summary>
        /// <param name="processInfos">Коллекция ключ-значение: ключ - идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/> представленный в строковом представлении по формату <see cref="ForkSecondaryProcessesRowIDFormat"/>; значение - коллекцию ключ-значение содержащая информацию по вложенному запускаемому процессу, тип значения <see cref="IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>.</param>
        /// <param name="rowID">Идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.</param>
        /// <returns>Коллекцию ключ-значение содержащую информацию по вложенному запускаемому процессу.</returns>
        protected static IDictionary<string, object> GetProcessInfo(
            IDictionary<string, object> processInfos,
            Guid rowID)
        {
            if (processInfos.TryGetValue(rowID.ToString(ForkSecondaryProcessesRowIDFormat), out var processInfoObj)
                && processInfoObj is IDictionary<string, object> processInfo)
            {
                return processInfo;
            }
            return new Dictionary<string, object>(StringComparer.Ordinal);
        }

        /// <summary>
        /// Устанавливает информацию по вложенному запускаемому процессу.
        /// </summary>
        /// <param name="processInfos">Коллекция ключ-значение: ключ - идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/> представленный в строковом представлении по формату <see cref="ForkSecondaryProcessesRowIDFormat"/>; значение - коллекцию ключ-значение содержащая информацию по вложенному запускаемому процессу, тип значения <see cref="IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>.</param>
        /// <param name="rowID">Идентификатор строки в коллекции <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.</param>
        /// <param name="processInfo">Коллекция ключ-значение содержащая информацию по вложенному запускаемому процессу.</param>
        protected static void SetProcessInfo(
            IDictionary<string, object> processInfos,
            Guid rowID,
            IDictionary<string, object> processInfo)
        {
            processInfos[rowID.ToString(ForkSecondaryProcessesRowIDFormat)] = processInfo;
        }

        /// <summary>
        /// Возвращает перечисление коллекций ключ-значение содержащих значения элементов из <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Перечисление коллекций ключ-значение содержащих значения элементов из <see cref="KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic"/>.</returns>
        protected static IEnumerable<IDictionary<string, object>> EnumerateSecondaryProcessRows(
            IStageTypeHandlerContext context)
        {
            var secondaryProcesses =
                context
                    .Stage
                    .SettingsStorage
                    .TryGet<IList>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic);

            if (secondaryProcesses is null)
            {
                yield break;
            }

            foreach (var secProcRow in secondaryProcesses)
            {
                if (secProcRow is IDictionary<string, object> dict)
                {
                    yield return dict;
                }
            }
        }

        #endregion
    }
}
