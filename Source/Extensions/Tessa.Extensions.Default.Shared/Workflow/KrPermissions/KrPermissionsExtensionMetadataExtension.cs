using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение метаданных, модифицирующее тип карточки правила доступа.
    /// Добавляет флаги правила доступа.
    /// Добавляет расширение правила доступа.
    /// Скрывает настройки ACL из правила доступа, если отсутствует модуль ACL.
    /// </summary>
    public sealed class KrPermissionsExtensionMetadataExtension :
        CardTypeMetadataExtension
    {
        #region Constructors

        public KrPermissionsExtensionMetadataExtension(
            IDbScope dbScope,
            ILicenseManager licenseManager)
        {
            //конструктор для сервера
            this.dbScope = NotNullOrThrow(dbScope);
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        public KrPermissionsExtensionMetadataExtension(
            ICardMetadata clientCardMetadata,
            ICardCache cache,
            ILicenseManager licenseManager)
            : base(clientCardMetadata)
        {
            //конструктор для клиента
            this.cache = NotNullOrThrow(cache);
            this.licenseManager = NotNullOrThrow(licenseManager);
        }

        #endregion

        #region Fields

        private readonly IDbScope dbScope;

        private readonly ICardCache cache;

        private readonly ILicenseManager licenseManager;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            CardType permissionsType = await this.TryGetCardTypeAsync(context, DefaultCardTypes.KrPermissionsTypeID, false).ConfigureAwait(false);
            if (permissionsType is null)
            {
                return;
            }
            await AddFlagsAsync(permissionsType, context).ConfigureAwait(false);
            await this.AddExtensionsAsync(permissionsType, context).ConfigureAwait(false);
            await this.ModifyAclSettingsAsync(permissionsType, context).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods

        private static async ValueTask AddFlagsAsync(CardType permissionsType, ICardMetadataExtensionContext context)
        {
            var mainForm = permissionsType.Forms[0];

            CardTypeBlock flagsBlock = mainForm.Blocks.FirstOrDefault(x => x.Name == "Flags");
            SchemeTable permissionsSection = await context.SchemeService
                .GetTableAsync("KrPermissions", context.CancellationToken).ConfigureAwait(false);

            if (flagsBlock != null
                && permissionsType.SchemeItems.FirstOrDefault(x => x.SectionID == permissionsSection.ID) is { } permissionsSchemeItem)
            {
                // Для каждого флага добавляем его контрол в блок и добавляем флаг в секцию, если он еще не был добавлен
                foreach (var flag in KrPermissionFlagDescriptors.Full.IncludedPermissions.OrderBy(x => x.Order))
                {
                    if (flag.IsVirtual
                        || permissionsSection.Columns.GetColumn(flag.SqlName) is not { } column)
                    {
                        continue;
                    }

                    var columnID = column.ID;
                    if (!permissionsSchemeItem.ColumnIDList.Contains(columnID))
                    {
                        permissionsSchemeItem.ColumnIDList.Add(columnID);
                    }

                    flagsBlock.Controls.Add(
                        new CardTypeEntryControl()
                        {
                            Caption = flag.ControlCaption,
                            Name = flag.Name,
                            PhysicalColumnIDList = new SealableList<Guid>() { columnID },
                            SectionID = permissionsSection.ID,
                            ToolTip = flag.ControlTooltip,
                            Type = CardControlTypes.Boolean,
                        });
                }
            }
        }

        private async ValueTask AddExtensionsAsync(CardType permissionsType, ICardMetadataExtensionContext context)
        {
            var extensionType = await this.TryGetExtensionTypeAsync(context).ConfigureAwait(false);
            if (extensionType is null)
            {
                return;
            }

            var sourceMainForm = extensionType.Forms[0];
            var targetMainForm = permissionsType.Forms[0];

            StorageHelper.Merge(sourceMainForm.FormSettings, targetMainForm.FormSettings);
            
            // регистрация глобальных объектов.
            using var ctx = new CardGlobalReferencesContext(context, extensionType);
            // все формы, кроме первой.
            extensionType.Forms.Skip(1).MakeGlobal(ctx);
            // все блоки первой формы.
            sourceMainForm.Blocks.MakeGlobal(ctx, sourceMainForm);
            // все валидаторы.
            extensionType.Validators.MakeGlobal(ctx);
            // все расширения типа.
            extensionType.Extensions.MakeGlobal(ctx);
            
            // Вставка элементов.
            await extensionType.SchemeItems.CopyToTheBeginningOfAsync(permissionsType.SchemeItems, context.CancellationToken).ConfigureAwait(false);
            
            await extensionType.Forms.InsertNonOrderableAsync(permissionsType.Forms, permissionsType.Forms.Count, 1,
                cancellationToken: context.CancellationToken);
            await extensionType.Validators.InsertNonOrderableAsync(permissionsType.Validators, cancellationToken: context.CancellationToken).ConfigureAwait(false);
            await extensionType.Extensions.InsertNonOrderableAsync(permissionsType.Extensions, cancellationToken: context.CancellationToken).ConfigureAwait(false);

            await InsertBlocksAsync(sourceMainForm, targetMainForm, context.CancellationToken).ConfigureAwait(false);
        }

        private async Task<CardType> TryGetExtensionTypeAsync(ICardMetadataExtensionContext context)
        {
            Guid? extensionTypeID;
            if (this.ClientMode)
            {
                if (context.CardTypes.All(x => x.ID != DefaultCardTypes.KrPermissionsTypeID))
                {
                    // на клиенте просматривают тип карточки с расширениями, но не KrPermissions
                    return null;
                }

                var krSettings = await this.cache.Cards.GetAsync("KrSettings", context.CancellationToken).ConfigureAwait(false);
                extensionTypeID = krSettings.IsSuccess
                    ? krSettings.GetValue().Sections["KrSettings"].Fields.Get<Guid?>("PermissionsExtensionTypeID")
                    : null;
            }
            else
            {
                await using (this.dbScope.Create())
                {
                    var db = this.dbScope.Db;
                    var builder = this.dbScope.BuilderFactory
                        .Select().Top(1).C("PermissionsExtensionTypeID")
                        .From("KrSettings").NoLock()
                        .Limit(1);

                    extensionTypeID = await db
                        .SetCommand(builder.Build())
                        .LogCommand()
                        .ExecuteAsync<Guid?>(context.CancellationToken).ConfigureAwait(false);
                }
            }

            // нечего расширять
            if (!extensionTypeID.HasValue)
            {
                return null;
            }

            return await this.TryGetCardTypeAsync(context, extensionTypeID.Value).ConfigureAwait(false);
        }

        private static async ValueTask InsertBlocksAsync(CardTypeNamedForm sourceMainForm, CardTypeNamedForm targetMainForm, CancellationToken cancellationToken = default)
        {
            // поиск места вставки.
            int insertIndex = targetMainForm.Blocks.IndexOf(x => x.Name == "ExtensionMarker");
            insertIndex = insertIndex < 0 ? targetMainForm.Blocks.Count : insertIndex + 1;
            // вставка
            await sourceMainForm.Blocks.InsertNonOrderableAsync(targetMainForm.Blocks, insertIndex,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        private async ValueTask ModifyAclSettingsAsync(
            CardType permissionsType,
            ICardMetadataExtensionContext context)
        {
            var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken).ConfigureAwait(false);
            if (license.Modules.HasEnterpriseOrContains(LicenseModules.AclID))
            {
                return;
            }

            var permissionsSection = await context.SchemeService
                .GetTableAsync("KrPermissions", context.CancellationToken).ConfigureAwait(false);
            var columnToDelete = permissionsSection?.Columns
                .FirstOrDefault(x => x.Name.Equals("AclGenerationRules", StringComparison.Ordinal));
            var permissionRulesSection = await context.SchemeService
                .GetTableAsync("KrPermissionAclGenerationRules", context.CancellationToken).ConfigureAwait(false);

            if (columnToDelete is not null
                && permissionsType.SchemeItems.TryFirst(x => x.SectionID == permissionsSection.ID, out var sectionItem))
            {
                sectionItem.ColumnIDList.Remove(columnToDelete.ID);
            }

            if (permissionRulesSection is not null)
            {
                permissionsType.SchemeItems.RemoveAll(x => x.SectionID == permissionRulesSection.ID);
            }
            
            permissionsType.Forms[0].Blocks.RemoveAll(x => x.Name == "AclGenerationRules");
        }


        #endregion
    }
}
