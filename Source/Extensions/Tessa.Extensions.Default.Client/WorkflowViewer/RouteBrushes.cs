using System.Windows.Media;
using Tessa.UI.WorkflowViewer.Helpful;

namespace Tessa.Extensions.Default.Client.WorkflowViewer
{
    internal static class RouteBrushes
    {
        public static readonly Brush Finished = WorkflowBrushes.Gray;
        public static readonly Brush Approved = WorkflowBrushes.Green;
        public static readonly Brush Signed = Approved;
        public static readonly Brush Disapproved = WorkflowBrushes.Red;
        public static readonly Brush Declined = Disapproved;
        public static readonly Brush NotInWork = WorkflowBrushes.Blue;
        public static readonly Brush InWork = WorkflowBrushes.Yellow;
        public static readonly Brush NotCreatedNode = WorkflowBrushes.LightGray;
        public static readonly Brush RegisteredNode = WorkflowBrushes.Blue;
        public static readonly Brush DeregisteredNode = WorkflowBrushes.Red;
        public static readonly Brush Arrow = WorkflowBrushes.VeryDarkGray;
        public static readonly Brush Border = WorkflowBrushes.VeryDarkGray;
        public static readonly Brush Selected = WorkflowBrushes.Black;
        public static readonly Brush ObscureNodeForeground = WorkflowBrushes.DarkGray;
    }
}
