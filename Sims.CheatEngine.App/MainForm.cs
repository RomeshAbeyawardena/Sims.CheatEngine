using System.Collections.Generic;
using System.Windows.Forms;
using Sims.CheatEngine.Domains;
using Sims.CheatEngine.Domains.Data;

namespace Sims.CheatEngine.App
{
    public partial class MainForm : Form
    {
        private readonly DataAccess dataAccess = new DataAccess("http://localhost:5000/api/");
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            var request = dataAccess.RequestAsArray<Game>("Game/GetGames");
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            foreach (var game in (IEnumerable<Game>) e.Result)
            {
                var primaryListViewItem = new ListViewItem()
                {
                    Text = game.Id.ToString()
                };
                primaryListViewItem.SubItems.Add(game.Name);
                primaryListViewItem.SubItems.Add(game.Description);
                listView1.Items.Add(primaryListViewItem);
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = dataAccess.RequestAsArray<Game>("Game/GetGames");
        }
    }
}