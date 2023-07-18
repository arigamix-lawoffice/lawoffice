using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Объект, предоставляющий методы для сериализации параметров этапов.
    /// </summary>
    public interface IKrStageSerializer
    {
        /// <summary>
        /// Список секций, содержащих параметры этапов.
        /// </summary>
        IReadOnlyList<string> SettingsSectionNames { get; }

        /// <summary>
        /// Список полей, содержащих параметры этапов.
        /// </summary>
        IReadOnlyList<string> SettingsFieldNames { get; }

        /// <summary>
        /// Список прямых дочерних секций секции <see cref="KrConstants.KrStages.Name"/>.
        /// </summary>
        IReadOnlyList<ReferenceToStage> ReferencesToStages { get; }

        /// <summary>
        /// Список секций и столбцов, по которым выполняется сортировка.
        /// </summary>
        IReadOnlyList<OrderColumn> OrderColumns { get; }

        /// <summary>
        /// Установить информацию по сериализуемым секциям и полям.
        /// </summary>
        /// <param name="data">Информация по сериализуемым секциям и полям.</param>
        void SetData(
            KrStageSerializerData data);

        /// <summary>
        /// Сериализовать объект в JSON.
        /// </summary>
        /// <param name="value">Сериализуемый объект.</param>
        /// <returns>Сериализованный объект.</returns>
        string Serialize(
            object value);

        /// <summary>
        /// Десериализовать объект из JSON.
        /// </summary>
        /// <typeparam name="T">Тип десериализуемого объекта.</typeparam>
        /// <param name="json">Сериализованный объект.</param>
        /// <returns>Десериализованный объект.</returns>
        T Deserialize<T>(
            string json);

        /// <summary>
        /// Выполняет обновление параметров этапов в <paramref name="krStagesSection"/> в соответствии с <paramref name="updatedSections"/>.
        /// </summary>
        /// <param name="krStagesSection">Обновляемая секция содержащая информацию о этапах.</param>
        /// <param name="updatedSections">Секции содержащие изменённые параметры.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Словарь содержащий информацию по параметрам этапов. Ключ - идентификатор строки этапа. Значение - словарь содержащий параметры этапа.</returns>
        /// <remarks>Для правильной работы метода строки коллекционных и древовидных секций, содержащие параметры этапов, должны содержать идентификатор строки этапа (<see cref="KrConstants.Keys.ParentStageRowID"/>). Для его установки можно использовать <see cref="ParentStageRowIDVisitor"/>.</remarks>
        ValueTask<IDictionary<Guid, IDictionary<string, object>>> MergeStageSettingsAsync(
            CardSection krStagesSection,
            StringDictionaryStorage<CardSection> updatedSections,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Сериализует параметры этапа.
        /// </summary>
        /// <param name="stageRow">Строка содержащая информацию о этапе.</param>
        /// <param name="settingsStorage">Словарь содержащий параметры этапа.</param>
        void SerializeSettingsStorage(
            CardRow stageRow,
            IDictionary<string, object> settingsStorage);

        /// <summary>
        /// Десериализует параметры этапа.
        /// </summary>
        /// <param name="settings">Сериализованные параметры этапа.</param>
        /// <param name="rowID">Идентификатор строки этапа.</param>
        /// <param name="repairStorage">Значение <see langword="true"/>, если необходимо восстановить структуру в соответствии с текущей метаинформацией, иначе <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Словарь содержащий десериализованные параметры этапа.</returns>
        ValueTask<IDictionary<string, object>> DeserializeSettingsStorageAsync(
            string settings,
            Guid rowID,
            bool repairStorage = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Десериализует параметры этапа.
        /// </summary>
        /// <param name="stageRow">Строка содержащая информацию о этапе.</param>
        /// <param name="repairStorage">Значение <see langword="true"/>, если необходимо восстановить структуру в соответствии с текущей метаинформацией, иначе <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Словарь содержащий десериализованные параметры этапа.</returns>
        ValueTask<IDictionary<string, object>> DeserializeSettingsStorageAsync(
            CardRow stageRow,
            bool repairStorage = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Десериализует параметры этапа в виртуальные секции.
        /// </summary>
        /// <param name="sourceCard">
        /// Исходная карточка.
        /// </param>
        /// <param name="destCard">
        /// Карточка назначения.
        /// </param>
        /// <param name="predeserializedSettings">
        /// Подмножество десериализованных настроек, которое можно использовать вместо явной десериализации из секции.
        /// </param>
        /// <param name="hiddenStageMode">
        /// <inheritdoc cref="KrProcessSerializerHiddenStageMode" path="/summary"/>
        /// </param>
        /// <param name="cardContext">
        /// Внешний контекст расширения на карточку.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task DeserializeSectionsAsync(
            Card sourceCard,
            Card destCard,
            IDictionary<Guid, IDictionary<string, object>> predeserializedSettings = null,
            KrProcessSerializerHiddenStageMode hiddenStageMode = KrProcessSerializerHiddenStageMode.Ignore,
            ICardExtensionContext cardContext = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет сериализуемые параметры этапов в секции <see cref="KrConstants.KrStages.Name"/>
        /// </summary>
        /// <param name="innerCard">"Входная" карточка.</param>
        /// <param name="outerCard">"Выходная" карточка.</param>
        /// <param name="stageStorages">Коллекция ключ-значение, где ключ - идентификатор строки этапа, значение - коллекция ключ-значение содержащая параметры этапа.</param>
        /// <param name="krProcessCache">Кэш для данных из карточек шаблонов этапов.</param>
        /// <param name="cardContext">
        /// Внешний контекст расширения на карточку.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task UpdateStageSettingsAsync(
            Card innerCard,
            Card outerCard,
            IDictionary<Guid, IDictionary<string, object>> stageStorages,
            IKrProcessCache krProcessCache,
            ICardExtensionContext cardContext = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Заполняет хранилище с параметрами этапа данными содержащимися в указанной строке содержащей информацию об этапе.
        /// </summary>
        /// <param name="row">Строка содержащая информацию об этапе.</param>
        /// <param name="storage">Коллекция ключ-значение содержащая параметры этапа.</param>
        void FillStageSettings(
            CardRow row,
            IDictionary<string, object> storage);
    }
}
