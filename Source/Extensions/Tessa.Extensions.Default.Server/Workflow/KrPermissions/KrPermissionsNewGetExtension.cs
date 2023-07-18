using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение на создание и получение карточки, которое рассчитывает доступ к карточке.
    /// </summary>
    public sealed class KrPermissionsNewGetExtension : CardNewGetExtension
    {
        #region Constants And Static Fields

        private const string DocTypeIDKey = CardHelper.SystemKeyPrefix + nameof(KrPermissionsNewGetExtension) + "DocTypeID";

        #endregion

        #region Constructors

        public KrPermissionsNewGetExtension(
            ICardCache cache,
            IKrTokenProvider krTokenProvider,
            IKrPermissionsManager permissionsManager,
            ICardMetadata cardMetadata,
            IKrTypesCache typesCache,
            IKrScope krScope)
        {
            Check.ArgumentNotNull(cache, nameof(cache));
            Check.ArgumentNotNull(krTokenProvider, nameof(krTokenProvider));
            Check.ArgumentNotNull(permissionsManager, nameof(permissionsManager));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(typesCache, nameof(typesCache));
            Check.ArgumentNotNull(krScope, nameof(krScope));

            this.cache = cache;
            this.krTokenProvider = krTokenProvider;
            this.permissionsManager = permissionsManager;
            this.cardMetadata = cardMetadata;
            this.typesCache = typesCache;
            this.krScope = krScope;
        }

        #endregion

        #region Fields

        private readonly ICardCache cache;

        private readonly IKrTokenProvider krTokenProvider;

        private readonly IKrPermissionsManager permissionsManager;

        private readonly ICardMetadata cardMetadata;

        private readonly IKrTypesCache typesCache;

        private readonly IKrScope krScope;

        #endregion

        #region Private Methods

        /// <summary>
        /// Устанавливает на секции и строки карточки разрешения в зависимости от полученных разрешений.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="card">Карточка.</param>
        /// <param name="permissionsResult">Результат расчета прав доступа.</param>
        /// <param name="fileSettings">Хеш настроек доступа к файлам.</param>
        /// <param name="filesSettings">Настройки доступа файлов пользователя.</param>
        /// <param name="cancelationToken">Объект, посредством которого можно отменить задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SetCardPermissionsAsync(
            Guid userID,
            Card card,
            IKrPermissionsManagerResult permissionsResult,
            HashSet<Guid, KrPermissionsFileSettings> fileSettings,
            KrPermissionsFilesSettings filesSettings,
            CancellationToken cancelationToken = default)
        {
            var permissions = card.Permissions;
            var krPermissions = permissionsResult.Permissions;

            if (permissionsResult.WithExtendedSettings)
            {
                await this.SetCardExtendedPermissionsAsync(card, permissionsResult.ExtendedCardSettings, cancelationToken);
            }
            foreach (var task in card.Tasks)
            {
                if (permissionsResult.ExtendedTasksSettings.TryGetValue(task.TypeID, out var extendedTaskSettings))
                {
                    await this.SetCardExtendedPermissionsAsync(task.Card, extendedTaskSettings, cancelationToken);
                }
            }

            //Права на редактирование карточки
            permissions.SetCardPermissions(
                krPermissions.Contains(KrPermissionFlagDescriptors.EditCard)
                    ? CardPermissionFlags.AllowModify
                    | CardPermissionFlags.AllowDeleteRow
                    | CardPermissionFlags.AllowInsertRow
                    : CardPermissionFlags.ProhibitModify
                    | CardPermissionFlags.ProhibitDeleteRow
                    | CardPermissionFlags.ProhibitInsertRow);

            // Права на подписание файлов
            permissions.SetCardPermissions(
                krPermissions.Contains(KrPermissionFlagDescriptors.SignFiles)
                    ? CardPermissionFlags.AllowSignFile
                    : CardPermissionFlags.ProhibitSignFile);

            //Право на редактирование (и удаление) файлов
            for (var i = card.Files.Count - 1; i >= 0; i--)
            {
                var file = card.Files[i];
                var isOwnFile = file.Card.CreatedByID == userID;
                KrPermissionsFileSettings settings = null;
                if (fileSettings is not null && fileSettings.TryGetItem(file.RowID, out settings))
                {
                    switch (settings.ReadAccessSetting)
                    {
                        case KrPermissionsHelper.FileReadAccessSettings.FileNotAvailable:
                            card.Files.RemoveAt(i);
                            permissions.FilePermissions.Remove(file.RowID);
                            continue;

                        case KrPermissionsHelper.FileReadAccessSettings.ContentNotAvailable:
                        case KrPermissionsHelper.FileReadAccessSettings.OnlyLastVersion:
                            file.VersionsLoaded = true;
                            break;
                    }
                }

                //Пользователь может редактировать файлы, которые он добавил, если они не виртуальные
                if (file.IsVirtual)
                {
                    permissions.SetFilePermissions(file.RowID, CardPermissionFlagValues.ProhibitAllFile, overwrite: true);
                }
                else
                {
                    var canEdit = settings?.EditAccessSetting is null
                        ? // есть разрешение на редактирование файлов
                            krPermissions.Contains(KrPermissionFlagDescriptors.EditFiles)
                            // или есть разрешение на редактирование своих файлов и файл его
                            || krPermissions.Contains(KrPermissionFlagDescriptors.EditOwnFiles) && isOwnFile
                        : settings.EditAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Allowed;

                    var canDelete = settings?.DeleteAccessSetting is null
                        ? // есть разрешение на редактирование файлов
                            krPermissions.Contains(KrPermissionFlagDescriptors.DeleteFiles)
                            // или есть разрешение на редактирование своих файлов и файл его
                            || krPermissions.Contains(KrPermissionFlagDescriptors.DeleteOwnFiles) && isOwnFile
                        : settings.DeleteAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Allowed;

                    var canSign = settings?.SignAccessSetting is null
                            ? krPermissions.Contains(KrPermissionFlagDescriptors.SignFiles)
                            : settings?.SignAccessSetting == KrPermissionsHelper.FileEditAccessSettings.Allowed;

                    permissions.SetFilePermissions(
                        file.RowID,
                        (canEdit
                            ? CardPermissionFlags.AllowModify | CardPermissionFlags.AllowReplaceFile
                            : CardPermissionFlags.ProhibitModify | CardPermissionFlags.ProhibitReplaceFile)
                        |
                        // право на удаление файлов
                        (canDelete
                            ? CardPermissionFlags.AllowDeleteFile
                            : CardPermissionFlags.ProhibitDeleteFile)
                        |
                        // право на подписание файлов
                        (canSign
                            ? CardPermissionFlags.AllowSignFile
                            : CardPermissionFlags.ProhibitSignFile),
                        overwrite: true);
                }
            }

            // Добавление файлов в карточку.
            permissions.SetCardPermissions(
                krPermissions.Contains(KrPermissionFlagDescriptors.AddFiles)
                || filesSettings?.GlobalSettings?.AddAllowed == true
                || filesSettings?.GlobalSettings?.AllowedCategories is { Count: > 0 } == true
                || filesSettings?.TryGetExtensionSettings()?.Values.Any(x => x.AddAllowed || x.AllowedCategories is { Count: > 0 }) == true
                    ? CardPermissionFlags.AllowInsertFile
                    : CardPermissionFlags.ProhibitInsertFile);

            // Управление номерами в карточке.
            permissions.SetCardPermissions(
                krPermissions.Contains(KrPermissionFlagDescriptors.EditNumber)
                    ? CardPermissionFlags.AllowEditNumber
                    : CardPermissionFlags.ProhibitEditNumber);

            // Редактирование маршрута согласования.

            // Редактирование функциональных ролей заданий
            if (krPermissions.Contains(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles))
            {
                foreach (var task in card.Tasks)
                {
                    task.Flags |= CardTaskFlags.CanModifyTaskAssignedRoles;
                }
            }

            if (krPermissions.Contains(KrPermissionFlagDescriptors.ModifyOwnTaskAssignedRoles))
            {
                foreach (var task in card.Tasks)
                {
                    if (task.TaskSessionRoles.Count > 0)
                    {
                        task.Flags |= CardTaskFlags.CanModifyTaskAssignedRoles;
                    }
                }
            }

            //Редактирование этапов
            var stagesSection =
                permissions.Sections.GetOrAdd(KrConstants.KrStages.Virtual);
            stagesSection.Type = CardSectionType.Table;

            //И согласующих
            var approversSection =
                permissions.Sections.GetOrAdd(KrConstants.KrPerformersVirtual.Synthetic);
            approversSection.Type = CardSectionType.Table;

            if (krPermissions.Contains(KrPermissionFlagDescriptors.EditRoute))
            {
                //Если можно редактировать маршрут - позволяем изменять / добавлять / удалять этапы и согласантов
                stagesSection.SetSectionPermissions(
                    CardPermissionFlags.AllowModify
                    | CardPermissionFlags.AllowInsertRow
                    | CardPermissionFlags.AllowDeleteRow,
                    overwrite: true);

                approversSection.SetSectionPermissions(
                    CardPermissionFlags.AllowModify
                    | CardPermissionFlags.AllowInsertRow
                    | CardPermissionFlags.AllowDeleteRow,
                    overwrite: true);
            }
            else
            {
                stagesSection.SetSectionPermissions(
                    CardPermissionFlags.ProhibitModify
                    | CardPermissionFlags.ProhibitInsertRow
                    | CardPermissionFlags.ProhibitDeleteRow,
                    overwrite: true);

                approversSection.SetSectionPermissions(
                    CardPermissionFlags.ProhibitModify
                    | CardPermissionFlags.ProhibitInsertRow
                    | CardPermissionFlags.ProhibitDeleteRow,
                    overwrite: true);
            }

            var stagesRows = stagesSection.Rows;
            var approverRows = approversSection.Rows;
            if (card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var stagesDataSection))
            {
                foreach (var stage in stagesDataSection.Rows)
                {
                    var isInactiveStage = stage.Get<int>(KrConstants.KrStages.StateID) == KrStageState.Inactive.ID;
                    var canEditStage = krPermissions.Contains(KrPermissionFlagDescriptors.EditRoute)
                        && isInactiveStage;
                    //Если нет прав или этап активен или завершен - редактирование запрещено
                    stagesRows
                        .GetOrAdd(stage.RowID)
                        .SetRowPermissions(canEditStage
                                ? CardPermissionFlagValues.AllowAllRow
                                : CardPermissionFlagValues.ProhibitAllRow,
                            overwrite: true);

                    foreach (var approver in card.Sections[KrConstants.KrPerformersVirtual.Synthetic].Rows)
                    {
                        if (approver.Fields.Get<Guid>(KrConstants.KrPerformersVirtual.StageRowID) != stage.RowID)
                        {
                            continue;
                        }

                        approverRows
                            .GetOrAdd(approver.RowID)
                            .SetRowPermissions(canEditStage
                                    ? CardPermissionFlagValues.AllowAllRow
                                    : CardPermissionFlagValues.ProhibitAllRow,
                                overwrite: true);
                    }

                    if (isInactiveStage
                        && KrProcessSharedHelper.CanBeSkipped(stage))
                    {
                        CardPermissionFlags flags;
                        if (krPermissions.Contains(KrPermissionFlagDescriptors.CanSkipStages))
                        {
                            flags = stage.TryGet<bool>(KrConstants.KrStages.Skip)
                                ? CardPermissionFlags.ProhibitDeleteRow
                                : CardPermissionFlags.AllowDeleteRow;
                        }
                        else
                        {
                            flags = CardPermissionFlags.ProhibitDeleteRow;
                        }

                        stagesRows
                            .GetOrAdd(stage.RowID)
                            .SetRowPermissions(flags);
                    }
                }
            }

            //если сателлит еще не создан - состояние = драфт
            var state = card.Sections.ContainsKey(KrConstants.KrApprovalCommonInfo.Virtual)
                && card.Sections[KrConstants.KrApprovalCommonInfo.Virtual].Fields.ContainsKey(KrConstants.KrApprovalCommonInfo.StateID)
                && card.Sections[KrConstants.KrApprovalCommonInfo.Virtual].Fields.Get<object>(KrConstants.KrApprovalCommonInfo.StateID) is not null
                    ? (KrState) card.Sections[KrConstants.KrApprovalCommonInfo.Virtual].Fields.Get<int>(KrConstants.KrApprovalCommonInfo.StateID)
                    : KrState.Draft;

            approversSection.SetSectionPermissions(
                krPermissions.Contains(KrPermissionFlagDescriptors.EditRoute)
                && state != KrState.Approved
                    ? CardPermissionFlags.AllowInsertRow
                    : CardPermissionFlags.ProhibitInsertRow);

            // Запуск процесса согласования проверяется непосредственно при попытке запуска процесса
            // Отзыв, возврат, отмена процесса согласования проверяется непосредственно при попытке

            //Даем право на удаление чтобы не скрывался тайл
            permissions.SetCardPermissions(CardPermissionFlags.AllowDeleteCard);
        }

        /// <summary>
        /// Устанавливает расширенные настройки прав доступа на карточку.
        /// </summary>
        /// <param name="card">Карточка.</param>
        /// <param name="extendedCardSettings">Расширенные настройки прав доступа.</param>
        /// <param name="cancelationToken">Объект, посредством которого можно отменить задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SetCardExtendedPermissionsAsync(
            Card card,
            HashSet<Guid, IKrPermissionSectionSettings> extendedCardSettings,
            CancellationToken cancellationToken = default)
        {
            var permissions = card.Permissions;
            var cardTypeMeta = await this.cardMetadata.GetMetadataForTypeAsync(card.TypeID, cancellationToken);
            var cardTypeMetaSections = await cardTypeMeta.GetSectionsAsync(cancellationToken);
            foreach (var sectionSettings in extendedCardSettings)
            {
                this.SetSectionPermission(card, permissions, cardTypeMetaSections, sectionSettings);
            }
        }

        /// <summary>
        /// Устанавливает права на секцию.
        /// </summary>
        /// <param name="card">Карточка.</param>
        /// <param name="permissions">Права на карточку.</param>
        /// <param name="cardMetadataSections">Метаданные секций.</param>
        /// <param name="sectionSettings">Расширенные настройки секции.</param>
        private void SetSectionPermission(
            Card card,
            CardPermissionInfo permissions,
            CardMetadataSectionCollection cardMetadataSections,
            IKrPermissionSectionSettings sectionSettings)
        {
            if (cardMetadataSections.TryGetValue(sectionSettings.ID, out var sectionMeta)
                && card.Sections.TryGetValue(sectionMeta.Name, out var section))
            {
                var sectionPermissions = permissions.Sections.GetOrAdd(sectionMeta.Name);
                sectionPermissions.Type = sectionMeta.SectionType;
                var isTable = sectionMeta.SectionType == CardSectionType.Table;

                // Запрет редактирования всей секции выше всего, остальное можно игнорировать сразу же
                if (sectionSettings.IsDisallowed)
                {
                    if (isTable)
                    {
                        foreach (var row in section.Rows)
                        {
                            var rowPermissions = sectionPermissions.Rows.GetOrAdd(row.RowID);
                            rowPermissions.SetRowPermissions(CardPermissionFlags.ProhibitModify);
                        }
                        this.SetChildSectionsDisalowed(
                            card,
                            permissions,
                            sectionMeta,
                            cardMetadataSections);
                    }
                    else
                    {
                        sectionPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitModify);
                    }
                }
                else if (sectionSettings.IsAllowed)
                {
                    sectionPermissions.SetSectionPermissions(CardPermissionFlags.AllowModify);
                }

                if (isTable)
                {
                    if (sectionSettings.IsAllowed)
                    {
                        sectionPermissions.SetSectionPermissions(CardPermissionFlags.AllowInsertRow | CardPermissionFlags.AllowDeleteRow);
                    }
                    if (sectionSettings.DisallowRowAdding)
                    {
                        // Запрет на добавление строк также расширяет запрет на редактирование строк
                        sectionPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitInsertRow);
                        if (sectionSettings.IsDisallowed)
                        {
                            sectionPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitModify);
                        }
                    }
                    if (sectionSettings.DisallowRowDeleting)
                    {
                        sectionPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitDeleteRow);
                    }
                }

                foreach (var field in sectionSettings.AllowedFields)
                {
                    SetFieldPermission(sectionMeta, sectionPermissions, field, CardPermissionFlags.AllowModify);
                }
                foreach (var field in sectionSettings.DisallowedFields)
                {
                    SetFieldPermission(sectionMeta, sectionPermissions, field, CardPermissionFlags.ProhibitModify);
                }
            }
        }

        /// <summary>
        /// Метод для установки запрета на редактирование дочерних секций.
        /// </summary>
        /// <param name="card">Карточка.</param>
        /// <param name="permissions">Права на карточку.</param>
        /// <param name="sectionMeta">Метаданные родительской секции.</param>
        /// <param name="cardMetadataSections">Метаданные секций.</param>
        private void SetChildSectionsDisalowed(
            Card card,
            CardPermissionInfo permissions,
            CardMetadataSection sectionMeta,
            CardMetadataSectionCollection cardMetadataSections)
        {
            foreach (var posibleChildSectionMeta in cardMetadataSections)
            {
                if (posibleChildSectionMeta.SectionType == CardSectionType.Table
                    && posibleChildSectionMeta.Columns.Any(x => x.ColumnType == CardMetadataColumnType.Complex && x.ReferencedSection?.ID == sectionMeta.ID))
                {
                    var sectionPermissions = permissions.Sections.GetOrAdd(posibleChildSectionMeta.Name);
                    sectionPermissions.Type = CardSectionType.Table;
                    if (card.Sections.TryGetValue(posibleChildSectionMeta.Name, out var section))
                    {
                        foreach (var row in section.Rows)
                        {
                            var rowPermission = sectionPermissions.Rows.GetOrAdd(row.RowID);
                            rowPermission.SetRowPermissions(CardPermissionFlags.ProhibitModify);
                        }
                    }

                    this.SetChildSectionsDisalowed(
                        card,
                        permissions,
                        posibleChildSectionMeta,
                        cardMetadataSections);
                }
            }
        }

        /// <summary>
        /// Устанавливает права на поле секции с учетом комплексных полей.
        /// </summary>
        /// <param name="sectionMeta">Метаданные секции.</param>
        /// <param name="sectionPermissions">Права секции.</param>
        /// <param name="field">Идентификатор поля.</param>
        /// <param name="permissionFlags">Устанавливаемые права доступа.</param>
        private static void SetFieldPermission(
            CardMetadataSection sectionMeta,
            CardSectionPermissionInfo sectionPermissions,
            Guid field,
            CardPermissionFlags permissionFlags)
        {
            if (sectionMeta.Columns.TryGetValue(field, out var columnMeta))
            {
                if (columnMeta.ColumnType == CardMetadataColumnType.Complex)
                {
                    foreach (var refColumnMeta in sectionMeta.Columns
                        .Where(x => x.ColumnType == CardMetadataColumnType.Physical
                            && x.ComplexColumnIndex == columnMeta.ComplexColumnIndex))
                    {
                        sectionPermissions.SetFieldPermissions(refColumnMeta.Name, permissionFlags);
                    }
                }
                else
                {
                    sectionPermissions.SetFieldPermissions(columnMeta.Name, permissionFlags);
                }
            }
        }

        /// <summary>
        /// Сохраняет оригинальную карточку в токен.
        /// </summary>
        /// <param name="token">Токен прав доступа.</param>
        /// <param name="card">Карточка.</param>
        private void StoreOriginalSource(KrToken token, Card card)
        {
            var cardSource = new Dictionary<string, object>(StringComparer.Ordinal);
            foreach (var section in card.Sections.Values)
            {
                if (this.permissionsManager.IgnoreSections.Contains(section.Name))
                {
                    continue;
                }

                if (section.Type == CardSectionType.Entry)
                {
                    Dictionary<string, object> sectionSource = null;
                    foreach (var field in section.RawFields)
                    {
                        if (field.Value is not null)
                        {
                            if (sectionSource is null)
                            {
                                cardSource[section.Name] = sectionSource = new Dictionary<string, object>(StringComparer.Ordinal);
                            }

                            sectionSource[field.Key] = field.Value;
                        }
                    }
                }
                else
                {
                    List<object> sectionSource = null;
                    foreach (var row in section.Rows)
                    {
                        Dictionary<string, object> rowSource = null;
                        foreach (var field in row)
                        {
                            if (field.Value is not null)
                            {
                                rowSource ??= new Dictionary<string, object>(StringComparer.Ordinal);

                                rowSource[field.Key] = field.Value;
                            }
                        }
                        if (rowSource is not null)
                        {
                            if (sectionSource is null)
                            {
                                cardSource[section.Name] = sectionSource = new List<object>();
                            }
                            sectionSource.Add(rowSource);
                        }
                    }
                }
            }

            var fileSoucrces = new List<object>();
            cardSource["Files"] = fileSoucrces;
            foreach (var file in card.Files)
            {
                fileSoucrces.Add(new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    ["ExternalSource"] = file.ExternalSource?.GetStorage(),
                    ["RowID"] = file.RowID,
                });
            }

            token.Info[KrPermissionsHelper.NewCardSourceKey] = cardSource;
        }

        #endregion

        #region Base Overrides New

        ///<inheritdoc/>
        public override async Task BeforeRequest(ICardNewExtensionContext context)
        {
            if (context.CardType is null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardTypeID = context.CardType.ID,
                    DocTypeID = context.Method == CardNewMethod.Template
                        && context.Request.TryGetTemplateCard() is Card templateCard
                        ? KrProcessSharedHelper.GetDocTypeID(templateCard)
                        : context.Request.Info.TryGet<Guid?>(KrConstants.Keys.DocTypeID), // Если для типа карточки используются типы документов - тип документа д.б. указан
                    WithExtendedPermissions = true,
                    ValidationResult = context.ValidationResult,
                    AdditionalInfo = context.Info,
                    PrevToken = KrToken.TryGet(context.Request.Info),
                    ExtensionContext = context,
                    ServerToken = context.Info.TryGetServerToken(),
                },
                cancellationToken: context.CancellationToken);

            if (permContext is not null)
            {
                var result = await this.permissionsManager.GetEffectivePermissionsAsync(
                    permContext,
                    KrPermissionFlagDescriptors.CreateCard,
                    KrPermissionFlagDescriptors.FullCardPermissionsGroup);

                // Проверяем возможность создания карточки
                // если возможность создания дана, то даем все права на карточку, т.е. ничего не закрываем
                // после сохранения при обновлении карточка уже будет получаться в соответствии с указанными
                // правами
                if (!result.Permissions.Contains(KrPermissionFlagDescriptors.CreateCard)
                    && context.ValidationResult.IsSuccessful())
                {
                    await permContext.AddErrorAsync(this, "$KrMessages_HaveNoPermissionsToCreateCard", context.CancellationToken);
                    return;
                }

                context.Info[nameof(KrPermissionsNewGetExtension)] = result;

                if (permContext.DocTypeID.HasValue)
                {
                    context.Info[DocTypeIDKey] = permContext.DocTypeID;
                }
            }
        }

        ///<inheritdoc/>
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (context.CardType is null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.RequestIsSuccessful
                || !context.Response.ValidationResult.IsSuccessful())
            {
                return;
            }

            var card = context.Response.Card;
            if (context.Info.TryGetValue(nameof(KrPermissionsNewGetExtension), out var obj)
                && obj is IKrPermissionsManagerResult result)
            {
                var docTypeID = context.Info.TryGet<Guid?>(DocTypeIDKey);
                await this.CalcPermissionCanFullRecalcRouteAsync(
                    card,
                    docTypeID,
                    result,
                    (CardTaskDialogStoreMode?) context.Request.TryGetInfo()?.TryGet<int?>(CardTaskDialogHelper.StoreMode),
                    true,
                    context.CancellationToken);

                var extendedCardSettings = await result.CreateExtendedCardSettingsAsync(context.Session.User.ID, card, context.CancellationToken);
                var fileSettings = GetFileSettings(extendedCardSettings);

                await this.SetCardPermissionsAsync(
                    context.Session.User.ID,
                    card,
                    result,
                    fileSettings,
                    extendedCardSettings?.TryGetOwnFilesSettings(),
                    context.CancellationToken);

                // Создаем токен
                var token = this.krTokenProvider.CreateToken(
                    card,
                    result.Version,
                    result.Permissions,
                    extendedCardSettings,
                    (t) =>
                    {
                        this.StoreOriginalSource(t, card);
                        t.SetDocTypeID(docTypeID);
                    });

                //кладем токен в карточку
                token.Set(card.Info);
            }
        }

        private static HashSet<Guid, KrPermissionsFileSettings> GetFileSettings(IKrPermissionExtendedCardSettings extendedCardSettings)
        {
            if (extendedCardSettings is null)
            {
                return null;
            }

            var fileSettings = extendedCardSettings.GetFileSettings();
            if (fileSettings == null
                || fileSettings.Count == 0)
            {
                return null;
            }
            else
            {
                return new HashSet<Guid, KrPermissionsFileSettings>(x => x.FileID, fileSettings);
            }
        }

        /// <summary>
        /// Рассчитывает, для указанной карточки, возможность полного пересчёта процесса.
        /// </summary>
        /// <param name="card">Карточка для которой выполняется расчёт <see cref="KrPermissionFlagDescriptors.CanFullRecalcRoute"/>.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="permissionsResult">Результат выполнения проверки прав доступа в <see cref="IKrPermissionsManager"/>.</param>
        /// <param name="storeMode">Режим сохранения карточки.</param>
        /// <param name="isNewRequest">Значение <see langword="true"/>, если расчёт прав выполняется в расширении на получение карточки, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        private async ValueTask CalcPermissionCanFullRecalcRouteAsync(
            Card card,
            Guid? docTypeID,
            IKrPermissionsManagerResult permissionsResult,
            CardTaskDialogStoreMode? storeMode,
            bool isNewRequest,
            CancellationToken cancellationToken = default)
        {
            if (permissionsResult.Has(KrPermissionFlagDescriptors.CanFullRecalcRoute)
                && (storeMode.HasValue && (isNewRequest || storeMode != CardTaskDialogStoreMode.Card)
                    || (await KrComponentsHelper.GetKrComponentsAsync(
                            card.TypeID,
                            docTypeID,
                            this.typesCache,
                            cancellationToken)).HasNot(KrComponents.Routes)
                    || !isNewRequest
                    && (await this.krScope.TryGetKrSatelliteAsync(card.ID, cancellationToken: cancellationToken))?.GetStagesSection()
                        .Rows
                        .All(static p =>
                            (p.TryGet<int?>(KrConstants.KrStages.StateID) ?? KrStageState.Inactive.ID) == KrStageState.Inactive) == false))
            {
                permissionsResult.Permissions.Remove(KrPermissionFlagDescriptors.CanFullRecalcRoute);
            }
        }

        #endregion

        #region Base Overrides Get

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;

            if (!context.RequestIsSuccessful
                || !context.ValidationResult.IsSuccessful()
                || context.CardType is null
                || (card = context.Response.TryGetCard()) is null
                || !await KrComponentsHelper.HasBaseAsync(card.TypeID, this.cache, context.CancellationToken))
            {
                return;
            }

            if (context.Method == CardGetMethod.Default)
            {
                var needMessage = false;
                var requriedPermissions = new List<KrPermissionFlagDescriptor>();

                var calculateFullPermissions = context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculatePermissionsMark);

                if (calculateFullPermissions)
                {
                    //Нужно рассчитать как бы все права, но на самом деле многие права (удаление/отмена и т.д.)
                    //нужны непосредственно при попытке совершения действия - поэтому будем проверять не все права
                    requriedPermissions.Add(KrPermissionFlagDescriptors.FullCardPermissionsGroup);

                    if (!context.Request.Info.TryGet<bool>(KrPermissionsHelper.PermissionsCalculatedMark))
                    {
                        //Если пришел признак что права уже были рассчитаны по плитке "редактировать"
                        //- не будем отображать сообщение
                        needMessage = true;
                    }
                }
                else
                {
                    requriedPermissions.Add(KrPermissionFlagDescriptors.ReadCard);
                    if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateSuperModeratorPermissions))
                    {
                        requriedPermissions.Add(KrPermissionFlagDescriptors.SuperModeratorMode);
                    }

                    if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateEditMyMessagesPermissions))
                    {
                        requriedPermissions.Add(KrPermissionFlagDescriptors.EditMyMessages);
                    }

                    if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateAddTopicPermissions))
                    {
                        requriedPermissions.Add(KrPermissionFlagDescriptors.AddTopics);
                    }

                    if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateResolutionPermissionsMark))
                    {
                        //Нужно рассчитать как бы все права, но на самом деле многие права (удаление/отмена и т.д.)
                        //нужны непосредственно при попытке совершения действия - поэтому будем проверять не все права
                        requriedPermissions.Add(KrPermissionFlagDescriptors.CreateResolutions);
                    }

                    if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateTaskAssignedRolesPermissionsMark))
                    {
                        requriedPermissions.Add(KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles);
                        requriedPermissions.Add(KrPermissionFlagDescriptors.ModifyOwnTaskAssignedRoles);
                    }
                }

                var permContext = await this.permissionsManager.TryCreateContextAsync(
                    new KrPermissionsCreateContextParams
                    {
                        Card = context.Response.Card,
                        // Расчет обязательных прав и расширенных настроек на сервере не имеет смысла
                        WithRequiredPermissions = context.Request.ServiceType != CardServiceType.Default,
                        WithExtendedPermissions = context.Request.ServiceType != CardServiceType.Default,
                        ValidationResult = context.ValidationResult,
                        AdditionalInfo = context.Info,
                        PrevToken = KrToken.TryGet(context.Request.Info),
                        ExtensionContext = context,
                        ServerToken = context.Info.TryGetServerToken(),
                    },
                    cancellationToken: context.CancellationToken);

                // Или была ошибка, и тогда она записалась в context.ValidationResult
                // или карточка не относится к типовому решению
                if (permContext is null)
                {
                    return;
                }

                IKrPermissionsManagerResult result;
                await using (context.DbScope.Create())
                {
                    result = await this.permissionsManager.GetEffectivePermissionsAsync(
                        permContext,
                        requriedPermissions.ToArray());

                    context.Info[nameof(KrPermissionsNewGetExtension)] = result;
                }

                //Если был запрос на полный расчет прав - отметим это в инфо карточки, чтобы не отображать
                //плитку "редактировать"
                if (calculateFullPermissions
                    //Или если все права были получены
                    || result.Has(KrPermissionFlagDescriptors.FullCardPermissionsGroup))
                {
                    card.Info[KrPermissionsHelper.PermissionsCalculatedMark] = true;
                }

                if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateSuperModeratorPermissions)
                    || result.Has(KrPermissionFlagDescriptors.SuperModeratorMode))
                {
                    card.Info.Add(KrPermissionsHelper.SuperModeratorPermissionsCalculated, true);
                }

                if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateEditMyMessagesPermissions))
                {
                    card.Info.Add(KrPermissionsHelper.EditMyMessagesPermissionsCalculated, true);
                }

                if (context.Request.Info.TryGet<bool>(KrPermissionsHelper.CalculateAddTopicPermissions)
                    || result.Has(KrPermissionFlagDescriptors.SuperModeratorMode)) // TODO тут по логике должно быть AddTopic
                {
                    card.Info.Add(KrPermissionsHelper.AddTopicPermissionsCalculated, true);
                }

                //Право на создание проверяется в соотв. расширении

                //Права на чтение карточки
                // Не пишем ошибку об отсутствии доступа на чтение, если есть другие ошибки
                if (!result.Permissions.Contains(KrPermissionFlagDescriptors.ReadCard)
                    && context.ValidationResult.IsSuccessful())
                {
                    await permContext.AddErrorAsync(this, "$KrMessages_HaveNoPermissionsToReadCard", context.CancellationToken);
                    return;
                }

                await this.CalcPermissionCanFullRecalcRouteAsync(
                    card,
                    permContext.DocTypeID,
                    result,
                    (CardTaskDialogStoreMode?) context.Request.TryGetInfo()?.TryGet<int?>(CardTaskDialogHelper.StoreMode),
                    false,
                    context.CancellationToken);

                var extendedCardSettings = await result.CreateExtendedCardSettingsAsync(context.Session.User.ID, card, context.CancellationToken);
                var fileSettings = GetFileSettings(extendedCardSettings);

                await this.SetCardPermissionsAsync(
                    context.Session.User.ID,
                    card,
                    result,
                    fileSettings,
                    extendedCardSettings?.TryGetOwnFilesSettings(),
                    context.CancellationToken);

                //Если был запрос на полный расчет прав (по плитке "Редактировать") и не было дано
                //право на редактирование карточки, то, чтобы пользователь понял что расчет был -
                //отобразим информационное сообщение какие права были получены
                if (needMessage && !result.Permissions.Contains(KrPermissionFlagDescriptors.EditCard))
                {
                    var message = KrPermissionsHelper.GetGrantedPermissionsMessage(result.Permissions.ToArray());
                    context.ValidationResult.AddInfo(this, message);
                }

                // Создаем токен
                var token = this.krTokenProvider.CreateToken(
                    card,
                    result.Version,
                    result.Permissions,
                    extendedCardSettings,
                    t => t.SetDocTypeID(permContext.DocTypeID));

                //кладем токен в карточку
                token.Set(card.Info);
            }
            // Проверка экспорта не затирание данных при нем не выполняется для админов
            else if (context.Method == CardGetMethod.Export
                && !context.Session.User.IsAdministrator())
            {
                var permContext = await this.permissionsManager.TryCreateContextAsync(
                    new KrPermissionsCreateContextParams
                    {
                        Card = context.Response.Card,
                        // Состояние грузится напрямую через базу, т.к. в карточке при экспорте его нет
                        KrState = await KrProcessSharedHelper.GetKrStateAsync(context.Response.Card.ID, context.DbScope, context.CancellationToken),
                        WithExtendedPermissions = true,
                        ValidationResult = context.ValidationResult,
                        AdditionalInfo = context.Info,
                        ExtensionContext = context,
                        ServerToken = context.Info.TryGetServerToken(),
                    },
                    cancellationToken: context.CancellationToken);

                // Или была ошибка, и тогда она записалась в context.ValidationResult
                // или карточка не относится к типовому решению
                if (permContext is null)
                {
                    return;
                }

                IKrPermissionsManagerResult result;
                await using (context.DbScope.Create())
                {
                    result = await this.permissionsManager.GetEffectivePermissionsAsync(
                        permContext,
                        KrPermissionFlagDescriptors.ReadCard, KrPermissionFlagDescriptors.CreateTemplateAndCopy);
                    context.Info[nameof(KrPermissionsNewGetExtension)] = result;
                }

                // Если при расчете прав нет права на создание шаблона и копирование, то пишем ошибку
                if (result.Has(KrPermissionFlagDescriptors.CreateTemplateAndCopy))
                {
                    var extendedCardSettings = await result.CreateExtendedCardSettingsAsync(context.Session.User.ID, card, context.CancellationToken);
                    var fileSettings = GetFileSettings(extendedCardSettings);

                    if (fileSettings is not null
                        && fileSettings.Count > 0)
                    {
                        var cardFiles = card.Files;
                        for (var i = cardFiles.Count - 1; i >= 0; i--)
                        {
                            var file = cardFiles[i];
                            if (fileSettings.TryGetItem(file.RowID, out var settings)
                                && settings.ReadAccessSetting <= KrPermissionsHelper.FileReadAccessSettings.ContentNotAvailable)
                            {
                                card.Files.RemoveAt(i);
                            }
                        }
                    }
                }
                else if (context.ValidationResult.IsSuccessful()) // Не пишем ошибку об отсутствии доступа, если есть другие ошибки
                {
                    await permContext.AddErrorAsync(
                        this,
                        KrPermissionsHelper.GetNotEnoughPermissionsErrorMessage(
                            KrPermissionFlagDescriptors.CreateTemplateAndCopy),
                        context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
