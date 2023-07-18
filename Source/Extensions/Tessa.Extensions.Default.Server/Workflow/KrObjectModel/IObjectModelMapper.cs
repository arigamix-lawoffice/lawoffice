using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Описывает объект обеспечивающий работу с хранилищами Kr процесса и объектной моделью процесса.
    /// </summary>
    public interface IObjectModelMapper
    {
        /// <summary>
        /// Загружает из сателлита-холдера информацию по текущему процессу.
        /// </summary>
        /// <param name="processHolderSatellite">Сателлит-холдер процесса из которого выполняется загрузка информации по текущему процессу.</param>
        /// <param name="withInfo">Значение <see langword="true"/>, если необходимо загрузить дополнительную информацию по процессу, иначе - <see langword="false"/>.</param>
        /// <returns>Информация по текущему процессу.</returns>
        MainProcessCommonInfo GetMainProcessCommonInfo(
            Card processHolderSatellite,
            bool withInfo = true);

        /// <summary>
        /// Асинхронно устанавливает в сателлите-холдере процесса информацию по основному процессу.
        /// </summary>
        /// <param name="mainCard">Карточка документа.</param>
        /// <param name="processHolderSatellite">Сателлит-холдер процесса в который необходимо установить информацию по основному процессу.</param>
        /// <param name="processCommonInfo">Информация по основному процессу.</param>
        /// <param name="cancellationToken">
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask SetMainProcessCommonInfoAsync(
            Card mainCard,
            Card processHolderSatellite,
            MainProcessCommonInfo processCommonInfo,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Загружает из сателлита-холдера основную информацию по вложенным процессам.
        /// </summary>
        /// <param name="processHolderSatellite">Сателлит-холдер содержащий загружаемую информацию.</param>
        /// <returns>Список объектов <see cref="NestedProcessCommonInfo"/> содержащих информацию по вложенным процессам.</returns>
        List<NestedProcessCommonInfo> GetNestedProcessCommonInfos(
            Card processHolderSatellite);

        /// <summary>
        /// Устанавливает в сателлит-холдер основную информацию по вложенным процессам.
        /// </summary>
        /// <param name="processHolderSatellite">Сателлит-холдер в который должна быть сохранена информация.</param>
        /// <param name="nestedProcessCommonInfos">Список объектов <see cref="NestedProcessCommonInfo"/> содержащих информацию по вложенным процессам.</param>
        void SetNestedProcessCommonInfos(
            Card processHolderSatellite,
            IReadOnlyCollection<NestedProcessCommonInfo> nestedProcessCommonInfos);

        /// <summary>
        /// Заполняет информацию в объектной модели указанной информацией по текущем и основному процессу.
        /// </summary>
        /// <param name="workflowProcess">Объектная модель в которую выполняется запись информации.</param>
        /// <param name="commonInfo">Информация о текущем процессе.</param>
        /// <param name="primaryProcessCommonInfo">Информация об основном процессе.</param>
        void FillWorkflowProcessFromPci(
            WorkflowProcess workflowProcess,
            ProcessCommonInfo commonInfo,
            MainProcessCommonInfo primaryProcessCommonInfo);

        /// <summary>
        /// Преобразует секционную модель процесса маршрутов в объектную модель. Метод предназначен для преобразования карточек шаблона этапов.
        /// </summary>
        /// <param name="stageTemplate">Объект, содержащий информацию о шаблоне этапов.</param>
        /// <param name="runtimeStages">Доступная только для чтения коллекция этапов содержащихся в шаблоне <paramref name="stageTemplate"/>.</param>
        /// <param name="primaryPci">Информация об основном процессе.</param>
        /// <param name="initialStage">Значение <see langword="true"/>, если объект создан при первичном построении исходного маршрута, иначе - <see langword="false"/>.</param>
        /// <param name="saveInitialStages">Значение <see langword="true"/>, если необходимо сохранить текущее состояние процесса в <see cref="WorkflowProcess.InitialWorkflowProcess"/>, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Объектная модель процесса.</returns>
        ValueTask<WorkflowProcess> CardRowsToObjectModelAsync(
            IKrStageTemplate stageTemplate,
            IReadOnlyCollection<IKrRuntimeStage> runtimeStages,
            MainProcessCommonInfo primaryPci,
            bool initialStage = true,
            bool saveInitialStages = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Переносит информацию о процессе из объектной модели (<paramref name="process"/>) в: <paramref name="pci"/>, <paramref name="mainPci"/>, <paramref name="primaryPci"/>.
        /// </summary>
        /// <param name="process">Объектная модель процесса.</param>
        /// <param name="pci">Основная информация о текущем процессе</param>
        /// <param name="mainPci">Информация о текущем процессе.</param>
        /// <param name="primaryPci">Информация об основном процессе.</param>
        void ObjectModelToPci(
            WorkflowProcess process,
            ProcessCommonInfo pci,
            MainProcessCommonInfo mainPci,
            MainProcessCommonInfo primaryPci);

        /// <summary>
        /// Преобразовать секционную модель процесса маршрутов в объектную модель. Метод предназначен для преобразования карточек документов.
        /// </summary>
        /// <param name="processHolder">Карточка процессного сателлита. Содержит информацию о текущем процессе (для вложенного процесса - это сателлит родительского процесса). Если текущий процесс является основным, то он равен контекстуальному сателлиту.</param>
        /// <param name="pci">Информация о процессе.</param>
        /// <param name="mainPci">Информацию о текущем процессе.</param>
        /// <param name="templates">Доступная только для чтения коллекция пар ключ - значение содержащая: ключ - идентификатор шаблона этапов, значение - объект, содержащий информацию о шаблоне этапов.</param>
        /// <param name="runtimeStages">Доступная только для чтения коллекция пар ключ - значение содержащая: ключ - идентификатор шаблона этапов, значение - доступная только для чтения коллекция этапов содержащихся в шаблоне имеющим заданный идентификатор.</param>
        /// <param name="processTypeName">Имя типа текущего процесса.</param>
        /// <param name="initialStage">Значение <see langword="true"/>, если объект создан при первичном построении исходного маршрута, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Объектная модель процесса.</returns>
        ValueTask<WorkflowProcess> CardRowsToObjectModelAsync(
            Card processHolder,
            ProcessCommonInfo pci,
            MainProcessCommonInfo mainPci,
            IReadOnlyDictionary<Guid, IKrStageTemplate> templates,
            IReadOnlyDictionary<Guid, IReadOnlyCollection<IKrRuntimeStage>> runtimeStages,
            string processTypeName,
            bool initialStage = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Преобразует объектную модель процесса маршрутов в секционную модель с отслеживанием изменений.
        /// </summary>
        /// <param name="process">
        /// Переносимый процесс.
        /// </param>
        /// <param name="baseCard">
        /// Карточка, в которую необходимо перенести процесс.
        /// </param>
        /// <param name="npci">
        /// Информация о вложенном процессе. Может иметь значение <see langword="null"/>, если текущий процесс не является вложенным.
        /// </param>
        /// <param name="cancellationToken">
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </param>
        /// <returns>Список содержащий информацию о различиях в процессах до и после переноса.</returns>
        ValueTask<List<RouteDiff>> ObjectModelToCardRowsAsync(
            WorkflowProcess process,
            Card baseCard,
            NestedProcessCommonInfo npci = null,
            CancellationToken cancellationToken = default);
    }
}
