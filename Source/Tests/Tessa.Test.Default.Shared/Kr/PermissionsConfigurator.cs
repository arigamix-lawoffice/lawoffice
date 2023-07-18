using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы, выполняющие настройку правил доступа.
    /// </summary>
    public sealed class PermissionsConfigurator :
        CardCollectionConfigurator<Guid>,
        IConfiguratorScopeManager<TestConfigurationBuilder>,
        IPendingActionsExecutor<PermissionsConfigurator>
    {
        #region Fields

        private readonly ICardLifecycleCompanionDependencies deps;

        private readonly ConfiguratorScopeManager<TestConfigurationBuilder> configuratorScopeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PermissionsConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        public PermissionsConfigurator(
            ICardLifecycleCompanionDependencies deps)
            : this(deps, default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PermissionsConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        /// <param name="scope">Конфигуратор верхнего уровня.</param>
        public PermissionsConfigurator(
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
        /// Возвращает объект выполняющий конфигурирование карточки правил доступа имеющую указанный идентификатор.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки правила доступа.</param>
        /// <param name="isLoad">Значение <see langword="true"/>, если карточка правила доступа c идентификатором <paramref name="cardID"/> должна быть загружена, если она отсутствует в кэше конфигуратора, иначе создана - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator GetPermissionsCard(
            Guid cardID,
            bool isLoad = default)
        {
            this.SetCurrent(
                cardID,
                () =>
                {
                    var permCardCompanion =
                        new CardLifecycleCompanion(
                            cardID,
                            DefaultCardTypes.KrPermissionsTypeID,
                            DefaultCardTypes.KrPermissionsTypeName,
                            this.deps);
                    if (isLoad)
                    {
                        permCardCompanion
                            .Load();
                    }
                    else
                    {
                        permCardCompanion
                            .Create()
                            .ApplyAction((clc, _) =>
                            {
                                var krPermissionsFields = clc.GetCardOrThrow().Sections["KrPermissions"].Fields;

                                krPermissionsFields["Caption"] = TestContext.CurrentContext.Test?.FullName + "_" + cardID.ToString("B");
                                krPermissionsFields["Types"] = string.Empty;
                                krPermissionsFields["Roles"] = string.Empty;
                                krPermissionsFields["AclGenerationRules"] = string.Empty;
                                krPermissionsFields["States"] = string.Empty;
                                krPermissionsFields["Permissions"] = string.Empty;
                            })
                            ;
                    }

                    return permCardCompanion;
                });

            return this;
        }

        /// <summary>
        /// Выставляет указанное право доступа.
        /// </summary>
        /// <param name="flag">Дескриптор права доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddFlags(
            KrPermissionFlagDescriptor flag)
        {
            Check.ArgumentNotNull(flag, nameof(flag));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddFlags),
                (_, _) =>
                {
                    UpdateCanFlag(
                        this.Current.GetCardOrThrow(),
                        true,
                        flag);

                    return new ValueTask<ValidationResult>(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Выставляет указанные права доступа.
        /// </summary>
        /// <param name="flags">Массив дескрипторов прав доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddFlags(
            params KrPermissionFlagDescriptor[] flags)
        {
            Check.ArgumentNotNull(flags, nameof(flags));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddFlags),
                (_, _) =>
                {
                    UpdateCanFlags(
                        this.Current.GetCardOrThrow(),
                        true,
                        flags);

                    return new ValueTask<ValidationResult>(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет указанное право доступа.
        /// </summary>
        /// <param name="flag">Дескриптор права доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveFlags(
            KrPermissionFlagDescriptor flag)
        {
            Check.ArgumentNotNull(flag, nameof(flag));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(RemoveFlags),
                (_, _) =>
                {
                    UpdateCanFlag(
                        this.Current.GetCardOrThrow(),
                        false,
                        flag);

                    return new ValueTask<ValidationResult>(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет указанные права доступа.
        /// </summary>
        /// <param name="flags">Массив дескрипторов прав доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveFlags(
            params KrPermissionFlagDescriptor[] flags)
        {
            Check.ArgumentNotNull(flags, nameof(flags));
            this.CheckCurrent();

            if (flags.Length > 0)
            {
                var pendingAction = new PendingAction(
                    nameof(PermissionsConfigurator) + "." + nameof(RemoveFlags),
                    (_, _) =>
                    {
                        UpdateCanFlags(
                            this.Current.GetCardOrThrow(),
                            false,
                            flags);

                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    });

                this.Current.AddPendingAction(pendingAction);
            }

            return this;
        }

        /// <summary>
        /// Заменяет текущие права доступа указанными.
        /// </summary>
        /// <param name="flag">Дескриптор права доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ReplaceFlags(
            KrPermissionFlagDescriptor flag)
        {
            Check.ArgumentNotNull(flag, nameof(flag));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ReplaceFlags),
                (_, _) =>
                {
                    var card = this.Current.GetCardOrThrow();
                    DropFlags(card);
                    UpdateCanFlag(card, true, flag);

                    return new ValueTask<ValidationResult>(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Заменяет текущие права доступа указанными.
        /// </summary>
        /// <param name="flags">Массив дескрипторов прав доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ReplaceFlags(
            params KrPermissionFlagDescriptor[] flags)
        {
            Check.ArgumentNotNull(flags, nameof(flags));
            this.CheckCurrent();

            if (flags.Length > 0)
            {
                var pendingAction = new PendingAction(
                    nameof(PermissionsConfigurator) + "." + nameof(ReplaceFlags),
                    (_, _) =>
                    {
                        var card = this.Current.GetCardOrThrow();
                        DropFlags(card);
                        UpdateCanFlags(card, true, flags);

                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    });

                this.Current.AddPendingAction(pendingAction);
            }

            return this;
        }

        /// <summary>
        /// Добавляет в список типов, к которым применяется текущее правило доступа, указанный тип.
        /// </summary>
        /// <param name="typeID">Идентификатор типа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddType(
            Guid typeID)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddType),
                async (_, ct) =>
                {
                    await AddRowInternalAsync(
                        this.Current.GetCardOrThrow(),
                        typeID,
                        "KrPermissionTypes",
                        "TypeID",
                        static (typeID, r, _) =>
                        {
                            r.Fields["TypeCaption"] = "TypeCaption_" + typeID.ToString("B");
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет из списка типов, к которым применяется текущее правило доступа, указанный тип.
        /// </summary>
        /// <param name="typeID">Идентификатор типа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveType(
            Guid typeID)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(RemoveType),
                (_, _) =>
                {
                    RemoveRowInternal(
                        this.Current.GetCardOrThrow(),
                        typeID,
                        "KrPermissionTypes",
                        "TypeID");

                    return ValueTask.FromResult(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Изменяет список типов к которым применяется текущее правило доступа.
        /// </summary>
        /// <param name="modifyAction">Функция выполняющая модификацию списка типов. Принимаемое значение: текущий список идентификаторов типов карточек. Возвращаемое значение: результирующий список идентификаторов типов.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ModifyTypes(
            Func<IList<Guid>, IEnumerable<Guid>> modifyAction)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ModifyTypes),
                async (_, ct) =>
                {
                    await ModifyRowsInternalAsync(
                        this.Current.GetCardOrThrow(),
                        modifyAction,
                        "KrPermissionTypes",
                        "TypeID",
                        static (typeID, r, _) =>
                        {
                            r.Fields["TypeCaption"] = "TypeCaption_" + typeID.ToString("B");
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Добавляет указанное состояние.
        /// </summary>
        /// <param name="state">Состояние карточки.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddState(
            KrState state)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddState),
                async (_, ct) =>
                {
                    await AddRowInternalAsync(
                        this.Current.GetCardOrThrow(),
                        state.ID,
                        "KrPermissionStates",
                        "StateID",
                        async (stateID, r, ct) =>
                        {
                            r.Fields["StateID"] = Int32Boxes.Box(stateID);
                            r.Fields["StateName"] = await this.deps.CardMetadata.GetDocumentStateNameAsync((KrState) stateID, ct);
                        },
                        ct);
                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет указанное состояние.
        /// </summary>
        /// <param name="state">Состояние карточки.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveState(
            KrState state)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(RemoveState),
                (_, _) =>
                {
                    RemoveRowInternal(
                        this.Current.GetCardOrThrow(),
                        state.ID,
                        "KrPermissionStates",
                        "StateID");
                    return ValueTask.FromResult(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Изменяет список состояний в текущем правиле доступа.
        /// </summary>
        /// <param name="modifyAction">Функция выполняющая модификацию списка состояний. Принимаемое значение: текущий список состояний. Возвращаемое значение: результирующий список состояний.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ModifyStates(
            Func<IList<KrState>, IEnumerable<KrState>> modifyAction)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ModifyStates),
                async (_, ct) =>
                {
                    await ModifyRowsInternalAsync<int>(
                        this.Current.GetCardOrThrow(),
                        i => modifyAction(i.Select(j => (KrState) j).ToList()).Select(j => j.ID),
                        "KrPermissionStates",
                        "StateID",
                        async (stateID, r, ct) =>
                        {
                            r.Fields["StateID"] = Int32Boxes.Box(stateID);
                            r.Fields["StateName"] = await this.deps.CardMetadata.GetDocumentStateNameAsync((KrState) stateID, ct);
                        },
                        ct);
                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Добавляет в список ролей новую роль.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddRole(
            Guid roleID,
            bool contextRole = false)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddRole),
                async (_, ct) =>
                {
                    await AddRowInternalAsync(
                        this.Current.GetCardOrThrow(),
                        roleID,
                        "KrPermissionRoles",
                        "RoleID",
                        (roleID, r, _) =>
                        {
                            r.Fields["RoleName"] = "RoleName_" + roleID.ToString("B");
                            r.Fields["IsContext"] = BooleanBoxes.Box(contextRole);
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет указанную роль из списка ролей.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveRole(
            Guid roleID)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(RemoveRole),
                (_, _) =>
                {
                    RemoveRowInternal(
                        this.Current.GetCardOrThrow(),
                        roleID,
                        "KrPermissionRoles",
                        "RoleID");

                    return ValueTask.FromResult(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Изменяет список ролей. В списке ролей можно указать, в том числе, и контекстные роли.
        /// </summary>
        /// <param name="modifyAction">Функция выполняющая модификацию списка ролей. Принимаемое значение: текущий список идентификаторов ролей. Возвращаемое значение: результирующий список идентификаторов ролей.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ModifyRoles(
            Func<IList<Guid>, IEnumerable<Guid>> modifyAction,
            bool contextRole = false)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ModifyRoles),
                async (_, ct) =>
                {
                    await ModifyRowsInternalAsync(
                        this.Current.GetCardOrThrow(),
                        modifyAction,
                        "KrPermissionRoles",
                        "RoleID",
                        (roleID, r, _) =>
                        {
                            r.Fields["RoleName"] = "RoleName_" + roleID.ToString("B");
                            r.Fields["IsContext"] = BooleanBoxes.Box(contextRole);
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Добавляет в список правил расчёта ACL новое правило.
        /// </summary>
        /// <param name="ruleID">Идентификатор правила расчёта ACL.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator AddAclGenerationRule(
            Guid ruleID)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(AddAclGenerationRule),
                async (_, ct) =>
                {
                    await AddRowInternalAsync(
                        this.Current.GetCardOrThrow(),
                        ruleID,
                        "KrPermissionAclGenerationRules",
                        "RuleID",
                        (ruleID, r, _) =>
                        {
                            r.Fields["RuleName"] = "RuleName_" + ruleID.ToString("B");
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Удаляет указанное правило расчёта ACL из списка правил.
        /// </summary>
        /// <param name="ruleID">Идентификатор правила расчёта ACL.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator RemoveAclGenerationRule(
            Guid ruleID)
        {
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(RemoveAclGenerationRule),
                (_, _) =>
                {
                    RemoveRowInternal(
                        this.Current.GetCardOrThrow(),
                        ruleID,
                        "KrPermissionAclGenerationRules",
                        "RuleID");

                    return ValueTask.FromResult(ValidationResult.Empty);
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Изменяет список правил расчёта ACL.
        /// </summary>
        /// <param name="modifyAction">
        /// Функция, выполняющая модификацию списка правил расчёта ACL.
        /// Принимаемое значение: текущий список идентификаторов правил расчёта ACL. Возвращаемое значение: результирующий список идентификаторов правил расчёта ACL.
        /// </param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ModifyAclGenerationRules(
            Func<IList<Guid>, IEnumerable<Guid>> modifyAction)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ModifyAclGenerationRules),
                async (_, ct) =>
                {
                    await ModifyRowsInternalAsync(
                        this.Current.GetCardOrThrow(),
                        modifyAction,
                        "KrPermissionAclGenerationRules",
                        "RuleID",
                        (ruleID, r, _) =>
                        {
                            r.Fields["RuleName"] = "RuleName_" + ruleID.ToString("B");
                            return ValueTask.CompletedTask;
                        },
                        ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);

            return this;
        }

        /// <summary>
        /// Выполняет указанное действие над объектом управляющим жизненным циклом текущей карточки правила доступа.
        /// </summary>
        /// <param name="modifyAction">Действие, выполняемое над объектом, управляющим жизненным циклом текущей карточки правила доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        public PermissionsConfigurator ModifyCard(
            Action<PermissionsConfigurator, CardLifecycleCompanion> modifyAction)
        {
            Check.ArgumentNotNull(modifyAction, nameof(modifyAction));
            this.CheckCurrent();
            
            modifyAction(this, this.Current);

            return this;
        }

        /// <summary>
        /// Выполняет указанный асинхронное действие над объектом, управляющим жизненным циклом текущей карточки правила доступа.
        /// </summary>
        /// <param name="modifyActionAsync">Действие, выполняемое над объектом, управляющим жизненным циклом текущей карточки правила доступа.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PermissionsConfigurator ModifyCard(
            Func<PermissionsConfigurator, CardLifecycleCompanion, CancellationToken, ValueTask> modifyActionAsync)
        {
            Check.ArgumentNotNull(modifyActionAsync, nameof(modifyActionAsync));
            this.CheckCurrent();

            var pendingAction = new PendingAction(
                nameof(PermissionsConfigurator) + "." + nameof(ModifyCard),
                async (_, ct) =>
                {
                    await modifyActionAsync(this, this.Current, ct);

                    return ValidationResult.Empty;
                });

            this.Current.AddPendingAction(pendingAction);
            return this;
        }

        /// <inheritdoc/>
        public async ValueTask<PermissionsConfigurator> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            foreach (var clc in this.Where(p => p.Value.HasPendingActions || p.Value.Card?.HasChanges() == true))
            {
                await clc.Value.Save().GoAsync(validationFunc: validationFunc, cancellationToken: cancellationToken);
            }

            return this;
        }

        /// <inheritdoc/>
        public TestConfigurationBuilder Complete() => this.configuratorScopeManager.Complete();

        #endregion

        #region Private Methods

        private static async ValueTask ModifyRowsInternalAsync<T>(
            Card card,
            Func<IList<T>, IEnumerable<T>> modifyAction,
            string section,
            string field,
            Func<T, CardRow, CancellationToken, ValueTask> addRowFuncAsync,
            CancellationToken cancellationToken)
        {
            var rows = card.Sections[section].Rows;
            var sourceIDs = rows
                .Where(p => p.State != CardRowState.Deleted)
                .Select(p => p.Fields.Get<T>(field))
                .ToList();
            var resultIDs = modifyAction(sourceIDs).ToList();

            var toDelete = sourceIDs.Except(resultIDs);
            foreach (var deleteID in toDelete)
            {
                var idx = rows.IndexOf(p => Equals(p.Get<object>(field), deleteID));
                var row = rows[idx];

                if (row.State == CardRowState.Inserted)
                {
                    rows.RemoveAt(idx);
                }
                else
                {
                    row.State = CardRowState.Deleted;
                }
            }

            var toAdd = resultIDs.Except(sourceIDs);
            foreach (var addID in toAdd)
            {
                var idx = rows.IndexOf(p => Equals(p.Get<object>(field), addID));
                if (idx != -1)
                {
                    rows[idx].State = CardRowState.None;
                }
                else
                {
                    var row = rows.Add();
                    row.RowID = Guid.NewGuid();
                    row.State = CardRowState.Inserted;
                    row.Fields[field] = addID;
                    await addRowFuncAsync(addID, row, cancellationToken);
                }
            }
        }

        private static async ValueTask AddRowInternalAsync<T>(
            Card card,
            T value,
            string section,
            string field,
            Func<T, CardRow, CancellationToken, ValueTask> addRowFuncAsync,
            CancellationToken cancellationToken)
        {
            var rows = card.Sections[section].Rows;
            var sourceIDs = rows
                .Where(p => p.State != CardRowState.Deleted)
                .Select(p => p.Get<T>(field));

            if (!sourceIDs.Contains(value))
            {
                var idx = rows.IndexOf(p => Equals(p.Get<object>(field), value));
                if (idx != -1)
                {
                    rows[idx].State = CardRowState.None;
                }
                else
                {
                    var row = rows.Add();
                    row.RowID = Guid.NewGuid();
                    row.State = CardRowState.Inserted;
                    row.Fields[field] = value;
                    await addRowFuncAsync(value, row, cancellationToken);
                }
            }
        }

        private static void RemoveRowInternal<T>(
            Card card,
            T value,
            string section,
            string field)
        {
            var rows = card.Sections[section].Rows;
            var idx = rows.IndexOf(p => Equals(p.Get<object>(field), value));
            var row = rows[idx];

            if (row.State == CardRowState.Inserted)
            {
                rows.RemoveAt(idx);
            }
            else
            {
                row.State = CardRowState.Deleted;
            }
        }

        private static void DropFlags(Card card) =>
            UpdateCanFlags(card, false, KrPermissionFlagDescriptors.Full.IncludedPermissions);

        private static void UpdateCanFlags(
            Card card,
            bool isAllow,
            IEnumerable<KrPermissionFlagDescriptor> flags)
        {
            foreach (var flag in flags)
            {
                UpdateCanFlag(card, isAllow, flag);
            }
        }

        private static void UpdateCanFlag(
            Card card,
            bool isAllow,
            KrPermissionFlagDescriptor flag)
        {
            var sec = card.Sections["KrPermissions"];

            if (flag.IncludedPermissions is not null
                && flag.IncludedPermissions.Count > 0)
            {
                UpdateCanFlags(card, isAllow, flag.IncludedPermissions);
            }

            if (flag.IsVirtual)
            {
                return;
            }

            sec.Fields[flag.SqlName] = BooleanBoxes.Box(isAllow);
        }

        #endregion
    }
}
