using System;
using System.Collections.Generic;
using System.Linq;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Билдер правил видимости элементов управления, который возвращает все добавленные правила с учетом приоритетов.
    /// </summary>
    public sealed class KrPermissionVisibilitySettingsBuilder
    {
        #region Fields

        private readonly Dictionary<int, List<KrPermissionVisibilitySettings>> settingsList =
            new();

        #endregion

        #region Public Methods

        /// <summary>
        /// Метод для добавления правила видимости с приоритетом.
        /// </summary>
        /// <param name="settings">Настройки правил видимости.</param>
        /// <param name="priority">Приоритет правила.</param>
        /// <returns>Текущий билдер.</returns>
        public KrPermissionVisibilitySettingsBuilder Add(IEnumerable<KrPermissionVisibilitySettings> settings, int priority = 0)
        {
            if (this.settingsList.TryGetValue(priority, out var list))
            {
                list.AddRange(settings);
            }
            else
            {
                this.settingsList[priority] = new List<KrPermissionVisibilitySettings>(settings);
            }

            return this;
        }

        /// <summary>
        /// Метод для построения списка правил видимости с учетом приоритетов.
        /// </summary>
        /// <returns>Список правил видимости.</returns>
        public IList<KrPermissionVisibilitySettings> Build()
        {
            if (this.settingsList.Count == 0)
            {
                return Array.Empty<KrPermissionVisibilitySettings>();
            }

            if (this.settingsList.Count == 1)
            {
                return this.settingsList.First().Value;
            }
            else
            {
                var result = new List<KrPermissionVisibilitySettings>();
                foreach (var (_, settings) in this.settingsList.OrderByDescending(x => x.Key))
                {
                    result.AddRange(settings);
                }
                return result;
            }
        }

        #endregion
    }
}
