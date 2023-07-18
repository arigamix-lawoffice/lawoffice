#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// При сохранении карточки "Правила доступа" прописывает флаг IsContext для всех ролей,
    /// производит валидацию полей "Роли" и "Правила расчёта ACL",
    /// а также выполняет изменение строковых полей для представления во вложенном сохранении.
    /// </summary>
    public sealed class KrPermissionsRulesStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public KrPermissionsRulesStoreExtension(
            [Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardRepository extendedWithoutTransaction,
            ICardGetStrategy getStrategy,
            IKrPermissionsCacheContainer permissionsCache,
            IKrPermissionsLockStrategy lockStrategy,
            ICardMetadata cardMetadata,
            IConditionTypesProvider conditionTypesProvider,
            ILicenseManager licenseManager)
        {
            this.extendedWithoutTransaction = NotNullOrThrow(extendedWithoutTransaction);
            this.getStrategy = NotNullOrThrow(getStrategy);
            this.permissionsCache = NotNullOrThrow(permissionsCache);
            this.lockStrategy = NotNullOrThrow(lockStrategy);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.conditionTypesProvider = NotNullOrThrow(conditionTypesProvider);
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        #endregion

        #region Fields

        private readonly ICardRepository extendedWithoutTransaction;
        private readonly ICardGetStrategy getStrategy;
        private readonly IKrPermissionsCacheContainer permissionsCache;
        private readonly IKrPermissionsLockStrategy lockStrategy;
        private readonly ICardMetadata cardMetadata;
        private readonly IConditionTypesProvider conditionTypesProvider;
        private readonly ILicenseManager licenseManager;

        private const string SkipPermissionsStoreKey = "SkipPermissionsStore";

        #endregion

        #region Private Methods

        private bool ValidateRolesAndAclGenerationRules(
            Card card,
            IValidationResultBuilder validationResult,
            bool withAcl)
        {
            var sections = card.Sections;
            var rolesSection = sections["KrPermissionRoles"];
            if (rolesSection.Rows.Count > 0)
            {
                return true;
            }

            if (!withAcl)
            {
                validationResult.AddError(
                    this,
                    "$KrMessages_NeedToSpecifyAtLeastOneRole");
                return false;
            }

            var aclGenerationRulesSection = sections["KrPermissionAclGenerationRules"];
            if (aclGenerationRulesSection.Rows.Count == 0)
            {
                validationResult.AddError(
                    this,
                    "$KrMessages_NeedToSpecifyAtLeastOneRoleOrAclGenerationRule");
                return false;
            }

            return true;
        }

        private void UpdateTextFields(
            Card card,
            IValidationResultBuilder validationResult,
            bool withAcl)
        {
            var text = StringBuilderHelper.Acquire(128);

            var sections = card.Sections;
            var permissionsSection = sections["KrPermissions"];
            var permissionsFields = permissionsSection.RawFields;

            var hasCreating = permissionsFields.Get<bool>(KrPermissionFlagDescriptors.CreateCard.SqlName);

            foreach (var permission in KrPermissionFlagDescriptors.Full.IncludedPermissions.OrderBy(x => x.Order))
            {
                if (!permission.IsVirtual)
                {
                    if (permissionsFields.Get<bool>(permission.SqlName))
                    {
                        text.Append('{').Append(permission.Description).AppendLine("}");
                    }
                }
            }

            if (text.Length == 0)
            {
                validationResult.AddWarning(this, "$KrMessages_PermissionsNotSpecified");
            }

            permissionsSection.Fields["Permissions"] = text.ToStringAndClear().TrimEnd();

            //Типы
            var permissionTypes = sections["KrPermissionTypes"];

            foreach (var row in permissionTypes.Rows)
            {
                if (row.State != CardRowState.Deleted)
                {
                    var typeCaption = row.Get<string>("TypeCaption");
                    if (typeCaption?.StartsWith("$", StringComparison.Ordinal) == true)
                    {
                        typeCaption = "{" + typeCaption + "}";
                    }

                    text.AppendLine(typeCaption);
                }
            }

            permissionsSection.Fields["Types"] = text.ToStringAndClear().TrimEnd();

            //Состояния
            var permissionsStates = sections["KrPermissionStates"];

            foreach (var row in permissionsStates.Rows)
            {
                if (row.State != CardRowState.Deleted)
                {
                    var stateName = row.Get<string>("StateName");
                    if (stateName?.StartsWith("$", StringComparison.Ordinal) == true)
                    {
                        stateName = "{" + stateName + "}";
                    }

                    text.AppendLine(stateName);
                }
            }

            //Достаточно, наверно, просто проверить что текстовое поле не было заполнено
            if (!hasCreating && string.IsNullOrWhiteSpace(text.ToString()))
            {
                validationResult.AddWarning(this, "$KrMessages_StatesNotSpecified");
            }

            permissionsSection.Fields["States"] = text.ToStringAndClear().TrimEnd();

            //Роли
            var permissionsRoles = sections["KrPermissionRoles"];

            foreach (var row in permissionsRoles.Rows)
            {
                if (row.State != CardRowState.Deleted)
                {
                    var roleName = row.Get<string>("RoleName");
                    if (roleName?.StartsWith("$", StringComparison.Ordinal) == true)
                    {
                        roleName = "{" + roleName + "}";
                    }

                    text.AppendLine(roleName);
                }
            }

            permissionsSection.Fields["Roles"] = text.ToStringAndRelease().TrimEnd();

            //Правила расчёта ACL
            if (withAcl)
            {
                var aclGenerationRules = sections["KrPermissionAclGenerationRules"];

                foreach (var row in aclGenerationRules.Rows)
                {
                    if (row.State != CardRowState.Deleted)
                    {
                        var roleName = row.Get<string>("RuleName");
                        if (roleName?.StartsWith("$", StringComparison.Ordinal) == true)
                        {
                            roleName = "{" + roleName + "}";
                        }

                        text.AppendLine(roleName);
                    }
                }

                permissionsSection.Fields["AclGenerationRules"] = text.ToStringAndRelease().TrimEnd();
            }
        }

        private async Task UpdateConditionsAsync(ICardStoreExtensionContext context)
        {
            // Алгоритм сохранения
            // 1. Проверяем наличие изменений секций с условиями. Если есть, продолжаем
            // 2. Загружаем текущие настройки и десериализуем
            // 3. Мержим изменения
            // 4. Сериализуем настройки и записываем в поле карточки
            var mainCard = context.Request.Card;
            var checkSections = new HashSet<string>() { ConditionHelper.ConditionSectionName };

            var conditionBaseType = await this.cardMetadata.GetMetadataForTypeAsync(ConditionHelper.ConditionsBaseTypeID, context.CancellationToken);
            var sections = await conditionBaseType.GetSectionsAsync(context.CancellationToken);
            checkSections.AddRange(sections.Select(x => x.Name));

            if (mainCard.Sections.Any(x => checkSections.Contains(x.Key)))
            {
                await using (context.DbScope!.Create())
                {
                    var db = context.DbScope.Db;
                    var oldSettings =
                        await db.SetCommand(
                                context.DbScope.BuilderFactory
                                    .Select().Top(1).C("Conditions")
                                    .From("KrPermissions").NoLock()
                                    .Where().C("ID").Equals().P("CardID")
                                    .Limit(1)
                                    .Build(),
                                db.Parameter("CardID", mainCard.ID))
                            .LogCommand()
                            .ExecuteAsync<string>(context.CancellationToken);

                    var oldCard = new Card();
                    oldCard.Sections.GetOrAdd("KrPermissions").RawFields["Conditions"] = oldSettings;
                    await ConditionHelper.DeserializeConditionsToEntrySectionAsync(
                        oldCard,
                        this.cardMetadata,
                        this.conditionTypesProvider,
                        "KrPermissions",
                        "Conditions",
                        false,
                        context.CancellationToken);

                    foreach (var section in mainCard.Sections.Values)
                    {
                        if (checkSections.Contains(section.Name))
                        {
                            var oldSection = oldCard.Sections.GetOrAdd(section.Name);
                            oldSection.Type = section.Type;

                            CardHelper.MergeSection(section, oldSection);
                            mainCard.Sections.Remove(section.Name);
                        }
                    }

                    var conditionsSection = oldCard.Sections.GetOrAddTable(ConditionHelper.ConditionSectionName);

                    foreach (var conditionRow in conditionsSection.Rows)
                    {
                        await ConditionHelper.SerializeConditionRowAsync(
                            conditionRow,
                            oldCard,
                            mainCard.TypeID,
                            this.cardMetadata,
                            this.conditionTypesProvider,
                            true,
                            context.CancellationToken);
                    }

                    mainCard.Sections.GetOrAdd("KrPermissions").RawFields["Conditions"] =
                        StorageHelper.SerializeToTypedJson((List<object>) conditionsSection.Rows.GetStorage(), false);
                }
            }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.DropPermissionsCacheMark))
            {
                context.Request.ForceTransaction = true;
            }

            if (context.Method == CardStoreMethod.Default)
            {
                await this.UpdateConditionsAsync(context);
            }

            if (context.Request.Info.TryGet<bool>(SkipPermissionsStoreKey)
                || context.Request.Method is not CardStoreMethod.Default and not CardStoreMethod.Import
                || !context.ValidationResult.IsSuccessful()
                || context.Request.TryGetCard() is not { } card
                || card.TryGetSections() is not { } sections)
            {
                return;
            }

            // заполняем строковые поля при первом сохранении карточки
            if (card.StoreMode == CardStoreMode.Insert)
            {
                var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
                var withAcl = license.Modules.HasEnterpriseOrContains(LicenseModules.AclID);
                if (!this.ValidateRolesAndAclGenerationRules(
                        card,
                        context.ValidationResult,
                        withAcl))
                {
                    return;
                }
                this.UpdateTextFields(card, context.ValidationResult, withAcl);
            }

            if (context.Request.Method != CardStoreMethod.Default)
            {
                return;
            }

            // определяем, являются ли роли контекстными
            await using (context.DbScope!.Create())
            {
                await SetIsContextForAllRolesAsync(
                    sections,
                    context.DbScope,
                    context.CancellationToken);
            }
        }

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.DropPermissionsCacheMark))
            {
                await this.lockStrategy.ClearLocksAsync(
                    context.CancellationToken);
            }

            var result = await this.lockStrategy.ObtainWriterLockAsync(context.CancellationToken);
            if (result.HasErrors)
            {
                context.ValidationResult.AddError(
                    this,
                    "$KrPermissions_PermissionsStoreErrorMessage");
            }
            else
            {
                await this.permissionsCache.UpdateVersionAsync(context.CancellationToken);
            }
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            // если поменялась любая секция при изменении уже созданной карточки,
            // то выполняем расчёт текстовых свойств и повторное сохранение
            if (context.Request.Info.TryGet<bool>(SkipPermissionsStoreKey)
                || context.Method is not CardStoreMethod.Default and not CardStoreMethod.Import
                || !context.ValidationResult.IsSuccessful()
                || context.Request.TryGetCard() is not { } card
                || card.StoreMode == CardStoreMode.Insert
                || card.TryGetSections() is not { Count: > 0 } sections)
            {
                return;
            }

            var getContext = await this.getStrategy
                .TryLoadCardInstanceAsync(
                    card.ID,
                    context.DbScope!.Db,
                    context.CardMetadata,
                    context.ValidationResult,
                    cancellationToken: context.CancellationToken);

            if (getContext is null)
            {
                return;
            }

            if (!await this.getStrategy.LoadSectionsAsync(getContext, context.CancellationToken))
            {
                return;
            }

            var updatedCard = getContext.Card;
            var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
            var withAcl = license.Modules.HasEnterpriseOrContains(LicenseModules.AclID);

            if (!this.ValidateRolesAndAclGenerationRules(
                    updatedCard,
                    context.ValidationResult,
                    withAcl))
            {
                return;
            }
            this.UpdateTextFields(updatedCard, context.ValidationResult, withAcl);

            if (updatedCard.HasChanges())
            {
                updatedCard.RemoveAllButChanged();

                var storeRequest = new CardStoreRequest
                {
                    Card = updatedCard,
                    Info = { [SkipPermissionsStoreKey] = BooleanBoxes.True }
                };
                var storeResponse = await this.extendedWithoutTransaction.StoreAsync(storeRequest, context.CancellationToken);
                context.ValidationResult.Add(storeResponse.ValidationResult);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Устанавливает признак "Контекстные роль" на секцию ролей правила доступа.
        /// </summary>
        /// <param name="sections">Секции карточки.</param>
        /// <param name="dbScope"><inheritdoc cref="IDbScope" path="/summary"/></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async ValueTask SetIsContextForAllRolesAsync(
            StringDictionaryStorage<CardSection> sections,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            if (!sections.TryGetValue("KrPermissionRoles", out var rolesSection)
                || rolesSection.TryGetRows() is not { Count: > 0 } rows)
            {
                return;
            }

            Dictionary<Guid, bool>? isContextByRoleID = null;
            foreach (CardRow row in rows)
            {
                if (row.State == CardRowState.Inserted
                    || row.State == CardRowState.Modified)
                {
                    isContextByRoleID ??= new();

                    Guid? roleID = row.TryGet<Guid?>("RoleID");
                    if (roleID.HasValue)
                    {
                        if (!isContextByRoleID.TryGetValue(roleID.Value, out bool roleIsContext))
                        {
                            DbManager db = dbScope.Db;
                            var builderFactory = dbScope.BuilderFactory;

                            Guid roleTypeID = await db
                                .SetCommand(
                                    builderFactory
                                        .Select().C("TypeID")
                                        .From("Instances").NoLock()
                                        .Where().C("ID").Equals().P("RoleID")
                                        .Build(),
                                    db.Parameter("RoleID", roleID.Value))
                                .LogCommand()
                                .ExecuteAsync<Guid>(cancellationToken);

                            roleIsContext = roleTypeID == RoleHelper.ContextRoleTypeID;
                            isContextByRoleID.Add(roleID.Value, roleIsContext);
                        }

                        row["IsContext"] = BooleanBoxes.Box(roleIsContext);
                    }
                }
            }
        }

        #endregion
    }
}
