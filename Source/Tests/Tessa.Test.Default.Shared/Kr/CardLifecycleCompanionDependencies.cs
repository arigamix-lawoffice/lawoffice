using System;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Platform;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <inheritdoc cref="ICardLifecycleCompanionDependencies"/>
    public class CardLifecycleCompanionDependencies :
        ICardLifecycleCompanionDependencies
    {
        #region Fields

        private ICardFileManager cardFileManager;
        private readonly Func<ICardFileManager> cardFileManagerFunc;

        private ICardStreamServerRepository cardStreamServerRepository;
        private readonly Func<ICardStreamServerRepository> cardStreamServerRepositoryFunc;

        private ICardStreamClientRepository cardStreamClientRepository;
        private readonly Func<ICardStreamClientRepository> cardStreamClientRepositoryFunc;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public ICardRepository CardRepository { get; }

        /// <inheritdoc/>
        public ICardMetadata CardMetadata { get; }

        /// <inheritdoc/>
        public IDbScope DbScope { get; }

        /// <inheritdoc/>
        public ICardFileManager CardFileManager => this.cardFileManager ??= cardFileManagerFunc();

        /// <inheritdoc/>
        public ICardStreamServerRepository CardStreamServerRepository
        {
            get
            {
                return this.cardStreamServerRepositoryFunc is null
                    ? null
                    : this.cardStreamServerRepository ??= this.cardStreamServerRepositoryFunc();
            }
        }

        /// <inheritdoc/>
        public ICardStreamClientRepository CardStreamClientRepository
        {
            get
            {
                return this.cardStreamClientRepositoryFunc is null
                    ? null
                    : this.cardStreamClientRepository ??= this.cardStreamClientRepositoryFunc();
            }
        }

        /// <inheritdoc/>
        public ICardCache CardCache { get; }

        /// <inheritdoc/>
        public ICardLifecycleCompanionRequestExtender RequestExtender { get; }

        /// <inheritdoc/>
        public bool ServerSide { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanionDependencies"/>.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardMetadata">Репозиторий содержащий метаинформацию.</param>
        /// <param name="cardFileManagerFunc">Функция возвращающая объект управляющий объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="cardCache">Кэш карточек.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных. Должен быть обязательно задан, если используются серверные зависимости. Если используются клиентские зависимости, то данный параметр, если он не задан, устанавливается равным значению свойства <see cref="DbScope.Default"/>.</param>
        /// <param name="requestExtender">Объект предоставляющий методы выполняющие расширение запросов выполняемых <see cref="CardLifecycleCompanion"/>.</param>
        private CardLifecycleCompanionDependencies(
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            Func<ICardFileManager> cardFileManagerFunc,
            ICardCache cardCache,
            IDbScope dbScope,
            ICardLifecycleCompanionRequestExtender requestExtender = default)
        {
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(cardFileManagerFunc, nameof(cardFileManagerFunc));
            Check.ArgumentNotNull(cardCache, nameof(cardCache));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            this.CardRepository = cardRepository;
            this.CardMetadata = cardMetadata;
            this.cardFileManagerFunc = cardFileManagerFunc;
            this.CardCache = cardCache;
            this.DbScope = dbScope;
            this.RequestExtender = requestExtender;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanionDependencies"/> серверными зависимостями.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardMetadata">Репозиторий содержащий метаинформацию.</param>
        /// <param name="cardFileManagerFunc">Функция возвращающая объект управляющий объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="cardStreamServerRepositoryFunc">Функция возвращающая репозиторий для потокового управления карточками на сервере.</param>
        /// <param name="cardCache">Кэш карточек.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="requestExtender">Объект предоставляющий методы выполняющие расширение запросов выполняемых <see cref="CardLifecycleCompanion"/>. Может быть не задан.</param>
        public CardLifecycleCompanionDependencies(
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            Func<ICardFileManager> cardFileManagerFunc,
            Func<ICardStreamServerRepository> cardStreamServerRepositoryFunc,
            ICardCache cardCache,
            IDbScope dbScope,
            [OptionalDependency] ICardLifecycleCompanionRequestExtender requestExtender = default)
            : this(
                  cardRepository,
                  cardMetadata,
                  cardFileManagerFunc,
                  cardCache,
                  dbScope,
                  requestExtender)
        {
            Check.ArgumentNotNull(cardStreamServerRepositoryFunc, nameof(cardStreamServerRepositoryFunc));

            this.cardStreamServerRepositoryFunc = cardStreamServerRepositoryFunc;
            this.ServerSide = true;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CardLifecycleCompanionDependencies"/> клиентскими зависимостями.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardMetadata">Репозиторий содержащий метаинформацию.</param>
        /// <param name="cardFileManagerFunc">Функция возвращающая объект управляющий объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        /// <param name="cardStreamClientRepositoryFunc">Функция возвращающая репозиторий для потокового управления карточками на клиенте.</param>
        /// <param name="cardCache">Кэш карточек.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных. Если параметр не задан, то он устанавливается равным значению свойства <see cref="DbScope.Default"/>.</param>
        /// <param name="requestExtender">Объект предоставляющий методы выполняющие расширение запросов выполняемых <see cref="CardLifecycleCompanion"/>. Может быть не задан.</param>
        public CardLifecycleCompanionDependencies(
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            Func<ICardFileManager> cardFileManagerFunc,
            Func<ICardStreamClientRepository> cardStreamClientRepositoryFunc,
            ICardCache cardCache,
            [OptionalDependency] IDbScope dbScope = default,
            [OptionalDependency] ICardLifecycleCompanionRequestExtender requestExtender = default)
            : this(
                  cardRepository,
                  cardMetadata,
                  cardFileManagerFunc,
                  cardCache,
                  dbScope ?? Tessa.Platform.Data.DbScope.CreateDefault(),
                  requestExtender)
        {
            Check.ArgumentNotNull(cardStreamClientRepositoryFunc, nameof(cardStreamClientRepositoryFunc));

            this.cardStreamClientRepositoryFunc = cardStreamClientRepositoryFunc;
        }

        #endregion
    }
}
