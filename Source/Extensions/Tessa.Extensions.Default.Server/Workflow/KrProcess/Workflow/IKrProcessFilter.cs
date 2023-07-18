using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает объект предоставляющий возможность фильтрации объекта типа <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип фильтруемого объекта.</typeparam>
    public interface IKrProcessFilter<out T>
    {
        /// <summary>
        /// Возвращает доступную только для чтения коллекцию исключённых объектов.
        /// </summary>
        IReadOnlyCollection<T> Excluded { get; }
    }
}