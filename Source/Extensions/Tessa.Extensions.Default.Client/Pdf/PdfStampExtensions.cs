using System;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Formatting;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Вспомогательные методы-расширения для пространства имён <c>Tessa.Extensions.Platform.Client.Models.PdfStamps</c>.
    /// </summary>
    public static class PdfStampExtensions
    {
        #region IExtensionContainer Extensions

        public static IExtensionContainer RegisterPdfStampExtensionTypes(this IExtensionContainer extensionContainer)
        {
            return extensionContainer
                .RegisterType<IPdfStampExtension>(x => x
                    .MethodAsync<IPdfStampExtensionContext>(y => y.OnGenerationStarted)
                    .MethodAsync<IPdfStampExtensionContext>(y => y.GenerateForPage)
                    .MethodAsync<IPdfStampExtensionContext>(y => y.OnGenerationEnded))
                ;
        }

        #endregion

        #region IExtensionPolicyContainer Members

        /// <summary>
        /// Регистрирует политику фильтрации выполнения методов расширений <see cref="IPdfStampExtension"/>
        /// в соответствии с функцией <paramref name="isAllowedFunc"/>, которая проверяет контекст расширений.
        ///
        /// Если зарегистрировано несколько политик, то должны выполняться все из них.
        /// </summary>
        /// <param name="policyContainer">Контейнер политик, ассоциированных с расширениями.</param>
        /// <param name="isAllowedFunc">
        /// Функция, получающая контекст (не равный <c>null</c>) и возвращающая признак того,
        /// что контекст удовлетворяет политике. Параметр не равен <c>null</c>.
        ///
        /// Исключения логируются в логгере <see cref="TessaLoggers.Extensions"/>, а также добавляются в контекст как сообщение валидации.
        /// Расширение, для которого возникло исключение, пропускается.
        /// </param>
        /// <returns>Заданный контейнер <paramref name="policyContainer"/> для цепочки расширений.</returns>
        public static IExtensionPolicyContainer WhenPdfStampFunc(
            this IExtensionPolicyContainer policyContainer,
            Func<IPdfStampExtensionContext, bool> isAllowedFunc) =>
            policyContainer.WhenFunc(isAllowedFunc);

        #endregion

        #region PdfStampWriter Extensions

        /// <summary>
        /// Добавляет строку с заданной датой в штамп. Дата не конвертируется в локальное время и выводится как есть.
        /// </summary>
        /// <param name="stampWriter">Объект, выполняющий вывод штампа на странице PDF.</param>
        /// <param name="date">
        /// Выводимая дата (время и часовой пояс игнорируются) или <c>null</c>, если дата неизвестна.
        /// </param>
        /// <returns>Объект <paramref name="stampWriter"/> для цепочки вызовов.</returns>
        public static PdfStampWriter AppendDate(this PdfStampWriter stampWriter, DateTime? date)
        {
            if (stampWriter == null)
            {
                throw new ArgumentNullException("stampWriter");
            }

            return stampWriter
                .AppendLine(
                    LocalizationManager.Format(
                        "$UI_Controls_Scan_DateStamp",
                        FormattingHelper.FormatDate(date, convertToLocal: false)));
        }

        #endregion
    }
}
