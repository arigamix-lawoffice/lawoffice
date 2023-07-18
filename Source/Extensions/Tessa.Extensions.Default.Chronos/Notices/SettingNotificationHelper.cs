using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Chronos.Plugins;
using NLog;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    /// <summary>
    /// Вспомогательные методы для плагинов, рассылающих системные уведомления.
    /// </summary>
    public static class SettingNotificationHelper
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Helpers

        /// <summary>
        /// Возвращает уникальный список адресов электронной почты,
        /// которым надо отослать уведомления для секции с заданным именем.
        /// </summary>
        /// <param name="dbScope">Объект, обеспечивающий взаимодействие с базой данных.</param>
        /// <param name="roleSectionName">Имя секции с ролями, которые содержат пользователей для отправки уведомлений.</param>
        /// <returns>Полученный список получателей</returns>
        public static async Task<List<Guid>> GetEmailSettingsAsync(IDbScope dbScope, string roleSectionName, CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;
                return await db
                    .SetCommand(
                        builderFactory
                            .SelectDistinct().C("pr", "ID")
                            .From(roleSectionName, "n").NoLock()
                            .InnerJoin("RoleUsers", "ru").NoLock()
                                .On().C("ru", "ID").Equals().C("n", "RoleID")
                            .InnerJoin("PersonalRoles", "pr").NoLock()
                                .On().C("pr", "ID").Equals().C("ru", "UserID")
                            .Where().C("pr", "Email").IsNotNull()
                                .And().C("pr", "Email").NotEquals().V(string.Empty)
                            .Build())
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает информацию по сообщению, которое используется для отправки уведомлений.
        /// </summary>
        /// <param name="config">Конфигурационный файл плагина, содержащий искомую информацию.</param>
        /// <param name="subject">Тема сообщения.</param>
        /// <param name="body">Тело сообщения.</param>
        /// <returns>
        /// <c>true</c>, если удалось получить информацию;
        /// <c>false</c>, если при получении информации произошли ошибки.
        /// </returns>
        [Obsolete]
        public static bool TryGetNotificationMessage(XElement config, out string subject, out string body)
        {
            XElement subjectElement = config.PluginElement("subject");
            if (subjectElement != null)
            {
                subject = subjectElement.Value;
            }
            else
            {
                logger.Error("Config file should contain <subject> element.");

                subject = null;
                body = null;
                return false;
            }

            XElement bodyElement = config.PluginElement("body");
            if (bodyElement != null)
            {
                body = bodyElement.Value;
            }
            else
            {
                logger.Error("Config file should contain <body> element.");

                subject = null;
                body = null;
                return false;
            }

            return true;
        }

        #endregion
    }
}
