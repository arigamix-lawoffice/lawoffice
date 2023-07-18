using Tessa.UI.Views;

namespace Tessa.Extensions.Client.Views
{
    /// <summary>
    /// Создание виртуальной карточки "Дело" по дабл-клику. Только для ЛК.
    /// </summary>
    public class LawCreateCaseViewExtension : IWorkplaceViewComponentExtension
    {
        /// <inheritdoc />
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context) { }

        /// <inheritdoc />
        public void Initialize(IWorkplaceViewComponent model) { }

        /// <inheritdoc />
        public void Initialized(IWorkplaceViewComponent model) { }
    }
}