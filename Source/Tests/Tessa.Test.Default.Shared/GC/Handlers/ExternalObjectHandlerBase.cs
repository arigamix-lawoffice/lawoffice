using System;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Базовый абстрактный класс обработчика внешнего объекта.
    /// </summary>
    public abstract class ExternalObjectHandlerBase :
        IExternalObjectHandler
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ExternalObjectHandlerBase"/>.
        /// </summary>
        /// <param name="id">Идентификатор типа обрабатываемых объектов.</param>
        protected ExternalObjectHandlerBase(Guid id)
        {
            this.ID = id;
        }

        #endregion

        #region IExternalObjectHandler Members

        /// <summary>
        /// Возвращает идентификатор типа обрабатываемых объектов.
        /// </summary>
        public Guid ID { get; }

        /// <inheritdoc/>
        public virtual ValueTask HandleAsync(IExternalObjectHandlerContext context) =>
            ValueTask.CompletedTask;

        #endregion
    }
}
