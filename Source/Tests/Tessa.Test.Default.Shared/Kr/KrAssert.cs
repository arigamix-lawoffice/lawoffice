using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет статические методы для выражения утверждений.
    /// </summary>
    /// <seealso href="https://github.com/nunit/docs/wiki/Assertions">NUnit assertions</seealso>
    public static class KrAssert
    {
        #region Static Fields

        /// <inheritdoc cref="KrConstants.KrTaskTypeIDList"/>
        /// <remarks>Оптимизация, для использования при проверке условий.</remarks>
        private static readonly object[] KrTaskTypeIDObjList = KrConstants.KrTaskTypeIDList.Cast<object>().ToArray();

        #endregion

        #region Public Methods

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, есть хотя бы один этап находящийся в состоянии <see cref="KrStageState.Active"/>.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        public static void IsAtLeastOneStageActive(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            IsAtLeastOneStageActive(clc.GetCardOrThrow());
        }

        /// <summary>
        /// Проверяет, что в карточке есть хотя бы один этап находящийся в состоянии <see cref="KrStageState.Active"/>.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        public static void IsAtLeastOneStageActive(Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var states = card
                .GetStagesSection()
                .Rows
                .Select(p => p.Get<int>(KrConstants.KrStages.StateID));

            Assert.That(states, Has.Some.EqualTo(Int32Boxes.Box(KrStageState.Active.ID)));
        }

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, ни один из этапов не находится в состоянии <see cref="KrStageState.Active"/>.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        public static void IsNoneStageActive(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            IsNoneStageActive(clc.GetCardOrThrow());
        }

        /// <summary>
        /// Проверяет, что в карточке ни один из этапов не находится в состоянии <see cref="KrStageState.Active"/>.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        public static void IsNoneStageActive(Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var rows = card
                .GetStagesSection()
                .Rows;

            Assert.That(
                rows.Select(p => p.Get<int>(KrConstants.KrStages.StateID)),
                Has.None.EqualTo(KrStageState.Active.ID),
                () => $"Has not stage is state ID = {KrStageState.Active.ID}:{Environment.NewLine}{StageRowsToString(rows)}");
        }

        /// <summary>
        /// Проверяет, что этап, информация о котором содержится в указанной строке, имеет заданное состояние.
        /// </summary>
        /// <param name="row">Строка, содержащая информацию об этапе. Строка должна соответствовать строке секции <see cref="KrConstants.KrStages.Name"/>.</param>
        /// <param name="expectedState">Ожидаемое состояние этапа.</param>
        public static void StateIs(
            CardRow row,
            KrStageState expectedState)
        {
            Check.ArgumentNotNull(row, nameof(row));

            var state = (KrStageState) row.Get<int>(KrConstants.KrStages.StateID);

            Assert.That(
                state,
                Is.EqualTo(expectedState),
                () => $"Expected: {expectedState.TryGetDefaultName()}{Environment.NewLine}"
                + $"But got: {state.TryGetDefaultName()}");
        }

        /// <summary>
        /// Проверяет, что карточка имеет заданное состояние.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="expectedState">Ожидаемое состояние проверяемой карточки.</param>
        public static void StateIs(
            Card card,
            KrState expectedState)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var aci = card.GetApprovalInfoSection();
            var state = (KrState) aci.RawFields.Get<int>(KrConstants.KrApprovalCommonInfo.StateID);

            const string defaultNameStr = "<The card state does not have a default name. See ID.>";

            Assert.That(
                state,
                Is.EqualTo(expectedState),
                () => $"Expected: {expectedState.TryGetDefaultName() ?? defaultNameStr}{Environment.NewLine}"
                + $"But got: {state.TryGetDefaultName() ?? defaultNameStr}");

            if (card.Sections.TryGetValue(KrConstants.DocumentCommonInfo.Name, out var dci)
                && dci.RawFields.TryGetValue(KrConstants.DocumentCommonInfo.StateID, out var dciStateIDObj))
            {
                var dciState = (KrState) (int) dciStateIDObj;

                Assert.That(
                    dciState,
                    Is.EqualTo(expectedState),
                    () => $"Expected: {expectedState.TryGetDefaultName() ?? defaultNameStr}{Environment.NewLine}"
                    + $"But got from section {KrConstants.DocumentCommonInfo.Name}: {dciState.TryGetDefaultName() ?? defaultNameStr}");
            }
        }

        /// <summary>
        /// Проверяет, что карточка, которой управляет указанный объект, имеет заданное состояние.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="expectedState">Ожидаемое состояние проверяемой карточки.</param>
        public static void StateIs(
            ICardLifecycleCompanion clc,
            KrState expectedState)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            StateIs(clc.GetCardOrThrow(), expectedState);
        }

        /// <summary>
        /// Проверяет, что этап, информация о котором содержится в указанной строке, имеет заданный тип.
        /// </summary>
        /// <param name="row">Строка, содержащая информацию об этапе. Строка должна соответствовать строке секции <see cref="KrConstants.KrStages.Name"/>.</param>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        public static void StageTypeIs(
            CardRow row,
            StageTypeDescriptor descriptor)
        {
            Check.ArgumentNotNull(row, nameof(row));

            Assert.That(
                row[KrConstants.KrStages.StageTypeID],
                Is.EqualTo(descriptor.ID),
                "Expected: {0} ({1}){2}But got: {3} ({4})",
                descriptor.ID,
                descriptor.Caption,
                Environment.NewLine,
                row[KrConstants.KrStages.StageTypeID],
                row[KrConstants.KrStages.StageTypeCaption]);
        }

        /// <summary>
        /// Проверяет, что указанная карточка содержит хотя бы одно задание отправленное типовым процессом согласования.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        public static void HasKrProcessTasks(
            Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.TryGetTasks()?.Select(p => p.TypeID),
                Is.Not.Null.And.Not.Empty.And.Some.AnyOf(KrTaskTypeIDObjList),
                nameof(Card) + "." + nameof(Card.Tasks) + " does not contain any task with type, associated with kr process. " +
                "See " + nameof(KrConstants) + "." + nameof(KrConstants.KrTaskTypeIDList) + ".");
        }

        /// <summary>
        /// Проверяет, что указанная карточка содержит хотя бы одно задание отправленное типовым процессом согласования.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        public static void HasKrProcessTasks(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasKrProcessTasks(clc.GetCardOrThrow());
        }

        /// <summary>
        /// Проверяет, что указанная карточка не содержит заданий отправленных типовым процессом согласования.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        public static void HasNoKrProcessTasks(
            Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.TryGetTasks()?.Select(p => p.TypeID),
                Is.Null.Or.Empty.Or.Not.Some.AnyOf(KrTaskTypeIDObjList),
                () => nameof(Card) + "." + nameof(Card.Tasks) + " contain task with type, associated with kr process. "
                + "See " + nameof(KrConstants) + "." + nameof(KrConstants.KrTaskTypeIDList) + "." + Environment.NewLine
                + "Identifiers of task types that violate the condition: "
                + string.Join(", ", card.Tasks.Select(p => p.TypeID).Intersect(KrConstants.KrTaskTypeIDList)));
        }

        /// <summary>
        /// Проверяет, что карточка, которой управляет указанный объект, не содержит заданий отправленных типовым процессом согласования.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        public static void HasNoKrProcessTasks(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasNoKrProcessTasks(clc.GetCardOrThrow());
        }

        /// <summary>
        /// Проверяет, что маршрут, в указанной карточке, содержит хотя бы один активный этап заданного типа.
        /// </summary>
        /// <param name="card">Карточка, содержащая проверяемый маршрут.</param>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        public static void IsStageTypeActive(
            Card card,
            StageTypeDescriptor descriptor)
        {
            // Проверка корректности значения card выполняется в GetStagesSection.

            var stages = card.GetStagesSection();
            var row = stages
                .Rows
                .FirstOrDefault(p => p.Get<int>(KrConstants.KrStages.StateID) == KrStageState.Active.ID);

            Assert.That(row, Is.Not.Null, "Route has no active stage.");

            var actualStageTypeID = row[KrConstants.KrStages.StageTypeID] as Guid?;

            Assert.That(
                actualStageTypeID,
                Is.EqualTo(descriptor.ID),
                () => $"Expected stage type id is \"{descriptor.Caption}\" but got \"{row[KrConstants.KrStages.StageTypeCaption]}\".");
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит хотя бы один активный этап заданного типа.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        public static void IsStageTypeActive(
            ICardLifecycleCompanion clc,
            StageTypeDescriptor descriptor)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            IsStageTypeActive(clc.GetCardOrThrow(), descriptor);
        }

        /// <summary>
        /// Проверяет, что маршрут, в указанной карточке, содержит хотя бы один активный этап с заданным именем.
        /// </summary>
        /// <param name="card">Карточка, содержащая проверяемый маршрут.</param>
        /// <param name="stageName">Имя этапа.</param>
        public static void IsStageActive(
            Card card,
            string stageName)
        {
            // Проверка корректности значения card выполняется в GetStagesSection.

            var stages = card.GetStagesSection();
            var rows = stages
                .Rows
                .Where(p => p.Get<int>(KrConstants.KrStages.StateID) == KrStageState.Active.ID);

            var row = rows.FirstOrDefault(p => string.Equals(p.Get<string>(KrConstants.KrStages.NameField), stageName, StringComparison.Ordinal));

            Assert.That(
                row,
                Is.Not.Null,
                () => $"Route has no active stage with name \"{stageName}\".{Environment.NewLine}"
                + $"Active stages:{Environment.NewLine}{StageRowsToString(stages.Rows)}");
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит хотя бы один активный этап с заданным именем.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="stageName">Имя этапа.</param>
        public static void IsStageActive(
            ICardLifecycleCompanion clc,
            string stageName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            IsStageActive(clc.GetCardOrThrow(), stageName);
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит хотя бы один этап имеющий заданное имя и состояние.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="stageName">Имя этапа.</param>
        /// <param name="state">Состояние этапа.</param>
        public static void StageHasState(
            ICardLifecycleCompanion clc,
            string stageName,
            KrStageState state)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            StageHasState(clc.GetCardOrThrow(), stageName, state);
        }

        /// <summary>
        /// Проверяет, что маршрут в указанной карточке содержит хотя бы один этап имеющий заданное имя и состояние.
        /// </summary>
        /// <param name="card">Карточка, содержащая маршрут с искомым этапом.</param>
        /// <param name="stageName">Имя этапа.</param>
        /// <param name="state">Состояние этапа.</param>
        public static void StageHasState(
            Card card,
            string stageName,
            KrStageState state)
        {
            // Проверка корректности значения card выполняется в GetStagesSection.

            var stages = card.GetStagesSection();
            var row = stages
                .Rows
                .FirstOrDefault(p => string.Equals(p.Get<string>(KrConstants.KrStages.NameField), stageName, StringComparison.Ordinal));

            Assert.That(
                row,
                Is.Not.Null,
                () => $"Route has no stage with name \"{stageName}\".{Environment.NewLine}Actual stages:{Environment.NewLine}{StageRowsToString(stages.Rows)}");

            Assert.That(
                row[KrConstants.KrStages.StateID],
                Is.EqualTo(Int32Boxes.Box(state.ID)),
                () => $"Expected state ID is \"{state.ID}\" at stage \"{stageName}\" but got \"{row[KrConstants.KrStages.StateID]}\".");
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке содержит один этап с указанным именем.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="name">Имя этапа.</param>
        public static void HasStage(
            Card card,
            string name)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.GetStagesSection().Rows.Select(r => r.Get<string>(KrConstants.KrStages.NameField)).ToArray(),
                Has.One.EqualTo(name));
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит один этап с указанным именем.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="name">Имя этапа.</param>
        public static void HasStage(
            ICardLifecycleCompanion clc,
            string name)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasStage(clc.GetCardOrThrow(), name);
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке не содержит этапа с указанным именем.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="name">Имя этапа.</param>
        public static void HasNoStage(
            Card card,
            string name)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.GetStagesSection().Rows.Select(r => r.Get<string>(KrConstants.KrStages.NameField)).ToArray(),
                Has.None.EqualTo(name));
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, не содержит этапа с указанным именем.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="name">Имя этапа.</param>
        public static void HasNoStage(
            ICardLifecycleCompanion clc,
            string name)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasNoStage(clc.GetCardOrThrow(), name);
        }

        /// <summary>
        /// Проверяет наличие в карточке задания указанного типа.
        /// </summary>
        /// <param name="clc">Объект управляющий жизненным циклом проверяемой карточки.</param>
        /// <param name="typeID">Тип задания.</param>
        /// <param name="exactly">Число ожидаемых экземпляров заданий. Если не задано, то проверка считается успешной, если карточка содержит хотя бы одно задание указанного типа.</param>
        /// <param name="message">Сообщение отображаемое при не выполнении проверки.</param>
        public static void HasTask(
            ICardLifecycleCompanion clc,
            Guid typeID,
            int? exactly = null,
            string message = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasTask(clc.GetCardOrThrow(), typeID, exactly, message);
        }

        /// <summary>
        /// Проверяет наличие в карточке задания указанного типа.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="typeID">Тип задания.</param>
        /// <param name="exactly">Число ожидаемых заданий. Если не задано, то проверка считается успешной, если карточка содержит хотя бы одно задание указанного типа.</param>
        /// <param name="message">Сообщение отображаемое при не выполнении проверки.</param>
        public static void HasTask(
            Card card,
            Guid typeID,
            int? exactly = default,
            string message = default)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var tasks = card.Tasks;

            if (exactly.HasValue)
            {
                Assert.That(
                    tasks.Select(i => i.TypeID),
                    Has.Exactly(exactly.Value).EqualTo(typeID),
                    GetMessage);
            }
            else
            {
                Assert.That(
                    tasks.Select(i => i.TypeID),
                    Has.Some.EqualTo(typeID),
                    GetMessage);
            }

            string GetMessage()
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = " " + message + Environment.NewLine;
                }

                return $"{message}Actual tasks:"
                    + $"{Environment.NewLine}{CardTasksToString(tasks)}";
            }
        }

        /// <summary>
        /// Проверяет, что в карточке не содержится заданий указанного типа.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="typeID">Тип задания, которого не должно быть в карточке.</param>
        public static void HasNoTask(
            Card card,
            Guid typeID)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.Tasks.Select(i => i.TypeID),
                Is.Not.Contains(typeID),
                () => $"{nameof(Card)}.{nameof(Card.Tasks)} contains task with type ID = {typeID:B}.");
        }

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, не содержится заданий указанного типа.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="typeID">Тип задания, которого не должно быть в карточке.</param>
        public static void HasNoTask(
            ICardLifecycleCompanion clc,
            Guid typeID)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasNoTask(clc.GetCardOrThrow(), typeID);
        }

        /// <summary>
        /// Проверяет, что в карточке не содержится заданий.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        public static void HasNoTask(
            Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(
                card.TryGetTasks(),
                Is.Null.Or.Empty,
                () => nameof(Card) + "." + nameof(Card.Tasks) + " contains tasks:"
                    + Environment.NewLine + CardTasksToString(card.Tasks));
        }

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, не содержится заданий.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        public static void HasNoTask(
            ICardLifecycleCompanion clc)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            HasNoTask(clc.GetCardOrThrow());
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке содержит этап с указанным названием и порядковым номером.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="name">Имя этапа.</param>
        /// <param name="order">Порядковый номер этапа.</param>
        public static void StageHasOrder(
            Card card,
            string name,
            int order)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var rows = card
                .GetStagesSection()
                .Rows;

            var stage = rows.FirstOrDefault(p => string.Equals(p.Fields.Get<string>(KrConstants.KrStages.NameField), name, StringComparison.Ordinal));

            Assert.That(stage, Is.Not.Null, () => $"Route doesn't contain stage \"{name}\".{Environment.NewLine}{StageRowsToString(rows)}");

            var actual = stage.Fields.Get<int>(KrConstants.KrStages.Order);

            Assert.That(
                actual,
                Is.EqualTo(order),
                () => $"Stage \"{name}\" has {actual} order actual but expected {order}.{Environment.NewLine}{StageRowsToString(rows)}");
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит этап с указанным названием и порядковым номером.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="name">Имя этапа.</param>
        /// <param name="order">Порядковый номер этапа.</param>
        public static void StageHasOrder(
            ICardLifecycleCompanion clc,
            string name,
            int order)
        {
            ThrowIfNull(clc);

            StageHasOrder(clc.GetCardOrThrow(), name, order);
        }

        /// <summary>
        /// Проверяет, что в карточке маршрут содержит приведенные этапы в указанном порядке.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="stageNames">Имена этапов, перечисленные в порядке в котором они должны быть в маршруте.</param>
        public static void SequenceOfStagesIs(
            Card card,
            IEnumerable<string> stageNames)
        {
            ThrowIfNull(card);
            ThrowIfNull(stageNames);

            var i = 0;
            foreach (var stageName in stageNames)
            {
                StageHasOrder(card, stageName, i);
                i++;
            }

            VisibleStagesCount(card, i);
        }

        /// <summary>
        /// Проверяет, что в карточке маршрут содержит приведенные этапы в указанном порядке.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="stageNames">Имена этапов, перечисленные в порядке в котором они должны быть в маршруте.</param>
        public static void SequenceOfStagesIs(
            Card card,
            params string[] stageNames)
        {
            // Параметры будут проверены в SequenceOfStagesIs.
            SequenceOfStagesIs(card, (IEnumerable<string>) stageNames);
        }

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, маршрут содержит приведенные этапы в указанном порядке.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="stageNames">Имена этапов, перечисленные в порядке в котором они должны быть в маршруте.</param>
        public static void SequenceOfStagesIs(
            ICardLifecycleCompanion clc,
            params string[] stageNames)
        {
            ThrowIfNull(clc);
            // Параметр stageNames будет проверен в SequenceOfStagesIs.

            SequenceOfStagesIs(clc.GetCardOrThrow(), stageNames);
        }

        /// <summary>
        /// Проверяет, что в карточке, которой управляет указанный объект, маршрут содержит приведенные этапы в указанном порядке.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="stageNames">Имена этапов, перечисленные в порядке в котором они должны быть в маршруте.</param>
        public static void SequenceOfStagesIs(
            ICardLifecycleCompanion clc,
            IEnumerable<string> stageNames)
        {
            ThrowIfNull(clc);
            // Параметр stageNames будет проверен в SequenceOfStagesIs.

            SequenceOfStagesIs(clc.GetCardOrThrow(), stageNames);
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке содержит указанное число видимых этапов.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="expectedCnt">Ожидаемое число этапов.</param>
        public static void VisibleStagesCount(
            Card card,
            int expectedCnt)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var rows = card
                .GetStagesSection()
                .Rows;

            var cnt = rows.Count;

            Assert.That(
                cnt,
                Is.EqualTo(expectedCnt),
                () => $"Visible stages: {Environment.NewLine}{StageRowsToString(rows)}");
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит указанное число видимых этапов.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="expectedCnt">Ожидаемое число этапов.</param>
        public static void VisibleStagesCount(
            ICardLifecycleCompanion clc,
            int expectedCnt)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            VisibleStagesCount(clc.GetCardOrThrow(), expectedCnt);
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке содержит указанное число этапов.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="expectedCnt">Ожидаемое число этапов.</param>
        public static void StagesCount(
            Card card,
            int expectedCnt)
        {
            Check.ArgumentNotNull(card, nameof(card));

            Assert.That(card.GetStagePositions(), Has.Count.EqualTo(expectedCnt));
        }

        /// <summary>
        /// Проверяет, что маршрут в карточке, которой управляет указанный объект, содержит указанное число этапов.
        /// </summary>
        /// <param name="clc">Объект, содержащий проверяемую карточку.</param>
        /// <param name="expectedCnt">Ожидаемое число этапов.</param>
        public static void StagesCount(
            ICardLifecycleCompanion clc,
            int expectedCnt)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            StagesCount(clc.GetCardOrThrow(), expectedCnt);
        }

        /// <summary>
        /// Проверяет, что по карточке, с указанным идентификатором, есть хотя бы один запущенный типовой процесс согласования указанного типа.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="mainCardID">Идентификатор проверяемой карточки.</param>
        /// <param name="processType">Тип проверяемого процесса. Если не указано значение по умолчанию для типа, то проверяется наличие любого процесса по карточке с указанным идентификатором.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>\
        /// <returns>Асинхронная задача.</returns>
        public static async Task HasWorkflowProcessAsync(
            IDbScope dbScope,
            Guid mainCardID,
            string processType = default,
            CancellationToken cancellationToken = default)
        {
            // Параметр dbScope будет проверен в GetWorkflowProcessAsync.

            var workflowProcesses = await KrTestHelper.GetWorkflowProcessAsync(dbScope, mainCardID, cancellationToken);
            var expression = processType is null ? (IResolveConstraint) Is.Not.Empty : Is.Not.Not.Contains(processType);

            Assert.That(workflowProcesses, expression, "Card with ID = {0:B} has no workflow API process.", mainCardID);
        }

        /// <summary>
        /// Проверяет, что по карточке, с указанным идентификатором, нет запущенных процессов.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="mainCardID">Идентификатор проверяемой карточки.</param>
        /// <param name="processType">Тип проверяемого процесса или значение <see langword="null"/>, если по карточке не должно быть запущено никаких процессов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task HasNoWorkflowProcessAsync(
            IDbScope dbScope,
            Guid mainCardID,
            string processType = default,
            CancellationToken cancellationToken = default)
        {
            // Параметр dbScope будет проверен в GetWorkflowProcessAsync.

            var workflowProcesses = await KrTestHelper.GetWorkflowProcessAsync(dbScope, mainCardID, cancellationToken);
            var expression = processType is null ? (IResolveConstraint) Is.Empty : Is.Not.Contains(processType);

            Assert.That(workflowProcesses, expression, "Card with ID = {0:B} has workflow API process.", mainCardID);
        }

        /// <summary>
        /// Проверяет число строк в коллекционной или древовидной секции.<para/>
        /// Отсутствие секции эквивалентно отсутствию строк.<para/>
        /// Создаёт исключение <see cref="AssertionException"/>, если проверка не пройдена.
        /// </summary>
        /// <param name="sections">Секции.</param>
        /// <param name="sectionName">Название секции.</param>
        /// <param name="expectedRowCount">Ожидаемое число строк.</param>
        public static void RowsCount(
            IReadOnlyDictionary<string, CardSection> sections,
            string sectionName,
            int expectedRowCount)
        {
            Check.ArgumentNotNull(sections, nameof(sections));

            if (sections.TryGetValue(sectionName, out var section)
                && section.Type == CardSectionType.Entry)
            {
                throw new ArgumentException(
                    $"Unexpected card section type: {section.Type}. Section name: \"{sectionName}\". Expected section type: {CardSectionType.Table}.",
                    nameof(sectionName));
            }

            Assert.That(
                section?.TryGetRows(),
                expectedRowCount == 0
                    ? Is.Null.Or.Count.EqualTo(expectedRowCount)
                    : Is.Not.Null.And.Count.EqualTo(expectedRowCount));
        }

        /// <summary>
        /// Проверяет, что все присутствующие в заданной строковой секции карточки поля, имеют значения по умолчанию.
        /// </summary>
        /// <param name="card">Карточка, содержащая проверяемую секцию.</param>
        /// <param name="sectionName">Название проверяемой секции.</param>
        /// <param name="cardMetadataSections">Коллекция, содержащая объекты <see cref="CardMetadataSection"/>.</param>
        /// <param name="chechedFields">Коллекция имён проверяемых полей. Если не задана, то проверяются все поля.</param>
        /// <exception cref="InvalidOperationException">Секция является коллекционной или древовидной.</exception>
        public static void HasDefaultValue(
            Card card,
            string sectionName,
            CardMetadataSectionCollection cardMetadataSections,
            IReadOnlyCollection<string> chechedFields = null)
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(cardMetadataSections, nameof(cardMetadataSections));

            var fields = card.Sections[sectionName].Fields;
            var columnsMetadata = cardMetadataSections[sectionName].Columns;

            foreach (var field in fields)
            {
                if (chechedFields?.Contains(field.Key) != true)
                {
                    continue;
                }

                if (columnsMetadata.TryGetValue(field.Key, out var columnMetadata))
                {
                    Assert.That(
                        field.Value,
                        Is.EqualTo(columnMetadata.DefaultValue),
                        "The value in the \"{0}.{1}\" field is not the default value.",
                        sectionName,
                        field.Key);
                }
            }
        }

        /// <summary>
        /// Проверяет корректность указанного в задании значения <see cref="CardTask.Planned"/>.
        /// </summary>
        /// <param name="businessCalendarService">Бизнес календарь.</param>
        /// <param name="task">Проверяемое задание.</param>
        /// <param name="timeLimitation">Смещение в рабочих днях от даты создания задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task CheckTaskPlannedAsync(
            IBusinessCalendarService businessCalendarService,
            CardTask task,
            double timeLimitation,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(businessCalendarService, nameof(businessCalendarService));
            Check.ArgumentNotNull(task, nameof(task));

            Assert.That(
                task.Planned,
                Is.Not.Null,
                () => $"Planned is required. Task: {TestHelper.GetTaskInfo(task)}.");

            var additionalApprovalTaskPlannedExpected = await businessCalendarService.AddWorkingDaysToDateAsync(
                    task.Card.Created.Value,
                    timeLimitation,
                    task.CalendarID.Value,
                    TimeSpan.FromMinutes(task.TimeZoneUtcOffsetMinutes.Value),
                    cancellationToken);

            Assert.That(
                task.Planned,
                Is.EqualTo(additionalApprovalTaskPlannedExpected));
        }

        /// <summary>
        /// Проверяет содержит ли объект, управляющий жизненным циклом карточки, карточку указанного типа.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки.</param>
        /// <param name="expectedTypeID">Идентификатор ожидаемого типа.</param>
        /// <param name="expectedTypeName">Имя ожидаемого типа.</param>
        /// <exception cref="ArgumentException">The <see cref="ICardLifecycleCompanion"/> object must contain a card of type <paramref name="expectedTypeName"/> (ID = "<paramref name="expectedTypeID"/>"). Actual type <see cref="Card.TypeCaption"/> (ID = "<see cref="Card.TypeID"/>").</exception>
        public static void CheckCardType(
            ICardLifecycleCompanion clc,
            Guid expectedTypeID,
            string expectedTypeName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            var card = clc.GetCardOrThrow();

            CheckCardType(card, expectedTypeID, expectedTypeName);
        }

        /// <summary>
        /// Проверяет, является ли карточка указанного типа.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки.</param>
        /// <param name="expectedTypeID">Идентификатор ожидаемого типа.</param>
        /// <param name="expectedTypeName">Имя ожидаемого типа.</param>
        /// <exception cref="ArgumentException">Expected a card of type <paramref name="expectedTypeName"/> (ID = "<paramref name="expectedTypeID"/>"). Actual type <see cref="Card.TypeCaption"/> (ID = "<see cref="Card.TypeID"/>").</exception>
        public static void CheckCardType(
            Card card,
            Guid expectedTypeID,
            string expectedTypeName)
        {
            Check.ArgumentNotNull(card, nameof(card));

            if (card.TypeID != expectedTypeID)
            {
                throw new ArgumentException($"The {nameof(ICardLifecycleCompanion)} object must contain a card of type {expectedTypeName} (ID = \"{expectedTypeID}\"). Actual type {card.TypeName} (ID = \"{card.TypeID}\").", nameof(card));
            }
        }

        /// <summary>
        /// Проверяет, что запись в истории заданий содержит указанное описание результата завершения.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку с проверяемой историей заданий.</param>
        /// <param name="taskRowID">Идентификатор задания.</param>
        /// <param name="expectedResult">Ожидаемое описание результата завершения.</param>
        public static void CheckTaskHistoryResult(
            ICardLifecycleCompanion clc,
            Guid taskRowID,
            string expectedResult)
        {
            clc
                .GetCardTaskHistoryItemOrThrow(
                    taskRowID,
                    out var cardTaskHistoryItem);

            Assert.That(
                cardTaskHistoryItem.Result,
                Is.EqualTo(expectedResult));
        }

        #endregion

        #region Private Methods

        private static string EnumerableToString<T>(IEnumerable<T> rows)
        {
            var str = string.Join(Environment.NewLine, rows);
            return string.IsNullOrEmpty(str) ? "<EMPTY>" : str;
        }

        private static string StageRowsToString(IEnumerable<CardRow> rows) =>
            EnumerableToString(rows.Select(StageRowToString));

        private static string StageRowToString(CardRow row)
        {
            return
                $"RowState = {row.State}, " +
                $"RowID = {FormattingHelper.FormatNullable(row.TryGetRowID(), "B")}, " +
                $"State = {row.Get<string>(KrConstants.KrStages.StateName)} (ID = {row.Get<int>(KrConstants.KrStages.StateID)}), " +
                $"Name = \"{FormattingHelper.FormatNullable(row.Get<string>(KrConstants.KrStages.NameField))}\", " +
                $"Order = {row.Get<int>(KrConstants.KrStages.Order)}, " +
                $"TemplateName = \"{FormattingHelper.FormatNullable(row.TryGet<string>(KrConstants.KrStages.BasedOnStageTemplateName))}\"";
        }

        private static string CardTasksToString(IEnumerable<CardTask> rows) =>
            EnumerableToString(rows.Select(TestHelper.GetTaskInfo));

        #endregion
    }
}
