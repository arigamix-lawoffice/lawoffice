using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Объект, предоставляющий информацию этапе.
    /// </summary>
    public interface IKrRuntimeStage :
        IRuntimeSources,
        IExtraSources
    {
        /// <summary>
        /// Идентификатор шаблона этапов.
        /// </summary>
        Guid TemplateID { get; }

        /// <summary>
        /// Название шаблона этапов.
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Идентификатор группы, в которой находится шаблон, в котором находится этап.
        /// </summary>
        Guid GroupID { get; }

        /// <summary>
        /// Имя группы этапов, в которой находится шаблон, в котором находится этап.
        /// </summary>
        string GroupName { get; }

        /// <summary>
        /// Порядок группы, в которой находится шаблон, в котором находится этап.
        /// </summary>
        int GroupOrder { get; }

        /// <summary>
        /// Идентификатор этапа.
        /// </summary>
        Guid StageID { get; }

        /// <summary>
        /// Название этапа.
        /// </summary>
        string StageName { get; }

        /// <summary>
        /// Порядок этапа в шаблоне.
        /// </summary>
        int? Order { get; }

        /// <summary>
        /// Срок выполнения (рабочие дни).
        /// </summary>
        double? TimeLimit { get; }

        /// <summary>
        /// Дата выполнения.
        /// </summary>
        DateTime? Planned { get; }

        /// <summary>
        /// Этап является скрытым.
        /// </summary>
        bool Hidden { get; }

        /// <summary>
        /// Идентификатор типа этапа.
        /// </summary>
        Guid StageTypeID { get; }

        /// <summary>
        /// Отображаемое название типа этапа.
        /// </summary>
        string StageTypeCaption { get; }

        /// <summary>
        /// Запрос для вычисления SQL исполнителей.
        /// </summary>
        string SqlRoles { get; }

        /// <summary>
        /// Получить настройки этапа. Возвращает доступную для изменения копию настроек этапа.
        /// </summary>
        ValueTask<IDictionary<string, object>> GetSettingsAsync();

        /// <summary>
        /// Флаг пропуска этапа.
        /// </summary>
        bool Skip { get; }

        /// <summary>
        /// Возвращает значение признака, показывающего, разрешено ли пропускать этап.
        /// </summary>
        bool CanBeSkipped { get; }
    }
}
