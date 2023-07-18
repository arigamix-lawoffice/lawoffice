using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    /// <summary>
    /// Базовый класс для прокси объектной модели на настройки этапа.
    /// Для сериализации не используется, StorageObject нужен исключительно для декорирования настроек.
    /// </summary>
    public class Performer:
        StorageObject,
        IEquatable<Performer>,
        ISealable
    {
        #region fields

        private static readonly Dictionary<string, object> emptyDict = new Dictionary<string, object>();

        #endregion

        #region constructors

        /// <inheritdoc />
        protected Performer()
            : base(emptyDict)
        {
        }

        /// <inheritdoc />
        protected Performer(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        /// <inheritdoc />
        public Performer(
            Guid performerID,
            string performerName)
            : base(emptyDict)
        {
            this.PerformerID = performerID;
            this.PerformerName = performerName;
        }

        #endregion

        #region properties

        /// <summary>
        /// Идентификатор строки исполнителя. Используется только для представления в виртуальных секциях
        /// </summary>
        public virtual Guid RowID => default;

        /// <summary>
        /// Признак того, что исполнитель подставлен через SQL
        /// </summary>
        public virtual bool  IsSql => default;

        /// <summary>
        /// ID роли исполнителя
        /// </summary>
        public virtual Guid PerformerID { get; private set; }

        /// <summary>
        /// Имя роли исполнителя
        /// </summary>
        public virtual string PerformerName { get; private set; }

        /// <summary>
        /// ID этапа.
        /// </summary>
        public virtual Guid StageRowID => default;

        #endregion

        #region operators

        public static bool operator ==(Performer left, Performer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Performer left, Performer right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region object

        public override string ToString()
        {
            return $"Performer: RowID = {this.RowID:B}, ID = {this.PerformerID:B}, Name = {this.PerformerName}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is Performer approver && this.Equals(approver);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }

        #endregion

        #region IEquatable

        /// <summary>
        /// Сравнение по ID и Name без учета коллекции
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Performer other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.RowID.Equals(other.RowID)
                && this.PerformerID.Equals(other.PerformerID)
                && string.Equals(this.PerformerName, other.PerformerName, StringComparison.Ordinal);
        }

        #endregion

        #region ISealable Members

        /// <summary>
        /// Признак того, что объект был защищён от изменений.
        /// </summary>
        public bool IsSealed { get; } = true;

        /// <summary>
        /// Защищает объект от изменений.
        /// </summary>
        public void Seal()
        {
        }

        #endregion

    }
}