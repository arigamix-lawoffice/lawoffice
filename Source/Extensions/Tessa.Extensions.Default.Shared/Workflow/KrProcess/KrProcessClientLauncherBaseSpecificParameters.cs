using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Описывает параметры запуска процесса используемые <see cref="KrProcessClientLauncherBase"/>.
    /// </summary>
    public class KrProcessClientLauncherBaseSpecificParameters :
        KrProcessLauncherSpecificParametersBase
    {
        #region Properties

        /// <summary>
        /// Возвращает или задаёт дополнительную информацию, передаваемую в запросе на сохранение карточки для расширений. Данные должны быть сериализуемых типов. Может иметь значение по умолчанию для типа.
        /// </summary>
        public IDictionary<string, object> RequestInfo { get; set; }

        #endregion
    }
}
