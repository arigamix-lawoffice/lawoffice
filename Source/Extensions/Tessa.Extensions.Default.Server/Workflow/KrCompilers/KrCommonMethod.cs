#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrCommonMethod"/>
    public class KrCommonMethod :
        IKrCommonMethod
    {
        #region Constants And Static Fields

        /// <summary>
        /// Объект, представляющий неиспользуемый метод.
        /// </summary>
        public static KrCommonMethod FakeObject { get; } = new KrCommonMethod(
            Guid.Empty,
            "UnusedMethod");

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="id"><inheritdoc cref="ID" path="/summary"/></param>
        /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
        /// <param name="source"><inheritdoc cref="Source" path="/summary"/></param>
        public KrCommonMethod(
            Guid id,
            string name,
            string? source = null)
        {
            this.ID = id;
            this.Name = NotEmptyOrThrow(name);
            this.Source = source ?? string.Empty;
        }

        #endregion

        #region IKrCommonMethod Members

        /// <inheritdoc/>
        public Guid ID { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Source { get; }

        #endregion
    }
}
