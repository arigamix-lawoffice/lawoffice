using Tessa.UI.Views;

namespace Tessa.Extensions.Client.Views
{
    /// <summary>
    /// Creating a virtual "Case" card by double-click.
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