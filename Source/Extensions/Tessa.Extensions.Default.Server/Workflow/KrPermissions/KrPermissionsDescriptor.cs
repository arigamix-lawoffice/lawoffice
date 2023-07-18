using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Дескриптор расчёта прав доступа. 
    /// </summary>
    public sealed class KrPermissionsDescriptor
    {
        #region Fields

        private readonly HashSet<KrPermissionFlagDescriptor> originalRequiredPermissions;

        #endregion

        #region Constructors

        public KrPermissionsDescriptor(params KrPermissionFlagDescriptor[] requiredPermissions)
        {
            this.StillRequired = CalculatePermissions(requiredPermissions);
            this.originalRequiredPermissions = new HashSet<KrPermissionFlagDescriptor>(this.StillRequired);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Список разрешенных прав доступа.
        /// </summary>
        public HashSet<KrPermissionFlagDescriptor> Permissions { get; } = new HashSet<KrPermissionFlagDescriptor>();

        /// <summary>
        /// Список прав доступа, которые ещё требуется проверить.
        /// </summary>
        public HashSet<KrPermissionFlagDescriptor> StillRequired { get; }

        /// <summary>
        /// Список расширенных настроек прав доступа к секциям карточек.
        /// </summary>
        public HashSet<Guid, IKrPermissionSectionSettingsBuilder> ExtendedCardSettings { get; } = new HashSet<Guid, IKrPermissionSectionSettingsBuilder>(x => x.SectionID);

        /// <summary>
        /// Списки расширенных настроек прав доступа к секциям заданий, разбитый по типу задания.
        /// </summary>
        public Dictionary<Guid, HashSet<Guid, IKrPermissionSectionSettingsBuilder>> ExtendedTasksSettings { get; }
            = new Dictionary<Guid, HashSet<Guid, IKrPermissionSectionSettingsBuilder>>();

        /// <summary>
        /// Список расширенных настроек обязательности полей.
        /// </summary>
        public List<KrPermissionMandatoryRule> ExtendedMandatorySettings { get; } = new List<KrPermissionMandatoryRule>();

        /// <summary>
        /// Список расширенных настроек видимости элементов управления.
        /// </summary>
        public KrPermissionVisibilitySettingsBuilder VisibilitySettings { get; } = new KrPermissionVisibilitySettingsBuilder();

        /// <summary>
        /// Список расширенных настроек прав доступа к файлам.
        /// </summary>
        public List<IKrPermissionsFileRule> FileRules { get; } = new List<IKrPermissionsFileRule>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Добавляет или удаляет право доступа из списка, в зависимости от параметра <paramref name="allow"/>.
        /// </summary>
        /// <param name="flag">Настройка прав доступа, которую нужно добавить к списку разрешенных прав или исключить из него.</param>
        /// <param name="allow">Определяет, нужно ли добавить настройку прав к списку разрешенных прав или исключить из него.
        /// Если параметр <paramref name="force"/> равен <c>false</c>, то настройка прав доступа добавляется только в том улчае, если она была запрошена.
        /// </param>
        /// <param name="force">Определяет, должна ли данная настройка установиться принудительно, даже если она не была запрошена.</param>
        /// <returns>Текущий объект для цепочки вызовов.</returns>
        public KrPermissionsDescriptor Set(KrPermissionFlagDescriptor flag, bool allow, bool force = false)
        {
            if (allow)
            {
                this.AddFlag(flag, force);
            }
            else
            {
                this.RemoveFlag(flag);
            }

            return this;
        }

        /// <summary>
        /// Проверяет, что список разрешенных прав доступа содержит переданную настройку.
        /// </summary>
        /// <param name="flag">Проверяемая настройка прав доступа.</param>
        /// <returns>Значение <c>true</c>, если список разрешенных настроек прав доступа содержит переданную настройку, иначе <c>false</c>.</returns>
        public bool Has(KrPermissionFlagDescriptor flag)
        {
            return flag.IsVirtual ? this.Has(flag.IncludedPermissions) : this.Permissions.Contains(flag);
        }

        /// <summary>
        /// Проверяет, что список разрешенных прав доступа содержит все переданные настройки.
        /// </summary>
        /// <param name="flag">Список проверяемых настроек прав доступа.</param>
        /// <returns>Значение <c>true</c>, если список разрешенных настроек прав доступа содержит все переданные настройки, иначе <c>false</c>.</returns>
        public bool Has(IEnumerable<KrPermissionFlagDescriptor> flags)
        {
            foreach (var flag in flags)
            {
                if (!this.Permissions.Contains(flag))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Метод проверяет, были ли добавлены все запрашиваемые права доступа.
        /// </summary>
        /// <returns>Значение <c>true</c>, если все запрашиваемые права доступа уже были добавлены, иначе <c>false</c>.</returns>
        public bool AllChecked()
        {
            return this.StillRequired.Count == 0;
        }

        #endregion

        #region Private Methods

        private void RemoveFlag(KrPermissionFlagDescriptor flag)
        {
            if (this.originalRequiredPermissions.Contains(flag))
            {
                this.StillRequired.Add(flag);
            }

            if (flag.IsVirtual || this.Permissions.Remove(flag))
            {
                if (flag.IncludedPermissions.Count > 0)
                {
                    foreach (var includeFlag in flag.IncludedPermissions)
                    {
                        this.RemoveFlag(includeFlag);
                    }
                }
            }
        }

        private void AddFlag(KrPermissionFlagDescriptor flag, bool force)
        {
            if (this.StillRequired.Remove(flag)
                || force
                || flag.IsVirtual)
            {
                if ((flag.IsVirtual || this.Permissions.Add(flag))
                    && flag.IncludedPermissions.Count > 0)
                {
                    foreach (var includeFlag in flag.IncludedPermissions)
                    {
                        this.AddFlag(includeFlag, force);
                    }
                }
            }
        }

        /// <summary>
        /// Метод для разворота цепочки прав доступа с включением в список подчиненных прав доступа и исключением виртуальных
        /// </summary>
        /// <param name="requiredPermissions">Запрашиваемый набор прав доступа</param>
        /// <returns>Возвращает набор прав доступа с учетом подчиеннных прав доступа и с исключением виртуальных</returns>
        private static HashSet<KrPermissionFlagDescriptor> CalculatePermissions(KrPermissionFlagDescriptor[] requiredPermissions)
        {
            HashSet<KrPermissionFlagDescriptor> result = new HashSet<KrPermissionFlagDescriptor>();
            HashSet<KrPermissionFlagDescriptor> virtuals = null;
            void FillFromPermission(KrPermissionFlagDescriptor krPermissionFlag)
            {
                if (krPermissionFlag.IsVirtual)
                {
                    if (virtuals is null)
                    {
                        virtuals = new HashSet<KrPermissionFlagDescriptor>();
                    }
                    if (!virtuals.Add(krPermissionFlag))
                    {
                        // Уже был обработан, значит не добавляем вложенные элементы снова
                        return;
                    }
                }
                else
                {
                    if (!result.Add(krPermissionFlag))
                    {
                        // Уже был добавлен, значит не добавляем вложенные элементы снова
                        return;
                    }
                }

                if (krPermissionFlag.IncludedPermissions.Count > 0)
                {
                    foreach (var permission in krPermissionFlag.IncludedPermissions)
                    {
                        FillFromPermission(permission);
                    }
                }
            }

            for (int i = 0; i < requiredPermissions.Length; i++)
            {
                FillFromPermission(requiredPermissions[i]);
            }

            return result;
        }

        #endregion
    }
}
