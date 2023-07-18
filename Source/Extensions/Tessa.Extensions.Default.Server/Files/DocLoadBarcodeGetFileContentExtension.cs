using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using BarcodeLib;
using NLog;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Platform.Server.DocLoad;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение получения
    /// </summary>
    public sealed class DocLoadBarcodeGetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly ICardCache cardCache;
        private readonly ICardRepository cardRepository;
        private readonly ILicenseManager licenseManager;
        private readonly IBarcodeConverter barcodeConverter;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public DocLoadBarcodeGetFileContentExtension(
            IDbScope dbScope,
            ICardCache cardCache,
            ICardRepository cardRepository,
            ILicenseManager licenseManager,
            IBarcodeConverter barcodeConverter)
        {
            this.dbScope = dbScope;
            this.cardCache = cardCache;
            this.cardRepository = cardRepository;
            this.licenseManager = licenseManager;
            this.barcodeConverter = barcodeConverter;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            var docLoad = await cardCache.Cards.GetAsync("DocLoad", context.CancellationToken);
            if (!docLoad.IsSuccess)
            {
                return;
            }

            IDictionary<string, object> fields = docLoad.GetValue().Sections["DocLoadSettings"].Fields;
            var tableName = fields.TryGet<string>("DefaultBarcodeTableName");
            var fieldName = fields.TryGet<string>("DefaultBarcodeFieldName");
            var barcodeType = fields.TryGet<string>("BarcodeWriteName");
            var barcodeLabel = fields.TryGet<bool>("BarcodeLabel");
            var barcodeWidth = fields.TryGet<double>("BarcodeWidth");
            var barcodeHeight = fields.TryGet<double>("BarcodeHeight");

            await using (this.dbScope.Create())
            {
                DbManager db = this.dbScope.Db;
                context.Response = new CardGetFileContentResponse();
                var isEnabled = fields.TryGet<bool>("IsEnabled");
                if (!isEnabled
                    || !(await this.licenseManager.GetLicenseAsync(context.CancellationToken)).Modules.Contains(LicenseModules.DocLoadID))
                {
                    return;
                }

                var barcode = await db
                    .SetCommand(this.dbScope.BuilderFactory
                            .Select()
                            .C(fieldName)
                            .From(tableName).NoLock()
                            .Where()
                            .C("ID").Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", context.Request.CardID))
                    .LogCommand()
                    .ExecuteAsync<string>(context.CancellationToken);

                if (string.IsNullOrWhiteSpace(barcode))
                {
                    CardGetRequest getRequest = new CardGetRequest { CardID = context.Request.CardID };
                    CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, context.CancellationToken);
                    if (!getResponse.ValidationResult.IsSuccessful())
                    {
                        context.ValidationResult.Add(getResponse.ValidationResult.Build());
                        return;
                    }

                    Card card = getResponse.Card;
                    string cardDigest = context.Request.TryGetDigest();
                    if (string.IsNullOrEmpty(cardDigest))
                    {
                        cardDigest = await this.cardRepository.GetDigestAsync(card, CardDigestEventNames.DocLoadStore, context.CancellationToken);
                    }

                    card.RemoveAllButChanged();

                    var storeRequest = new CardStoreRequest { Card = card, Info = { [DocLoadBarcodeStoreExtension.CreateBarcodeKey] = BooleanBoxes.True } };
                    storeRequest.SetDigest(cardDigest);

                    var storeResponse = await this.cardRepository.StoreAsync(storeRequest, context.CancellationToken);
                    if (!storeResponse.ValidationResult.IsSuccessful())
                    {
                        context.ValidationResult.Add(storeResponse.ValidationResult.Build());
                        return;
                    }

                    barcode = storeResponse.Info.Get<string>(DocLoadBarcodeStoreExtension.CreateBarcodeKey);
                    context.Response.Info["RefreshCard"] = BooleanBoxes.True;
                }

                barcode = TransformBarcode(barcodeType, barcode);

                try
                {
                    using Barcode b = new Barcode { IncludeLabel = barcodeLabel };
                    byte[] data;

                    using (var img = (Bitmap) b.Encode((TYPE) this.barcodeConverter.GetBarcodeForWrite(barcodeType), barcode, (int) barcodeWidth, (int) barcodeHeight))
                    {
                        await using var stream = new MemoryStream();
                        img.Save(stream, ImageFormat.Bmp);
                        data = stream.ToArray();
                    }

                    context.ContentFuncAsync = ct => new ValueTask<Stream>(new MemoryStream(data));
                    context.Response.Size = data.LongLength;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    logger.LogException(e, LogLevel.Error);
                    throw new InvalidOperationException("Invalid input barcode. Please, check barcode format.", e);
                }
            }
        }

        private static string TransformBarcode(string barcodeType, string barcode) =>
            barcodeType switch
            {
                "CODABAR" => $"A{barcode}B",
                _ => barcode
            };

        #endregion
    }
}