using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Контекст расширения на сериализацию этапов.
    /// </summary>
    public sealed class KrStageRowExtensionContext : IKrStageRowExtensionContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageRowExtensionContext"/>.
        /// </summary>
        /// <param name="stageStorages">Коллекция ключ-значение, где ключ - идентификатор строки этапа, значение - коллекция ключ-значение содержащая параметры этапа. Может быть <see langword="null"/>.</param>
        /// <param name="cardToRepair">Карточка с настройками этапа, используемая для восстановления при загрузке. Может быть <see langword="null"/>.</param>
        /// <param name="innerCard">"Входная" карточка.</param>
        /// <param name="outerCard">"Выходная" карточка.</param>
        /// <param name="cardType">Тип карточки. Перед сериализацией настроек этапов (<see cref="IKrStageRowExtension.BeforeSerialization(IKrStageRowExtensionContext)"/>) тип <paramref name="innerCard"/>, после десериализации настроек этапов тип <paramref name="outerCard"/>.</param>
        /// <param name="routeCardType">Тип карточки маршрута. Перед сериализацией настроек этапов (<see cref="IKrStageRowExtension.BeforeSerialization(IKrStageRowExtensionContext)"/>) тип <paramref name="innerCard"/>, после десериализации настроек этапов тип <paramref name="outerCard"/>.</param>
        /// <param name="cardContext">Внешний контекст расширения. Может быть <see langword="null"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public KrStageRowExtensionContext(
            IDictionary<Guid, IDictionary<string, object>> stageStorages,
            Card cardToRepair,
            Card innerCard,
            Card outerCard,
            CardType cardType,
            RouteCardType routeCardType,
            ICardExtensionContext cardContext,
            CancellationToken cancellationToken = default)
        {
            this.StageStorages = stageStorages;
            this.CardToRepair = cardToRepair;
            this.InnerCard = innerCard;
            this.OuterCard = outerCard;
            this.CardType = cardType;
            this.RouteCardType = routeCardType;
            this.CardContext = cardContext;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IExtensionContext Members

        /// <doc path='info[@type="IExtensionContext" and @item="CancellationToken"]'/>
        public CancellationToken CancellationToken { get; set; }

        #endregion

        #region IKrStageRowExtensionContext Members

        /// <inheritdoc />
        public IDictionary<Guid, IDictionary<string, object>> StageStorages { get; }

        /// <inheritdoc />
        public Card CardToRepair { get; }

        /// <inheritdoc />
        public Card InnerCard { get; }

        /// <inheritdoc />
        public Card OuterCard { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public RouteCardType RouteCardType { get; }

        /// <inheritdoc />
        public ICardExtensionContext CardContext { get; }

        #endregion
    }
}