using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Roles.NestedRoles;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsManager" />
    public sealed class KrPermissionsManager : IKrPermissionsManager
    {
        #region Nested Types

        private delegate void CalcAction(IKrPermissionsManagerContext context, IKrPermissionRuleSettings rule);
        private delegate bool FilterFunc(IKrPermissionRuleSettings rule);

        private class InnerContext : IAsyncDisposable
        {
            #region Fields

            private Dictionary<string, object> resultInfo;

            #endregion

            #region Constructors

            public InnerContext(IKrPermissionsManagerContext context, bool checkPermissions)
            {
                this.Context = context;
                this.CheckPermissions = checkPermissions;
            }

            #endregion

            #region Properties

            public IKrPermissionsManagerContext Context { get; }

            /// <summary>
            /// Флаг определяет, что происходит проверка прав.
            /// Если флег имеет значение false, значеит идет расчет прав.
            /// </summary>
            public bool CheckPermissions { get; }

            /// <summary>
            /// Флаг определяет, что расширенные настройки уже были рассчитаны
            /// </summary>
            public bool ExtendedSettingsCalculated { get; set; }

            public HashSet<Guid> SubmittedRules { get; } = new HashSet<Guid>();
            public HashSet<Guid> RejectedRules { get; } = new HashSet<Guid>();
            public HashSet<Guid> SubmittedRoles { get; } = new HashSet<Guid>();
            public HashSet<Guid> RejectedRoles { get; } = new HashSet<Guid>();

            public IKrPermissionsCache PermissionsCache => this.Context.PermissionsCache;
            public IConditionContext ConditionContext { get; set; }
            public IExtensionExecutor RuleExecutor { get; set; }

            public Dictionary<string, object> ResultInfo => this.resultInfo ??= new Dictionary<string, object>(StringComparer.Ordinal);

            public bool HasResultInfo => this.resultInfo is not null;

            #endregion

            #region IAsyncDisposable Implementation

            public async ValueTask DisposeAsync()
            {
                if (this.RuleExecutor is not null)
                {
                    await this.RuleExecutor.DisposeAsync();
                    this.RuleExecutor = null;
                }
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly IExtensionContainer extensionContainer;
        private readonly ISession session;
        private readonly IDbScope dbScope;
        private readonly ICardRepository cardRepository;
        private readonly ICardServerPermissionsProvider permissionsProvider;
        private readonly IRoleGetStrategy roleGetStrategy;
        private readonly IContextRoleManager contextRoleManager;
        private readonly ICardContextRoleCache roleCache;
        private readonly ICardGetStrategy cardGetStrategy;
        private readonly ICardMetadata cardMetadata;
        private readonly IKrTokenProvider krTokenProvider;
        private readonly IKrTypesCache krTypesCache;
        private readonly IConditionExecutor conditionExecutor;
        private readonly IKrPermissionsCacheContainer permissionsCacheContainer;
        private readonly INestedRoleContextSelector nestedRoleContextSelector;
        private readonly IKrPermissionsFilesManager krPermissionsFilesManager;
        private readonly ILicenseManager licenseManager;
        private readonly IUnityContainer unityContainer;

        private readonly CalcAction[] mandatoryCalcAction = new CalcAction[]
        {
            CalcExtendedMandatoryFromRule,
        };

        private readonly FilterFunc mandatoryFilterFunc = new(x => x.MandatoryRules.Count > 0);

        private readonly CalcAction[] fileRulesCalcAction = new CalcAction[]
        {
            CalcExtendedFileRulesFromRule,
        };

        private readonly FilterFunc fileRulesFilterFunc = new(x => x.FileRules.Count > 0);

        private readonly CalcAction[] allCalcActionsWithEdit = new CalcAction[]
        {
            CalcExtendedPermissionsFromRuleWithEdit,
            CalcExtendedMandatoryFromRule,
            CalcVisibilityFromRule,
            CalcExtendedFileRulesFromRule,
        };

        private readonly CalcAction[] allCalcActionsWithoutEdit = new CalcAction[]
        {
            CalcExtendedPermissionsFromRuleWithoutEdit,
            CalcExtendedMandatoryFromRule,
            CalcVisibilityFromRule,
            CalcExtendedFileRulesFromRule,
        };

        private const int serverTokenPriority = int.MaxValue;

        #endregion

        #region Constructors

        public KrPermissionsManager(
            IExtensionContainer extensionContainer,
            ISession session,
            IDbScope dbScope,
            [Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)] ICardRepository cardRepository,
            ICardServerPermissionsProvider permissionsProvider,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            ICardContextRoleCache roleCache,
            ICardGetStrategy cardGetStrategy,
            ICardMetadata cardMetadata,
            IKrTokenProvider krTokenProvider,
            IKrTypesCache krTypesCache,
            IConditionExecutor conditionExecutor,
            IKrPermissionsCacheContainer permissionsCacheContainer,
            INestedRoleContextSelector nestedRoleContextSelector,
            IKrPermissionsFilesManager krPermissionsFilesManager,
            ILicenseManager licenseManager,
            IUnityContainer unityContainer)
        {
            ThrowIfNull(extensionContainer);
            ThrowIfNull(session);
            ThrowIfNull(dbScope);
            ThrowIfNull(cardRepository);
            ThrowIfNull(permissionsProvider);
            ThrowIfNull(roleGetStrategy);
            ThrowIfNull(contextRoleManager);
            ThrowIfNull(roleCache);
            ThrowIfNull(cardGetStrategy);
            ThrowIfNull(cardMetadata);
            ThrowIfNull(krTokenProvider);
            ThrowIfNull(krTypesCache);
            ThrowIfNull(conditionExecutor);
            ThrowIfNull(permissionsCacheContainer);
            ThrowIfNull(nestedRoleContextSelector);
            ThrowIfNull(krPermissionsFilesManager);
            ThrowIfNull(licenseManager);
            ThrowIfNull(unityContainer);

            this.extensionContainer = extensionContainer;
            this.session = session;
            this.dbScope = dbScope;
            this.cardRepository = cardRepository;
            this.permissionsProvider = permissionsProvider;
            this.roleGetStrategy = roleGetStrategy;
            this.contextRoleManager = contextRoleManager;
            this.roleCache = roleCache;
            this.cardGetStrategy = cardGetStrategy;
            this.cardMetadata = cardMetadata;
            this.krTokenProvider = krTokenProvider;
            this.krTypesCache = krTypesCache;
            this.conditionExecutor = conditionExecutor;
            this.permissionsCacheContainer = permissionsCacheContainer;
            this.nestedRoleContextSelector = nestedRoleContextSelector;
            this.krPermissionsFilesManager = krPermissionsFilesManager;
            this.licenseManager = licenseManager;
            this.unityContainer = unityContainer;
        }

        #endregion

        #region IKrPermissionsManager Implementation

        /// <inheritdoc />
        public ICollection<string> IgnoreSections { get; } = new HashSet<string>
        {
            KrConstants.KrStages.Virtual,
            KrConstants.KrActiveTasks.Virtual,
            KrConstants.KrPerformersVirtual.Synthetic,
            KrConstants.KrApprovalCommonInfo.Virtual,
            "FmTopicParticipantsVirtual",
            "FmTopicParticipantsInfoVirtual",
            "FmTopicRoleParticipantsInfoVirtual",
            "FmAddTopicInfoVirtual",
        };

        /// <inheritdoc />
        public async Task<IKrPermissionsManagerContext> TryCreateContextAsync(
            KrPermissionsCreateContextParams param,
            CancellationToken cancellationToken = default)
        {
            KrPermissionsCheckMode mode = KrPermissionsCheckMode.WithoutCard;

            if (param.Card is not null)
            {
                mode = param.IsStore ? KrPermissionsCheckMode.WithStoreCard : KrPermissionsCheckMode.WithCard;
                param.CardTypeID ??= param.Card.TypeID;

                if (param.Card.StoreMode == CardStoreMode.Update)
                {
                    param.KrState ??=
                        (param.IsStore
                        ? await KrProcessSharedHelper.GetKrStateAsync(param.Card.ID, this.dbScope, cancellationToken)
                        : await KrProcessSharedHelper.GetKrStateAsync(param.Card, this.dbScope, cancellationToken))
                        ?? KrState.Draft;
                }
            }
            else if (param.CardID.HasValue)
            {
                mode = KrPermissionsCheckMode.WithCardID;
                param.CardTypeID ??= await this.cardRepository.GetTypeIDAsync(param.CardID.Value, CardInstanceType.Card, cancellationToken);

                param.KrState ??= await KrProcessSharedHelper.GetKrStateAsync(param.CardID.Value, this.dbScope, cancellationToken) ?? KrState.Draft;
            }

            if (param.CardTypeID.HasValue
                && (await this.cardMetadata.GetCardTypesAsync(cancellationToken)).TryGetValue(param.CardTypeID.Value, out var cardType))
            {
                var components = await KrComponentsHelper.GetKrComponentsAsync(param.CardTypeID.Value, this.krTypesCache, cancellationToken);

                if (components.HasNot(KrComponents.Base))
                {
                    // Для данного типа карточек проверка не предусмотрена
                    return null;
                }

                if (components.Has(KrComponents.DocTypes))
                {
                    if (!param.DocTypeID.HasValue)
                    {
                        if (param.Card is not null)
                        {
                            param.DocTypeID ??= await KrProcessSharedHelper.GetDocTypeIDAsync(param.Card, this.dbScope, cancellationToken);
                        }
                        else if (param.CardID.HasValue)
                        {
                            param.DocTypeID ??= await KrProcessSharedHelper.GetDocTypeIDAsync(param.CardID.Value, this.dbScope, cancellationToken);
                        }
                    }
                    if (!param.DocTypeID.HasValue)
                    {
                        param.ValidationResult?.AddError(this, "$KrMessages_DocTypeNotSpecified");
                        return null;
                    }
                }
                else
                {
                    param.DocTypeID = null;
                }
            }
            else
            {
                return null; // Тип карточки не известен или не задан, тогда проверку прав доступа не выполняем
            }

            if (param.PermissionsCache is null)
            {
                var getCacheResult = new ValidationResultBuilder();
                param.PermissionsCache = await this.permissionsCacheContainer.TryGetCacheAsync(getCacheResult, cancellationToken);
                param.ValidationResult?.Add(getCacheResult);
                if (!getCacheResult.IsSuccessful())
                {
                    return null;
                }
            }

            return new KrPermissionsManagerContext(
                this.dbScope,
                this.session,
                param.PermissionsCache,
                this.cardMetadata,
                this.krTypesCache,
                param.Card,
                param.CardID,
                cardType,
                param.DocTypeID,
                param.KrState,
                param.FileID,
                param.FileVersionID,
                param.WithRequiredPermissions,
                param.WithExtendedPermissions,
                param.IgnoreSections,
                mode,
                param.ValidationResult,
                StorageHelper.Clone(param.AdditionalInfo),
                param.PrevToken,
                param.ServerToken,
                param.ExtensionContext,
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IKrPermissionsManagerResult> GetEffectivePermissionsAsync(
            IKrPermissionsManagerContext context,
            params KrPermissionFlagDescriptor[] requiredPermissions)
        {
            ThrowIfNull(context);

            if (requiredPermissions is null
                || requiredPermissions.Length == 0)
            {
                return KrPermissionsManagerResult.Empty;
            }

            context.Descriptor = new KrPermissionsDescriptor(requiredPermissions);
            context.Method = nameof(GetEffectivePermissionsAsync);

            // Последовательность расчета прав:
            // 1) Если запрашиваем обязательные права доступа, сперва считаем их. Если все права рассчитаны, завершаемся
            // 2) Выполняем расширения по заданиям. Они могут как выдать права, так и запросить новые права.
            // 3) Выполняем расширения по карточке. Они могут как выдать права, так и запросить новые права.
            // 4) Проверяем расширения по токену. Проверка идет после расширений, чтобы была возможность перенести из токена запрашиваемые в расширениях права.
            //    если все права рассчитаны, завершаемся.
            // 5) Выполняем расчет прав по правилам доступа.
            // 6) Завершаемся
            // Важно! Если запрашивается расширенный расчет прав доступа, то при построении результата также рассчитываем расширенный доступ по полям.
            await using var innerContext = new InnerContext(context, false);
            if (!context.ValidationResult.IsSuccessful())
            {
                return KrPermissionsManagerResult.Empty;
            }

            if (context.WithRequiredPermissions)
            {
                await this.ExtendRequiredPermissionsWithRulesAsync(innerContext);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return KrPermissionsManagerResult.Empty;
                }
            }

            await this.ExtendPermissionsWithTasksAsync(context);
            if (!context.ValidationResult.IsSuccessful())
            {
                return KrPermissionsManagerResult.Empty;
            }

            await this.CheckPermissionsWithCardAndTokenAsync(innerContext);
            if (!context.ValidationResult.IsSuccessful())
            {
                return KrPermissionsManagerResult.Empty;
            }

            if (context.Descriptor.AllChecked())
            {
                return await this.CreateResultAsync(innerContext);
            }

            if (await this.CheckRulesAsync(innerContext))
            {
                return await this.CreateResultAsync(innerContext);
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return KrPermissionsManagerResult.Empty;
            }

            return await this.CreateResultAsync(innerContext);
        }

        /// <inheritdoc />
        public async Task<KrPermissionsManagerCheckResult> CheckRequiredPermissionsAsync(
            IKrPermissionsManagerContext context,
            params KrPermissionFlagDescriptor[] requiredPermissions)
        {
            ThrowIfNull(context);

            if (requiredPermissions is null
                || requiredPermissions.Length == 0)
            {
                return true;
            }

            context.Descriptor = new KrPermissionsDescriptor(requiredPermissions);
            context.Method = nameof(CheckRequiredPermissionsAsync);

            // Последовательность проверки прав:
            // 1) Выполняем расширения по заданиям. Они могут как выдать права, так и запросить новые права.
            // 2) Выполняем расширения по карточке. Они могут как выдать права, так и запросить новые права.
            // 3) Проверяем расширения по токену. Проверка идет после расширений, чтобы была возможность перенести из токена запрашиваемые в расширениях права.
            //    если прав доступа достаточно, завершаемся.
            // 4) Если запрошена расширенная проверка прав доступа по полям, производим проверку прав доступа по полям.
            //    Если проверка прав по полям исчерпывающая (нет не проверенных полей), то проверка на редактирование карточки считается успешной.
            //    Расширенная проверка прав может вернуть ошибку, если есть изменения полей/секций, которые запрещены для изменения.
            // 5) Выполняем проверку прав по правилам доступа.
            // 6) Если при проверке прав какие-то из прав не были успешно проверены, записываем сообщение об ошибки в результат валидации.
            await using (var innerContext = new InnerContext(context, true))
            {
                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                await this.ExtendPermissionsWithTasksAsync(context);
                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                await this.CheckPermissionsWithCardAndTokenAsync(innerContext);
                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                if (context.WithExtendedPermissions)
                {
                    if (context.Mode == KrPermissionsCheckMode.WithStoreCard)
                    {
                        if (requiredPermissions.Contains(KrPermissionFlagDescriptors.EditCard)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.EditFiles)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.EditOwnFiles)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.DeleteFiles)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.DeleteOwnFiles)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.SignFiles)
                            || requiredPermissions.Contains(KrPermissionFlagDescriptors.AddFiles))
                        {
                            await this.CheckExtendedPermissionsAsync(innerContext);
                            if (!context.ValidationResult.IsSuccessful())
                            {
                                return false;
                            }
                        }

                        await this.CheckExtendedMandatoryAsync(innerContext);
                        if (!context.ValidationResult.IsSuccessful())
                        {
                            if (innerContext.HasResultInfo)
                            {
                                return new KrPermissionsManagerCheckResult(false, innerContext.ResultInfo);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (context.FileID.HasValue
                        && requiredPermissions.Contains(KrPermissionFlagDescriptors.ReadCard))
                    {
                        await this.CheckContextFileRulesAsync(innerContext);
                        if (!context.ValidationResult.IsSuccessful())
                        {
                            return false;
                        }
                    }
                }

                if (context.Descriptor.AllChecked())
                {
                    if (innerContext.HasResultInfo)
                    {
                        return new KrPermissionsManagerCheckResult(true, innerContext.ResultInfo);
                    }
                    else
                    {
                        return true;
                    }
                }

                if (await this.CheckRulesAsync(innerContext))
                {
                    if (innerContext.HasResultInfo)
                    {
                        return new KrPermissionsManagerCheckResult(true, innerContext.ResultInfo);
                    }
                    else
                    {
                        return true;
                    }
                }

                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                if (context.Descriptor.AllChecked())
                {
                    if (innerContext.HasResultInfo)
                    {
                        return new KrPermissionsManagerCheckResult(true, innerContext.ResultInfo);
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            await this.AddErrorAsync(
                context,
                KrPermissionsHelper.GetNotEnoughPermissionsErrorMessage(context.Descriptor.StillRequired.ToArray()));

            return false;
        }

        #endregion

        #region Private Methods

        #region Check Methods

        private async Task CheckPermissionsWithCardAndTokenAsync(InnerContext innerContext)
        {
            var context = innerContext.Context;

            try
            {
                await using var extensions =
                    await this.extensionContainer.ResolveExecutorAsync<ICardPermissionsExtension>(context.CancellationToken);

                await this.ExtendPermissionsWithCardAsync(context, extensions);
                await this.CheckPermissionsWithPreviousTokenAsync(innerContext, extensions);
                await CheckPermissionsWithServerTokenAsync(innerContext);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                context.ValidationResult.AddException(this, ex);
            }
        }

        private static async Task CheckPermissionsWithServerTokenAsync(
            InnerContext innerContext)
        {
            var context = innerContext.Context;
            var serverToken = context.ServerToken;
            if (serverToken is null)
            {
                return;
            }
            var descriptor = context.Descriptor;

            foreach (var permission in serverToken.Permissions)
            {
                // НЕ Принудительно добавляем настройки доступа из предыдущего валидного токена
                // Если права были запрошены, они перенесутся. Если нет - проигнорируются.
                descriptor.Set(permission, true);
            }

            if (innerContext.CheckPermissions
                && context.WithExtendedPermissions
                && serverToken.ExtendedCardSettings is not null)
            {
                if (serverToken.ExtendedCardSettings.GetCardSettings() is ICollection<IKrPermissionSectionSettings> cardSettings)
                {
                    // Если в дескрипторе уже есть настройки из предыдущего токена, то мержим их
                    foreach (var settings in cardSettings)
                    {
                        if (descriptor.ExtendedCardSettings.TryGetItem(settings.ID, out var existedSettingsBuilder))
                        {
                            existedSettingsBuilder.Add(settings, serverTokenPriority);
                        }
                        else
                        {
                            descriptor.ExtendedCardSettings.Add(
                                new KrPermissionSectionSettingsBuilder(settings.ID)
                                    .Add(KrPermissionSectionSettings.ConvertFrom(settings), serverTokenPriority));
                        }
                    }
                }

                if (serverToken.ExtendedCardSettings.GetTasksSettings() is var tasksSettings
                    && tasksSettings is not null
                    && tasksSettings.Count > 0)
                {
                    foreach (var taskSettings in tasksSettings)
                    {
                        var taskType = taskSettings.Key;
                        if (!descriptor.ExtendedTasksSettings.TryGetValue(taskType, out var existedTaskSettings)
                            || existedTaskSettings is null)
                        {
                            descriptor.ExtendedTasksSettings[taskType]
                                = existedTaskSettings
                                = new HashSet<Guid, IKrPermissionSectionSettingsBuilder>(
                                    x => x.SectionID);
                        }

                        // Если в дескрипторе уже есть настройки из предыдущего токена, то мержим их
                        foreach (var settings in taskSettings.Value)
                        {
                            if (existedTaskSettings.TryGetItem(settings.ID, out var existedSettingsBuilder))
                            {
                                existedSettingsBuilder.Add(settings, serverTokenPriority);
                            }
                            else
                            {
                                descriptor.ExtendedCardSettings.Add(
                                    new KrPermissionSectionSettingsBuilder(settings.ID)
                                        .Add(KrPermissionSectionSettings.ConvertFrom(settings), serverTokenPriority));
                            }
                        }
                    }
                }
            }
        }

        private async Task CheckPermissionsWithPreviousTokenAsync(
            InnerContext innerContext,
            IExtensionExecutor extensions)
        {
            var context = innerContext.Context;
            if (context.PreviousToken is null)
            {
                return;
            }

            var card = await this.GetCardForTokenAsync(context);

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            switch (await this.krTokenProvider.ValidateTokenAsync(card, context.PreviousToken, context.ValidationResult))
            {
                case KrTokenValidationResult.Fail:
                    //Сообщение об ошибке было установлено в krTokenProvider.ValidateToken
                    return;

                case KrTokenValidationResult.Success:
                    // если в токене нет всех запрашиваемых прав, то выкидываем этот токен и перерасчитываем новый;
                    // или если есть расширение на правило доступа, которое говорит "пересчитай токен", то опять же перерасчитываем новый токен
                    if (!await CheckPermissionsRecalcRequiredWithExtensionsAsync(context, extensions))
                    {
                        foreach (var permission in context.PreviousToken.Permissions)
                        {
                            // НЕ Принудительно добавляем настройки доступа из предыдущего валидного токена
                            // Если права были запрошены, они перенесутся. Если нет - проигнорируются.
                            context.Descriptor.Set(permission, true);
                        }

                        if (innerContext.CheckPermissions
                            && context.WithExtendedPermissions)
                        {
                            // Считаем, что настройки рассчитаны только, если они были в токене
                            if (context.PreviousToken.ExtendedCardSettings is not null)
                            {
                                innerContext.ExtendedSettingsCalculated = true;
                                // Переносим настройки из токена в дескриптор
                                context.Descriptor.ExtendedCardSettings.AddRange(
                                    context.PreviousToken.ExtendedCardSettings.GetCardSettings().Select(x => KrPermissionSectionSettings.ConvertFrom(x)));

                                if (context.PreviousToken.ExtendedCardSettings.GetTasksSettings() is var tasksSettings
                                    && tasksSettings is not null
                                    && tasksSettings.Count > 0)
                                {
                                    foreach (var taskSettings in tasksSettings)
                                    {
                                        var taskType = taskSettings.Key;
                                        context.Descriptor.ExtendedTasksSettings[taskType]
                                            = new HashSet<Guid, IKrPermissionSectionSettingsBuilder>(
                                                x => x.SectionID,
                                                taskSettings.Value.Select(x => KrPermissionSectionSettings.ConvertFrom(x)));
                                    }
                                }

                                // Если идет проверка по файлу, то пытаемся сформировать правило доступа для этого файла
                                if (context.PreviousToken.ExtendedCardSettings.GetFileSettings() is { Count: > 0 } fileSettings)
                                {
                                    foreach (var fileSetting in fileSettings)
                                    {
                                        context.Descriptor.FileRules.Add(
                                            new KrPermissionsDirectFileRule
                                            {
                                                FileID = fileSetting.FileID,
                                                ReadAccessSetting = fileSetting.ReadAccessSetting,
                                                EditAccessSetting = fileSetting.EditAccessSetting,
                                                DeleteAccessSetting = fileSetting.DeleteAccessSetting,
                                                SignAccessSetting = fileSetting.SignAccessSetting,
                                                FileSizeLimit = fileSetting.FileSizeLimit,
                                            });
                                    }
                                }
                                if (context.PreviousToken.ExtendedCardSettings.TryGetOwnFilesSettings() is { } ownFilesSettings)
                                {
                                    if (ownFilesSettings.GlobalSettings is { } globalSettings)
                                    {
                                        this.AddNewFileSettings(context, globalSettings, 0, KrPermissionsHelper.FileCheckRules.OwnFiles);
                                    }

                                    if (ownFilesSettings.TryGetExtensionSettings() is { Count: > 0 } extensionsSettings)
                                    {
                                        foreach (var (_, extensionSettings) in extensionsSettings)
                                        {
                                            this.AddNewFileSettings(context, extensionSettings, 2, KrPermissionsHelper.FileCheckRules.OwnFiles);
                                        }
                                    }
                                }
                                if (context.PreviousToken.ExtendedCardSettings.TryGetOtherFilesSettings() is { } otherFilesSettings)
                                {
                                    if (otherFilesSettings.GlobalSettings is { } globalSettings)
                                    {
                                        this.AddNewFileSettings(context, globalSettings, 0, KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers);
                                    }

                                    if (otherFilesSettings.TryGetExtensionSettings() is { Count: > 0 } extensionsSettings)
                                    {
                                        foreach (var (_, extensionSettings) in extensionsSettings)
                                        {
                                            this.AddNewFileSettings(context, extensionSettings, 2, KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers);
                                        }
                                    }
                                }
                            }
                        }

                        return;
                    }
                    break;

                case KrTokenValidationResult.NeedRecreating:
                    return;
            }

            return;
        }

        private void AddNewFileSettings(
            IKrPermissionsManagerContext context,
            KrPermissionsFileExtensionSettings extensionSettings,
            int basePriority,
            int fileCheckRule)
        {
            // У полученных с клиента обрезанных правил приоритет такой:
            // Если стоит флаг IsDisallowed, то:
            // 0 - правило запрета
            // 1 - правило разрешения конкретных категорий
            //
            // Если не стоит флаг IsDisallowed, то:
            // 0 - правило разрешения всех или конкретных категорий
            // 1 - правило запрета конкретных категорий
            //
            // К приоритету добавляется basePriority, т.к. для глобальных настроек приоритет должен быть ниже, чем для разбитых по расширениям
            var extensions = string.IsNullOrEmpty(extensionSettings.Extension)
                ? null
                : new string[] { extensionSettings.Extension };
            if (extensionSettings.DisallowedCategories is not null
                || extensionSettings.AddDisallowed)
            {
                context.Descriptor.FileRules.Add(
                    new KrPermissionsFileRule(
                        this.dbScope,
                        extensions,
                        extensionSettings.DisallowedCategories)
                    {
                        AddAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Disallowed,
                        FileCheckRule = fileCheckRule,
                        Priority = basePriority + (extensionSettings.AddDisallowed ? 0 : 1),
                    });
            }
            if (extensionSettings.AllowedCategories is not null
                || extensionSettings.AddAllowed)
            {
                context.Descriptor.FileRules.Add(
                    new KrPermissionsFileRule(
                        this.dbScope,
                        extensions,
                        extensionSettings.AllowedCategories)
                    {
                        AddAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Allowed,
                        Priority = basePriority + (extensionSettings.AddDisallowed ? 1 : 0),
                        FileCheckRule = fileCheckRule,
                    });
            }

            if (extensionSettings.SizeLimitSettings is not null)
            {
                foreach (var sizeLimitSetting in extensionSettings.SizeLimitSettings.OrderByDescending(x => x.Categories?.Count ?? 0))
                {
                    context.Descriptor.FileRules.Add(
                        new KrPermissionsFileRule(
                            this.dbScope,
                            extensions,
                            sizeLimitSetting.Categories)
                        {
                            Priority = basePriority + (extensionSettings.AddDisallowed ? 1 : 0),
                            FileSizeLimit = sizeLimitSetting.Limit,
                            FileCheckRule = fileCheckRule,
                        });
                }
            }

            // Правила проверки доступа на подписание
            if (extensionSettings.SignDisallowedCategories is not null
                || extensionSettings.SignDisallowed)
            {
                context.Descriptor.FileRules.Add(
                    new KrPermissionsFileRule(
                        this.dbScope,
                        extensions,
                        extensionSettings.SignDisallowedCategories)
                    {
                        SignAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Disallowed,
                        FileCheckRule = fileCheckRule,
                        Priority = basePriority + (extensionSettings.SignDisallowed ? 0 : 1),
                    });
            }
            if (extensionSettings.SignAllowedCategories is not null
                || extensionSettings.SignAllowed)
            {
                context.Descriptor.FileRules.Add(
                    new KrPermissionsFileRule(
                        this.dbScope,
                        extensions,
                        extensionSettings.SignAllowedCategories)
                    {
                        SignAccessSetting = KrPermissionsHelper.FileEditAccessSettings.Allowed,
                        Priority = basePriority + (extensionSettings.SignDisallowed ? 1 : 0),
                        FileCheckRule = fileCheckRule,
                    });
            }
        }

        /// <summary>
        /// Метод для проверки доступа по правилам
        /// </summary>
        /// <param name="context">Контекст проверки доступа</param>
        /// <returns>Возвращает true, если проверка прав доступа полностью завершена, иначе false</returns>
        private async Task<bool> CheckRulesAsync(
            InnerContext innerContext)
        {
            if (!await this.FillInnerContextAsync(innerContext))
            {
                return false;
            }

            var context = innerContext.Context;
            //Проверенные правила - успешные и неуспешные
            var checkingRules = new HashSet<Guid, IKrPermissionRuleSettings>(
                x => x.ID,
                innerContext.PermissionsCache.GetRulesByTypeAndState(
                    context.DocTypeID ?? context.CardType.ID,
                    context.DocState)
                    .Where(x => x.Flags.Any(y => context.Descriptor.StillRequired.Contains(y))));

            if (await this.CheckRulesForStaticRolesAsync(
                    innerContext,
                    checkingRules))
            {
                return true;
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return false;
            }

            if (await this.CheckRulesForAclGenerationRulesAsync(
                    innerContext,
                    checkingRules))
            {
                return true;
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return false;
            }

            if (await this.CheckRulesForContextRolesAsync(
                    innerContext,
                    checkingRules))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод для проверки доступа по статическим ролям
        /// </summary>
        /// <param name="innerContext">Контекст проверки прав доступа.</param>
        /// <param name="checkingRules">Список проверяемых правил.</param>
        /// <param name="force">Определяет, должны ли настройки успешно проверенных правил установиться принудительно, даже если они не были запрошены.</param>
        /// <returns>Возвращает <c>true</c>, если проверка прав доступа полностью завершена, иначе <c>false</c>.</returns>
        private async Task<bool> CheckRulesForStaticRolesAsync(
            InnerContext innerContext,
            HashSet<Guid, IKrPermissionRuleSettings> checkingRules,
            bool force = false)
        {
            var context = innerContext.Context;

            // Нет смысла проверять список ролей, если правило уже было отклонено
            foreach (var rejectedRuleID in innerContext.RejectedRules)
            {
                checkingRules.RemoveByKey(rejectedRuleID);
            }

            foreach (var submittedRuleID in innerContext.SubmittedRules)
            {
                // Нет смысла проверять список ролей, если правило уже было проверено
                if (checkingRules.TryGetItem(submittedRuleID, out var rule))
                {
                    //Карточка была успешно проверена ранее - добавляем соотв. разрешение
                    foreach (var flag in rule.Flags)
                    {
                        context.Descriptor.Set(flag, true, force);
                    }
                    checkingRules.RemoveByKey(submittedRuleID);
                }
            }

            if (!force && context.Descriptor.AllChecked())
            {
                return true;
            }

            var nestedContextID = await this.GetNestedContextIDAsync(context);
            var rulesForStaticRoles =
                await this.GetRulesForStaticRolesAsync(
                    checkingRules,
                    nestedContextID,
                    context.CancellationToken);

            foreach (var ruleID in rulesForStaticRoles)
            {
                if (!force && context.Descriptor.AllChecked())
                {
                    return true;
                }

                var rule = checkingRules[ruleID];
                if (force)
                {
                    // Если нет флагов, которые можно было бы добавить, то не проверяем правило
                    if (rule.Flags.All(x => context.Descriptor.Permissions.Contains(x)))
                    {
                        continue;
                    }
                }
                else
                {
                    // Если нет флагов, которые нужно проверить
                    if (!context.Descriptor.StillRequired.Any(x => rule.Flags.Contains(x)))
                    {
                        continue;
                    }
                }

                bool success = await this.CheckWithConditionsAsync(innerContext, rule)
                            && await this.CheckWithExtensionsAsync(innerContext, ruleID);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                if (success)
                {
                    innerContext.SubmittedRules.Add(ruleID);
                    //Карточка "пролезла" через правило - добавляем соотв. разрешение
                    foreach (var flag in rule.Flags)
                    {
                        context.Descriptor.Set(flag, true, force);
                    }
                }
                else
                {
                    innerContext.RejectedRules.Add(ruleID);
                }
            }

            return false;
        }

        /// <summary>
        /// Метод для проверки доступа по правилам расчёта ACL.
        /// </summary>
        /// <param name="innerContext">Контекст проверки прав доступа.</param>
        /// <param name="checkingRules">Список проверяемых правил.</param>
        /// <param name="force">Определяет, должны ли настройки успешно проверенных правил установиться принудительно, даже если они не были запрошены.</param>
        /// <returns>Возвращает <c>true</c>, если проверка прав доступа полностью завершена, иначе <c>false</c>.</returns>
        private async ValueTask<bool> CheckRulesForAclGenerationRulesAsync(
            InnerContext innerContext,
            HashSet<Guid, IKrPermissionRuleSettings> checkingRules,
            bool force = false)
        {
            var context = innerContext.Context;
            if (context.Mode == KrPermissionsCheckMode.WithoutCard
                || context.CardID is null)
            {
                return false;
            }

            var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
            if (!license.Modules.HasEnterpriseOrContains(LicenseModules.AclID))
            {
                return false;
            }

            // Нет смысла проверять список ролей, если правило уже было отклонено
            foreach (var rejectedRuleID in innerContext.RejectedRules)
            {
                checkingRules.RemoveByKey(rejectedRuleID);
            }

            foreach (var submittedRuleID in innerContext.SubmittedRules)
            {
                // Нет смысла проверять список ролей, если правило уже было проверено
                if (checkingRules.TryGetItem(submittedRuleID, out var rule))
                {
                    //Карточка была успешно проверена ранее - добавляем соотв. разрешение
                    foreach (var flag in rule.Flags)
                    {
                        context.Descriptor.Set(flag, true, force);
                    }
                    checkingRules.RemoveByKey(submittedRuleID);
                }
            }

            if (!force && context.Descriptor.AllChecked())
            {
                return true;
            }

            var rulesForAclGenerationRules =
                await this.GetRulesForAclGenerationRulesAsync(
                    checkingRules,
                    context.CardID.Value,
                    context.CancellationToken);

            foreach (var ruleID in rulesForAclGenerationRules)
            {
                if (!force && context.Descriptor.AllChecked())
                {
                    return true;
                }

                var rule = checkingRules[ruleID];
                if (force)
                {
                    // Если нет флагов, которые можно было бы добавить, то не проверяем правило
                    if (rule.Flags.All(x => context.Descriptor.Permissions.Contains(x)))
                    {
                        continue;
                    }
                }
                else
                {
                    // Если нет флагов, которые нужно проверить
                    if (!context.Descriptor.StillRequired.Any(x => rule.Flags.Contains(x)))
                    {
                        continue;
                    }
                }

                bool success = await this.CheckWithConditionsAsync(innerContext, rule)
                            && await this.CheckWithExtensionsAsync(innerContext, ruleID);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                if (success)
                {
                    innerContext.SubmittedRules.Add(ruleID);
                    //Карточка "пролезла" через правило - добавляем соотв. разрешение
                    foreach (var flag in rule.Flags)
                    {
                        context.Descriptor.Set(flag, true, force);
                    }
                }
                else
                {
                    innerContext.RejectedRules.Add(ruleID);
                }
            }

            return false;
        }

        /// <summary>
        /// Метод для получения идентификатора контекста карточки для определения корректных вложенных ролей.
        /// </summary>
        /// <param name="context">Контекст обработки проверки прав доступа.</param>
        /// <returns>Идентификатор контекста карточки, или null, если для карточки нет контекста для определения вложенных ролей.</returns>
        private async ValueTask<Guid?> GetNestedContextIDAsync(IKrPermissionsManagerContext context)
        {
            if (context.Info.TryGetValue("NestedContextID", out var nestedContextObjcet))
            {
                return nestedContextObjcet as Guid?;
            }

            Guid? nestedContextID;
            if (context.Mode == KrPermissionsCheckMode.WithCard && context.Card is not null)
            {
                nestedContextID = await this.nestedRoleContextSelector.GetContextAsync(context.Card, context.CancellationToken);
            }
            else if (context.Mode == KrPermissionsCheckMode.WithStoreCard && context.Card is { StoreMode: CardStoreMode.Insert })
            {
                nestedContextID = await this.nestedRoleContextSelector.GetContextAsync(context.Card, context.CancellationToken);
            }
            else if (context.CardID.HasValue)
            {
                nestedContextID = await this.nestedRoleContextSelector.GetContextAsync(context.CardID.Value, context.CancellationToken);
            }
            else
            {
                nestedContextID = context.DocTypeID ?? context.CardType?.ID;
            }
            context.Info["NestedContextID"] = nestedContextID;
            return nestedContextID;
        }

        /// <summary>
        /// Метод для проверки доступа по контекстным ролям
        /// </summary>
        /// <param name="context">Контекст проверки доступа</param>
        /// <param name="submittedRules">Список правил доступа, которые уже прошли проверку</param>
        /// <param name="rejectedRules">Список правил доступа, которые не прошли проверку</param>
        /// <param name="executor">Объект, выполняющий расширения правил доступа</param>
        /// <param name="conditionContext">Контекст проверки условий для правил доступа. Задан, если предполагается проверка по условиям</param>
        /// <param name="conditionCompilationResult">Результат компиляции условий. Задан, если предполагается проверка по условиям</param>
        /// <returns>Возвращает true, если проверка прав доступа полностью завершена, иначе false</returns>
        private async Task<bool> CheckRulesForContextRolesAsync(
            InnerContext innerContext,
            HashSet<Guid, IKrPermissionRuleSettings> checkingRules,
            bool force = false)
        {
            var context = innerContext.Context;
            if (context.Mode == KrPermissionsCheckMode.WithoutCard)
            {
                return false;
            }

            foreach (var rule in checkingRules)
            {
                if (!force && context.Descriptor.AllChecked())
                {
                    return true;
                }

                // Если правило уже было проверено и не прошло, или в правиле нет контекстных ролей
                if (rule.ContextRoles.Count == 0
                    || innerContext.RejectedRules.Contains(rule.ID))
                {
                    continue;
                }

                if (force)
                {
                    // Если нет флагов, которые можно было бы добавить, то не проверяем правило
                    if (rule.Flags.All(x => context.Descriptor.Permissions.Contains(x)))
                    {
                        continue;
                    }
                }
                else
                {
                    // Если нет флагов, которые нужно проверить
                    if (!context.Descriptor.StillRequired.Any(x => rule.Flags.Contains(x)))
                    {
                        continue;
                    }
                }

                // Если правило уже было проверено и прошло
                if (innerContext.SubmittedRules.Contains(rule.ID))
                {
                    //Карточка была успешно проверена ранее - добавляем соотв. разрешение
                    foreach (var flag in rule.Flags)
                    {
                        context.Descriptor.Set(flag, true, force);
                    }
                    continue;
                }

                bool success = await this.CheckWithConditionsAsync(innerContext, rule)
                    && await this.CheckWithExtensionsAsync(innerContext, rule.ID)
                    && await this.CheckUserInContextRolesAsync(
                        innerContext,
                        rule);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return false;
                }

                if (success)
                {
                    innerContext.SubmittedRules.Add(rule.ID);

                    foreach (var flag in rule.Flags)
                    {
                        //Карточка "пролезла" через правило - добавляем соотв. разрешение
                        context.Descriptor.Set(flag, true, force);
                    }
                }
                else
                {
                    //Карточка не "пролезла" через правило
                    //Запоминаем что правило проверено не успешно
                    innerContext.RejectedRules.Add(rule.ID);
                }
            }

            return false;
        }

        private async Task<bool> CheckUserInContextRolesAsync(
            InnerContext innerContext,
            IKrPermissionRuleSettings rule)
        {
            // Сортировка по ролям обеспечивает уменьшение необходимости проверки всех контекстных ролей в правиле,
            // т.к. сперва всегда будут проверяться уже рассчитанные контекстные роли, а затем остальные
            var nestedContextID = await this.GetNestedContextIDAsync(innerContext.Context);
            foreach (Guid role in rule.ContextRoles)
            {
                if (innerContext.SubmittedRoles.Contains(role))
                {
                    return true;
                }
                else if (innerContext.RejectedRoles.Contains(role))
                {
                    continue;
                }

                Card contextRole = await this.roleCache.GetAsync(role);
                Dictionary<string, object> sectionFields = contextRole.Sections["ContextRoles"].RawFields;
                string sqlTextForUser = sectionFields.Get<string>("SqlTextForUser");
                string sqlTextForCard = sectionFields.Get<string>("SqlTextForCard");

                bool userInRole = await this.contextRoleManager.CheckUserInCardContextAsync(
                    role,
                    contextRole.Sections["Roles"].Fields.Get<string>("Name"),
                    sqlTextForUser,
                    sqlTextForCard,
                    innerContext.Context.CardID.Value,
                    this.session.User.ID,
                    nestedContextID: nestedContextID,
                    useSafeTransaction: false,
                    cancellationToken: innerContext.Context.CancellationToken);

                //юзер входит хотя бы в одну контекстную роль карточки
                if (userInRole)
                {
                    //Запоминаем как успешно проверенную роль
                    innerContext.SubmittedRoles.Add(role);
                    return true;
                }
                else
                {
                    //Запоминаем как не успешно проверенную роль
                    innerContext.RejectedRoles.Add(role);
                }
            }
            return false;
        }

        private static async Task<bool> CheckPermissionsRecalcRequiredWithExtensionsAsync(
            IKrPermissionsManagerContext context,
            IExtensionExecutor extensions)
        {
            if (extensions is not null)
            {
                await extensions.ExecuteAsync(nameof(ICardPermissionsExtension.IsPermissionsRecalcRequired), (IKrPermissionsRecalcContext) context);
                return ((IKrPermissionsRecalcContext) context).IsRecalcRequired;
            }

            return false;
        }

        private async Task<bool> CheckWithExtensionsAsync(
            InnerContext innerContext,
            Guid ruleID)
        {
            try
            {
                var ruleContext = new KrPermissionsRuleExtensionContext(innerContext.Context, ruleID);
                await innerContext.RuleExecutor.ExecuteAsync(nameof(IKrPermissionsRuleExtension.CheckRuleAsync), ruleContext);

                if (ruleContext.Cancel)
                {
                    return false;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                innerContext.Context.ValidationResult.AddException(this, ex);
                return false;
            }

            return true;
        }

        private async ValueTask<bool> CheckWithConditionsAsync(
            InnerContext innerContext,
            IKrPermissionRuleSettings rule)
        {
            if (innerContext.ConditionContext is not null)
            {
                await using (this.dbScope.Create())
                {
                    if (rule is not null)
                    {
                        return await
                            this.conditionExecutor.CheckConditionAsync(
                                rule.Conditions,
                                innerContext.ConditionContext);
                    }
                }
            }

            return true;
        }

        private async Task CheckContextFileRulesAsync(
            InnerContext innerContext)
        {
            var context = innerContext.Context;
            var descriptor = context.Descriptor;

            await this.TryCalcExtendedFileRulesAsync(innerContext);

            if (descriptor.FileRules.Count > 0)
            {
                var fileContext = new KrPermissionsFilesManagerContext(
                    context.Session,
                    KrPermissionsFileAccessSettingFlag.Read,
                    descriptor.FileRules,
                    true);
                fileContext.SetFile(context.FileID.Value, context.FileVersionID);

                var checkResult = await this.krPermissionsFilesManager.CheckPermissionsAsync(fileContext);

                if (!checkResult.AccessSettings.TryGetValue(KrPermissionsFileAccessSettingFlag.Read, out var currentAccessSetting))
                {
                    return;
                }

                if (checkResult.ValidationResult.Items.Count > 0)
                {
                    foreach (var item in checkResult.ValidationResult.Items)
                    {
                        if (item.Type == ValidationResultType.Error
                            && string.IsNullOrEmpty(item.Details))
                        {
                            await this.AddErrorAsync(
                                context,
                                item.Message);
                        }
                        else
                        {
                            context.ValidationResult.Add(item);
                        }
                    }
                }

                innerContext.ResultInfo[KrPermissionsHelper.FileReadAccessSettings.InfoKey] = currentAccessSetting;
            }
        }

        private async Task CheckExtendedMandatoryAsync(
            InnerContext innerContext)
        {
            var context = innerContext.Context;
            var descriptor = context.Descriptor;

            await this.TryCalcExtendedMandatoryAsync(innerContext);

            if (descriptor.ExtendedMandatorySettings.Count == 0)
            {
                return;
            }

            var checkCard = await this.GetCardForMandatoryCheckAsync(context);
            var cardMeta = await this.cardMetadata.GetMetadataForTypeAsync(checkCard.TypeID, context.CancellationToken);
            var cardMetaSections = await cardMeta.GetSectionsAsync(context.CancellationToken);
            var checkTasks = checkCard.Tasks.Where(x => x.Action == CardTaskAction.Complete && x.OptionID.HasValue).ToArray();

            foreach (var mandatoryRule in descriptor.ExtendedMandatorySettings)
            {
                bool hasError = false;
                switch (mandatoryRule.ValidationType)
                {
                    case KrPermissionsHelper.MandatoryValidationType.Always:
                    case KrPermissionsHelper.MandatoryValidationType.WhenOneFieldFilled:
                        hasError = CheckExtendedMandatoryForCard(
                            mandatoryRule,
                            checkCard,
                            cardMetaSections);
                        break;

                    case KrPermissionsHelper.MandatoryValidationType.OnTaskCompletion:
                        bool needCardCheck = true;
                        foreach (var task in checkTasks)
                        {
                            if (hasError)
                            {
                                break;
                            }

                            if (mandatoryRule.HasTaskTypes
                                && mandatoryRule.TaskTypes.Contains(task.TypeID)
                                && (!mandatoryRule.HasCompletionOptions
                                    || mandatoryRule.CompletionOptions.Contains(task.OptionID.Value)))
                            {
                                if (needCardCheck)
                                {
                                    needCardCheck = false;
                                    hasError = CheckExtendedMandatoryForCard(
                                        mandatoryRule,
                                        checkCard,
                                        cardMetaSections);
                                }

                                if (!hasError)
                                {
                                    var taskMetaSections =
                                           await (await this.cardMetadata.GetMetadataForTypeAsync(task.TypeID, context.CancellationToken))
                                               .GetSectionsAsync(context.CancellationToken);

                                    hasError = CheckExtendedMandatoryForCard(
                                        mandatoryRule,
                                        task.Card,
                                        taskMetaSections);
                                }
                            }
                        }
                        break;
                }

                if (hasError)
                {
                    await this.AddErrorAsync(
                        context,
                        LocalizationManager.Format(mandatoryRule.Text));

                    if (!innerContext.ResultInfo.TryGetValue(KrPermissionsHelper.FailedMandatoryRulesKey, out var failedRules)
                        || failedRules is not IList failedRulesList)
                    {
                        failedRulesList = new List<object>();
                        innerContext.ResultInfo[KrPermissionsHelper.FailedMandatoryRulesKey] = failedRulesList;
                    }

                    failedRulesList.Add(mandatoryRule);
                }
            }
        }

        private static bool CheckExtendedMandatoryForCard(
            KrPermissionMandatoryRule mandatoryRule,
            Card checkCard,
            CardMetadataSectionCollection cardMetaSections)
        {
            if (cardMetaSections.TryGetValue(mandatoryRule.SectionID, out var sectionMeta)
                && checkCard.Sections.TryGetValue(sectionMeta.Name, out var section))
            {
                if (mandatoryRule.HasColumns)
                {
                    return CheckExtendedMandatoryForSection(
                        mandatoryRule,
                        section,
                        sectionMeta);
                }
                else if (sectionMeta.SectionType == CardSectionType.Table)
                {
                    return section.Rows.Count == 0;
                }
            }

            return false;
        }

        private static bool CheckExtendedMandatoryForSection(
            KrPermissionMandatoryRule mandatoryRule,
            CardSection section,
            CardMetadataSection sectionMeta)
        {
            if (sectionMeta.SectionType == CardSectionType.Entry)
            {
                return CheckExtendedMandatoryForEntry(
                    mandatoryRule,
                    section.RawFields,
                    sectionMeta);
            }
            else
            {
                foreach (var row in section.Rows)
                {
                    if (CheckExtendedMandatoryForEntry(
                        mandatoryRule,
                        row,
                        sectionMeta))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckExtendedMandatoryForEntry(
            KrPermissionMandatoryRule mandatoryRule,
            IDictionary<string, object> entry,
            CardMetadataSection sectionMeta)
        {
            bool hasError = false,
                 needCheck = mandatoryRule.ValidationType != KrPermissionsHelper.MandatoryValidationType.WhenOneFieldFilled;
            foreach (var column in mandatoryRule.ColumnIDs)
            {
                if (sectionMeta.Columns.TryGetValue(column, out var columnMeta))
                {
                    if (columnMeta.ColumnType == CardMetadataColumnType.Complex)
                    {
                        columnMeta = sectionMeta.Columns.FirstOrDefault(x =>
                            x.ComplexColumnIndex == columnMeta.ComplexColumnIndex
                            && x.ColumnType == CardMetadataColumnType.Physical
                            && x.IsReference);

                        if (columnMeta is null)
                        {
                            continue;
                        }
                    }

                    var checkValue = entry.TryGet<object>(columnMeta.Name);
                    if (checkValue is null
                        || (checkValue is string stringValue
                            && string.IsNullOrWhiteSpace(stringValue)))
                    {
                        hasError = true;
                        if (needCheck)
                        {
                            break;
                        }
                    }
                    else
                    {
                        needCheck = true;
                    }
                }
            }

            return needCheck
                && hasError;
        }

        private async Task CheckExtendedPermissionsAsync(
            InnerContext innerContext)
        {
            var context = innerContext.Context;
            var descriptor = context.Descriptor;

            Card checkCard = await this.GetCardForExtendedCheckAsync(context);
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }
            await this.TryCalcExtendedPermissionsAsync(
                innerContext,
                checkCard.Sections.Count > 0,
                checkCard.Tasks.Count > 0,
                checkCard.Files.Count > 0);

            bool editCheckCompleted = await this.CheckCardExtendedPermissionsAsync(
                context,
                checkCard,
                descriptor.ExtendedCardSettings);

            var tasks = checkCard.TryGetTasks();
            if (tasks?.Count > 0
                && descriptor.ExtendedTasksSettings.Count > 0)
            {
                foreach (var task in tasks)
                {
                    if (task.State == CardRowState.Inserted
                        || task.State == CardRowState.None)
                    {
                        continue;
                    }

                    if (descriptor.ExtendedTasksSettings.TryGetValue(task.TypeID, out var taskSettings))
                    {
                        if (task.Action != CardTaskAction.Complete)
                        {
                            await this.CheckCardExtendedPermissionsAsync(
                                context,
                                task.Card,
                                taskSettings);
                        }
                        else
                        {
                            var cardClone = task.Card.Clone();
                            cardClone.RemoveAllButChanged();
                            await this.CheckCardExtendedPermissionsAsync(
                                context,
                                cardClone,
                                taskSettings);
                        }
                    }
                }
            }

            if (descriptor.FileRules.Count > 0)
            {
                var fileCheckContext = new KrPermissionsFilesManagerContext(
                    this.session,
                    KrPermissionsFileAccessSettingFlag.None,
                    descriptor.FileRules,
                    true);

                KrPermissionsFileAccessSettingFlag stillRequired = KrPermissionsFileAccessSettingFlag.None;
                Dictionary<Guid, (CardFile File, long Limit)> fileSizes = null;

                foreach (var file in checkCard.Files)
                {
                    fileCheckContext.SetFile(file.RowID, storeFile: file);

                    var fileCheckResult = await this.krPermissionsFilesManager.CheckPermissionsAsync(fileCheckContext);
                    if (fileCheckResult.ValidationResult.Items.Count > 0)
                    {
                        foreach (var item in fileCheckResult.ValidationResult.Items)
                        {
                            if (item.Type == ValidationResultType.Error
                                && string.IsNullOrEmpty(item.Details))
                            {
                                await this.AddErrorAsync(
                                    context,
                                    item.Message);
                            }
                            else
                            {
                                context.ValidationResult.Add(item);
                            }
                        }
                    }

                    foreach (var (accessFlag, accessSetting) in fileCheckResult.AccessSettings)
                    {
                        if (accessSetting != KrPermissionsHelper.FileEditAccessSettings.Allowed)
                        {
                            stillRequired |= accessFlag;
                        }
                    }

                    if (fileCheckResult.FileSizeLimit.HasValue
                        && file.State is CardFileState.Inserted or CardFileState.Modified or CardFileState.ModifiedAndReplaced)
                    {
                        fileSizes ??= new Dictionary<Guid, (CardFile, long)>();
                        fileSizes.Add(file.RowID, (file, fileCheckResult.FileSizeLimit.Value));
                    }
                }

                if (stillRequired.HasNot(KrPermissionsFileAccessSettingFlag.Add))
                {
                    descriptor.Set(KrPermissionFlagDescriptors.AddFiles, true);
                }
                if (stillRequired.HasNot(KrPermissionsFileAccessSettingFlag.Edit))
                {
                    descriptor.Set(KrPermissionFlagDescriptors.EditFiles, true);
                    descriptor.Set(KrPermissionFlagDescriptors.EditOwnFiles, true);
                }
                if (stillRequired.HasNot(KrPermissionsFileAccessSettingFlag.Delete))
                {
                    descriptor.Set(KrPermissionFlagDescriptors.DeleteFiles, true);
                    descriptor.Set(KrPermissionFlagDescriptors.DeleteOwnFiles, true);
                }
                if (stillRequired.HasNot(KrPermissionsFileAccessSettingFlag.Sign))
                {
                    descriptor.Set(KrPermissionFlagDescriptors.SignFiles, true);
                }

                if (fileSizes is not null
                    && context.ExtensionContext is ICardStoreExtensionContext storeContext
                    && storeContext.ContentStorePending)
                {
                    storeContext.ContentStoreStarting += (s, e) =>
                    {
                        if (fileSizes.TryGetValue(e.ContentContext.FileID, out var fileAndLimit)
                            && e.ContentStream.Length > fileAndLimit.Limit)
                        {
                            var (file, limit) = fileAndLimit;

                            KrPermissionsHelper.AddFileValidationError(
                                e.ContentContext.ValidationResult,
                                this,
                                file.State == CardFileState.Inserted
                                    ? KrPermissionsHelper.KrPermissionsErrorAction.AddFile
                                    : KrPermissionsHelper.KrPermissionsErrorAction.EditFile,
                                KrPermissionsHelper.KrPermissionsErrorType.FileTooBig,
                                file.Name,
                                categoryCaption: file.CategoryCaption,
                                sizeLimit: limit);
                            e.Cancel = true;
                        }
                    };
                }
            }

            // Если проверка на Edit прошла по настройкам полей, то ставим его как проверенный
            if (editCheckCompleted)
            {
                context.Descriptor.Set(KrPermissionFlagDescriptors.EditCard, true);
            }
        }

        private async Task<bool> CheckCardExtendedPermissionsAsync(
            IKrPermissionsManagerContext context,
            Card card,
            HashSet<Guid, IKrPermissionSectionSettingsBuilder> settingsHash)
        {
            bool result = true;
            ICardMetadata cardTypeMeta = await this.cardMetadata.GetMetadataForTypeAsync(card.TypeID, context.CancellationToken);
            var sections = await cardTypeMeta.GetSectionsAsync(context.CancellationToken);

            foreach (var section in card.Sections.Values)
            {
                if (this.IgnoreSections.Contains(section.Name)
                    || context.IgnoreSections.Contains(section.Name)
                    || (section.Type == CardSectionType.Entry && section.RawFields.Count == 0)
                    || (section.Type == CardSectionType.Table && section.Rows.Count == 0))
                {
                    continue;
                }

                if (sections.TryGetValue(section.Name, out var sectionMeta)
                    && (sectionMeta.SectionType == CardSectionType.Entry
                        || await this.CheckParentSectionsExtendedPermissionsAsync(
                            context,
                            card,
                            section.Rows,
                            sections,
                            sectionMeta,
                            settingsHash,
                            true))
                    && settingsHash.TryGetItem(sectionMeta.ID, out var sectionSettingsBuilder))
                {
                    result =
                        await this.CheckSectionExtendedPermissionsAsync(
                            context,
                            section,
                            sectionMeta,
                            sectionSettingsBuilder.Build()) && result;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Метод для проверки доступа по цепочке родительских секций.
        /// Проверка осуществляется следующим образом:
        /// 1) Собираем список родительский секций.
        /// 2) Для каждой родительской секции из списка (обычно одна) производим проверку строк текущей секции.
        /// 3) Если для данной строки есть родительская строка в состоянии Inserted, то прекращаем проверку строки.
        /// 4) Если редактирование строк родительской секции запрещено, значит ошибка.
        /// 5) Если остались строки, для которых нужна проверка, производим ее уже по родительским секциям родительской секции, без проверки строк.
        /// </summary>
        /// <param name="context">Контекст проверки прав доступа.</param>
        /// <param name="card">Карточка, для которой производится проверка доступа расширенных настроек.</param>
        /// <param name="sectionRows">Строки секции, которые необходимо проверить.</param>
        /// <param name="sections">Коллекция метаданных секций.</param>
        /// <param name="sectionMeta">Метаданные текущей секции.</param>
        /// <param name="settingsHash">Хеш-таблица со всеми настройками правил доступа секций.</param>
        /// <param name="checkParentRows">Определяет, нужно ли производить проверку родительских строк (пункт 3).</param>
        /// <returns>Возвращает true, если проверка прошла успешно, или false, если при проверке произошли ошибки доступа.</returns>
        private async ValueTask<bool> CheckParentSectionsExtendedPermissionsAsync(
            IKrPermissionsManagerContext context,
            Card card,
            IList<CardRow> sectionRows,
            CardMetadataSectionCollection sections,
            CardMetadataSection sectionMeta,
            HashSet<Guid, IKrPermissionSectionSettingsBuilder> settingsHash,
            bool checkParentRows)
        {
            var parentSectionRows =
                sectionMeta.Columns
                    .Where(x => x.ParentRowSection is not null && x.ColumnType == CardMetadataColumnType.Complex)
                    .ToArray();

            if (parentSectionRows.Length == 0)
            {
                return true;
            }

            foreach (var parentSectionRow in parentSectionRows)
            {
                var sectionRowsForCheck = sectionRows.ToList();
                var parentSectionName = parentSectionRow.ParentRowSection.Name;
                card.Sections.TryGetValue(parentSectionName, out var parentSection);

                // Если для родительской секции есть расширенные настройки, то проверяем по ним
                if (settingsHash.TryGetItem(parentSectionRow.ReferencedSection.ID, out var parentSectionSettingsBuilder))
                {
                    var parentSectionSettings = parentSectionSettingsBuilder.Build();
                    for (int i = sectionRowsForCheck.Count - 1; i >= 0; i--)
                    {
                        if (checkParentRows
                            && parentSection is not null)
                        {
                            CardRow row = sectionRowsForCheck[i];
                            var parentRowID = row.TryGet<Guid?>(parentSectionRow.Name + "ID") ?? row.TryGet<Guid?>(parentSectionRow.Name + "RowID");

                            // Строки, родители которых были только добавлены, исключаем из проверки по родительской секции
                            if (parentRowID is not null
                                && parentSection.Rows.TryFirst(x => x.RowID == parentRowID, out CardRow parentRow)
                                && parentRow.State == CardRowState.Inserted)
                            {
                                sectionRowsForCheck.RemoveAt(i);
                                continue;
                            }
                        }

                        if (parentSectionSettings.IsDisallowed)
                        {
                            await this.AddErrorAsync(
                                context,
                                "$KrPermissions_SectionEditAccessErrorTemplate",
                                parentSectionName);

                            return false;
                        }
                    }
                }
                // Иначе проверяем просто по родительской секции факт, что строка была проверена
                else if (checkParentRows
                    && parentSection is not null)
                {
                    for (int i = sectionRowsForCheck.Count - 1; i >= 0; i--)
                    {
                        CardRow row = sectionRowsForCheck[i];
                        var parentRowID = row.TryGet<Guid?>(parentSectionRow.Name + "ID") ?? row.TryGet<Guid?>(parentSectionRow.Name + "RowID");

                        // Строки, родители которых были только добавлены, исключаем из проверки по родительской секции
                        if (parentRowID is not null
                            && parentSection.Rows.TryFirst(x => x.RowID == parentRowID, out CardRow parentRow)
                            && parentRow.State == CardRowState.Inserted)
                        {
                            sectionRowsForCheck.RemoveAt(i);
                        }
                    }
                }

                if (sectionRowsForCheck.Count > 0
                    && sections.TryGetValue(parentSectionName, out var parentSectionMeta))
                {
                    await this.CheckParentSectionsExtendedPermissionsAsync(
                        context,
                        card,
                        sectionRowsForCheck,
                        sections,
                        parentSectionMeta,
                        settingsHash,
                        false);
                }
            }

            return true;
        }

        private async ValueTask<bool> CheckSectionExtendedPermissionsAsync(
            IKrPermissionsManagerContext context,
            CardSection section,
            CardMetadataSection sectionMeta,
            IKrPermissionSectionSettings sectionSettings)
        {
            // Проверка секций идет по следующему алгоритму:
            // Сперва идем по полям секции/строки.
            // Если доступ к полю запрещен - ошибка
            // Если доступ к полю или самой секции разрешен, то считаем, что расчет редактирования для них прошел успешно
            // Если расчет не прошел до конца (в полях или строках еще есть непроверенные значения) и стоит запрет изменения секции, то ошибка

            // Расчет доступа на добавление/удаление строк живет отдельно.

            // Определяет, успешно ли прошла проверка. Если нет, то либо была ошибка, либо есть поля, проверка которых не прошла
            if (section.Type == CardSectionType.Entry)
            {
                var result =
                    await this.CheckEntryExtendedPermissionsAsync(
                        context,
                        section,
                        section.RawFields,
                        sectionMeta,
                        sectionSettings);

                if (!result
                    && sectionSettings.IsDisallowed)
                {
                    await this.AddErrorAsync(
                        context,
                        "$KrPermissions_SectionEditAccessErrorTemplate",
                        section.Name);
                }

                return result;
            }
            else
            {
                bool result = true,
                     modifySucceed = true,
                     addError = false,
                     deleteError = false;
                foreach (var row in section.Rows)
                {
                    switch (row.State)
                    {
                        case CardRowState.Deleted:
                            if (sectionSettings.DisallowRowDeleting)
                            {
                                deleteError = true;
                            }
                            break;

                        case CardRowState.Inserted:
                            if (sectionSettings.DisallowRowAdding)
                            {
                                addError = true;
                            }
                            break;

                        case CardRowState.Modified:
                            modifySucceed =
                                await this.CheckEntryExtendedPermissionsAsync(
                                    context,
                                    section,
                                    row,
                                    sectionMeta,
                                    sectionSettings) && modifySucceed;
                            break;
                    }
                }

                if (addError)
                {
                    await this.AddErrorAsync(
                        context,
                       "$KrPermissions_SectionAddRowAccessErrorTemplate",
                       section.Name);

                    result = false;
                }
                if (deleteError)
                {
                    await this.AddErrorAsync(
                        context,
                        "$KrPermissions_SectionDeleteRowAccessErrorTemplate",
                        section.Name);

                    result = false;
                }
                if (!modifySucceed)
                {
                    if (sectionSettings.IsDisallowed)
                    {
                        await this.AddErrorAsync(
                            context,
                            "$KrPermissions_SectionEditAccessErrorTemplate",
                            section.Name);
                    }

                    result = false;
                }

                return result;
            }
        }

        private async ValueTask<bool> CheckEntryExtendedPermissionsAsync(
            IKrPermissionsManagerContext context,
            CardSection section,
            IDictionary<string, object> rawFields,
            CardMetadataSection sectionMeta,
            IKrPermissionSectionSettings sectionSettings)
        {
            bool result = true;
            bool defaultSectionAccess = sectionSettings.IsAllowed && !sectionSettings.IsDisallowed;
            List<int> checkedComplexColumns = null;
            foreach (var field in rawFields)
            {
                if (sectionMeta.Columns.TryGetValue(field.Key, out var fieldMeta))
                {
                    // Если колонка часть комплексной, то проверяем комплексную колонку
                    if (fieldMeta.ComplexColumnIndex != -1)
                    {
                        if (checkedComplexColumns is null)
                        {
                            checkedComplexColumns = new List<int>();
                        }
                        else if (checkedComplexColumns.Contains(fieldMeta.ComplexColumnIndex))
                        {
                            // Не проверяем одну и ту же комплексную колонку дважды
                            continue;
                        }

                        checkedComplexColumns.Add(fieldMeta.ComplexColumnIndex);
                        fieldMeta = sectionMeta.Columns.First(x =>
                            x.ComplexColumnIndex == fieldMeta.ComplexColumnIndex
                            && x.ColumnType == CardMetadataColumnType.Complex);
                    }

                    if (sectionSettings.DisallowedFields.Contains(fieldMeta.ID))
                    {
                        await this.AddErrorAsync(
                            context,
                           "$KrPermissions_FieldEditAccessErrorTemplate",
                           fieldMeta.Name,
                           section.Name);

                        result = false;
                    }

                    result &= defaultSectionAccess || sectionSettings.AllowedFields.Contains(fieldMeta.ID);
                }
            }

            return result;
        }

        #endregion

        #region Calc Methods

        private async Task TryCalcExtendedMandatoryAsync(InnerContext innerContext)
        {
            var descriptor = innerContext.Context.Descriptor;
            // Если в дескрипторе еще нет рассчитанных прав, которые могли быть рассчитаны ранее, то рассчитываем права
            if (descriptor.ExtendedMandatorySettings.Count == 0)
            {
                await this.CalcExtendedPermissionsAsync(
                    innerContext,
                    this.mandatoryCalcAction,
                    this.mandatoryFilterFunc);
            }
        }

        private async Task TryCalcExtendedPermissionsAsync(
            InnerContext innerContext,
            bool withCard,
            bool withTasks,
            bool withFiles)
        {
            if (!innerContext.ExtendedSettingsCalculated)
            {
                bool withMandatory = withCard || withTasks;

                List<CalcAction> actions = null;
                FilterFunc filterFunc = null;
                if (withCard)
                {
                    actions ??= new List<CalcAction>(4);
                    actions.Add(CalcExtendedPermissionsFromRuleWithEdit);
                    filterFunc = new FilterFunc(s => s.CardSettings.Count > 0);
                }
                if (withMandatory)
                {
                    actions ??= new List<CalcAction>(3);
                    actions.Add(CalcExtendedMandatoryFromRule);
                    var filterFuncLocal = filterFunc;
                    filterFunc = filterFunc is null
                        ? new FilterFunc(s => s.MandatoryRules.Count > 0)
                        : new FilterFunc(s => filterFuncLocal(s) || s.MandatoryRules.Count > 0);
                }
                if (withTasks)
                {
                    actions ??= new List<CalcAction>(3);
                    actions.Add(CalcExtendedPermissionsFromRuleForTasks);
                    var filterFuncLocal = filterFunc;
                    filterFunc = filterFunc is null
                        ? new FilterFunc(s => s.TaskSettingsByTypes.Count > 0)
                        : new FilterFunc(s => filterFuncLocal(s) || s.TaskSettingsByTypes.Count > 0);
                }
                if (withFiles)
                {
                    actions ??= new List<CalcAction>(1);
                    actions.Add(CalcExtendedFileRulesFromRule);
                    var filterFuncLocal = filterFunc;
                    filterFunc = filterFunc is null
                        ? new FilterFunc(s => s.FileRules.Count > 0)
                        : new FilterFunc(s => filterFuncLocal(s) || s.FileRules.Count > 0);
                }
                innerContext.ExtendedSettingsCalculated = true;
                await this.CalcExtendedPermissionsAsync(innerContext, actions.ToArray(), filterFunc);
            }
        }

        private async Task TryCalcExtendedFileRulesAsync(InnerContext innerContext)
        {
            if (!innerContext.ExtendedSettingsCalculated)
            {
                innerContext.ExtendedSettingsCalculated = true;
                await this.CalcExtendedPermissionsAsync(innerContext, this.fileRulesCalcAction, this.fileRulesFilterFunc);
            }
        }

        private async Task CalcExtendedPermissionsAsync(
            InnerContext innerContext,
            CalcAction[] calcActions,
            FilterFunc filterFunc = null)
        {
            if (!await this.FillInnerContextAsync(innerContext))
            {
                return;
            }

            var context = innerContext.Context;
            var allExtendedRules =
                innerContext.PermissionsCache.GetExtendedRules(
                    context.DocTypeID ?? context.CardType.ID,
                    context.DocState);

            if (filterFunc is not null)
            {
                allExtendedRules = allExtendedRules.Where(x => filterFunc.Invoke(x));
            }
            var extendedRules = new HashSet<Guid, IKrPermissionRuleSettings>(x => x.ID, allExtendedRules);

            // Нет смысла проверять список ролей, если правило уже было отклонено
            foreach (var rejectedRuleID in innerContext.RejectedRules)
            {
                extendedRules.RemoveByKey(rejectedRuleID);
            }

            foreach (var submittedRuleID in innerContext.SubmittedRules)
            {
                // Нет смысла проверять список ролей, если правило уже было проверено
                if (extendedRules.TryGetItem(submittedRuleID, out var rule))
                {
                    foreach (var action in calcActions)
                    {
                        action(context, rule);
                    }

                    extendedRules.RemoveByKey(submittedRuleID);
                }
            }

            var nestedContextID = await this.GetNestedContextIDAsync(context);
            var rulesWithStaticRoles = await this.GetRulesForStaticRolesAsync(
                extendedRules,
                nestedContextID,
                context.CancellationToken);
            foreach (var ruleID in rulesWithStaticRoles)
            {
                var rule = extendedRules[ruleID];

                // Правило либо будет успешно проверено, либо нет, поэтому сразу удаляем из списка правил, чтобы не проверять его при проверке контекстных ролей
                extendedRules.RemoveByKey(ruleID);
                if (await this.CheckWithConditionsAsync(innerContext, rule)
                    && await this.CheckWithExtensionsAsync(innerContext, rule.ID))
                {
                    foreach (var action in calcActions)
                    {
                        action(context, rule);
                    }
                }
            }

            if (context.Mode != KrPermissionsCheckMode.WithoutCard)
            {
                foreach (var rule in extendedRules)
                {
                    if (rule.ContextRoles.Count == 0)
                    {
                        continue;
                    }

                    if (await this.CheckWithConditionsAsync(innerContext, rule)
                        && await this.CheckWithExtensionsAsync(innerContext, rule.ID)
                        && await this.CheckUserInContextRolesAsync(innerContext, rule))
                    {
                        foreach (var action in calcActions)
                        {
                            action(context, rule);
                        }
                    }
                }
            }
        }

        private static void CalcExtendedPermissionsFromRuleWithEdit(
           IKrPermissionsManagerContext context,
           IKrPermissionRuleSettings rule)
        {
            CalcExtendedPermissionsFromRule(
                context,
                rule,
                false);
        }

        private static void CalcExtendedPermissionsFromRuleWithoutEdit(
           IKrPermissionsManagerContext context,
           IKrPermissionRuleSettings rule)
        {
            CalcExtendedPermissionsFromRule(
                context,
                rule,
                true);
        }

        private static void CalcExtendedPermissionsFromRule(
            IKrPermissionsManagerContext context,
            IKrPermissionRuleSettings rule,
            bool withoutEdit)
        {
            var descriptor = context.Descriptor;

            // Сперва идет расчет прав на карточку
            foreach (var sectionSettings in rule.CardSettings)
            {
                var sectionSettingsToAdd = sectionSettings.Clone();
                if (withoutEdit
                    && !rule.IsRequired)
                {
                    sectionSettingsToAdd.AllowedFields.Clear();
                    sectionSettingsToAdd.IsAllowed = false;
                }
                if (descriptor.ExtendedCardSettings.TryGetItem(sectionSettingsToAdd.ID, out var existedSectionSettingsBuilder))
                {
                    existedSectionSettingsBuilder.Add(sectionSettingsToAdd, rule.Priority);
                }
                else
                {
                    descriptor.ExtendedCardSettings.Add(
                        new KrPermissionSectionSettingsBuilder(sectionSettingsToAdd.ID)
                            .Add(sectionSettingsToAdd, rule.Priority));
                }
            }
        }

        private static void CalcExtendedPermissionsFromRuleForTasks(
            IKrPermissionsManagerContext context,
            IKrPermissionRuleSettings rule)
        {
            var descriptor = context.Descriptor;

            if (!context.Info.TryGetValue(nameof(CalcExtendedPermissionsFromRule), out var taskTypesObj))
            {
                List<Guid> taskTypesList = null;
                if ((context.Mode == KrPermissionsCheckMode.WithCard
                    || context.Mode == KrPermissionsCheckMode.WithStoreCard)
                    && context.Card.Tasks.Count > 0)
                {
                    taskTypesList = new List<Guid>();
                    foreach (var task in context.Card.Tasks)
                    {
                        if (!taskTypesList.Contains(task.TypeID))
                        {
                            taskTypesList.Add(task.TypeID);
                        }
                    }
                }

                taskTypesObj = taskTypesList;
                context.Info[nameof(CalcExtendedPermissionsFromRule)] = taskTypesObj;
            }

            // Затем расчет прав по заданиям
            if (taskTypesObj is IEnumerable<Guid> taskTypes)
            {
                foreach (var taskType in taskTypes)
                {
                    if (!descriptor.ExtendedTasksSettings.TryGetValue(taskType, out var extendedTaskSettingsResult))
                    {
                        extendedTaskSettingsResult = descriptor.ExtendedTasksSettings[taskType] = new HashSet<Guid, IKrPermissionSectionSettingsBuilder>(x => x.SectionID);
                    }

                    if (rule.TaskSettingsByTypes.TryGetValue(taskType, out var extendedTaskSettings))
                    {
                        foreach (var sectionSettings in extendedTaskSettings)
                        {
                            if (extendedTaskSettingsResult.TryGetItem(sectionSettings.ID, out var existedSectionSettingsBuilder))
                            {
                                existedSectionSettingsBuilder.Add(sectionSettings.Clone(), rule.Priority);
                            }
                            else
                            {
                                extendedTaskSettingsResult.Add(
                                    new KrPermissionSectionSettingsBuilder(sectionSettings.ID)
                                        .Add(sectionSettings.Clone(), rule.Priority));
                            }
                        }
                    }
                }
            }
        }

        private static void CalcVisibilityFromRule(
            IKrPermissionsManagerContext context,
            IKrPermissionRuleSettings rule)
        {
            context.Descriptor.VisibilitySettings.Add(rule.VisibilitySettings, rule.Priority);
        }

        private static void CalcExtendedMandatoryFromRule(
            IKrPermissionsManagerContext context,
            IKrPermissionRuleSettings rule)
        {
            context.Descriptor.ExtendedMandatorySettings.AddRange(rule.MandatoryRules);
        }

        private static void CalcExtendedFileRulesFromRule(
            IKrPermissionsManagerContext context,
            IKrPermissionRuleSettings rule)
        {
            context.Descriptor.FileRules.AddRange(rule.FileRules);
        }

        #endregion

        #region Extend Methods

        /// <summary>
        ///  Метод для расширения рассчитываемых прав доступа по правилам доступа
        /// </summary>
        /// <param name="innerContext">Контекст проверки прав доступа</param>
        /// <returns>Возвращает асинхронную задачу.</returns>
        private async Task ExtendRequiredPermissionsWithRulesAsync(InnerContext innerContext)
        {
            if (!await this.FillInnerContextAsync(innerContext))
            {
                return;
            }

            var requiredRules = new HashSet<Guid, IKrPermissionRuleSettings>(
                x => x.ID,
                innerContext.PermissionsCache.GetRequiredRules(
                    innerContext.Context.DocTypeID ?? innerContext.Context.CardType.ID,
                    innerContext.Context.DocState));

            await this.CheckRulesForStaticRolesAsync(
                innerContext,
                requiredRules,
                true);

            if (!innerContext.Context.ValidationResult.IsSuccessful())
            {
                return;
            }

            await this.CheckRulesForContextRolesAsync(
                 innerContext,
                 requiredRules,
                 true);
        }

        private async Task ExtendPermissionsWithCardAsync(
            IKrPermissionsManagerContext context,
            IExtensionExecutor extensions)
        {
            try
            {
                await extensions.ExecuteAsync(nameof(ICardPermissionsExtension.ExtendPermissionsAsync), context);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                context.ValidationResult.AddException(this, ex);
                return;
            }
        }

        private async Task ExtendPermissionsWithTasksAsync(IKrPermissionsManagerContext context)
        {
            if (context.Mode == KrPermissionsCheckMode.WithoutCard)
            {
                return;
            }

            var tasks = await this.GetTasksAsync(context);

            if (!context.ValidationResult.IsSuccessful()
                || tasks is null
                || tasks.Count == 0)
            {
                return;
            }

            var taskContext = new TaskPermissionsExtensionContext(context);

            try
            {
                var cardTypes = await this.cardMetadata.GetCardTypesAsync(context.CancellationToken);
                foreach (CardTask task in tasks)
                {
                    await using var extensions = await this.extensionContainer.ResolveExecutorAsync<ITaskPermissionsExtension>(context.CancellationToken);
                    taskContext.Task = task;
                    taskContext.TaskType = cardTypes[task.TypeID];
                    await extensions.ExecuteAsync(nameof(ITaskPermissionsExtension.ExtendPermissionsAsync), taskContext);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                context.ValidationResult.AddException(this, ex);
                return;
            }

            return;
        }

        #endregion

        #region Additional Methods

        private async Task<bool> FillInnerContextAsync(InnerContext innerContext)
        {
            var context = innerContext.Context;

            if (innerContext.RuleExecutor is not null)
            {
                return true;
            }

            var extensionExecutor = await this.extensionContainer.ResolveExecutorAsync<IKrPermissionsRuleExtension>(true, context.CancellationToken);
            innerContext.RuleExecutor = extensionExecutor;
            if (context.Mode == KrPermissionsCheckMode.WithoutCard)
            {
                return true;
            }

            var conditionContext =
                new ConditionContext(
                    context.CardID.Value,
                    (ct) => this.GetFullCardAsync(context),
                    context.Mode == KrPermissionsCheckMode.WithStoreCard
                        ? context.Card
                        : null,
                    this.dbScope,
                    this.session,
                    context.ValidationResult,
                    this.unityContainer)
                {
                    CancellationToken = context.CancellationToken,
                };

            conditionContext.Info[nameof(IKrPermissionsManagerContext)] = innerContext.Context;

            innerContext.ConditionContext = conditionContext;

            return true;
        }

        private async Task<Card> GetCardForMandatoryCheckAsync(IKrPermissionsManagerContext context)
        {
            var fullCard = (await this.GetFullCardAsync(context));

            if (context.Mode == KrPermissionsCheckMode.WithStoreCard)
            {
                fullCard = fullCard.Clone();
                var storeCard = context.Card;
                foreach (var section in storeCard.Sections)
                {
                    if (fullCard.Sections.TryGetValue(section.Key, out var fullSection))
                    {
                        if (section.Value.Type == CardSectionType.Entry)
                        {
                            StorageHelper.Merge(section.Value.GetStorage(), fullSection.GetStorage());
                        }
                        else
                        {
                            foreach (var row in section.Value.Rows)
                            {
                                if (row.State == CardRowState.Inserted)
                                {
                                    fullSection.Rows.AddValue(row);
                                }
                                else if (row.State == CardRowState.Deleted
                                    && fullSection.Rows.TryFirst(x => x.RowID == row.RowID, out var fullRow))
                                {
                                    fullSection.Rows.Remove(fullRow);
                                }
                                else if (row.State == CardRowState.Modified
                                    && fullSection.Rows.TryFirst(x => x.RowID == row.RowID, out fullRow))
                                {
                                    StorageHelper.Merge(row.GetStorage(), fullRow.GetStorage());
                                }
                            }
                        }
                    }
                }

                fullCard.Tasks.Clear();
                fullCard.Tasks.AddItems(storeCard.Tasks);
            }

            return fullCard;
        }

        private async ValueTask<Card> GetCardForExtendedCheckAsync(IKrPermissionsManagerContext context)
        {
            var checkCard = context.Card;
            if (checkCard.StoreMode == CardStoreMode.Insert
                && context.PreviousToken is not null
                && context.PreviousToken.Info.TryGet<object>(KrPermissionsHelper.NewCardSourceKey) is Dictionary<string, object> cardSource)
            {
                checkCard = checkCard.Clone();
                foreach (var section in checkCard.Sections.Values)
                {
                    if (section.Type == CardSectionType.Entry)
                    {
                        var entrySource = cardSource.TryGet<Dictionary<string, object>>(section.Name);

                        foreach (var field in section.RawFields.ToArray())
                        {
                            var fieldSource = entrySource?.TryGet<object>(field.Key);
                            if ((fieldSource is null && field.Value is null)
                                || (fieldSource?.Equals(field.Value) ?? false))
                            {
                                section.RawFields.Remove(field.Key);
                            }
                        }

                        if (section.RawFields.Count == 0)
                        {
                            checkCard.Sections.Remove(section.Name);
                        }
                    }
                    else if (section.Type == CardSectionType.Table)
                    {
                        var rowSources = cardSource
                            .TryGet<IList>(section.Name)
                            ?.Cast<Dictionary<string, object>>().ToDictionary(x => x.TryGet<Guid>("RowID"), x => x);

                        var systemKeys = CardRow.GetPlatformKeys(section.TableType);
                        foreach (var row in section.Rows.ToArray())
                        {
                            Dictionary<string, object> rowSource = null;
                            rowSources?.TryGetValue(row.RowID, out rowSource);
                            if (row is not null)
                            {
                                int systemCount = 0;
                                foreach (var field in row.ToArray())
                                {
                                    // Не удаляем и не проверяем системные ключи
                                    if (systemKeys.Contains(field.Key)
                                        && field.Key != CardRow.ParentRowIDKey) // Кроме ParentRowID
                                    {
                                        systemCount++;
                                        continue;
                                    }

                                    var fieldSource = rowSource?.TryGet<object>(field.Key);
                                    if ((fieldSource is null && field.Value is null)
                                        || (fieldSource?.Equals(field.Value) ?? false))
                                    {
                                        row.Remove(field.Key);
                                    }
                                }
                                if (row.Count == systemCount) // Если остались только системные ключи, удаляем строку из проверки
                                {
                                    section.Rows.Remove(row);
                                }
                            }
                        }
                        if (section.Rows.Count == 0)
                        {
                            checkCard.Sections.Remove(section.Name);
                        }
                    }
                }

                if (cardSource.TryGetValue("Files", out var filesSourceObj)
                    && filesSourceObj is IList filesSource)
                {
                    bool filesChecked = true;
                    Dictionary<Guid, Dictionary<string, object>> filesSourceDict
                        = filesSource.Cast<Dictionary<string, object>>().ToDictionary(
                            x => x.Get<Guid>("RowID"),
                            x => x.Get<Dictionary<string, object>>("ExternalSource"));

                    foreach (var file in checkCard.Files)
                    {
                        if (filesSourceDict.TryGetValue(file.RowID, out var sourceFileExternalSource))
                        {
                            // Проверка, что внешний источник данных файла соответствует информации, используемой при создании карточки
                            var fileExternalSource = file.ExternalSource?.GetStorage();
                            if (fileExternalSource is null
                                && sourceFileExternalSource is not null)
                            {
                                filesChecked = false;
                            }
                            else if (fileExternalSource is not null
                                && (sourceFileExternalSource?.Any(x =>
                                {
                                    var value = fileExternalSource.TryGet<object>(x.Key);
                                    return !((value is null && x.Value is null) || (value?.Equals(x.Value) ?? false));
                                }) ?? true))
                            {
                                // Ошибка, которая возникает только при подделке внешнего источника файлов
                                await this.AddErrorAsync(
                                    context,
                                    "Unable to create file with unchecked external source");
                            }
                        }
                        else
                        {
                            filesChecked = false;
                        }
                    }

                    if (filesChecked)
                    {
                        context.Descriptor.Set(KrPermissionFlagDescriptors.AddFiles, true);
                    }
                }
            }

            return checkCard;
        }

        private async Task<Card> GetCardForTokenAsync(IKrPermissionsManagerContext context)
        {
            switch (context.Mode)
            {
                case KrPermissionsCheckMode.WithStoreCard:
                case KrPermissionsCheckMode.WithCard:
                    return context.Card;

                case KrPermissionsCheckMode.WithCardID:
                    await using (this.dbScope.Create())
                    {
                        var getContext = await this.cardGetStrategy.TryLoadCardInstanceAsync(
                            context.CardID.Value,
                            this.dbScope.Db,
                            this.cardMetadata,
                            context.ValidationResult,
                            cancellationToken: context.CancellationToken);

                        return getContext.Card;
                    }
                case KrPermissionsCheckMode.WithoutCard:
                    return new Card() { ID = context.CardType.ID, TypeID = context.CardType.ID };

                default:
                    return new Card();
            }
        }

        private async ValueTask<Card> GetFullCardAsync(IKrPermissionsManagerContext context)
        {
            if (context.Mode == KrPermissionsCheckMode.WithCard)
            {
                return context.Card;
            }
            else if (context.Info.TryGetValue("FullCard", out var cardObj)
                && cardObj is Card card)
            {
                return card;
            }
            else
            {
                var cardRequest = new CardGetRequest() { CardID = context.CardID };
                this.permissionsProvider.SetFullPermissions(cardRequest);

                var response = await this.cardRepository.GetAsync(cardRequest, context.CancellationToken);
                var result = response.ValidationResult.Build();
                context.ValidationResult.Add(result);

                var fullCard = result.IsSuccessful ? response.Card : null;
                context.Info["FullCard"] = fullCard;
                return fullCard;
            }
        }

        private async Task<ICollection<CardTask>> GetTasksAsync(IKrPermissionsManagerContext context)
        {
            if (context.Card is not null)
            {
                return context.Card.TryGetTasks()?.ToArray();
            }

            await using (this.dbScope.Create())
            {
                var card = new Card();
                var taskContexts = await this.cardGetStrategy.TryLoadTaskInstancesAsync(
                    context.CardID.Value,
                    card,
                    this.dbScope.Db,
                    this.cardMetadata,
                    context.ValidationResult,
                    this.session,
                    CardNewMode.Default,
                    CardGetTaskMode.Default,
                    cancellationToken: context.CancellationToken);

                if (taskContexts is not null
                    && taskContexts.Count > 0)
                {
                    foreach (var taskContext in taskContexts)
                    {
                        await this.cardGetStrategy.LoadSectionsAsync(taskContext, context.CancellationToken);
                    }

                    return card.Tasks.ToArray();
                }
            }

            return Array.Empty<CardTask>();
        }

        private async Task<IEnumerable<Guid>> GetRulesForStaticRolesAsync(
            HashSet<Guid, IKrPermissionRuleSettings> rules,
            Guid? nestedContextID,
            CancellationToken cancellationToken = default)
        {
            var count = rules.Count;
            if (count == 0)
            {
                return Enumerable.Empty<Guid>();
            }

            List<Guid> result = new List<Guid>();
            var userID = this.session.User.ID;
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                // Для PostgreSQL увеличиваем лимит только в 10 раз чтобы не висеть слишком долго на субд
                int step = db.GetDbms() == Dbms.PostgreSql ? 10000 : 1000;
                if (count <= step)
                {
                    var builder = builderFactory
                        .SelectDistinct().C("pr", "ID")
                        .From("KrPermissionRoles", "pr").NoLock()
                        .InnerJoin("RoleUsers", "ru").NoLock()
                            .On().C("ru", "ID").Equals().C("pr", "RoleID")
                        .Where().C("ru", "UserID").Equals().P("CurrentUserID")
                            .And().C("pr", "ID")
                            .InArray(rules.Select(x => x.ID), "RuleIDs", out var ruleIDs);

                    result = await db
                        .SetCommand(
                            builder.Build(),
                            DataParameters.Get(
                                db.Parameter("CurrentUserID", userID),
                                ruleIDs))
                        .LogCommand()
                        .ExecuteListAsync<Guid>(cancellationToken);
                }
                else
                {
                    int stepNum = 0;

                    while (stepNum * step < count)
                    {
                        var builder = builderFactory
                            .SelectDistinct().C("pr", "ID")
                            .From("KrPermissionRoles", "pr").NoLock()
                            .InnerJoin("RoleUsers", "ru").NoLock()
                            .On().C("ru", "ID").Equals().C("pr", "RoleID")
                            .Where().C("ru", "UserID").Equals().P("CurrentUserID")
                            .And().C("pr", "ID")
                                .InArray(rules.Skip(stepNum * step).Take(step).Select(x => x.ID), "RuleIDs", out var ruleIDs);

                        result.AddRange(await db
                            .SetCommand(
                                builder.Build(),
                                DataParameters.Get(
                                    db.Parameter("CurrentUserID", userID),
                                    ruleIDs))
                            .LogCommand()
                            .ExecuteListAsync<Guid>(cancellationToken));

                        stepNum++;
                    }
                }

                if (nestedContextID.HasValue
                    && result.Count < count)
                {
                    var checkNestedRules = new HashSet<Guid>(rules.Select(x => x.ID));
                    foreach (var rule in result)
                    {
                        checkNestedRules.Remove(rule);
                    }

                    if (checkNestedRules.Count <= step)
                    {
                        var builder = builderFactory
                            .SelectDistinct().C("pr", "ID")
                            .From("KrPermissionRoles", "pr").NoLock()
                            .InnerJoin("NestedRoles", "nr").NoLock()
                                .On().C("pr", "RoleID").Equals().C("nr", "ParentID")
                            .InnerJoin("RoleUsers", "ru").NoLock()
                                .On().C("ru", "ID").Equals().C("nr", "ID")
                            .Where().C("nr", "ContextID").Equals().P("ContextID")
                                .And().C("ru", "UserID").Equals().P("CurrentUserID")
                                .And().C("pr", "ID")
                                .InArray(rules.Select(x => x.ID), "NestedRuleIDs", out var nestedRuleIDs);

                        result.AddRange(await db
                            .SetCommand(
                                builder.Build(),
                                DataParameters.Get(
                                    db.Parameter("ContextID", nestedContextID),
                                    db.Parameter("CurrentUserID", userID),
                                    nestedRuleIDs))
                            .LogCommand()
                            .ExecuteListAsync<Guid>(cancellationToken));
                    }
                    else
                    {
                        int stepNum = 0;

                        while (stepNum * step < count)
                        {
                            var builder = builderFactory
                                .SelectDistinct().C("pr", "ID")
                                .From("KrPermissionRoles", "pr").NoLock()
                                .InnerJoin("NestedRoles", "nr").NoLock()
                                    .On().C("pr", "RoleID").Equals().C("nr", "ParentID")
                                .InnerJoin("RoleUsers", "ru").NoLock()
                                    .On().C("ru", "ID").Equals().C("pr", "RoleID")
                                .Where().C("nr", "ContextID").Equals().P("ContextID")
                                    .And().C("ru", "UserID").Equals().P("CurrentUserID")
                                    .And().C("pr", "ID")
                                    .InArray(checkNestedRules.Skip(stepNum * step).Take(step), "NestedRuleIDs", out var nestedRuleIDs);

                            result.AddRange(await db
                                .SetCommand(
                                    builder.Build(),
                                    DataParameters.Get(
                                        db.Parameter("ContextID", nestedContextID),
                                        db.Parameter("CurrentUserID", userID),
                                        nestedRuleIDs))
                                .LogCommand()
                                .ExecuteListAsync<Guid>(cancellationToken));

                            stepNum++;
                        }
                    }
                }

                return result;
            }
        }

        private async Task<IEnumerable<Guid>> GetRulesForAclGenerationRulesAsync(
            HashSet<Guid, IKrPermissionRuleSettings> rules,
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            var count = rules.Count;
            if (count == 0)
            {
                return Enumerable.Empty<Guid>();
            }

            List<Guid> result = new List<Guid>();
            var userID = this.session.User.ID;
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                // Для PostgreSQL увеличиваем лимит только в 10 раз чтобы не висеть слишком долго на субд
                int step = db.GetDbms() == Dbms.PostgreSql ? 10000 : 1000;
                if (count <= step)
                {
                    var builder = builderFactory
                        .SelectDistinct().C("pr", "ID")
                        .From("KrPermissionAclGenerationRules", "pr").NoLock()
                        .InnerJoin("Acl", "a").NoLock()
                            .On().C("pr", "RuleID").Equals().C("a", "RuleID")
                        .InnerJoin("RoleUsers", "ru").NoLock()
                            .On().C("ru", "ID").Equals().C("a", "RoleID")
                        .Where().C("ru", "UserID").Equals().P("CurrentUserID")
                            .And().C("a", "ID").Equals().P("CardID")
                            .And().C("pr", "ID").InArray(rules.Select(x => x.ID), "RuleIDs", out var ruleIDs);

                    result = await db
                        .SetCommand(
                            builder.Build(),
                            DataParameters.Get(
                                db.Parameter("CurrentUserID", userID, LinqToDB.DataType.Guid),
                                db.Parameter("CardID", cardID, LinqToDB.DataType.Guid),
                                ruleIDs))
                        .LogCommand()
                        .ExecuteListAsync<Guid>(cancellationToken);
                }
                else
                {
                    int stepNum = 0;

                    while (stepNum * step < count)
                    {
                        var builder = builderFactory
                            .SelectDistinct().C("pr", "ID")
                            .From("KrPermissionAclGenerationRules", "pr").NoLock()
                            .InnerJoin("Acl", "a").NoLock()
                                .On().C("pr", "RuleID").Equals().C("a", "RuleID")
                            .InnerJoin("RoleUsers", "ru").NoLock()
                                .On().C("ru", "ID").Equals().C("a", "RoleID")
                            .Where().C("ru", "UserID").Equals().P("CurrentUserID")
                                .And().C("pr", "ID")
                                .InArray(rules.Skip(stepNum * step).Take(step).Select(x => x.ID), "RuleIDs", out var ruleIDs);

                        result.AddRange(await db
                            .SetCommand(
                                builder.Build(),
                                DataParameters.Get(
                                    db.Parameter("CurrentUserID", userID),
                                    ruleIDs))
                            .LogCommand()
                            .ExecuteListAsync<Guid>(cancellationToken));

                        stepNum++;
                    }
                }

                return result;
            }
        }

        private async Task<IKrPermissionsManagerResult> CreateResultAsync(InnerContext innerContext)
        {
            var descriptor = innerContext.Context.Descriptor;
            if (innerContext.Context.WithExtendedPermissions)
            {
                bool isEditMode = descriptor.Permissions.Contains(KrPermissionFlagDescriptors.EditCard)
                    || descriptor.StillRequired.Contains(KrPermissionFlagDescriptors.EditCard);
                await this.CalcExtendedPermissionsAsync(innerContext, isEditMode ? this.allCalcActionsWithEdit : this.allCalcActionsWithoutEdit);

                var cardType = innerContext.Context.CardType;
                foreach (var settings in descriptor.ExtendedCardSettings.Select(x => KrPermissionSectionSettings.ConvertFrom(x.Build())).ToArray())
                {
                    if (!settings.CheckAndClean(cardType))
                    {
                        descriptor.ExtendedCardSettings.Remove(settings);
                    }
                }
            }

            return new KrPermissionsManagerResult(
                this.session,
                this.krPermissionsFilesManager,
                descriptor,
                innerContext.Context.WithExtendedPermissions,
                innerContext.PermissionsCache.Version);
        }

        #endregion

        #region Error Methods

        private ValueTask AddErrorAsync(
            IKrPermissionsManagerContext context,
            string errorText,
            params object[] args)
        {
            return context.AddErrorAsync(this, errorText, context.CancellationToken, args);
        }

        #endregion

        #endregion
    }
}
