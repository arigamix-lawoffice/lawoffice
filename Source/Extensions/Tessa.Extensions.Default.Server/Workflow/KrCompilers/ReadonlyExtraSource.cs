using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Неизменяемый объект предоставляющий информацию о дополнительном методе.
    /// </summary>
    public sealed class ReadonlyExtraSource : IExtraSource
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ReadonlyExtraSource"/>.
        /// </summary>
        /// <param name="displayName">Отображаемое имя метода.</param>
        /// <param name="name">Имя метода.</param>
        /// <param name="returnType">Тип возвращаемого значения.</param>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="source">Исходный код.</param>
        public ReadonlyExtraSource(
            string displayName,
            string name,
            string returnType,
            string parameterType,
            string parameterName,
            string source)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.ReturnType = returnType;
            this.ParameterType = parameterType;
            this.ParameterName = parameterName;
            this.Source = source;
        }

        /// <inheritdoc />
        public string DisplayName { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string ReturnType { get; }

        /// <inheritdoc />
        public string ParameterType { get; }

        /// <inheritdoc />
        public string ParameterName { get; }

        /// <inheritdoc />
        public string Source { get; }

        /// <inheritdoc />
        public IExtraSource ToMutable() =>
            new ExtraSource(
                this.DisplayName,
                this.Name,
                this.ReturnType, 
                this.ParameterType,
                this.ParameterName, 
                this.Source);

        /// <inheritdoc />
        public IExtraSource ToReadonly() => this;
    }
}