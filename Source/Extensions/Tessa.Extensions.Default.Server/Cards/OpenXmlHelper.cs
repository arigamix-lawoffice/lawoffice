using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Tessa.Platform;
using Tessa.Platform.Placeholders;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Вспомогательные методы для работы с документами формата OpenXml: .docx, .xlsx.
    /// </summary>
    public static class OpenXmlHelper
    {
        #region Static Fields

        /// <summary>
        /// Ширина вставляемой картинки по умолчанию в Emu, когда никакого другого способа определить её размер нет.
        /// </summary>
        public const long DefaultExtentsCx = 990000L;

        /// <summary>
        /// Высота вставляемой картинки по умолчанию в Emu, когда никакого другого способа определить её размер нет.
        /// </summary>
        public const long DefaultExtentsCy = 792000L;

        /// <summary>
        /// Определяет константу для хранения позиции в Info текста плейсхолдера
        /// </summary>
        public const string TextField = "Text";

        /// <summary>
        /// Определяет константу для хранения позиции в Info позиции плейсхолдера
        /// </summary>
        public const string PositionField = "Position";

        /// <summary>
        /// Определяет константу для хранения индекса в результате в Info плейсхолдера
        /// </summary>
        public const string IndexField = "Index";

        /// <summary>
        /// Определяет константу для хранения элемента документа в Info плейсхолдера
        /// </summary>
        public const string BaseElementField = "BaseElement";

        /// <summary>
        /// Определяет константу для хранения в Info плейсхолдера позиции плейсхолдера в элементе документа
        /// </summary>
        public const string OrderField = "Order";

        #endregion

        #region Static Methods

        /// <summary>
        /// Метод для получения элемента по позиции в документе
        /// </summary>
        /// <param name="mainPart">Объект документа, в котором происводится поиск элемента</param>
        /// <param name="position">Координаты объекта в дереве документа</param>
        /// <returns>Возвращает базовый элемент, который располагается по данной позиции</returns>
        public static OpenXmlElement GetElementByPosition(OpenXmlPart mainPart, IList position)
        {
            OpenXmlElement resultElement = mainPart.RootElement;
            OpenXmlPart part = mainPart;
            foreach (int index in position)
            {
                if (index < 0)
                {
                    part = part.Parts.ToArray()[~index].OpenXmlPart;
                    resultElement = part.RootElement;
                }
                else
                {
                    resultElement = resultElement.ChildElements[index];
                }
            }
            return resultElement;
        }

        public static ImagePartType GetImagePartTypeFromPlaceholder(string placeholderImageType)
        {
            switch (placeholderImageType)
            {
                case PlaceholderImageTypes.Bmp:
                    return ImagePartType.Bmp;

                case PlaceholderImageTypes.Emf:
                    return ImagePartType.Emf;

                case PlaceholderImageTypes.Gif:
                    return ImagePartType.Gif;

                case PlaceholderImageTypes.Icon:
                    return ImagePartType.Icon;

                case PlaceholderImageTypes.Jpeg:
                    return ImagePartType.Jpeg;

                case PlaceholderImageTypes.Png:
                    return ImagePartType.Png;

                case PlaceholderImageTypes.Tiff:
                    return ImagePartType.Tiff;

                case PlaceholderImageTypes.Wmf:
                    return ImagePartType.Wmf;

                default:
                    //case PlaceholderImageTypes.Exif:
                    //case PlaceholderImageTypes.Unknown:
                    return ImagePartType.Png;
            }
        }


        public static ImagePartType GetImagePartType(IPlaceholderImageParameters imageParameters)
        {
            return GetImagePartTypeFromPlaceholder(imageParameters.ImageType);
        }


        public static string GetPlaceholderImageType(ImagePartType imagePartType)
        {
            switch (imagePartType)
            {
                case ImagePartType.Bmp:
                    return PlaceholderImageTypes.Bmp;

                case ImagePartType.Gif:
                    return PlaceholderImageTypes.Gif;

                case ImagePartType.Png:
                    return PlaceholderImageTypes.Png;

                case ImagePartType.Tiff:
                    return PlaceholderImageTypes.Tiff;

                case ImagePartType.Icon:
                    return PlaceholderImageTypes.Icon;

                case ImagePartType.Jpeg:
                    return PlaceholderImageTypes.Jpeg;

                case ImagePartType.Emf:
                    return PlaceholderImageTypes.Emf;

                case ImagePartType.Wmf:
                    return PlaceholderImageTypes.Wmf;

                default:
                    //case ImagePartType.Pcx:
                    return PlaceholderImageTypes.Unknown;
            }
        }

        public static Int64Value PixelsToEmu(double pixels)
        {
            // в OpenXML используется единица измерения EMU: http://polymathprogrammer.com/2009/10/22/english-metric-units-and-open-xml/
            // в дюйме 914400 EMU, а заданные в плейсхолдере размеры считаем для 96 dpi
            // отсюда: EMU = pixel * 914400 / 96 = pixel * 9525

            return (long)pixels * 9525L;
        }


        public static Int64Value GetImageCx(Image image)
        {
            return image.Width * (long)(914400f / image.HorizontalResolution);
        }


        public static Int64Value GetImageCy(Image image)
        {
            return image.Height * (long)(914400f / image.VerticalResolution);
        }


        public static T FindParent<T>(OpenXmlElement element)
            where T : OpenXmlElement
        {
            OpenXmlElement current = element;

            while ((current = current.Parent) != null)
            {
                // сам родительский элемент является типом T
                if (current is T t)
                {
                    return t;
                }
            }

            return null;
        }


        public static T FindInParentChildren<T>(OpenXmlElement element)
            where T : OpenXmlElement
        {
            OpenXmlElement current = element;

            while ((current = current.Parent) != null)
            {
                // сам родительский элемент является типом T
                if (current is T t)
                {
                    return t;
                }

                // в родительском элементе в одном из его непосредственных детей есть объект
                T child = current.GetFirstChild<T>();
                if (child != null)
                {
                    return child;
                }
            }

            return null;
        }


        public static void AddPlaceholdersFromText(
            List<IPlaceholderText> result,
            string allTextString,
            IList position,
            bool hasOrder = true)
        {
            int order = 0;
            foreach (Match match in PlaceholderHelper.Regex.Matches(allTextString))
            {
                string text = match.Value;
                string value = PlaceholderHelper.TryGetValue(text);

                if (value != null)
                {
                    var newPlaceholder = new PlaceholderText(text, value);
                    newPlaceholder.Info[PositionField] = position;
                    newPlaceholder.Info[IndexField] = match.Index;

                    if (hasOrder)
                    {
                        newPlaceholder.Info[OrderField] = order++;
                    }

                    result.Add(newPlaceholder);
                }
            }
        }

        public static string TextPosition(IList position)
        {
            StringBuilder result = StringBuilderHelper.Acquire();

            foreach (object i in position)
            {
                result
                    .Append(i)
                    .Append("->");
            }

            return result
                .Remove(result.Length - 2, 2)
                .ToStringAndRelease();
        }


        public static bool IsLessOrEquals(IList position1, IList position2, bool resultOnCountMismatch = false)
        {
            for (int i = 0; i < position1.Count; i++)
            {
                if (position2.Count <= i)
                {
                    return resultOnCountMismatch;
                }

                var index1 = (int)position1[i];
                var index2 = (int)position2[i];

                // Если индекс меньше нуля, это это Part, который определяет источник данных
                if (index1 < 0
                    || index2 < 0)
                {
                    if (index1 == index2)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (index2 < index1)
                {
                    return false;
                }
            }
            return true;
        }

        internal static int GetIndex(OpenXmlElement element)
        {
            int index = 0;
            var prevSibling = element.PreviousSibling();
            while(prevSibling != null)
            {
                index += prevSibling.InnerText.Length;
                prevSibling = prevSibling.PreviousSibling();
            }

            return index;
        }
        #endregion
    }
}
