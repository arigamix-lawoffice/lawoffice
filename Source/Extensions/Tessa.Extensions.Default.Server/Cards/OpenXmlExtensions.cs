using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Tessa.Extensions.Default.Server.Cards
{
    public static class OpenXmlExtensions
    {
        /// <summary>
        /// Безопасное удаление элемента. Удаляет элемент в случае, если он прикреплен к родительскому элементу
        /// </summary>
        /// <param name="element">Удаляемый элемент</param>
        public static void SafeRemove(this OpenXmlElement element)
        {
            if (element.Parent != null)
            {
                element.Remove();
            }
        }

        /// <summary>
        /// Метод для получения OpenXmlPart по OpenXmlElement
        /// </summary>
        /// <param name="element">Элемент, относительно которого ведется поиск OpenXmlPart</param>
        /// <returns>Возвращает первый родительский OpenXmPart, или null, если OpenXmlPart не найден.</returns>
        public static OpenXmlPart GetPart(this OpenXmlElement element)
        {
            var checkElement = element;
            do
            {
                if (checkElement is OpenXmlPartRootElement partRootElement)
                {
                    switch (partRootElement)
                    {
                        case Document document:
                            return document.MainDocumentPart;

                        case Footer footer:
                            return footer.FooterPart;

                        case DocumentFormat.OpenXml.Wordprocessing.Header header:
                            return header.HeaderPart;

                        case Workbook workbook:
                            return workbook.WorkbookPart;
                    }
                }

                checkElement = checkElement.Parent;
            }
            while (checkElement != null);

            return null;
        }

        public static ImagePart AddImagePart(this OpenXmlPart openXmlPart, ImagePartType imagePartType)
        {
            switch (openXmlPart)
            {
                case MainDocumentPart document:
                    return document.AddImagePart(imagePartType);

                case FooterPart footer:
                    return footer.AddImagePart(imagePartType);

                case HeaderPart header:
                    return header.AddImagePart(imagePartType);

                case WorkbookPart workbook:
                    return workbook.AddImagePart(imagePartType);
            }

            return null;
        }
    }
}
