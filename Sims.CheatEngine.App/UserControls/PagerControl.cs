using System;
using System.Windows.Forms;
using Sims.CheatEngine.Domains;

namespace Sims.CheatEngine.App.UserControls 
{
    public partial class PagerControl : Control
    {
        private void PanelControls_OnRemove(object sender, NEventArgs<Panel> e)
        {
            Controls.Remove(e.Item);
        }

        private void PanelControls_OnAdd(object sender, NEventArgs<Panel> e)
        {
            e.Item.Dock = DockStyle.Fill;
            e.Item.Visible = false;
            Controls.Add(e.Item);
        }

        public void SetPage(int pageIndex)
        {
            if (pageIndex <= 0 || pageIndex >= PanelControls.Count) return;
            foreach (var panelControl in PanelControls)
            {
                panelControl.Visible = false;
            }

            PanelControls[pageIndex].Visible = true;
            _currentPageIndex = pageIndex;
            PageChanged?.Invoke(this, new NEventArgs<Panel>(PanelControls[pageIndex]));
        }

        public event EventHandler<NEventArgs<Panel>> PageChanged;
    }
}