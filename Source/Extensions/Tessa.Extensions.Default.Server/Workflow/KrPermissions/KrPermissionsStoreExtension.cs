using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение, проверяющее права доступа к карточке при сохранении.
    /// </summary>
    public sealed class KrPermissionsStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache cache;
        private readonly IKrTokenProvider krTokenProvider;
        private readonly IKrPermissionsManager permissionsManager;
        private readonly IKrScope krScope;

        #endregion

        #region Constructors

        public KrPermissionsStoreExtension(
            IKrTypesCache cache,
            IKrTokenProvider krTokenProvider,
            IKrPermissionsManager permissionsManager,
            IKrScope krScope)
        {
            Check.ArgumentNotNull(cache, nameof(cache));
            Check.ArgumentNotNull(krTokenProvider, nameof(krTokenProvider));
            Check.ArgumentNotNull(permissionsManager, nameof(permissionsManager));
            Check.ArgumentNotNull(krScope, nameof(krScope));

            this.cache = cache;
            this.krTokenProvider = krTokenProvider;
            this.permissionsManager = permissionsManager;
            this.krScope = krScope;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            // права на измененную карточку считаем в AfterBeginTransaction, чтобы считать доступ к карточке по данным,
            // по которым эти права были рассчитаны изначально

            Card card;
            if (context.CardType is null
                || (card = context.Request.TryGetCard()) is null
                || card.StoreMode != CardStoreMode.Update
                || (await KrComponentsHelper.GetKrComponentsAsync(card, this.cache, context.CancellationToken))
                    .HasNot(KrComponents.Base))
            {
                return;
            }

            await this.CheckPermissionsOnUpdatingAsync(context, card);
        }

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            // считаем права на возможность изменить только что созданную карточку внутри блокировки на запись карточки;
            // BeforeCommit нужен, чтобы карточка уже была в базе для выполнения контекстных ролей

            Card card;
            if (context.CardType is null
                || (card = context.Request.TryGetCard()) is null
                || card.StoreMode != CardStoreMode.Insert
                || (await KrComponentsHelper.GetKrComponentsAsync(card, this.cache, context.CancellationToken))
                    .HasNot(KrComponents.Base))
            {
                return;
            }

            await this.CheckPermissionsOnCreatingAsync(context, card);
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful)
            {
                PrepareMandatoryFailedData(context);
                return;
            }
            else if ((card = context.Request.TryGetCard()) is null
                    || (await KrComponentsHelper.GetKrComponentsAsync(card, this.cache, context.CancellationToken))
                        .HasNot(KrComponents.Base))
            {
                return;
            }

            //Текущую версию создал текущий пользователь - кинем в респонс одноразовый токен с правами на чтение карточки
            KrToken token = this.krTokenProvider
                .CreateToken(
                    context.Response.CardID,
                    //context.Response.CardVersion, // TODO permissions - прокидывать корректню версию карточки после доработки отслеживания версий
                    permissions: new[] { KrPermissionFlagDescriptors.ReadCard },
                    modifyTokenAction: (t) => t.ExpiryDate = DateTime.UtcNow.AddMinutes(1));

            token.Set(context.Response.Info);
        }

        #endregion

        #region Private Methods

        private static bool FileHasChangesExceptSignatures(CardFile file)
        {
            // подписи могут изменяться без прав на редактирование файлов
            CardFileState state = file.State;
            if (state == CardFileState.Replaced || state == CardFileState.ModifiedAndReplaced)
            {
                return true;
            }

            if (state != CardFileState.Modified)
            {
                return false;
            }

            // изменялись системные свойства
            if (file.Flags != CardFileFlags.None)
            {
                return true;
            }

            Card fileCard = file.TryGetCard();
            StringDictionaryStorage<CardSection> fileSections;
            if (fileCard is not null
                && (fileSections = fileCard.TryGetSections()) is not null)
            {
                // нет изменённых секций
                int count = fileSections.Count;
                if (count == 0)
                {
                    return false;
                }

                // изменялись другие секции, кроме как секции с подписями
                return count > 1 || fileSections.First().Key != CardSignatureHelper.SectionName;
            }

            // нет изменённых секций
            return false;
        }

        private async Task<KrPermissionFlagDescriptor[]> GetRequiredPermissionsAsync(
            Guid userID,
            Card card,
            IValidationResultBuilder validationResults,
            IDbScope dbScope,
            CancellationToken cancellationToken)
        {
            //Сначала посчитаем какие проверки нужны
            var required = new HashSet<KrPermissionFlagDescriptor>();

            if (card.TryGetSections()?.Any(x => x.Key != KrConstants.KrStages.Virtual
                                       && x.Key != KrConstants.KrActiveTasks.Virtual
                                       && x.Key != KrConstants.KrPerformersVirtual.Synthetic
                                       && (x.Key != KrConstants.KrApprovalCommonInfo.Virtual || x.Value.GetAllChanges().Any(p => p != "NeedRebuild"))
                                       && (x.Value.TryGetRawFields()?.Count > 0 || x.Value.TryGetRows()?.Count > 0)) == true
                || card.TryGetTasks()?.Any(x =>
                        x.State != CardRowState.None
                        && x.TryGetCard() is { } taskCard
                        && taskCard.Sections.Any(y =>
                            y.Value.TryGetRawFields()?.Count > 0 || y.Value.TryGetRows()?.Count > 0)) == true)
            {
                required.Add(KrPermissionFlagDescriptors.EditCard);
            }

            var files = card.TryGetFiles();
            if (files?.Count > 0)
            {
                foreach (var file in files)
                {
                    var fileCreatedByID = file.Card.CreatedByID;
                    if (FileHasChangesExceptSignatures(file))
                    {
                        if (fileCreatedByID == userID)
                        {
                            // изменение собственных файлов - право на это изменение определяется отдельно согласно
                            // заданиям карточки, поэтому пользователь может его получить, не обладая правом на
                            // редактирование файлов в целом
                            required.Add(KrPermissionFlagDescriptors.EditOwnFiles);
                        }
                        else
                        {
                            // изменение файлов
                            required.Add(KrPermissionFlagDescriptors.EditFiles);
                        }

                        // Изменение категории и расширения файла теперь требуют прав на добавление файла
                        if (file.Flags.Has(CardFileFlags.UpdateCategory)
                            || (file.Flags.Has(CardFileFlags.UpdateName)
                                && await CheckExtensionChangedAsync(dbScope, file, cancellationToken)))
                        {
                            required.Add(KrPermissionFlagDescriptors.AddFiles);
                        }
                    }

                    if (file.State == CardFileState.Inserted)
                    {
                        // добавление файлов
                        required.Add(KrPermissionFlagDescriptors.AddFiles);
                    }
                    else if (file.State == CardFileState.Deleted)
                    {
                        if (fileCreatedByID == userID)
                        {
                            // удаление собственных файлов
                            required.Add(KrPermissionFlagDescriptors.DeleteOwnFiles);
                        }
                        else
                        {
                            // удаление файлов
                            required.Add(KrPermissionFlagDescriptors.DeleteFiles);
                        }
                    }
                }
            }

            if (card.StoreMode == CardStoreMode.Update)
            {
                var containsKrStagesVirtual = card.TryGetStagesSection(out var krStagesVirtual);
                bool hasSkipStages = default;
                bool hasModifiedStages = default;
                if (containsKrStagesVirtual)
                {
                    Card satellite = default;
                    foreach (var stage in krStagesVirtual.Rows.Where(i => i.State == CardRowState.Deleted))
                    {
                        if (satellite is null)
                        {
                            satellite = await this.krScope.GetKrSatelliteAsync(card.ID, validationResults, cancellationToken: cancellationToken);

                            if (satellite is null)
                            {
                                break;
                            }
                        }

                        CardRow satelliteKrStage;
                        if (satellite.TryGetStagesSection(out var satelliteKrStages)
                            && ((satelliteKrStage = satelliteKrStages.Rows.SingleOrDefault(j => j.RowID == stage.RowID)) is not null)
                            && KrProcessSharedHelper.CanBeSkipped(satelliteKrStage))
                        {
                            hasSkipStages = true;
                            break;
                        }
                    }

                    hasModifiedStages = krStagesVirtual.Rows.Any(i => i.State == CardRowState.Modified || i.State == CardRowState.Inserted);
                }

                // Пропуск этапа.
                if (hasSkipStages)
                {
                    required.Add(KrPermissionFlagDescriptors.CanSkipStages);
                }

                // Изменение маршрута.
                if (hasModifiedStages
                    || card.Sections.TryGetValue(KrConstants.KrPerformersVirtual.Synthetic, out CardSection performersSection)
                    && performersSection.TryGetRows()?.Any(x => x.State != CardRowState.None) == true)
                {
                    required.Add(KrPermissionFlagDescriptors.EditRoute);
                }
            }

            // Подписание файлов
            if (CardSignatureHelper.AnySignatureRow(card,
                (file, signatureRow) => signatureRow.State != CardRowState.Deleted))
            {
                required.Add(KrPermissionFlagDescriptors.SignFiles);
            }

            return required.ToArray();
        }

        private async Task CheckPermissionsOnCreatingAsync(ICardStoreExtensionContext context, Card storeCard)
        {
            KrToken krToken = KrToken.TryGet(storeCard.Info);
            KrToken serverToken = context.Info.TryGetServerToken();

            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    Card = storeCard,
                    IsStore = true,
                    WithExtendedPermissions = true,
                    ValidationResult = context.ValidationResult,
                    AdditionalInfo = context.Info,
                    PrevToken = krToken,
                    ServerToken = serverToken,
                    ExtensionContext = context,
                },
                cancellationToken: context.CancellationToken);

            if (permContext is not null)
            {
                context.Info[nameof(KrPermissionsStoreExtension)] = await this.permissionsManager.CheckRequiredPermissionsAsync(
                    permContext,
                    await this.GetRequiredPermissionsAsync(context.Session.User.ID, storeCard, context.ValidationResult, context.DbScope, context.CancellationToken));
            }
        }

        private async Task CheckPermissionsOnUpdatingAsync(ICardStoreExtensionContext context, Card storeCard)
        {
            KrToken krToken = KrToken.TryGet(storeCard.Info);
            KrToken serverToken = context.Info.TryGetServerToken();
            if (krToken is null)
            {
                //Если запрос отправлен не из плагина Chronos или из неизвестного плагина (как правило это обычный запрос из клиента)
                if (!context.Request.GetIgnorePermissionsWarning()
                    && !context.Request.TryGetPluginType().HasValue)
                {
                    //Предупредим пользователя что что-то пошло не так и токен не был найден
                    context.ValidationResult.AddWarning(this, "$KrMessages_CardHasNoTokenWhenSaving");
                }
            }

            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    Card = storeCard,
                    IsStore = true,
                    WithExtendedPermissions = true,
                    ValidationResult = context.ValidationResult,
                    AdditionalInfo = context.Info,
                    PrevToken = krToken,
                    ServerToken = serverToken,
                    ExtensionContext = context,
                },
                cancellationToken: context.CancellationToken);

            if (permContext is not null)
            {
                context.Info[nameof(KrPermissionsStoreExtension)] = await this.permissionsManager.CheckRequiredPermissionsAsync(
                    permContext,
                    await this.GetRequiredPermissionsAsync(context.Session.User.ID, storeCard, context.ValidationResult, context.DbScope, context.CancellationToken));
            }
        }

        private static void PrepareMandatoryFailedData(ICardStoreExtensionContext context)
        {
            if (context.Info.TryGetValue(nameof(KrPermissionsStoreExtension), out var resultObject)
                && resultObject is KrPermissionsManagerCheckResult result
                && result.Info.TryGetValue(KrPermissionsHelper.FailedMandatoryRulesKey, out var rulesObj)
                && rulesObj is IList rules)
            {
                List<KrPermissionMandatoryRuleStorage> rulesForSend = new List<KrPermissionMandatoryRuleStorage>();
                foreach (KrPermissionMandatoryRule rule in rules)
                {
                    rulesForSend.Add(
                        new KrPermissionMandatoryRuleStorage(
                            rule.SectionID,
                            rule.HasColumns ? rule.ColumnIDs : null));
                }

                context.Response.Info[KrPermissionsHelper.FailedMandatoryRulesKey]
                    = rulesForSend.Select(x => (object) x.GetStorage());
            }
        }

        private static async ValueTask<bool> CheckExtensionChangedAsync(IDbScope dbScope, CardFile file, CancellationToken cancellationToken = default)
        {
            var oldName = await GetFileNameFromDbAsync(dbScope, file.RowID, cancellationToken);
            var oldExtension = FileHelper.GetExtension(oldName).ToLower().TrimStart('.');
            var newExtension = FileHelper.GetExtension(file.Name).ToLower().TrimStart('.');

            return !oldExtension.Equals(newExtension, StringComparison.Ordinal);
        }

        private static async ValueTask<string> GetFileNameFromDbAsync(IDbScope dbScope, Guid fileID, CancellationToken cancellationToken = default)
        {
            await using var _ = dbScope.Create();

            var db = dbScope.Db;
            var builder = dbScope.BuilderFactory;

            return await db
                .SetCommand(
                    builder
                        .Select().C("Name").From("Files").NoLock().Where().C("RowID").Equals().P("FileID")
                        .Build(),
                    db.Parameter("FileID", fileID))
                .LogCommand()
                .ExecuteAsync<string>(cancellationToken);
        }

        #endregion
    }
}
