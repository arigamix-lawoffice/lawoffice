using System;
using PdfSharp.Pdf;
using Tessa.Extensions.Platform.Client.Scanning;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Client.Pdf
{
    public sealed class PdfScanDocument :
        ScanDocument
    {
        #region Constructors

        public PdfScanDocument(PdfDocument pdf)
        {
            if (pdf == null)
            {
                throw new ArgumentNullException("pdf");
            }

            this.pdf = pdf;
        }

        #endregion

        #region Properties

        private readonly PdfDocument pdf;

        public PdfDocument Pdf
        {
            get { return this.pdf; }
        }

        #endregion

        #region Base Overrides

        public override string FileExtension
        {
            get { return ".pdf"; }
        }


        public override ITempFile File
        {
            get { return null; }
        }


        protected override void SaveCore(string fileName)
        {
            this.pdf.Save(fileName);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.pdf.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
