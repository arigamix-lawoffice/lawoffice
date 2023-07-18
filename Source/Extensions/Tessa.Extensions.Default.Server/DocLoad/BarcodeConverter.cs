using Tessa.Extensions.Platform.Server.DocLoad;

namespace Tessa.Extensions.Default.Server.DocLoad
{
    /// <inheritdoc />
    /// <remarks>
    /// Наследник класса может переопределять методы, возвращая идентификаторы для дополнительных штрих-кодов,
    /// например, доступных в рамках проекта с поддержкой другой библиотеки.
    /// </remarks>
    public class BarcodeConverter :
        IBarcodeConverter
    {
        #region IBarcodeConverter Members

        /// <inheritdoc />
        public virtual int GetBarcodeForRead(string name)
        {
            switch (name)
            {
                case "AZTEC":
                    return 1;

                case "CODABAR":
                    return 2;

                case "CODE_39":
                    return 4;

                case "CODE_93":
                    return 8;

                case "CODE_128":
                    return 16;

                case "DATA_MATRIX":
                    return 32;

                case "EAN_8":
                    return 64;

                case "EAN_13":
                    return 128;

                case "ITF":
                    return 256;

                case "MAXICODE":
                    return 512;

                case "PDF_417":
                    return 1024;

                case "QR_CODE":
                    return 2048;

                case "RSS_14":
                    return 4096;

                case "RSS_EXPANDED":
                    return 8192;

                case "UPC_A":
                    return 16384;

                case "UPC_E":
                    return 32768;

                case "All_1D":
                    return 61918;

                case "UPC_EAN_EXTENSION":
                    return 65536;

                case "MSI":
                    return 131072;

                case "PLESSEY":
                    return 262144;

                case "IMB":
                    return 524288;

                default:
                    return -1;
            }
        }


        /// <inheritdoc />
        public virtual int GetBarcodeForWrite(string name)
        {
            switch (name)
            {
                case "CODABAR":
                    return 16;

                case "CODE_39":
                    return 14;

                case "CODE_93":
                    return 36;

                case "CODE_128":
                    return 31;

                case "EAN_13":
                    return 5;

                case "EAN_8":
                    return 6;

                case "ITF":
                    return 35;

                case "UPC_A":
                    return 1;

                case "UPC_E":
                    return 2;

                case "MSI":
                    return 21;

                case "PLESSEY":
                    return 25;

                default:
                    return -1;
            }
        }

        #endregion
    }
}