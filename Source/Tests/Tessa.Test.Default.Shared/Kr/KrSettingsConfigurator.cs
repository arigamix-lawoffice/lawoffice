using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы, выполняющие настройку параметров типового решения (Правая панель -&gt; Настройки -&gt; Типовое решение).
    /// </summary>
    public sealed class KrSettingsConfigurator :
        CardLifecycleCompanion<KrSettingsConfigurator>,
        IConfiguratorScopeManager<TestConfigurationBuilder>
    {
        #region Fields

        private readonly HashSet<Guid, KrSettingsTypeConfigurator> cardTypeConfigurators;

        private readonly ConfiguratorScopeManager<TestConfigurationBuilder> configuratorScopeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSettingsConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrSettingsConfigurator(
            ICardLifecycleCompanionDependencies deps)
            : this(deps, default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSettingsConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        /// <param name="scope">Конфигуратор верхнего уровня.</param>
        public KrSettingsConfigurator(
            ICardLifecycleCompanionDependencies deps,
            TestConfigurationBuilder scope)
            : base(DefaultCardTypes.KrSettingsTypeID, DefaultCardTypes.KrSettingsTypeName, deps)
        {
            this.configuratorScopeManager = new ConfiguratorScopeManager<TestConfigurationBuilder>(scope);
            this.cardTypeConfigurators = new HashSet<Guid, KrSettingsTypeConfigurator>(i => i.CardTypeID);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Возвращает объект выполняющий настройку типа карточек.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточек который требуется настроить.</param>
        /// <returns>Объект <see cref="KrSettingsTypeConfigurator"/> выполняющий настройку.</returns>
        public KrSettingsTypeConfigurator GetCardTypeConfigurator(
            Guid cardTypeID)
        {
            if (!this.cardTypeConfigurators.TryGetItem(cardTypeID, out var cardTypeConfigurator))
            {
                cardTypeConfigurator = new KrSettingsTypeConfigurator(this, this.Dependencies.CardMetadata, cardTypeID);

                this.cardTypeConfigurators.Add(cardTypeConfigurator);
            }

            return cardTypeConfigurator;
        }

        /// <summary>
        /// Добавляет указанный идентификатор типа карточки расширяющий настройки правил доступа.
        /// </summary>
        /// <param name="permissionExtensionTypeID">Идентификатор типа карточки.</param>
        /// <param name="permissionExtensionTypeName">Имя типа карточки.</param>
        /// <returns>Объект <see cref="KrSettingsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSettingsConfigurator WithPermissionsExtension(
            Guid? permissionExtensionTypeID,
            string permissionExtensionTypeName)
        {
            this.SetValue("KrSettings", "PermissionsExtensionTypeID", permissionExtensionTypeID)
                .SetValue("KrSettings", "PermissionsExtensionTypeName", permissionExtensionTypeName);
            return this;
        }

        /// <summary>
        /// Добавляет указанный тип задания в список заданий используемых в подсистеме маршрутов (настройка типового решения "Расширенные настройки типов заданий, используемых в подсистеме маршрутов").
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="taskTypeName">Имя типа задания.</param>
        /// <param name="taskTypeCaption">Отображаемое имя типа задания.</param>
        /// <returns>Объект <see cref="KrSettingsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSettingsConfigurator WithRouteExtraTaskType(
            Guid taskTypeID,
            string taskTypeName,
            string taskTypeCaption)
        {
            this.ApplyAction(
                (clc, action) =>
                {
                    var row = clc.GetCardOrThrow().Sections[KrConstants.KrSettingsRouteExtraTaskTypes.Name].Rows.Add();
                    row.RowID = Guid.NewGuid();
                    row[KrConstants.KrSettingsRouteExtraTaskTypes.TaskTypeID] = taskTypeID;
                    row[KrConstants.KrSettingsRouteExtraTaskTypes.TaskTypeName] = taskTypeName;
                    row[KrConstants.KrSettingsRouteExtraTaskTypes.TaskTypeCaption] = taskTypeCaption;
                    row.State = CardRowState.Inserted;
                },
                name: nameof(KrSettingsConfigurator) + "." + nameof(WithRouteExtraTaskType));
            return this;
        }

        /// <summary>
        /// Устанавливает флаг "Доступ ACL на чтение карточки" в карточке настроек типового решения.
        /// </summary>
        /// <param name="aclReadCardAccess">Новое значение для флага "Доступ ACL на чтение карточки".</param>
        /// <returns>Объект <see cref="KrSettingsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSettingsConfigurator SetAclReadCardAccess(
            bool aclReadCardAccess)
        {
            return this.SetValue("KrSettings", "AclReadCardAccess", BooleanBoxes.Box(aclReadCardAccess));
        }

        /// <summary>
        /// Создаёт карточку типа <see cref="ICardLifecycleCompanion.CardTypeID"/>.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на создание карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendNewRequest(CardNewRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на создание карточки.<para/>
        /// После создания выполняется инициализация карточки значениями по умолчанию.
        /// </remarks>
        public override KrSettingsConfigurator Create(Action<CardNewRequest> modifyRequestAction = null) =>
            base.Create(modifyRequestAction).ApplyAction(this.Initialize);

        /// <summary>
        /// Сохраняет карточку <see cref="Card"/>.
        /// </summary>
        /// <param name="modifyRequestAction">Метод изменяющий запрос на сохранение карточки. Выполняется после <see cref="ICardLifecycleCompanionRequestExtender.ExtendStoreRequest(CardStoreRequest)"/>. Для централизованного управления запросами используйте объект <see cref="ICardLifecycleCompanionRequestExtender"/>.</param>
        /// <returns>Объект <see cref="KrSettingsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Не требуется явным образом планировать сохранение. Оно будет выполнено автоматически при обработке отложенных действий.<para/>
        /// Данный метод не выполняет никаких действий.
        /// </remarks>
        public override KrSettingsConfigurator Save(Action<CardStoreRequest> modifyRequestAction = null) =>
            // Сохранение будет выполнено при выполнении отложенных действий.
            this;

        /// <summary>
        /// Создаёт новую или загружает существующую карточку синглтон.
        /// </summary>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Если карточка была создана, то выполняется её инициализация значениями по умолчанию.
        /// </remarks>
        public override KrSettingsConfigurator CreateOrLoadSingleton() =>
            base.CreateOrLoadSingleton().ApplyAction(this.Initialize);

        /// <inheritdoc/>
        public override ValueTask<KrSettingsConfigurator> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            if (this.HasPendingActions
                || this.Card?.HasChanges() == true)
            {
                base.Save();
            }

            return base.GoAsync(validationFunc: validationFunc, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public TestConfigurationBuilder Complete() =>
            this.configuratorScopeManager.Complete();

        #endregion

        #region Private methods

        private void Initialize(
            KrSettingsConfigurator clc,
            IPendingAction _)
        {
            var card = clc.GetCardOrThrow();
            if (card.StoreMode == CardStoreMode.Update)
            {
                return;
            }

            var krSettingsFields = card.Sections["KrSettings"].Fields;
            
            krSettingsFields["NotificationsDefaultLanguageCaption"] = "English";
            krSettingsFields["NotificationsDefaultLanguageCode"] = LocalizationManager.EnglishLanguageCode;
            krSettingsFields["NotificationsDefaultLanguageID"] = Int32Boxes.Zero;
            
            krSettingsFields["NotificationsDefaultFormatCaption"] = "English";
            krSettingsFields["NotificationsDefaultFormatName"] = LocalizationManager.EnglishLanguageCode;
            krSettingsFields["NotificationsDefaultFormatID"] = new Guid("6a16aa56-b606-49ca-9573-f2651b1734b6");
        }

        #endregion
    }
}
