// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceScanSourceViewModel.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Scanning
{
    using Tessa.Host;
    using Tessa.Platform;
    using Tessa.Properties.Resharper;
    using Tessa.UI;

    /// <summary>
    ///     Модель-представление источника сканирования полученного от сервиса
    /// сканирования.
    /// </summary>
    public sealed class ServiceScanSourceViewModel :
        ViewModel<EmptyModel>,
        IScanSource
    {
        /// <summary>
        ///     The data source.
        /// </summary>
        [NotNull]
        private readonly IScanSource dataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceScanSourceViewModel"/> class.
        /// </summary>
        /// <param name="dataSource">
        /// Источник сканирования
        /// </param>
        public ServiceScanSourceViewModel([NotNull] IScanSource dataSource)
        {
            Check.ArgumentNotNull(dataSource, "dataSource");

            this.dataSource = dataSource;
        }

        /// <inheritdoc />
        public string DriverVersion
        {
            get
            {
                return this.dataSource.DriverVersion;
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int ID
        {
            get
            {
                return this.dataSource.ID;
            }
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.dataSource.Name;
            }
        }

        /// <inheritdoc />
        public string ProtocolVersion
        {
            get
            {
                return this.dataSource.ProtocolVersion;
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this.Equals(obj as IScanSource);
        }

        /// <summary>
        /// Осуществляет сравнение эквивалентности объектов
        /// </summary>
        /// <param name="other">
        /// Сравниваемый объект
        /// </param>
        /// <returns>
        /// Результат сравнения
        /// </returns>
        public bool Equals(IScanSource other)
        {
            // каждый раз, когда запрашиваются новый ScanSource-ы из драйвера, у них будут новые ID, поэтому,
            // чтобы восстановить предыдущий использованный сканер, сравниваем его по имени и другим показателям

            return other is ServiceScanSourceViewModel typedSource
                && this.Name == typedSource.Name;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}