using Tessa.Localization;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    /// <summary>
    /// Группировка по источнику файла.
    /// </summary>
    /// <remarks>
    /// Класс-наследник может переопределить поведение класса, например,
    /// установить сортировку по умолчанию для данной группировки.
    /// </remarks>
    public sealed class FileSourceGrouping :
        FileGrouping
    {
        #region Constructors

        public FileSourceGrouping(string name, string caption, bool isCollapsed = false)
            : base(name, caption, isCollapsed)
        {
        }

        #endregion

        #region Constants

        public const string CardSourceGroupName = "CardSourceGroup";

        public const string ExternalSourceGroupName = "ExternalSourceGroup";

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IFileGrouping" and @item="GetGroupInfo"]'/>
        protected override FileGroupInfo GetGroupInfoCore(IFileViewModel viewModel)
        {
            return viewModel.Model is ExternalFile
                ? new FileGroupInfo(ExternalSourceGroupName, LocalizationManager.GetString("KrTest_ExternalFilesGroup"))
                : new FileGroupInfo(CardSourceGroupName, LocalizationManager.GetString("KrTest_MainFilesGroup"));
        }

        #endregion
    }
}
