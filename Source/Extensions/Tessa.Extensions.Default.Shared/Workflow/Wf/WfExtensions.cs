using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    /// <summary>
    /// Методы-расширения для пространства имён <c>Tessa.Extensions.Default.Shared.Workflow.Wf</c>.
    /// </summary>
    public static class WfExtensions
    {
        #region WfResolutionFlags Extensions

        public static bool Has(this WfResolutionSendingFlags flags, WfResolutionSendingFlags flag)
        {
            return (flags & flag) == flag;
        }

        public static bool HasAny(this WfResolutionSendingFlags flags, WfResolutionSendingFlags flag)
        {
            return (flags & flag) != 0;
        }

        public static bool HasNot(this WfResolutionSendingFlags flags, WfResolutionSendingFlags flag)
        {
            return (flags & flag) == 0;
        }

        /// <summary>
        /// Возвращает нормализованное значение комментария, в котором заменены переводы строк
        /// и множественные пробелы на одиночные пробелы, а также удалены пробелы по краям строки.
        /// Комментарий при этом не усекается по длине.
        /// </summary>
        /// <param name="comment">Комментарий, который необходимо нормализовать. Может быть равен <c>null</c>.</param>
        /// <returns>Нормализованное значение комментария.</returns>
        public static string NormalizeComment(this string comment)
        {
            return comment.ReplaceLineEndingsAndTrim().NormalizeSpaces();
        }

        /// <summary>
        /// Возвращает нормализованное значение комментария, в котором возможны переводы строк в середине сообщения, но заменены
        /// и множественные пробелы на одиночные пробелы, а также удалены пробелы и переводы строк по краям строки.
        /// Комментарий при этом не усекается по длине.
        /// </summary>
        /// <param name="comment">Комментарий, который необходимо нормализовать. Может быть равен <c>null</c>.</param>
        /// <returns>Нормализованное значение комментария с переводами строк.</returns>
        public static string NormalizeCommentWithLineBreaks(this string comment)
        {
            return comment?.Trim().NormalizeSpaces();
        }

        #endregion
    }
}
