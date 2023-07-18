using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Объект, содержащий информацию о текущем флаге настроек прав доступа.
    /// </summary>
    public class KrPermissionFlagDescriptor : IEquatable<KrPermissionFlagDescriptor>
    {
        #region Constructors

        /// <summary>
        /// Объект, содержащий информацию о текущем флаге настроек прав доступа.
        /// </summary>
        /// <param name="id">Идентификатор флага.</param>
        /// <param name="name">Имя флага.</param>
        /// <param name="order">Определяет порядок флагов в карточке правил доступа и в сообщениях.</param>
        /// <param name="description">Заголовок флага. Выводится в сообщениях о правах на карточку или когда недостаточно прав.</param>
        /// <param name="controlCaption">Имя контрола флага в карточке правил доступа. Имеет смысл заполнять только совместно с <paramref name="sqlName"/>.</param>
        /// <param name="controlTooltip">Подсказка к флагу в карточке правил доступа.</param>
        /// <param name="sqlName">Имя поля в SQL в таблице KrPermissions. Если не задано, то флаг не хранится в базе.</param>
        /// <param name="includedPermissions">Список флагов, которые включены в данный флаг.</param>
        public KrPermissionFlagDescriptor(
            Guid id,
            string name,
            int order,
            string description,
            string controlCaption,
            string controlTooltip,
            string sqlName,
            params KrPermissionFlagDescriptor[] includedPermissions)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.Description = description;
            this.ControlCaption = controlCaption;
            this.ControlTooltip = controlTooltip;
            this.SqlName = sqlName;
            this.IncludedPermissions = new HashSet<KrPermissionFlagDescriptor>(includedPermissions);

            if (!this.IsVirtual)
            {
                KrPermissionFlagDescriptors.AddDescriptor(this);
            }
        }

        /// <summary>
        /// Объект, содержащий информацию о текущем флаге настроек прав доступа, не хранящийся в базе данных и не имеющий контрола в карточке прав доступа.
        /// </summary>
        /// <param name="id">Идентификатор флага.</param>
        /// <param name="name">Имя флага.</param>
        /// <param name="includedPermissions">Список флагов, которые включены в данный флаг.</param>
        public KrPermissionFlagDescriptor(
            Guid id,
            string name,
            params KrPermissionFlagDescriptor[] includedPermissions)
            : this(
                  id,
                  name,
                  -1,
                  null,
                  null,
                  null,
                  null,
                  includedPermissions: includedPermissions)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор флага.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Имя флага.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Порядковый номер флага. Определяет расположение флагов в карточке правила доступа.
        /// Данное свойство можно изменить, чтобы переопределить порядок стандартных флагов. Делать это следует на этапе регистрации объектов.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Описание флага. Выводится в ообщения о правах на карточку или когда недостаточно прав.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Имя контрола флага в карточке правил доступа.
        /// </summary>
        public string ControlCaption { get; }

        /// <summary>
        /// Подсказка к контролу флага в карточке правил доступа.
        /// </summary>
        public string ControlTooltip { get; }

        /// <summary>
        /// Имя поля в SQL в таблице KrPermissions. Если не задано, то флаг не хранится в базе.
        /// </summary>
        public string SqlName { get; }

        /// <summary>
        /// Определяет, что данный флаг виртуальный и его не нужно проверять по базе.
        /// </summary>
        public bool IsVirtual => this.SqlName is null;

        /// <summary>
        /// Список флагов, которые включает текущий флаг.
        /// </summary>
        public HashSet<KrPermissionFlagDescriptor> IncludedPermissions { get; }

        #endregion

        #region IEquatable Implementation

        public bool Equals(KrPermissionFlagDescriptor other)
        {
            return this.ID == other?.ID;
        }

        #endregion

        #region Base Override

        public static bool operator ==(KrPermissionFlagDescriptor flag, Guid guid)
        {
            return flag?.ID == guid;
        }

        public static bool operator !=(KrPermissionFlagDescriptor flag, Guid guid)
        {
            return flag?.ID != guid;
        }

        public static bool operator ==(KrPermissionFlagDescriptor flag, KrPermissionFlagDescriptor flag2)
        {
            return flag?.ID == flag2?.ID;
        }

        public static bool operator !=(KrPermissionFlagDescriptor flag, KrPermissionFlagDescriptor flag2)
        {
            return flag?.ID != flag2?.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is KrPermissionFlagDescriptor flag
                && this.Equals(flag);
        }

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
