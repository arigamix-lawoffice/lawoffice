using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tessa.Cards;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata.Types;

namespace Tessa.Extensions.Console.ConfigureGenerator
{
    public class NullViewService : IViewService
    {
        public ValueTask<IReadOnlyList<ITessaView>> GetAllViewsAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public ValueTask<ITessaView> GetByNameAsync(string viewName, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public ValueTask<IReadOnlyList<ITessaView>> GetByNamesAsync(IEnumerable<string> viewsNames, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public ValueTask<IReadOnlyList<ITessaView>> GetByReferencesAsync(string referenceName, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public ValueTask<IReadOnlyList<ITessaView>> GetByReferencesAsync([NotNull] IEnumerable<string> refSection, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class NullSession : ISession
    { // ReSharper disable UnassignedGetOnlyAutoProperty
        public Guid ID { get; }
        public Guid? ApplicationID { get; }
        public IUser User { get; } = new User(new Guid(0x3db19fa0, 0x228a, 0x497f, 0x87, 0x3a, 0x02, 0x50, 0xbf, 0x0a, 0x4c, 0xcb), "Admin", UserAccessLevel.Administrator);
        public string ServerCode { get; }
        public string InstanceName { get; }
        public CultureInfo ClientCulture { get; }
        public CultureInfo ClientUICulture { get; }
        public TimeSpan ClientUtcOffset { get; }
        public SessionType Type { get; }
        public ISessionToken Token { get; }
    }

    public class NullSchemeTypeConverter : ISchemeTypeConverter
    {
        private static readonly Dictionary<string, SchemeType> sqlServerTypeToSchemeType =
            new Dictionary<string, SchemeType>(StringComparer.OrdinalIgnoreCase)
            {
                {"bigint", SchemeType.Int64},
                {"binary", SchemeType.Binary},
                {"bit", SchemeType.Boolean},
                {"char", SchemeType.AnsiStringFixedLength},
                {"date", SchemeType.Date},
                {"datetime", SchemeType.DateTime},
                {"datetime2", SchemeType.DateTime2},
                {"datetimeoffset", SchemeType.DateTimeOffset},
                {"decimal", SchemeType.Decimal},
                {"float", SchemeType.Double},
                {"image", new SchemeType(SchemeType.Binary, -1L, false)},
                {"int", SchemeType.Int32},
                {"money", SchemeType.Decimal},
                {"nchar", SchemeType.StringFixedLength},
                {"ntext", SchemeType.String},
                {"numeric", SchemeType.Decimal},
                {"nvarchar", SchemeType.String},
                {"nvarchar(max)", SchemeType.String},
                {"real", SchemeType.Single},
                {"rowversion", new SchemeType(SchemeType.Binary, 8L, false)},
                {"smalldatetime", SchemeType.DateTime},
                {"smallint", SchemeType.Int16},
                {"smallmoney", SchemeType.Decimal},
                {"text", SchemeType.AnsiString},
                {"time", SchemeType.Time},
                {"timestamp", new SchemeType(SchemeType.Binary, 8L, false)},
                {"tinyint", SchemeType.Byte},
                {"uniqueidentifier", SchemeType.Guid},
                {"varbinary", new SchemeType(SchemeType.Binary, -1L, false)},
                {"varbinary(max)", new SchemeType(SchemeType.Binary, -1L, false)},
                {"varchar", SchemeType.AnsiString},
                {"varchar(max)", SchemeType.AnsiString},
                {"xml", SchemeType.Xml}
            };

        public async ValueTask<SchemeType> TryGetAsync(string typeName, Dbms dbms, CancellationToken cancellationToken = default)
            => !string.IsNullOrWhiteSpace(typeName)
                ? sqlServerTypeToSchemeType.TryGetValue(typeName, out var result)
                    ? result
                    : new SchemeType(SchemeType.String, true)
                : null;

        public string TryGetSqlTypeName(SchemeType schemeType, Dbms dbms) => schemeType?.Name ?? "empty";
    }

    public class ControlTypesVisitor : CardTypeVisitor
    {
        public readonly Dictionary<string, string> ControlsData = new();
        public readonly Dictionary<string, string> BlocksData = new();
        public readonly Dictionary<string, string> FormsData = new();
        public readonly Dictionary<string, string> TabsData = new();

        public ControlTypesVisitor(IValidationResultBuilder validationResult) : base(validationResult) { }

        public override async ValueTask VisitControlAsync(CardTypeControl control, CardTypeBlock block, CardTypeForm form, CardType type, CancellationToken cancellationToken = default)
        {
            if (form is CardTypeNamedForm namedForm)
            {
                if (!string.IsNullOrWhiteSpace(namedForm.Name))
                {
                    this.FormsData.TryAdd(namedForm.Name, namedForm.TabCaption);
                }
            }
            else if (form is CardTypeTabControlForm tabForm)
            {
                if (!string.IsNullOrWhiteSpace(tabForm.Name))
                {
                    this.BlocksData.TryAdd(tabForm.Name, tabForm.TabCaption);
                }
            }

            if (!string.IsNullOrWhiteSpace(block.Name))
            {
                this.BlocksData.TryAdd(block.Name, block.Caption);
            }

            var name = control.Name;
            if (!string.IsNullOrWhiteSpace(name)
                && !name.Contains("[", StringComparison.OrdinalIgnoreCase))
            {
                this.ControlsData.TryAdd(name, control.Caption);
            }
            await base.VisitControlAsync(control, block, form, type, cancellationToken);
        }
    }

    //http://37.18.79.41:3128
    public static class Translate
    {
        private const string YandexKey = "trnsl.1.1.20200130T135439Z.2d388541d73b3018.66df2b140acead938a7a4661739f433c804ff403";
        private const string Lang = "ru-en";
        public static string Do(string text)
        {
            var request = WebRequest.Create(string.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&text={1}&lang={2}", YandexKey, text, Lang));
            var response = request.GetResponse();

            using StreamReader sr = new StreamReader(response.GetResponseStream()!);
            var tmp = JsonConvert.DeserializeObject<JToken>(sr.ReadToEnd());
            var translation = tmp["text"][0].ToString();
            return ToName(translation);
        }

        public static string ToName(string name)
        {
            name = (char.ToUpper(name[0]) + name.Substring(1))
                .Replace(",", " ", StringComparison.OrdinalIgnoreCase)
                .Replace(".", " ", StringComparison.OrdinalIgnoreCase)
                .Replace("?", " ", StringComparison.OrdinalIgnoreCase)
                .Replace(";", "", StringComparison.OrdinalIgnoreCase)
                .Replace(":", "", StringComparison.OrdinalIgnoreCase)
                .Replace("!", "", StringComparison.OrdinalIgnoreCase)
                .Replace("@", "", StringComparison.OrdinalIgnoreCase)
                .Replace("#", "", StringComparison.OrdinalIgnoreCase)
                .Replace("$", "", StringComparison.OrdinalIgnoreCase)
                .Replace("%", "Procent", StringComparison.OrdinalIgnoreCase)
                .Replace("^", "", StringComparison.OrdinalIgnoreCase)
                .Replace("&", "", StringComparison.OrdinalIgnoreCase)
                .Replace("*", "", StringComparison.OrdinalIgnoreCase)
                .Replace("(", "", StringComparison.OrdinalIgnoreCase)
                .Replace(")", "", StringComparison.OrdinalIgnoreCase)
                .Replace("{", "", StringComparison.OrdinalIgnoreCase)
                .Replace("}", "", StringComparison.OrdinalIgnoreCase)
                .Replace("-", " ", StringComparison.OrdinalIgnoreCase)
                .Replace("+", "", StringComparison.OrdinalIgnoreCase)
                .Replace("=", "", StringComparison.OrdinalIgnoreCase)
                .Replace("'", "", StringComparison.OrdinalIgnoreCase)
                .Replace("[", "", StringComparison.OrdinalIgnoreCase)
                .Replace("]", "", StringComparison.OrdinalIgnoreCase)
                .Replace("<", "", StringComparison.OrdinalIgnoreCase)
                .Replace(">", "", StringComparison.OrdinalIgnoreCase)
                .Replace("\"", "", StringComparison.OrdinalIgnoreCase)
                .Replace("\\", "", StringComparison.OrdinalIgnoreCase)
                .Replace("/", "", StringComparison.OrdinalIgnoreCase)
                .Trim();
            int index;
            while ((index = name.IndexOf(" ", StringComparison.OrdinalIgnoreCase)) != -1)
            {
                var nextChar = name[index + 1];
                name = name.Remove(index, 2);
                name = name.Insert(index, nextChar.ToString().ToUpperInvariant());
            }

            return name;
        }

        // This is the set of types from the C# keyword list.
        private static readonly Dictionary<Type, string> typeAlias = new()
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },

            // Yes, this is an odd one.  Technically it's a type though.
            { typeof(void), "void" }
        };

        // This is the set of types from the C# keyword list.
        private static readonly Dictionary<Type, string> typeAliasWeb = new()
        {
            { typeof(Guid), "guid" },
            { typeof(bool), "boolean" },
            { typeof(byte), "symbol" },
            { typeof(char), "symbol" },
            { typeof(decimal), "number" },
            { typeof(double), "double" },
            { typeof(float), "number" },
            { typeof(int), "number" },
            { typeof(long), "bigint" },
            { typeof(object), "object" },
            { typeof(sbyte), "symbol" },
            { typeof(short), "number" },
            { typeof(string), "string" },
            { typeof(uint), "number" },
            { typeof(ulong), "bigint" },

            // Yes, this is an odd one.  Technically it's a type though.
            { typeof(void), "void" }
        };

        public static bool IsNotNull(string column, SchemeRecordCollection records)
        {
            foreach (var record in records)
            {
                var value = record[column];
                if (value == null || value is string s && string.IsNullOrWhiteSpace(s))
                {
                    return false;
                }
            }

            return true;
        }

        public static string AliasName(string name)
        {
            name = name
                .Replace("\"", string.Empty, StringComparison.InvariantCultureIgnoreCase)
                .Replace("\'", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            if (Regex.Matches(name, @"\p{IsCyrillic}").Count > 0)
            {
                name = Transliteration.Front(name);
            }

            var index = name.LastIndexOf("_", StringComparison.CurrentCultureIgnoreCase);
            if (name.StartsWith("$") && index != -1 && name.Length - 1 > index)
            {
                name = name[(index + 1)..];
            }

            name = name.Replace("'s", string.Empty, StringComparison.Ordinal);

            return ToName(name);
        }

        public static string UniqueAliasName(List<string> aliases, string alias)
        {
            if (!aliases.Contains(alias))
            {
                return alias;
            }

            var cnt = 1;
            do
            {
                cnt++;
            }
            while (aliases.Contains(alias + cnt));

            return alias + cnt;
        }

        public static string ToSingleLine(string name)
            => name
                .Replace("\r", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("\n", " ", StringComparison.OrdinalIgnoreCase)
                .Replace("\"", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Trim();

        public static string ToMultiLine(string name, int ident = 1, bool forWeb = false)
            => name
                .Replace("\r", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace(" \n", "\n", StringComparison.OrdinalIgnoreCase)
                .Replace(forWeb ? "\n\n" : "\n", "\n", StringComparison.OrdinalIgnoreCase)
                .Replace("\n", $"\n{string.Join(string.Empty, Enumerable.Range(1, ident).Select(x => forWeb ? " " : "    ").ToArray())}{(forWeb ? "* " : "///                  ")}", StringComparison.OrdinalIgnoreCase)
                .Trim();

        public static string TypeNameOrAlias(Type type, bool forWeb = false)
        {
            // Handle nullable value types
            var nullBase = Nullable.GetUnderlyingType(type);
            if (nullBase is not null)
            {
                return TypeNameOrAlias(nullBase) + "?";
            }

            // Handle arrays
            if (type.BaseType == typeof(Array))
            {
                return TypeNameOrAlias(type.GetElementType()) + "[]";
            }

            if (forWeb)
            {
                // Lookup alias for type
                return typeAliasWeb.TryGetValue(type, out var webAlias)
                    ? webAlias
                    : type.Name; // Default to CLR type name
            }

            // Lookup alias for type
            return typeAlias.TryGetValue(type, out var alias)
                ? alias
                : type.Name; // Default to CLR type name
        }

        public static string GetNullable(Type type, bool forWeb = false) => forWeb
            ? Nullable.GetUnderlyingType(type) != null || type == typeof(string)
                ? " | null"
                : string.Empty
            : Nullable.GetUnderlyingType(type) != null
                ? "?"
                : string.Empty;

        public static readonly char[] InvalidNameChars = { ',', '.', '?', ';', ':', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '{', '}', '-', '+', '=', '\'', '[', ']', '<', '>', '\"', '\\', '/' };
    }

    public static class Transliteration
    {
        private static readonly Dictionary<string, string> iso = new Dictionary<string, string>
        {
            {"Є", "YE"},
            {"І", "I"},
            {"Ѓ", "G"},
            {"і", "i"},
            {"№", "#"},
            {"є", "ye"},
            {"ѓ", "g"},
            {"А", "A"},
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ё", "YO"},
            {"Ж", "ZH"},
            {"З", "Z"},
            {"И", "I"},
            {"Й", "Y"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "H"},
            {"Ц", "C"},
            {"Ч", "CH"},
            {"Ш", "SH"},
            {"Щ", "SHH"},
            {"Ъ", ""},
            {"Ы", "I"},
            {"Ь", ""},
            {"Э", "E"},
            {"Ю", "YU"},
            {"Я", "YA"},
            {"а", "a"},
            {"б", "b"},
            {"в", "v"},
            {"г", "g"},
            {"д", "d"},
            {"е", "e"},
            {"ё", "yo"},
            {"ж", "zh"},
            {"з", "z"},
            {"и", "i"},
            {"й", "y"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "h"},
            {"ц", "c"},
            {"ч", "ch"},
            {"ш", "sh"},
            {"щ", "shh"},
            {"ъ", ""},
            {"ы", "i"},
            {"ь", ""},
            {"э", "e"},
            {"ю", "yu"},
            {"я", "ya"},
            {"«", ""},
            {"»", ""},
            {"—", "-"}
        };

        public static string Front(string text) => iso.Aggregate(text, (current, key) => current.Replace(key.Key, key.Value, StringComparison.Ordinal));

        public static string Back(string text) => iso.Aggregate(text, (current, key) => current.Replace(key.Value, key.Key, StringComparison.Ordinal));
    }

    public class SchemeTypeComparer : IComparer<SchemeType>
    {
        public static bool IsStringType(SchemeDbType type) =>
            type is SchemeDbType.AnsiString
                or SchemeDbType.AnsiStringFixedLength
                or SchemeDbType.String
                or SchemeDbType.StringFixedLength;

        public static bool IsIntegerType(SchemeDbType type) =>
            type is SchemeDbType.Byte
                or SchemeDbType.SByte
                or SchemeDbType.Int16
                or SchemeDbType.UInt16
                or SchemeDbType.Int32
                or SchemeDbType.UInt32
                or SchemeDbType.Int64
                or SchemeDbType.UInt64;

        /// <inheritdoc />
        public int Compare(SchemeType s1, SchemeType s2)
        {
            if (s1 == null)
            {
                return -1;
            }
            if (s2 == null)
            {
                return 1;
            }
            if (IsStringType(s1.DbType) && IsStringType(s2.DbType))
            {
                return 0;
            }
            if (IsStringType(s1.DbType) && !IsStringType(s2.DbType))
            {
                return 1;
            }
            if (!IsStringType(s1.DbType) && IsStringType(s2.DbType))
            {
                return -1;
            }

            if (IsIntegerType(s1.DbType) && IsIntegerType(s2.DbType))
            {
                return 0;
            }
            if (IsIntegerType(s1.DbType) && !IsIntegerType(s2.DbType))
            {
                return 1;
            }
            if (!IsIntegerType(s1.DbType) && IsIntegerType(s2.DbType))
            {
                return -1;
            }

            return 0;
        }
    }

    public static class TypeExtensions
    {
        public static string ToGenString(this Guid guid)
            => guid
                .ToString("X")
                .Replace("{", string.Empty, StringComparison.InvariantCultureIgnoreCase)
                .Replace("}", string.Empty, StringComparison.InvariantCultureIgnoreCase);

        public static string ToGenString(this string str)
            => str
                .Replace("\\", "\\\\", StringComparison.InvariantCultureIgnoreCase)
                .Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase);

        public static string ToFirstLower(this string str)
            => str.Length <= 1 ? str.ToLowerInvariant() : str[..1].ToLower() + str[1..];
    }
}