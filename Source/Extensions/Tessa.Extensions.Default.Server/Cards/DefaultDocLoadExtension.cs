using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Platform.Server.DocLoad;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение для модуля потокового ввода, используемое платформой по умолчанию.
    /// </summary>
    public class DefaultDocLoadExtension :
        DocLoadExtension
    {
        #region Constructor

        public DefaultDocLoadExtension(ISession session,
            IDbScope dbScope,
            IUnityContainer container,
            ICardFileManager cardFileManager,
            ICardRepository cardRepository,
            IPlaceholderManager placeholderManager,
            ICardServerPermissionsProvider permissionsProvider)
        {
            this.session = session;
            this.dbScope = dbScope;
            this.container = container;
            this.cardFileManager = cardFileManager;
            this.cardRepository = cardRepository;
            this.placeholderManager = placeholderManager;
            this.permissionsProvider = permissionsProvider;
        }

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ISession session;
        private readonly IDbScope dbScope;
        private readonly IUnityContainer container;
        private readonly ICardFileManager cardFileManager;
        private readonly ICardRepository cardRepository;
        private readonly IPlaceholderManager placeholderManager;
        private readonly ICardServerPermissionsProvider permissionsProvider;

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IDocLoadExtension" and @item="Request"]'/>
        public override async Task Request(IDocLoadExtensionContext context)
        {
            DbManager db = context.DbScope.Db;
            context.CardID = await db
                .SetCommand(
                    context.DbScope.BuilderFactory
                        .Select().C("ID")
                        .From(context.Settings.TableName).NoLock()
                        .Where().LowerC(context.Settings.FieldName).Equals().LowerP("Barcode")
                        .Build(),
                    db.Parameter("Barcode", context.Barcode))
                .ExecuteAsync<Guid?>(context.CancellationToken);
        }

        /// <doc path='info[@type="IDocLoadExtension" and @item="Save"]'/>
        public override async Task Save(IDocLoadExtensionContext context)
        {
            var document = context.Document;
            string outputFileName = LocalizationManager.Localize(
                await this.GetFileNameAsync(context.Settings, document.CardID, context.CancellationToken)) + document.GetExtension();

            await document.CloseAsync(context.CancellationToken);
            if (document.PageCount == 0)
            {
                logger.Warn($"Document with barcode {document.Barcode} doesn't have any pages. Skipped!");
                return;
            }

            CardGetRequest getRequest = new CardGetRequest { CardID = document.CardID, Info = new Dictionary<string, object> {{ CardHelper.DocLoadFlagKey, BooleanBoxes.True } }};
            this.permissionsProvider.SetFullPermissions(getRequest);

            CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, context.CancellationToken);
            if (!getResponse.ValidationResult.IsSuccessful())
            {
                logger.LogResult(getResponse.ValidationResult.Build());
                return;
            }

            await using ICardFileContainer container = await this.cardFileManager
                .CreateContainerAsync(getResponse.Card, cancellationToken: context.CancellationToken);

            await container.FileContainer
                .BuildFile(outputFileName)
                .SetContentReadOnly(document.File.Path)
                .AddWithNotificationAsync(cancellationToken: context.CancellationToken);

            CardStoreResponse storeResponse = await container.StoreAsync(
                (fileContainer, storeRequest, ct) =>
                {
                    storeRequest.Info = new Dictionary<string, object> {{ CardHelper.DocLoadFlagKey, BooleanBoxes.True } };
                    this.permissionsProvider.SetFullPermissions(storeRequest);
                    return new ValueTask();
                },
                cancellationToken: context.CancellationToken);

            if (!storeResponse.ValidationResult.IsSuccessful())
            {
                logger.LogResult(storeResponse.ValidationResult.Build());
            }
        }

        /// <doc path='info[@type="IDocLoadExtension" and @item="IsScanFile"]'/>
        public override Task<bool> IsScanFile(string filePath) =>
            Path.GetExtension(filePath).ToLowerInvariant() switch
            {
                ".tiff" => TaskBoxes.True,
                ".tif" => TaskBoxes.True,
                ".png" => TaskBoxes.True,
                ".jpg" => TaskBoxes.True,
                ".pdf" => filePath.EndsWith(".tmp.pdf", StringComparison.OrdinalIgnoreCase) ? TaskBoxes.False : TaskBoxes.True,
                _ => TaskBoxes.False
            };

        #endregion

        #region Private methods

        /// <summary>
        /// Получение имени файла
        /// </summary>
        /// <param name="settings">Настройки модуля потокового сканирования</param>
        /// <param name="cardID">ID карточки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        private async Task<string> GetFileNameAsync(IDocLoadSettings settings, Guid cardID, CancellationToken cancellationToken = default)
        {
            var info = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { PlaceholderHelper.ContextKey, null },
                { PlaceholderHelper.SessionKey, this.session },
                { PlaceholderHelper.UnityContainerKey, this.container },
                { PlaceholderHelper.DbScopeKey, this.dbScope },
                { PlaceholderHelper.CardIDKey, cardID },
            };
            if (settings.DocFormatName.Contains("{", StringComparison.Ordinal))
            {
                var fileNameDocument = new StringPlaceholderDocument(settings.DocFormatName);

                ValidationResult result = await this.placeholderManager.FindAndReplaceAsync(
                    fileNameDocument, info, FindingOptions.SkipUnknown, cancellationToken: cancellationToken);

                logger.LogResult(result);

                return result.IsSuccessful ? fileNameDocument.Text : "$CardTypes_TypesNames_DocLoad_Filename";
            }

            return settings.DocFormatName;
        }

        #endregion
    }
}