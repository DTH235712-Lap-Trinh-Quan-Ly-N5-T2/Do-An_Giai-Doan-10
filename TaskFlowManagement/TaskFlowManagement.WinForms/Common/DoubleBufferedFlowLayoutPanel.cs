// ============================================================
//  DoubleBufferedFlowLayoutPanel.cs
//  TaskFlowManagement.WinForms.Common
// ============================================================
using System.Windows.Forms;

namespace TaskFlowManagement.WinForms.Common
{
    /// <summary>
    /// FlowLayoutPanel chống xé hình khi scroll nhiều card.
    /// </summary>
    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        public DoubleBufferedFlowLayoutPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}