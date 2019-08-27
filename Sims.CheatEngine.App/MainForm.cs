using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Sims.CheatEngine.Domains;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common.Builders;

namespace Sims.CheatEngine.App
{
    public partial class MainForm : Form
    {
        private readonly DataAccess _dataAccess = new DataAccess("http://localhost:5000/api/");

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Thread.Sleep(1000);
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            backgroundWorker.WorkerSupportsCancellation = true;
            GetCheats(2, string.Empty);
        }

        private void GetCheats(int gameId, string query)
        {
            
            if(backgroundWorker.IsBusy)
                backgroundWorker.CancelAsync();
            
            backgroundWorker.RunWorkerAsync(DictionaryBuilder.CreateBuilder<string, string>()
                .Add("GameId", gameId.ToString())
                .Add("q", query)
                .ToDictionary());
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            listView1.Items.Clear();

            foreach (var game in (IEnumerable<Cheat>) e.Result)
            {
                var primaryListViewItem = new ListViewItem
                {
                    Name = game.Id.ToString(),
                    Text = game.Code
                };
                primaryListViewItem.SubItems.Add(game.Name);
                primaryListViewItem.SubItems.Add(game.Description);
                listView1.Items.Add(primaryListViewItem);
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = _dataAccess.RequestAsArray<Cheat>("Game/GetCheats", (IDictionary<string, string>) e.Argument);
        }

        private void TextBox1_TextChanged(object sender, System.EventArgs e)
        {
            GetCheats(2, textBox1.Text);
        }
    }
}