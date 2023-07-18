using System;
using System.Xml.Linq;
using Chronos.Plugins;
using NLog;

namespace Tessa.Extensions.Chronos.Helpers
{
    /// <summary>
    /// Вспомогательный класс для использования в плагинах.
    /// </summary>
    public static class PluginHelper
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private static void WarnAttributeNotFound(string attributeName, object defaultValue)
        {
            logger.Warn(
                "Attribute '{0}' was not found. Using default value '{1}'.",
                attributeName,
                defaultValue);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает положительное число, полученное из атрибута в конфигурационном файле плагина.
        /// </summary>
        /// <param name="config">Корневой xml-элемент в конфигурационном файле плагина.</param>
        /// <param name="attributeName">Имя атрибута, в котором хранится настройка.</param>
        /// <param name="defaultValue">Значение по умолчанию, используемое, когда атрибут отсутствует.</param>
        /// <returns>
        /// Значение атрибута <paramref name="attributeName"/> или значение по умолчанию <paramref name="defaultValue"/>,
        /// если атрибут не был задан.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Атрибут присутствовал, но содержал некорректное значение.</exception>
        public static int GetPositiveIntegerSetting(XElement config, string attributeName, int defaultValue)
        {
            XAttribute? attribute = config.PluginAttribute(attributeName);
            if (attribute is not null)
            {
                int value = int.Parse(attribute.Value);
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(attributeName);
                }

                return value;
            }

            WarnAttributeNotFound(attributeName, defaultValue);
            return defaultValue;
        }


        /// <summary>
        /// Возвращает непустую строку, полученную из атрибута в конфигурационном файле плагина.
        /// </summary>
        /// <param name="config">Корневой xml-элемент в конфигурационном файле плагина.</param>
        /// <param name="attributeName">Имя атрибута, в котором хранится настройка.</param>
        /// <param name="defaultValue">Значение по умолчанию, используемое, когда атрибут отсутствует.</param>
        /// <returns>
        /// Значение атрибута <paramref name="attributeName"/> или значение по умолчанию <paramref name="defaultValue"/>,
        /// если атрибут не был задан.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Атрибут присутствовал, но содержал некорректное значение.</exception>
        public static string GetNotEmptyStringSetting(XElement config, string attributeName, string defaultValue)
        {
            XAttribute? attribute = config.PluginAttribute(attributeName);
            if (attribute is not null)
            {
                if (string.IsNullOrEmpty(attribute.Value))
                {
                    throw new ArgumentOutOfRangeException(attributeName);
                }

                return attribute.Value;
            }

            WarnAttributeNotFound(attributeName, defaultValue);
            return defaultValue;
        }

        #endregion
    }
}
