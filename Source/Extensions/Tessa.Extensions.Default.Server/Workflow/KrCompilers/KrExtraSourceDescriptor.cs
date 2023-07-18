using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Дескриптор дополнительного метода этапа.
    /// </summary>
    public sealed class KrExtraSourceDescriptor
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrExtraSourceDescriptor"/>.
        /// </summary>
        /// <param name="methodName">Имя метода.</param>
        public KrExtraSourceDescriptor(
            string methodName)
        {
            this.MethodName = methodName;
            this.ReturnType = SourceIdentifiers.Void;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает имя метода.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Возвращает или задаёт отображаемое имя метода.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Возвращает или задаёт тип возвращаемого результата. По умолчанию имеет значение <see cref="SourceIdentifiers.Void"/>.
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя параметра.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Возвращает или задаёт тип параметра.
        /// </summary>
        public string ParameterType { get; set; }

        /// <summary>
        /// Возвращает или задаёт название поля содержащего тело метода в параметрах этапа.
        /// </summary>
        public string ScriptField { get; set; }

        #endregion
    }
}
