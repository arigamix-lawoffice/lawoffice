using System;
using System.Runtime.CompilerServices;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет статические методы для управления меткой о наличии доп. согласующих.
    /// </summary>
    public static class KrAdditionalApprovalMarker
    {
        /// <summary>
        /// Метка добавляемая к строке как признак доп. согласующих.
        /// </summary>
        public const string AdditionalApproverMark = "(+) ";

        /// <summary>
        /// Добавляет метку о наличии доп. согласующих, если она отсутствует, в указанную строку.
        /// </summary>
        /// <param name="str">Строка в котороую требуется добавить метку.</param>
        /// <returns>Результирующая строка.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Mark(
            string str)
        {
            _ = TryMark(str, out str);
            return str;
        }

        /// <summary>
        /// Добавляет метку о наличии доп. согласующих, если она отсутствует, в указанную строку.
        /// </summary>
        /// <param name="str">Строка в котороую требуется добавить метку.</param>
        /// <param name="resultStr">Результирующая строка.</param>
        /// <returns>Значение <see langword="true"/>, если строка была изменена, иначе - <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryMark(
            string str,
            out string resultStr)
        {
            if (HasMark(str))
            {
                resultStr = str;
                return false;
            }

            resultStr = str[0] == '$'
                ? AdditionalApproverMark + "{" + str + "}"
                : AdditionalApproverMark + str;

            return true;
        }

        /// <summary>
        /// Удаляет метку о доп. согласующих из указанной строки, если она присутствует.
        /// </summary>
        /// <param name="str">Строка из которой требуется удалить метку.</param>
        /// <returns>Результирующая строка.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Unmark(
            string str)
        {
            _ = TryUnmark(str, out str);
            return str;
        }

        /// <summary>
        /// Удаляет метку о доп. согласующих из указанной строки, если она присутствует.
        /// </summary>
        /// <param name="str">Строка из которой требуется удалить метку.</param>
        /// <param name="resultStr">Результирующая строка.</param>
        /// <returns>Значение <see langword="true"/>, если строка была изменена, иначе - <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryUnmark(
            string str,
            out string resultStr)
        {
            if (HasMark(str))
            {
                var from = 4;
                var len = str.Length - from;
                // (+) {} - Если расширенная локализация, скобки надо выпиливать.
                if (str.Length >= 6
                    && str[4] == '{'
                    && str[^1] == '}')
                {
                    from++;
                    len -= 2;
                }

                resultStr = str.Substring(from, len);
                return true;
            }

            resultStr = str;
            return false;
        }

        /// <summary>
        /// Возвращает значение, показывающее, присутсвует ли метка в указанной строке.
        /// </summary>
        /// <param name="str">Проверяемая строка.</param>
        /// <returns>Значение <see langword="true"/>, если проверяемая строка содержит метку, иначе - <see langword="false"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMark(
            string str) =>
            !string.IsNullOrEmpty(str) && str.StartsWith(AdditionalApproverMark, StringComparison.Ordinal);
    }
}