using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Sims.CheatEngine.Domains;

namespace Sims.CheatEngine.App.UserControls 
{
    public partial class PagerControl
    {
        public NCollection<Panel> PanelControls { get; }
        private int _currentPageIndex = 0;

        public int CurrentPageIndex
        {
            get => _currentPageIndex;
            set => SetPage(value);
        }

        public PagerControl()
        {
            PanelControls = new NCollection<Panel>();
            PanelControls.ItemAdded += PanelControls_OnAdd;
            PanelControls.ItemRemoved += PanelControls_OnRemove;
        }
    }
}