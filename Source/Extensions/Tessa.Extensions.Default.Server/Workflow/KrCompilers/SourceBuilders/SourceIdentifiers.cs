#nullable enable

using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    /// <summary>
    /// Содержит идентификаторы используемые при компиляции скриптов маршрутов.
    /// </summary>
    public static class SourceIdentifiers
    {
        /// <summary>
        /// Имя типа возвращаемого по умолчанию дополнительным методом.
        /// </summary>
        public const string Void = "void";

        /// <summary>
        /// Полное имя базового класса для сгенерированных классов содержащих скрипты.
        /// </summary>
        public static readonly string BaseClass = typeof(KrScript).FullName!;

        /// <summary>
        /// Имя пространства имён, в котором расположен сгенерированный класс.
        /// </summary>
        public const string Namespace = "Tessa.Generated.Kr";

        /// <summary>
        /// Имя базового класса для классов, содержащий методы расширения.
        /// </summary>
        public const string KrStageCommonClass = "KrStageCommon";

        /// <summary>
        /// Имя класса, содержащего сценарий построения маршрута.
        /// </summary>
        public const string KrDesignTimeClass = "KrDesignTime";

        /// <summary>
        /// Имя класса, содержащего сценарий выполнения маршрута.
        /// </summary>
        public const string KrRuntimeClass = "KrRuntime";

        /// <summary>
        /// Имя класса, содержащего сценарий условия видимости.
        /// </summary>
        public const string KrVisibilityClass = "KrVisibility";

        /// <summary>
        /// Имя класса, содержащего сценарий условия выполнимости процесса.
        /// </summary>
        public const string KrExecutionClass = "KrExecution";

        /// <summary>
        /// Имя метода, содержащего условие выполнения.
        /// </summary>
        public const string ConditionMethod = nameof(IKrScript.ConditionAsync);

        /// <summary>
        /// Имя метода, содержащего скрипт инициализации.
        /// </summary>
        public const string BeforeMethod = nameof(IKrScript.BeforeAsync);

        /// <summary>
        /// Имя метода, содержащего скрипт постобработки.
        /// </summary>
        public const string AfterMethod = nameof(IKrScript.AfterAsync);

        /// <summary>
        /// Имя метода, содержащего условие видимости.
        /// </summary>
        public const string VisibilityMethod = nameof(IKrScript.VisibilityAsync);

        /// <summary>
        /// Имя метода, содержащего условие выполнимости процесса.
        /// </summary>
        public const string ExecutionMethod = nameof(IKrScript.ExecutionAsync);

        /// <summary>
        /// Имя параметра по умолчанию для дополнительных методов.
        /// </summary>
        public const string DefaultExtraMethodParameterName = "Context";

        /// <summary>
        /// Алиас генерируемого класса, содержащего скрипты этапа.
        /// </summary>
        public const string StageAlias = "Stage";

        /// <summary>
        /// Алиас генерируемого класса, содержащего скрипты шаблона этапов.
        /// </summary>
        public const string TemplateAlias = "Template";

        /// <summary>
        /// Алиас генерируемого класса, содержащего скрипты группы этапов.
        /// </summary>
        public const string GroupAlias = "Group";

        /// <summary>
        /// Алиас генерируемого класса, содержащего скрипты вторичного процесса.
        /// </summary>
        public const string SecondaryProcessAlias = "SecondaryProcess";

    }
}
