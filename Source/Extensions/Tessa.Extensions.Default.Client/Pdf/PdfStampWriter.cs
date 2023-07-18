using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Tessa.Localization;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Объект, выполняющий вывод штампа на странице PDF.
    /// </summary>
    public class PdfStampWriter
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием используемого шрифта.
        /// Другие свойства экземпляра устанавливаются значениями по умолчанию.
        /// </summary>
        /// <param name="font">Используемый шрифт. Не может быть равен <c>null</c>.</param>
        public PdfStampWriter(XFont font)
        {
            this.left = 20.0;
            this.top = 20.0;
            this.font = font ?? throw new ArgumentNullException(nameof(font));
            this.padding = 5.0;
            this.border = DefaultBorder;
            this.foreground = DefaultForeground;
            this.background = DefaultBackground;
        }


        /// <summary>
        /// Создаёт экземпляр класса с указанием параметров используемого шрифта.
        /// Другие свойства экземпляра устанавливаются значениями по умолчанию.
        /// </summary>
        /// <param name="fontFamilyName">Название семейства шрифтов, например, <c>Times New Roman</c>.</param>
        /// <param name="fontEmSize">Размер шрифта.</param>
        public PdfStampWriter(string fontFamilyName = "Verdana", double fontEmSize = 12.0)
            : this(font: GetFont(fontFamilyName, fontEmSize))
        {
        }

        #endregion

        #region Protected Methods
        
        /// <summary>
        /// Выводим штамп на страницу PDF, для которой задан объект <see cref="XGraphics"/>.
        /// Если штамп не содержит строк, т.е. свойство <see cref="IsEmpty"/> вернуло <c>true</c>,
        /// то метод в реализации по умолчанию не выполняет действий по отрисовке.
        /// </summary>
        /// <param name="graphics">Объект, используемый для вывода штампа на страницу PDF. Гарантированно не равен <c>null</c>.</param>
        protected virtual void DrawCore(XGraphics graphics)
        {
            if (this.IsEmpty)
            {
                return;
            }

            XSize[] sizes = new XSize[this.Lines.Count];
            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = graphics.MeasureString(this.Lines[i] ?? string.Empty, this.Font, XStringFormats.TopLeft);
            }

            double totalWidth = (sizes.Length > 0 ? sizes.Max(t => t.Width) : 0.0) + 2.0 * this.Padding;
            double totalHeight = (sizes.Length > 0 ? sizes.Sum(t => t.Height) : 0.0) + 2.0 * this.Padding;

            if (this.Background == XBrushes.Transparent)
            {
                // прозрачная заливка для PDF - это всё равно белый, поэтому вместо заливаемого прямоугольника рисуем линии
                double right = this.Left + totalWidth;
                double bottom = this.Top + totalHeight;

                graphics.DrawLines(
                    this.Border,
                    this.Left, this.Top,
                    right, this.Top,
                    right, bottom,
                    this.Left, bottom,
                    this.Left, this.Top);
            }
            else
            {
                graphics.DrawRectangle(
                    this.Border,
                    this.Background,
                    this.Left,
                    this.Top,
                    totalWidth,
                    totalHeight);
            }

            double textLeft = this.Left + this.Padding;
            double textTop = this.Top + this.Padding;

            for (int i = 0; i < sizes.Length; i++)
            {
                graphics.DrawString(
                    this.Lines[i] ?? string.Empty,
                    this.Font,
                    this.Foreground,
                    textLeft,
                    textTop,
                    XStringFormats.TopLeft);

                textTop += sizes[i].Height;
            }
        }


        /// <summary>
        /// Возвращает шрифт <see cref="XFont"/>, рекомендуемый для использования при выводе штампов,
        /// по заданному семейству шрифтов <paramref name="familyName"/> и размера шрифта <paramref name="emSize"/>.
        /// </summary>
        /// <param name="familyName">Семейство шрифтов, например, <c>"Verdana"</c>.</param>
        /// <param name="emSize">Размер шрифта в единицах <c>em</c>.</param>
        /// <returns>Объект шрифта, созданный для заданных параметров.</returns>
        protected static XFont GetFont(string familyName, double emSize)
        {
            return new XFont(
                familyName,
                emSize,
                XFontStyle.Regular,
                new XPdfFontOptions(PdfFontEncoding.Unicode));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Коллекция строк текста, которые выводятся в штампе.
        /// </summary>
        public List<string> Lines { get; } = new List<string>();

        /// <summary>
        /// Признак того, что штамп не содержит строк и поэтому не будет выведен.
        /// </summary>
        public virtual bool IsEmpty => this.Lines.Count == 0;


        private double left;

        /// <summary>
        /// Координата X для левого верхнего угла штампа, измеренная в точках PDF.
        /// Не может быть меньше нуля. По умолчанию равна <c>20.0</c>.
        /// </summary>
        public double Left
        {
            get { return this.left; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Left));
                }

                this.left = value;
            }
        }


        private double top;

        /// <summary>
        /// Координата Y для левого верхнего угла штампа, измеренная в точках PDF.
        /// Не может быть меньше нуля. По умолчанию равна <c>20.0</c>.
        /// </summary>
        public double Top
        {
            get { return this.top; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Top));
                }

                this.top = value;
            }
        }



        private double padding;

        /// <summary>
        /// Отступ от обводки до текста.
        /// Не может быть меньше нуля. По умолчанию равен <c>5</c> точкам.
        /// </summary>
        public double Padding
        {
            get { return this.padding; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Padding));
                }

                this.padding = value;
            }
        }
        

        private XFont font;

        /// <summary>
        /// Шрифт, который используется для вывода текста.
        /// Установить шрифт можно также посредством метода <see cref="SetFont(string,double)"/>.
        /// </summary>
        public XFont Font
        {
            get => this.font;
            set => this.font = value ?? throw new ArgumentNullException(nameof(Font));
        }


        private XPen border;

        /// <summary>
        /// Цвет обводки. По умолчанию используется <see cref="DefaultBorder"/>.
        /// </summary>
        public XPen Border
        {
            get => this.border;
            set => this.border = value ?? throw new ArgumentNullException(nameof(Border));
        }


        private XBrush foreground;

        /// <summary>
        /// Цвет текста. По умолчанию используется <see cref="DefaultForeground"/>.
        /// </summary>
        public XBrush Foreground
        {
            get => this.foreground;
            set => this.foreground = value ?? throw new ArgumentNullException(nameof(Foreground));
        }


        private XBrush background;

        /// <summary>
        /// Цвет заливки. По умолчанию используется <see cref="DefaultBackground"/>.
        /// Если заливка должна отсутствовать, то используйте кисть <see cref="XBrushes.Transparent"/>.
        /// </summary>
        public XBrush Background
        {
            get => this.background;
            set => this.background = value ?? throw new ArgumentNullException(nameof(Background));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Очищает список строк <see cref="Lines"/>.
        /// </summary>
        /// <returns>Текущий объект для цепочки вызовов.</returns>
        public PdfStampWriter Clear()
        {
            this.Lines.Clear();
            return this;
        }


        /// <summary>
        /// Добавляет строку в список строк <see cref="Lines"/>.
        /// </summary>
        /// <param name="text">Добавляемая строка текста. Значение <c>null</c> трактуется как пустая строка.</param>
        /// <param name="localize">
        /// Признак того, что текст локализуется вызовом метода <see cref="LocalizationManager.Format(string)"/>.
        /// </param>
        /// <returns>Текущий объект для цепочки вызовов.</returns>
        public PdfStampWriter AppendLine(string text, bool localize = true)
        {
            this.Lines.Add(localize ? LocalizationManager.Format(text) : text);
            return this;
        }


        /// <summary>
        /// Выводим штамп на страницу PDF, для которой задан объект <see cref="XGraphics"/>.
        /// Если штамп не содержит строк, т.е. свойство <see cref="IsEmpty"/> вернуло <c>true</c>,
        /// то метод в реализации по умолчанию не выполняет действий по отрисовке.
        /// </summary>
        /// <param name="graphics">Объект, используемый для вывода штампа на страницу PDF.</param>
        /// <returns>Текущий объект для цепочки вызовов.</returns>
        public PdfStampWriter Draw(XGraphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            this.DrawCore(graphics);
            return this;
        }


        /// <summary>
        /// Устанавливает текущий шрифт по заданным параметрам.
        /// </summary>
        /// <param name="fontFamily">Название семейства шрифтов, например, <c>Times New Roman</c>.</param>
        /// <param name="emSize">Размер шрифта.</param>
        /// <returns>Текущий объект для цепочки вызовов.</returns>
        public PdfStampWriter SetFont(string fontFamily, double emSize)
        {
            this.Font = GetFont(fontFamily, emSize);
            return this;
        }

        #endregion

        #region Static Properties

        private static XSolidBrush defaultForeground;

        /// <summary>
        /// Цвет <see cref="Foreground"/> с цветом по умолчанию.
        /// </summary>
        public static XSolidBrush DefaultForeground
        {
            get
            {
                return defaultForeground ??= new XSolidBrush(XColor.FromArgb(0xFF, 0x8C, 0x00, 0xFF));
            }
        }


        private static XSolidBrush defaultBackground;

        /// <summary>
        /// Цвет <see cref="Background"/> с цветом по умолчанию.
        /// </summary>
        public static XSolidBrush DefaultBackground
        {
            get
            {
                return defaultBackground ??= new XSolidBrush(XColor.FromArgb(0x80, 0xFF, 0xFF, 0xFF));
            }
        }


        private static XPen defaultBorder;

        /// <summary>
        /// Цвет <see cref="Border"/> с цветом по умолчанию.
        /// </summary>
        public static XPen DefaultBorder
        {
            get
            {
                return defaultBorder ??= new XPen(DefaultForeground.Color);
            }
        }

        #endregion
    }
}
