using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class StageTypeDescriptorBuilder
    {
        /// <summary>
        /// Уникальный идентификатор дескриптора типа этапа.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Название типа этапа.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Стандартное название для нового этапа.
        /// </summary>
        public string DefaultStageName { get; set; }

        /// <summary>
        /// Идентификатор типа карточки настроек.
        /// </summary>
        public Guid? SettingsCardTypeID { get; set; }

        /// <summary>
        /// Режим использования стандартного поля с исполнителями.
        /// </summary>
        public PerformerUsageMode PerformerUsageMode { get; set; }

        /// <summary>
        /// Проверить наличие хотя бы одного исполнителя при редактировании в UI и перед стартом этапа.
        /// </summary>
        public bool PerformerIsRequired { get; set; }

        /// <summary>
        /// Заголовок элемента управления исполнителя/исполнителей.
        /// </summary>
        public string PerformerCaption { get; set;  }

        /// <summary>
        /// Использовать поле "От имени"
        /// </summary>
        public bool CanOverrideAuthor { get; set; }

        /// <summary>
        /// Использовать поле "Срок"
        /// </summary>
        public bool UseTimeLimit { get; set; }

        /// <summary>
        /// Использовать поле "Дата выполнения"
        /// </summary>
        public bool UsePlanned { get; set; }
        
        /// <summary>
        /// Разрешить скрывать этап в документах.
        /// </summary>
        public bool CanBeHidden { get; set; }

        /// <summary>
        /// Разрешить переопределять группу истории заданий в настройках этапа.
        /// </summary>
        public bool CanOverrideTaskHistoryGroup { get; set; }

        /// <summary>
        /// Разрешить выбирать вид задания.
        /// </summary>
        public bool UseTaskKind { get; set; }
        
        /// <summary>
        /// Поддерживаемые режимы выполнения.
        /// </summary>
        public List<KrProcessRunnerMode> SupportedModes { get; set; } = new List<KrProcessRunnerMode>();

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, возможен ли пропуск этапа.
        /// </summary>
        public bool CanBeSkipped { get; set; }
    }
}