using Tessa.Cards;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Вспомогательные методы для работы с ФРЗ.
    /// </summary>
    public static class TaskAssignedRolesHelper
    {
        /// <summary>
        /// Имя ключа, по которому в <c>Info</c> запроса <see cref="CardGetRequest"/> содержится идентификатор загружаемого задания. Тип значения: <see cref="System.Guid"/>. 
        /// </summary>
        public const string EspecialTaskRowIDKey = CardHelper.SystemKeyPrefix + "TaskRowID";
    }
}
