using System;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Положение относительно этапов, добавленных вручную.
    /// </summary>
    public sealed class GroupPosition : IEquatable<GroupPosition>
    {
        #region Static Fields

        /// <summary>
        /// Информация о позиции не определена.
        /// </summary>
        public static readonly GroupPosition Unspecified = new GroupPosition(null, null);

        /// <summary>
        /// В начале группы.
        /// </summary>
        public static readonly GroupPosition AtFirst = new GroupPosition(0, "AtFirst");

        /// <summary>
        /// В конце группы.
        /// </summary>
        public static readonly GroupPosition AtLast = new GroupPosition(1, "AtLast");

        private static readonly GroupPosition[] positions = { AtFirst, AtLast };

        #endregion

        #region Constructors

        [UsedImplicitly]
        private GroupPosition()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupPosition"/>.
        /// </summary>
        /// <param name="id">Идентификатор позиции группы относительно этапов, добавленных вручную.</param>
        /// <param name="name">Имя позиции группы относительно этапов, добавленных вручную.</param>
        private GroupPosition(int? id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает идентификатор положения группы этапов.
        /// </summary>
        public int? ID { get; [UsedImplicitly]  private set; }

        /// <summary>
        /// Возвращает строковую подпись положения группы этапов.
        /// </summary>
        public string Name { get; [UsedImplicitly] private set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает объект <see cref="GroupPosition"/> в соответствии с заданным идентификатором положения группы этапов.
        /// </summary>
        /// <param name="idObj">Идентификатор положения группы этапов.</param>
        /// <returns>Объект <see cref="GroupPosition"/> соответствующий заданному идентификатору положения группы этапов или значение <see cref="Unspecified"/>, если заданное значение не соответствует ни одному из известных положений групп этапов.</returns>
        public static GroupPosition GetByID(object idObj)
        {
            return idObj is int id ?
                GetByID(id) :
                Unspecified;
        }

        /// <summary>
        /// Возвращает объект <see cref="GroupPosition"/> в соответствии с заданным идентификатором положения группы этапов.
        /// </summary>
        /// <param name="id">Идентификатор положения группы этапов.</param>
        /// <returns>Объект <see cref="GroupPosition"/> соответствующий заданному идентификатору положения группы этапов или значение <see cref="Unspecified"/>, если заданное значение не соответствует ни одному из известных положений групп этапов.</returns>
        public static GroupPosition GetByID(int? id)
        {
            return id.HasValue && (0 <= id && id < 2) ?
                positions[id.Value] :
                Unspecified;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Сравнивает два экземпляра объектов типа <see cref="GroupPosition"/>.
        /// </summary>
        /// <param name="gp1">Первый объект.</param>
        /// <param name="gp2">Второй объект.</param>
        /// <returns>Значение <see langword="true"/>, если объекты равны, иначе - <see langword="false"/>.</returns>
        public static bool operator ==(GroupPosition gp1, GroupPosition gp2)
        {
            return gp1 is object && gp1.Equals(gp2);
        }

        /// <summary>
        /// Сравнивает два экземпляра объектов типа <see cref="GroupPosition"/>.
        /// </summary>
        /// <param name="gp1">Первый объект.</param>
        /// <param name="gp2">Второй объект.</param>
        /// <returns>Значение <see langword="true"/>, если объекты не равны, иначе - <see langword="false"/>.</returns>
        public static bool operator !=(GroupPosition gp1, GroupPosition gp2)
        {
            return gp1 is object && !gp1.Equals(gp2);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as GroupPosition);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.ID switch
            {
                0 => "AtFirst",
                1 => "AtLast",
                _ => "Unspecified",
            };
        }

        #endregion

        #region IEquatable Members

        /// <inheritdoc/>
        public bool Equals(GroupPosition other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.ID == this.ID || (other.ID == null && this.ID == null);
        }

        #endregion
    }
}
