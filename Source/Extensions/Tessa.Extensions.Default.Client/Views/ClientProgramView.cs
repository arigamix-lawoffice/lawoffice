using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    ///     Пример программного представления.
    /// </summary>
    public sealed class ClientProgramView : ITessaView
    {
        #region Constants

        /// <summary>
        ///     Алиас представления
        /// </summary>
        private const string ViewAlias = "ClientProgramView";

        #endregion

        #region Fields

        /// <summary>
        ///     The cities.
        /// </summary>
        private readonly string[] cities =
        {
            "Москва", "Санкт-Петербург", "Новосибирск", "Екатиренбург",
            "Ростов-на-Дону"
        };

        /// <summary>
        ///     The creators.
        /// </summary>
        private readonly string[] creators = { "Admin", "System", "Иванов", "Петров", "Сидиров" };

        /// <summary>
        /// Данные представления.
        /// Ленивая инициализация используется только для уменьшения времени создания представления.
        /// </summary>
        private readonly Lazy<IEnumerable<DataRow>> data;

        /// <summary>
        ///     Метаданные представления.
        ///     Ленивая инциализация используется только уменьшения времени создания данного элемента.
        /// </summary>
        private readonly Lazy<IViewMetadata> metadata;

        /// <summary>
        ///     The names.
        /// </summary>
        private readonly string[] names =
        {
            "ООО Одуванчик", "ОАО Фиалка", "ОАО Фиалка-1", "ОАО Фиалка-2", "ЗАО Георгин",
            "ЗАО Георгины", "ООО Гвоздика", "ЧП Роза"
        };

        /// <summary>
        ///     The streets.
        /// </summary>
        private readonly string[] streets =
        {
            "ул. Васильков", "ул. Лютиков", "ул. им. Серени", "ул. им Дефенбахии",
            "пер. Розовый"
        };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientProgramView" /> class.
        ///     Инициализирует новый экземпляр класса <see cref="T:System.Object" />.
        /// </summary>
        public ClientProgramView()
        {
            this.metadata = new Lazy<IViewMetadata>(InitializeMetadata);
            this.data = new Lazy<IEnumerable<DataRow>>(this.GenerateRandomData);
        }

        #endregion

        #region Properties

        public IViewMetadata Metadata => this.metadata.Value;

        /// <summary>
        ///     Gets Данные представления.
        ///     Генерация данных осуществляется при первом обращении к данному свойству.
        /// </summary>
        private IEnumerable<DataRow> Data => this.data.Value;

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public ValueTask<IViewMetadata> GetMetadataAsync(CancellationToken cancellationToken = default) =>
            new ValueTask<IViewMetadata>(this.Metadata);

        /// <summary>
        /// Выполняет получение данных из представеления
        ///     на основании полученного <see cref="ITessaViewRequest">запроса</see>
        /// </summary>
        /// <param name="request">
        /// Запрос к представлению
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <see cref="ITessaViewResult">Результат</see> выполнения запроса
        /// </returns>
        public async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (request.View == null || !ParserNames.IsEquals(request.View.Alias, ViewAlias))
            {
                return null;
            }

            return this.GetResult(this.GetFilteringOperation(request), this.Data);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Создает и возвращает метаданные представления
        /// </summary>
        /// <returns>Метаданные представления</returns>
        private static IViewMetadata InitializeMetadata()
        {
            var metadata = new ViewMetadata { Alias = ViewAlias, Caption = "Клиентское программное представление" };

            metadata.Columns.Add(new ViewColumnMetadata { Alias = "CustomerID", Caption = "Идентификатор" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "CustomerName", Caption = "Название" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "City", Caption = "Город" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "Street", Caption = "Улица" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "Number", Caption = "Номер" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "CreationDate", Caption = "Дата создания" });
            metadata.Columns.Add(new ViewColumnMetadata { Alias = "Creator", Caption = "Создатель" });

            metadata.Parameters.Add(
                new ViewParameterMetadata
                {
                    Alias = "Name",
                    Caption = "Название",
                    Multiple = false,
                    RefSection = new[] { "Customer" },
                    SchemeType = SchemeType.String
                });

            metadata.References.Add(
                new ViewReferenceMetadata
                {
                    ColPrefix = "Customer",
                    DisplayValueColumn = "CustomerName",
                    RefSection = new[] { "customer", "partner" },
                    IsCard = false,
                    OpenOnDoubleClick = false
                });
            metadata.Seal();
            return metadata;
        }

        /// <summary>
        ///     Осуществляет генерацию рандомных данных
        /// </summary>
        /// <returns>
        ///     Список сгенерированных данных
        /// </returns>
        private IEnumerable<DataRow> GenerateRandomData()
        {
            var random = new Random(int.MaxValue);
            var result = new List<DataRow>();
            for (int i = 0; i < random.Next(100); i++)
            {
                result.Add(new DataRow(this.names, this.cities, this.streets, this.creators));
            }

            return result;
        }

        /// <summary>
        /// Возвращает операцию фильтрации представлению.
        ///     Поддерживаются операции равенство, диапазон, содержимое.
        ///     Для остальных операций будет возвращен весь список элементов
        /// </summary>
        /// <param name="request">
        /// Запрос к представлению
        /// </param>
        /// <returns>
        /// Операция фильтрации
        /// </returns>
        private FilterOperation GetFilteringOperation(ITessaViewRequest request)
        {
            var noFilterOperation = new NoFilterOperation();
            var parameter = request.TryGetParameter("Name");
            if (parameter == null)
            {
                return noFilterOperation;
            }

            var criteria = parameter.CriteriaValues.FirstOrDefault();
            if (criteria == null)
            {
                return noFilterOperation;
            }

            if (criteria.CriteriaName == CriteriaOperatorConst.Equality)
            {
                return new EqualsFilterOperation { Text = criteria.Values.First().Text };
            }

            if (criteria.CriteriaName == CriteriaOperatorConst.Between)
            {
                return new BetweenFilterOperation
                {
                    StartText = criteria.Values[0].Text,
                    EndText = criteria.Values[1].Text
                };
            }

            if (criteria.CriteriaName == CriteriaOperatorConst.Contains)
            {
                return new ContainsFilterOperation { Text = criteria.Values.First().Text };
            }

            return noFilterOperation;
        }

        /// <summary>
        /// Осуществляет фильтрацию данных <paramref name="data"/> в соответствии
        ///     с операцией фильтрации <paramref name="filterOperation"/>
        /// </summary>
        /// <param name="filterOperation">
        /// Операция фильтрации
        /// </param>
        /// <param name="data">
        /// Список входящих данных
        /// </param>
        /// <returns>
        /// Результат отбора данных
        /// </returns>
        private ITessaViewResult GetResult(FilterOperation filterOperation, IEnumerable<DataRow> data)
        {
            var filteredData = data.Where(filterOperation.IsSatisfy).ToArray();
            var result = new TessaViewResult
            {
                Columns = (from c in this.Metadata.Columns select (object) c.Alias).ToList(),
                DataTypes =
                    new object[]
                    {
                        "uniqueidentifier", "nvarchar", "nvarchar", "nvarchar", "int", "datetime",
                        "nvarchar"
                    },
                SchemeTypes = new[]
                {
                    SchemeType.Guid, SchemeType.String, SchemeType.String, SchemeType.String,
                    SchemeType.Int32, SchemeType.DateTime, SchemeType.String
                },
                Rows = (from d in filteredData select d.ToArray()).ToList()
            };

            return result;
        }

        #endregion

        /// <summary>
        ///     Операция фильтрации, отбирающая диапазон значений, удовлетворяющих условию.
        /// </summary>
        private sealed class BetweenFilterOperation : FilterOperation
        {
            #region Public Properties

            /// <summary>
            ///     Gets or sets Текст фильтра - окончание диапазона
            /// </summary>
            public string EndText { get; set; }

            /// <summary>
            ///     Gets or sets Текст фильтра - начало диапазона
            /// </summary>
            public string StartText { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Осуществляет проверку строки на соответствие условиям
            /// </summary>
            /// <param name="row">
            /// Строка данных
            /// </param>
            /// <returns>
            /// True - строка удовлетворяет условиям, False - строка не удовлетворяет условиям
            /// </returns>
            public override bool IsSatisfy(DataRow row)
            {
                return string.Compare(this.StartText, row.Name, StringComparison.Ordinal) <= 0
                    && string.Compare(this.EndText, row.Name, StringComparison.Ordinal) >= 0;
            }

            #endregion
        }

        /// <summary>
        ///     Операция фильтрации данных по содержимому строки.
        /// </summary>
        private sealed class ContainsFilterOperation : FilterOperation
        {
            #region Public Properties

            /// <summary>
            ///     Gets or sets Текст фильтра
            /// </summary>
            public string Text { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Осуществляет проверку строки на соответствие условиям
            /// </summary>
            /// <param name="row">
            /// Строка данных
            /// </param>
            /// <returns>
            /// True - строка удовлетворяет условиям, False - строка не удовлетворяет условиям
            /// </returns>
            public override bool IsSatisfy(DataRow row)
            {
                return row.Name.Contains(this.Text, StringComparison.Ordinal);
            }

            #endregion
        }

        /// <summary>
        ///     Данные строки.
        /// </summary>
        private sealed class DataRow
        {
            #region Static Fields

            /// <summary>
            /// The random.
            /// </summary>
            private static readonly Random random = new Random(int.MaxValue);

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="DataRow"/> class.
            ///     Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            /// <param name="names">
            /// Список имен
            /// </param>
            /// <param name="cities">
            /// Список городов
            /// </param>
            /// <param name="streets">
            /// Список улиц
            /// </param>
            /// <param name="creators">
            /// Список создателей записи
            /// </param>
            public DataRow(string[] names, string[] cities, string[] streets, string[] creators)
            {
                this.Name = names[random.Next(names.Length)];
                this.City = cities[random.Next(cities.Length)];
                this.Street = streets[random.Next(streets.Length)];
                this.Creator = creators[random.Next(creators.Length)];
                this.Number = random.Next();
                this.ID = Guid.NewGuid();
                this.CreationDate = DateTime.Now;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///     Gets or sets the city.
            /// </summary>
            public string City { get; set; }

            /// <summary>
            ///     Gets or sets the creation date.
            /// </summary>
            public DateTime CreationDate { get; set; }

            /// <summary>
            ///     Gets or sets the creator.
            /// </summary>
            public string Creator { get; set; }

            /// <summary>
            ///     Gets or sets the id.
            /// </summary>
            public Guid ID { get; set; }

            /// <summary>
            ///     Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///     Gets or sets the number.
            /// </summary>
            public int Number { get; set; }

            /// <summary>
            ///     Gets or sets the street.
            /// </summary>
            public string Street { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Преобразует содержимое объекта в строку данных результата выполенения представления
            /// </summary>
            /// <returns>
            ///     Результат преобразования
            /// </returns>
            public object ToArray()
            {
                return new object[]
                {
                    this.ID, this.Name, this.City, this.Street, this.Number, this.CreationDate, this.Creator
                };
            }

            #endregion
        }

        /// <summary>
        ///     Операция фильтраци по равенству.
        /// </summary>
        private sealed class EqualsFilterOperation : FilterOperation
        {
            #region Public Properties

            /// <summary>
            ///     Gets or sets Текст фильтра
            /// </summary>
            public string Text { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Осуществляет проверку строки на соответствие условиям
            /// </summary>
            /// <param name="row">
            /// Строка данных
            /// </param>
            /// <returns>
            /// True - строка удовлетворяет условиям, False - строка не удовлетворяет условиям
            /// </returns>
            public override bool IsSatisfy(DataRow row)
            {
                return row.Name == this.Text;
            }

            #endregion
        }

        /// <summary>
        ///     Базовый класс для операций фильтрации.
        /// </summary>
        private abstract class FilterOperation
        {
            #region Public Methods and Operators

            /// <summary>
            /// Осуществляет проверку строки на соответствие условиям
            /// </summary>
            /// <param name="row">
            /// Строка данных
            /// </param>
            /// <returns>
            /// True - строка удовлетворяет условиям, False - строка не удовлетворяет условиям
            /// </returns>
            public abstract bool IsSatisfy(DataRow row);

            #endregion
        }

        /// <summary>
        ///     Дефолтная операция фильтрации.
        /// </summary>
        private sealed class NoFilterOperation : FilterOperation
        {
            #region Public Methods and Operators

            /// <summary>
            /// Осуществляет проверку строки на соответствие условиям
            /// </summary>
            /// <param name="row">
            /// Строка данных
            /// </param>
            /// <returns>
            /// True - строка удовлетворяет условиям, False - строка не удовлетворяет условиям
            /// </returns>
            public override bool IsSatisfy(DataRow row)
            {
                return true;
            }

            #endregion
        }
    }
}