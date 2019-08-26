using System.Windows.Forms;
using System.Drawing;

namespace Sims.CheatEngine.App.UserControls
{
    public partial class TitleBarControl : Control
    {
        private Color _textForeColour;

        public Color TextForeColour
        {
            get => _textForeColour;
            set
            {
                _textForeColour = value;
                Refresh();
            }
        }

        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Refresh(); }
        }
    }
}