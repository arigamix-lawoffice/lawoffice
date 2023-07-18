using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.DocLoad
{
    public class PrintDialogProvider :
        IPrintDialogProvider
    {
        #region Constructors

        public PrintDialogProvider(ICardCache cardCache) =>
            this.cardCache = cardCache ?? throw new ArgumentNullException(nameof(cardCache));

        #endregion

        #region Fields

        private readonly ICardCache cardCache;

        private volatile PrintDialog printDialog;

        #endregion

        #region Private Methods

        /// <summary>
        /// Указание смещения UI элементов в печатной форме
        /// </summary>
        /// <param name="ui">UI элемент</param>
        /// <param name="left">смещение слева</param>
        /// <param name="top">смещение сверху</param>
        private static void SetPos(UIElement ui, double left, double top)
        {
            FixedPage.SetLeft(ui, left);
            FixedPage.SetTop(ui, top);
        }

        #endregion

        #region Protected Methods

        protected virtual PrintDialog CreatePrintDialog() =>
            new PrintDialog
            {
                PageRangeSelection = PageRangeSelection.AllPages,
                UserPageRangeEnabled = true
            };

        /// <summary>
        /// Размер страницы
        /// </summary>
        /// <returns></returns>
        protected virtual Size GetPageSize() =>
            new Size(this.printDialog.PrintableAreaWidth, this.printDialog.PrintableAreaHeight);

        /// <summary>
        /// Генерация печатной формы документа
        /// </summary>
        /// <param name="digest">Дайджест карточки</param>
        /// <param name="barcodeBytes">Штрих-код</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        protected virtual async ValueTask<DocumentPaginator> PreparePrintDocumentAsync(
            string digest,
            byte[] barcodeBytes,
            CancellationToken cancellationToken = default)
        {
            //Document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = this.GetPageSize();

            //Page of document
            FixedPage page = new FixedPage
            {
                Width = document.DocumentPaginator.PageSize.Width,
                Height = document.DocumentPaginator.PageSize.Height
            };

            //Barcode image
            object bitmapSource = new ImageSourceConverter().ConvertFrom(barcodeBytes);
            if (bitmapSource is null)
            {
                throw new InvalidOperationException("Barcode bitmap source is null");
            }

            Image barcodeImage = new Image { Source = (BitmapSource) bitmapSource };

            Card settingsCard = (await cardCache.Cards.GetAsync("DocLoad", cancellationToken)).GetValue();

            IDictionary<string, object> fields = settingsCard.Sections["DocLoadSettings"].Fields;
            var showHeader = fields.TryGet<bool>("ShowHeader");
            var offsetWidth = fields.TryGet<double>("OffsetWidth");
            var offsetHeight = fields.TryGet<double>("OffsetHeight");

            if (showHeader)
            {
                //Heading: Continuous document load
                TextBlock textBlock = new TextBlock
                {
                    Text = await LocalizationManager.GetStringAsync("CardTypes_TypesNames_DocLoad", cancellationToken),
                    FontSize = 27,
                    Margin = new Thickness(96)
                };
                SetPos(textBlock, 170, 35);
                page.Children.Add(textBlock);

                //Document number (digest)
                TextBlock textBlock2 = new TextBlock
                {
                    Text = string.Format(await LocalizationManager.GetStringAsync("CardTypes_Controls_DocLoad_DocNumber", cancellationToken), digest),
                    FontSize = 20,
                    Margin = new Thickness(96)
                };
                SetPos(textBlock2, 170, 65);
                page.Children.Add(textBlock2);

                //Tessa logo
                Image image = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Arigamix.UI;component/Images/launcher.png", UriKind.Absolute)) };
                SetPos(image, 50, 50);
                page.Children.Add(image);
                SetPos(barcodeImage,
                    document.DocumentPaginator.PageSize.Width / 2.0 - barcodeImage.Source.Width / 2.0 + offsetWidth,
                    document.DocumentPaginator.PageSize.Height / 2.0 - barcodeImage.Source.Height / 2.0 + offsetHeight);
            }
            else
            {
                SetPos(barcodeImage, offsetWidth, offsetHeight);
            }

            page.Children.Add(barcodeImage);

            document.Pages.Add(new PageContent { Child = page });
            return document.DocumentPaginator;
        }

        #endregion

        #region IPrintDialogProvider Members

        /// <inheritdoc />
        public bool IsPrinterSelectionEnabled() => this.printDialog != null;

        /// <inheritdoc />
        public virtual ValueTask<bool?> SelectPrinterDialogAsync(bool forceShow, CancellationToken cancellationToken = default)
        {
            bool? result = !forceShow;
            DispatcherHelper.InvokeInUI(() =>
            {
                if (this.printDialog is null || forceShow) //Диалога нет или нужно его показать
                {
                    this.printDialog = this.CreatePrintDialog();

                    result = this.printDialog.ShowDialog();

                    if (!forceShow && result != true) //Скрываем диалог, если не был выбран способ печати
                    {
                        this.printDialog = null;
                    }
                }
            });

            return new ValueTask<bool?>(result);
        }

        /// <inheritdoc />
        public virtual async Task PrintDocumentAsync(string digest, byte[] barcodeBytes, CancellationToken cancellationToken = default)
        {
            if (this.printDialog is null)
            {
                return;
            }

            Task task = await DispatcherHelper.InvokeInUIAsync(
                async () => this.printDialog?.PrintDocument(
                    await this.PreparePrintDocumentAsync(digest, barcodeBytes, cancellationToken), digest));

            await task;
        }

        #endregion
    }
}