using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sims.CheatEngine.App.UserControls
{
    public partial class TitleBarControl
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            var lGb = new LinearGradientBrush(this.DisplayRectangle, ForeColor, BackColor,
                LinearGradientMode.Vertical);
            pevent.Graphics.FillRectangle(lGb, DisplayRectangle);
            TextRenderer.DrawText(pevent.Graphics, Text, Font, DisplayRectangle, TextForeColour);
        }
    }
}