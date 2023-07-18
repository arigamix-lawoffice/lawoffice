using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы, выполняющие настройку типов документов.
    /// </summary>
    public sealed class KrDocTypesConfigurator :
        CardCollectionConfigurator<Guid>,
        IConfiguratorScopeManager<TestConfigurationBuilder>,
        IPendingActionsExecutor<KrDocTypesConfigurator>,
        ITypeConfigurator<KrDocTypesConfigurator>
    {
        #region Fields

        private readonly ICardLifecycleCompanionDependencies deps;

        private readonly ConfiguratorScopeManager<TestConfigurationBuilder> configuratorScopeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrDocTypesConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public KrDocTypesConfigurator(
            ICardLifecycleCompanionDependencies deps)
            : this(deps, default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrDocTypesConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        /// <param name="scope">Конфигуратор верхнего уровня.</param>
        public KrDocTypesConfigurator(
            ICardLifecycleCompanionDependencies deps,
            TestConfigurationBuilder scope)
            : base()
        {
            Check.ArgumentNotNull(deps, nameof(deps));

            this.deps = deps;
            this.configuratorScopeManager = new ConfiguratorScopeManager<TestConfigurationBuilder>(scope);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Возвращает объект выполняющий конфигурирование карточки типа документа.
        /// </summary>
        /// <param name="cardDocTypeID">Идентификатор типа документа.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки. Должен быть задан при создании нового типа документа.</param>
        /// <param name="isLoad">Значение <see langword="true"/>, если карточка типа документа c идентификатором <paramref name="cardDocTypeID"/> должна быть загружена, если она отсутствует в кэше конфигуратора, иначе создана - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <exception cref="ArgumentException">Card type isn't specified when creating new document type (ID = "<paramref name="cardDocTypeID"/>").</exception>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator GetDocTypeCard(
            Guid cardDocTypeID,
            Guid cardTypeID = default,
            bool isLoad = default)
        {
            this.SetCurrent(
                cardDocTypeID,
                () =>
                {
                    var docTypeCard =
                        new CardLifecycleCompanion(
                            cardDocTypeID,
                            DefaultCardTypes.KrDocTypeTypeID,
                            DefaultCardTypes.KrDocTypeTypeName,
                            this.deps);

                    if (isLoad)
                    {
                        docTypeCard
                            .Load();
                    }
                    else
                    {
                        if (cardTypeID == Guid.Empty)
                        {
                            throw new ArgumentException($"Card type isn't specified when creating new document type (ID = \"{cardDocTypeID:B}\").", nameof(cardTypeID));
                        }

                        docTypeCard
                            .Create()
                            .ApplyAction(async (clc, action, ct) =>
                            {
                                var cardType = (await this.deps.CardMetadata.GetCardTypesAsync(cancellationToken: ct))[cardTypeID];

                                var krDocTypeFields = clc.Card.Sections["KrDocType"].Fields;
                                krDocTypeFields["Title"] = cardDocTypeID.ToString("B");
                                krDocTypeFields["CardTypeID"] = cardType.ID;
                                krDocTypeFields["CardTypeName"] = cardType.Name;
                                krDocTypeFields["CardTypeCaption"] = cardType.Caption;

                                return ValidationResult.Empty;
                            })
                            ;
                    }

                    return docTypeCard;
                });

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator UseApproving(
            bool useApproving = true,
            bool hideRouteTab = default,
            bool useRoutesInWorkflowEngine = default)
        {
            this.Current
                .SetValue("KrDocType", "UseApproving", BooleanBoxes.Box(useApproving))
                .SetValue("KrDocType", "HideRouteTab", BooleanBoxes.Box(hideRouteTab))
                .SetValue("KrDocType", "UseRoutesInWorkflowEngine", BooleanBoxes.Box(useRoutesInWorkflowEngine));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator UseAutoApprove(
            bool useAutoApprove = true,
            double exceededDays = default,
            double notifyBefore = default,
            string autoApproveComment = default)
        {
            this.Current
                .SetValue("KrDocType", "UseAutoApprove", BooleanBoxes.Box(useAutoApprove))
                .SetValue("KrDocType", "ExceededDays", DoubleBoxes.Box(exceededDays))
                .SetValue("KrDocType", "NotifyBefore", DoubleBoxes.Box(notifyBefore))
                .SetValue("KrDocType", "AutoApproveComment", autoApproveComment);

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator UseRegistration(
            bool value = true)
        {
            this.Current
                .SetValue("KrDocType", "UseRegistration", BooleanBoxes.Box(value));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator ConfigureDocNumberRegular(
            KrDocNumberRegularAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default)
        {
            this.Current
                .SetValue("KrDocType", "DocNumberRegularAutoAssignmentID", Int32Boxes.Box((int)autoAssignment))
                .SetValue("KrDocType", "DocNumberRegularAutoAssignmentDescription", autoAssignment.ToString())
                .SetValue("KrDocType", "DocNumberRegularSequence", sequence)
                .SetValue("KrDocType", "DocNumberRegularFormat", format)
                .SetValue("KrDocType", "AllowManualRegularDocNumberAssignment", BooleanBoxes.Box(allowManualAssignment))
                .SetValue("KrDocType", "ReleaseRegularNumberOnFinalDeletion", BooleanBoxes.Box(releaseNumber));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator ConfigureDocNumberRegistration(
            KrDocNumberRegistrationAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default)
        {
            this.Current
                .SetValue("KrDocType", "DocNumberRegistrationAutoAssignmentID", Int32Boxes.Box((int)autoAssignment))
                .SetValue("KrDocType", "DocNumberRegistrationAutoAssignmentDescription", autoAssignment.ToString())
                .SetValue("KrDocType", "DocNumberRegistrationSequence", sequence)
                .SetValue("KrDocType", "DocNumberRegistrationFormat", format)
                .SetValue("KrDocType", "AllowManualRegistrationDocNumberAssignment", BooleanBoxes.Box(allowManualAssignment))
                .SetValue("KrDocType", "ReleaseRegistrationNumberOnFinalDeletion", BooleanBoxes.Box(releaseNumber));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator DisableChildResolutionDateCheck(
            bool value = true)
        {
            this.Current.SetValue(
                "KrDocType",
                "DisableChildResolutionDateCheck",
                BooleanBoxes.Box(value));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator UseResolutions(
            bool value = true)
        {
            this.Current.SetValue(
                "KrDocType",
                "UseResolutions",
                BooleanBoxes.Box(value));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrDocTypesConfigurator UseForum(
            bool useForum = true,
            bool useDefaultForumTab = default)
        {
            this.Current
                .SetValue("KrDocType", "UseForum", BooleanBoxes.Box(useForum))
                .SetValue("KrDocType", "UseDefaultDiscussionTab", BooleanBoxes.Box(useDefaultForumTab));

            return this;
        }

        /// <summary>
        /// Выполняет указанное действие над объектом управляющим жизненным циклом текущей карточки типа документа.
        /// </summary>
        /// <param name="modifyAction">Действие, выполняемое над объектом управляющим жизненным циклом текущей карточки типа документа.</param>
        /// <returns>Объект <see cref="KrDocTypesConfigurator"/> для создания цепочки.</returns>
        public KrDocTypesConfigurator ModifyCard(
            Action<CardLifecycleCompanion> modifyAction)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));

            modifyAction(this.Current);

            return this;
        }

        /// <inheritdoc/>
        public async ValueTask<KrDocTypesConfigurator> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            foreach (var clc in this.Where(p => p.Value.HasPendingActions || p.Value.Card?.HasChanges() == true))
            {
                await clc.Value
                    .Save()
                    .GoAsync(
                        validationFunc: validationFunc,
                        cancellationToken: cancellationToken);
            }

            return this;
        }

        /// <inheritdoc/>
        public TestConfigurationBuilder Complete()
        {
            return this.configuratorScopeManager.Complete();
        }

        #endregion
    }
}
