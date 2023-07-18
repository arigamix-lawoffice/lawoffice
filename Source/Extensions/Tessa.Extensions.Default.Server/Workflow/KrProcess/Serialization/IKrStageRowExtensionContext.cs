using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Описывает контекст расширения на сериализацию этапов.
    /// </summary>
    public interface IKrStageRowExtensionContext :
        IExtensionContext
    {
        /// <summary>
        /// Коллекция ключ-значение, где ключ - идентификатор строки этапа, значение - коллекция ключ-значение содержащая параметры этапа. Имеет значение <see langword="null"/> после десериализации  параметров этапов: <see cref="IKrStageRowExtension.DeserializationBeforeRepair(IKrStageRowExtensionContext)"/> и <see cref="IKrStageRowExtension.DeserializationAfterRepair(IKrStageRowExtensionContext)"/>.
        /// </summary>
        IDictionary<Guid, IDictionary<string, object>> StageStorages { get; }

        /// <summary>
        /// Карточка с настройками этапа, используемая для восстановления при загрузке. Имеет значение <see langword="null"/> перед началом сериализации настроек этапов: <see cref="IKrStageRowExtension.BeforeSerialization(IKrStageRowExtensionContext)"/>.
        /// </summary>
        Card CardToRepair { get; }

        /// <summary>
        /// "Входная" карточка.
        /// </summary>
        Card InnerCard { get; }

        /// <summary>
        /// "Выходная" карточка.
        /// </summary>
        Card OuterCard { get; }

        /// <summary>
        /// Тип карточки. Перед сериализацией настроек этапов (<see cref="IKrStageRowExtension.BeforeSerialization(IKrStageRowExtensionContext)"/>) возвращает тип <see cref="InnerCard"/>, после десериализации настроек этапов возвращает тип <see cref="OuterCard"/>.
        /// </summary>
        CardType CardType { get; }

        /// <summary>
        /// Тип карточки маршрута. Перед сериализацией настроек этапов (<see cref="IKrStageRowExtension.BeforeSerialization(IKrStageRowExtensionContext)"/>) тип <paramref name="innerCard"/>, после десериализации настроек этапов тип <paramref name="outerCard"/>.
        /// </summary>
        RouteCardType RouteCardType { get; }

        /// <summary>
        /// Внешний контекст расширения. Может быть <see langword="null"/>.
        /// </summary>
        ICardExtensionContext CardContext { get; }
    }
}