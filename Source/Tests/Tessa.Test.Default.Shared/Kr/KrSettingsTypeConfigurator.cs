using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы для настройки типа карточки (Правая панель -&gt; Типовое решение -&gt; Типы карточек).<para/>
    /// Данный конфигуратор предназначен только для внутреннего использования. Для конфигурирования типов карточек используйте возвращаемое значение метода <see cref="KrSettingsConfigurator.GetCardTypeConfigurator(Guid, string)"/>.
    /// </summary>
    public sealed class KrSettingsTypeConfigurator :
        ConfiguratorScopeManager<KrSettingsConfigurator>,
        ITypeConfigurator<KrSettingsTypeConfigurator>
    {
        #region Fields

        private readonly KrSettingsConfigurator krSettingsConfigurator;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор настраиваемого типа карточки.
        /// </summary>
        public Guid CardTypeID { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSettingsTypeConfigurator"/>.
        /// </summary>
        /// <param name="krSettingsConfigurator">Конфигуратор верхнего уровня.</param>
        /// <param name="cardMetadata">Репозиторий содержащий метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="cardTypeID">Идентификатор настраиваемого типа карточки.</param>
        internal KrSettingsTypeConfigurator(
            KrSettingsConfigurator krSettingsConfigurator,
            ICardMetadata cardMetadata,
            Guid cardTypeID)
            : base(krSettingsConfigurator)
        {
            Check.ArgumentNotNull(krSettingsConfigurator, nameof(krSettingsConfigurator));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));

            this.krSettingsConfigurator = krSettingsConfigurator;
            this.CardTypeID = cardTypeID;

            this.Initialize(cardMetadata, cardTypeID);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Использовать типы документов.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если необходимо использовать типы документов, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSettingsTypeConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseDocTypes(
            bool value = true)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseDocTypes"] = BooleanBoxes.Box(value);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(UseDocTypes));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseApproving(
            bool useApproving = true,
            bool hideRouteTab = default,
            bool useRoutesInWorkflowEngine = default)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseApproving"] = BooleanBoxes.Box(useApproving);
                fields["HideRouteTab"] = BooleanBoxes.Box(hideRouteTab);
                fields["UseRoutesInWorkflowEngine"] = BooleanBoxes.Box(useRoutesInWorkflowEngine);
            },
            nameof(KrSettingsTypeConfigurator) + "." + nameof(UseApproving));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseAutoApprove(
            bool useAutoApprove = true,
            double exceededDays = default,
            double notifyBefore = default,
            string autoApproveComment = default)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseAutoApprove"] = BooleanBoxes.Box(useAutoApprove);
                fields["ExceededDays"] = DoubleBoxes.Box(exceededDays);
                fields["NotifyBefore"] = DoubleBoxes.Box(notifyBefore);
                fields["AutoApproveComment"] = autoApproveComment;
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(UseAutoApprove));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseRegistration(
            bool value = true)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseRegistration"] = BooleanBoxes.Box(value);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(UseRegistration));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator ConfigureDocNumberRegular(
            KrDocNumberRegularAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["DocNumberRegularAutoAssignmentID"] = Int32Boxes.Box((int) autoAssignment);
                fields["DocNumberRegularAutoAssignmentDescription"] = autoAssignment.ToString();
                fields["DocNumberRegularSequence"] = sequence;
                fields["DocNumberRegularFormat"] = format;
                fields["AllowManualRegularDocNumberAssignment"] = BooleanBoxes.Box(allowManualAssignment);
                fields["ReleaseRegularNumberOnFinalDeletion"] = BooleanBoxes.Box(releaseNumber);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(ConfigureDocNumberRegular));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator ConfigureDocNumberRegistration(
            KrDocNumberRegistrationAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["DocNumberRegistrationAutoAssignmentID"] = Int32Boxes.Box((int) autoAssignment);
                fields["DocNumberRegistrationAutoAssignmentDescription"] = autoAssignment.ToString();
                fields["DocNumberRegistrationSequence"] = sequence;
                fields["AllowManualRegistrationDocNumberAssignment"] = BooleanBoxes.Box(allowManualAssignment);
                fields["ReleaseRegistrationNumberOnFinalDeletion"] = BooleanBoxes.Box(releaseNumber);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(ConfigureDocNumberRegistration));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator DisableChildResolutionDateCheck(
            bool value = true)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["DisableChildResolutionDateCheck"] = BooleanBoxes.Box(value);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(DisableChildResolutionDateCheck));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseResolutions(
            bool value = true)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseResolutions"] = BooleanBoxes.Box(value);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(UseResolutions));

            return this;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/> конфигуратора верхнего уровня.
        /// </remarks>
        public KrSettingsTypeConfigurator UseForum(
            bool useForum = true,
            bool useDefaultForumTab = default)
        {
            this.krSettingsConfigurator.ApplyAction((clc, action) =>
            {
                var fields = this.GetCardTypeRowOrDefault(clc).Fields;

                fields["UseForum"] = BooleanBoxes.Box(useForum);
                fields["UseDefaultDiscussionTab"] = BooleanBoxes.Box(useDefaultForumTab);
            },
            name: nameof(KrSettingsTypeConfigurator) + "." + nameof(UseForum));

            return this;
        }

        #endregion

        #region Private methods

        private void Initialize(
            ICardMetadata cardMetadata,
            Guid cardTypeID)
        {
            this.krSettingsConfigurator
                .ApplyAction(async (clc, action, ct) =>
                {
                    var rows = GetKrSettingsCardTypes(clc);
                    var row = GetCardTypeRowOrDefault(rows, cardTypeID);

                    if (row is null)
                    {
                        var newRow = rows.Add();
                        newRow.RowID = Guid.NewGuid();

                        var newRowFields = newRow.Fields;
                        newRowFields["CardTypeID"] = cardTypeID;
                        newRowFields["CardTypeCaption"] = (await cardMetadata.GetCardTypesAsync(cancellationToken: ct))[cardTypeID].Caption;

                        newRow.State = CardRowState.Inserted;
                        row = newRow;
                    }

                    return ValidationResult.Empty;
                },
                name: nameof(KrSettingsTypeConfigurator) + "." + nameof(Initialize));

            this.ConfigureDocNumberRegular(KrDocNumberRegularAutoAssignmentID.None);
            this.ConfigureDocNumberRegistration(KrDocNumberRegistrationAutoAssignmentID.None);
        }

        private static ListStorage<CardRow> GetKrSettingsCardTypes(
            ICardLifecycleCompanion clc)
        {
            return clc
                .GetCardOrThrow()
                .Sections["KrSettingsCardTypes"]
                .Rows;
        }

        private static CardRow GetCardTypeRowOrDefault(
            IEnumerable<CardRow> rows,
            Guid cardTypeID) =>
            rows.FirstOrDefault(i => i.Fields.Get<Guid>("CardTypeID") == cardTypeID);

        private CardRow GetCardTypeRowOrDefault(
            ICardLifecycleCompanion clc) =>
            GetCardTypeRowOrDefault(GetKrSettingsCardTypes(clc), this.CardTypeID);

        #endregion
    }
}
