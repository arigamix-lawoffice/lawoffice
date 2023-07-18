using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Тип карточки или документа, включенный в типовое решение
    /// </summary>
    public interface IKrType
    {
        /// <summary>
        /// ИД типа
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// Имя типа
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Отображаемое имя типа
        /// </summary>
        string Caption { get; }
        /// <summary>
        /// Использовать согласование
        /// </summary>
        bool UseApproving { get; }
        /// <summary>
        /// Использовать регистрацию
        /// </summary>
        bool UseRegistration { get; }
        /// <summary>
        /// Использовать резолюции
        /// </summary>
        bool UseResolutions { get; }
        /// <summary>
        /// Признак того, что требуется выполнить проверку на дату запланированного завершения
        /// дочерней резолюции, чтобы она не превышала дату завершения родительской резолюции более, чем на один бизнес-день.
        /// </summary>
        bool DisableChildResolutionDateCheck { get; }
        /// <summary>
        /// Вариант автоматического выделения номера
        /// </summary>
        KrDocNumberRegularAutoAssignmentID DocNumberRegularAutoAssignmentID { get; }
        /// <summary>
        /// Последовательность для выделения номера
        /// </summary>
        string DocNumberRegularSequence { get; }
        /// <summary>
        /// Формат выделяемого номера
        /// </summary>
        string DocNumberRegularFormat { get; }
        /// <summary>
        /// Разрешить ручное выделение номера
        /// </summary>
        bool AllowManualRegularDocNumberAssignment { get; }
        /// <summary>
        /// Вариант автоматического выделения номера при регистрации
        /// </summary>
        KrDocNumberRegistrationAutoAssignmentID DocNumberRegistrationAutoAssignmentID { get; }
        /// <summary>
        /// Последовательность для веделения номера при регистрации
        /// </summary>
        string DocNumberRegistrationSequence { get; }
        /// <summary>
        /// Формат выделяемого номера при регистрации
        /// </summary>
        string DocNumberRegistrationFormat { get; }
        /// <summary>
        /// Разрешить ручное выделение номера при регистрации
        /// </summary>
        bool AllowManualRegistrationDocNumberAssignment { get; }
        /// <summary>
        /// Флаг освобождения первичного номера при окончательном удалении
        /// </summary>
        bool ReleaseRegistrationNumberOnFinalDeletion { get; }
        /// <summary>
        /// Флаг освобождения вторичного номера при окончательном удалении
        /// </summary>
        bool ReleaseRegularNumberOnFinalDeletion { get; }
        /// <summary>
        /// Флаг скрытия плитки создания карточки для пользователя
        /// </summary>
        bool HideCreationButton { get; }
        /// <summary>
        /// Флаг скрытия вкладки маршрута
        /// </summary>
        bool HideRouteTab { get; }

        /// <summary>
        /// Флаг использовать ли систему форумов
        /// </summary>
        bool UseForum { get; }
        
        /// <summary>
        /// Флаг, при использовании системы форумов использовать ли стандартную вкладку с обсуждеиями
        /// </summary>
        bool UseDefaultDiscussionTab { get; }

        /// <summary>
        /// Возвращает значение, показывающее, используются ли маршруты в бизнес процессах или нет.
        /// </summary>
        bool UseRoutesInWorkflowEngine { get; }
    }
}
