using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Информация по сериализуемым секциям и полям используемая сериализатором <see cref="IKrStageSerializer"/>.
    /// </summary>
    public sealed class KrStageSerializerData
    {
        /// <summary>
        /// Список секций, содержащих параметры этапов.
        /// </summary>
        public List<string> SettingsSectionNames { get; } = new List<string>();

        /// <summary>
        /// Список полей, содержащих параметры этапов.
        /// </summary>
        public List<string> SettingsFieldNames { get; } = new List<string>();

        /// <summary>
        /// Список прямых дочерних секций секции <see cref="KrConstants.KrStages.Name"/>.
        /// </summary>
        public List<ReferenceToStage> ReferencesToStages { get; } = new List<ReferenceToStage>();

        /// <summary>
        /// Список секций и столбцов, по которым выполняется сортировка.
        /// </summary>
        public List<OrderColumn> OrderColumns { get; } = new List<OrderColumn>();
    }
}