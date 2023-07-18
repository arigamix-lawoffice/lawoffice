using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public static class StageTypeSettingsNaming
    {
        private const string SectionNameRegex = "\\w+__Synthetic";

        private const string PlainNameRegex = "\\w+__\\w+(.*(?<!__Synthetic)$)";


        /// <summary>
        /// Является ли строка форматтированным именем секции
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSectionName(
            string name) =>
            Regex.IsMatch(
                name, 
                SectionNameRegex,
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Является ли строка форматтированный именем поля
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPlainName(
            string name) =>
            Regex.IsMatch(
                name,
                PlainNameRegex,
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Форматирование названия секции при подстановке в качестве настроек.
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string SectionName(
            string sectionName) =>
            $"{sectionName}_Synthetic";

        /// <summary>
        /// Форматирование стоблца, подставляемого в KrStages в качестве настроек
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string PlainColumnName(
            string sectionName,
            string fieldName) =>
            $"{sectionName}__{fieldName}";
    }
}