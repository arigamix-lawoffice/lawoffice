using System.IO;
using System.Text;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Обертка для TextWriter, которая позволяет задавать префикс, а также его цвет в для консоли
    /// при записи строк через метод <see cref="WriteLine(string)"/>.
    /// </summary>
    public class TestTextWriter : TextWriter
    {
        #region Private Fields

        private readonly TextWriter writer;
        private const string StdPrefix = ">>> ";
        private const string SequenceForSetColorStart = "\u001b[";
        private const string SequenceForSetColorEnd = "m";
        private const string SequenceForResetColor = "\u001b[0m";

        private TextWriterColor? prefixColor;
        private string computedSequenceForResetColor;
        private string computedSequenceForSetColor;

        #endregion

        #region Public Properties

        public string Prefix { get; set; }

        public override Encoding Encoding => this.writer.Encoding;

        public TextWriterColor? PrefixColor
        {
            get => this.prefixColor;
            set
            {
                switch (value)
                {
                    case null:
                        this.computedSequenceForSetColor = string.Empty;
                        this.computedSequenceForResetColor = string.Empty;
                        break;
                    default:
                        this.computedSequenceForSetColor =
                            $"{SequenceForSetColorStart}{(int) value}{SequenceForSetColorEnd}";
                        this.computedSequenceForResetColor = SequenceForResetColor;
                        break;
                }

                this.prefixColor = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает экземпляр <see cref="TestTextWriter"/>.
        /// </summary>
        /// <param name="writer">TextWriter для которого создается обертка.</param>
        /// <param name="prefix">Префикс, который должен записываться перед строкой.</param>
        /// <param name="prefixColor">Цвет префикса для консоли.</param>
        public TestTextWriter(TextWriter writer, string prefix = StdPrefix, TextWriterColor? prefixColor = null)
            : base(writer.FormatProvider)
        {
            this.writer = writer;
            this.PrefixColor = prefixColor;
            this.Prefix = prefix;
        }

        #endregion

        #region Methods Override

        public override void WriteLine(string value)
        {
            this.writer.Write(this.computedSequenceForSetColor);
            this.writer.Write(this.Prefix);
            this.writer.Write(this.computedSequenceForResetColor);
            this.writer.WriteLine(value);
        }

        public override void Flush() => this.writer.Flush();

        #endregion
    }
}