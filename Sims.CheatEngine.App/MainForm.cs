using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sims.CheatEngine.Domains;
using Sims.CheatEngine.Domains.Data;
using WebToolkit.Common;
using WebToolkit.Common.Builders;
using WebToolkit.Contracts;
using WebToolkit.Shared;

namespace Sims.CheatEngine.App
{
    public partial class MainForm : Form
    {
        private readonly DataAccess _dataAccess = new DataAccess("http://localhost:5000/api/");
        private readonly IAsyncMessageQueue _requestMessageQueue = AsyncMessageQueue.CreateMessageQueue(500);

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Thread.Sleep(1000);
            GetCheats(2, string.Empty);
            _requestMessageQueue.BeginProcessingQueue();
        }

        private void GetCheats(int gameId, string query)
        {
            var parameters = DictionaryBuilder.CreateBuilder<string, string>()
                .Add("gameId", gameId.ToString());

            if(!string.IsNullOrWhiteSpace(query))
                parameters.Add("q", query);

            _requestMessageQueue.Enqueue(new AsyncMessage(parameters.ToDictionary(), 
                DoWork, WorkCompleted));
;
        }

        private void WorkCompleted(object result)
        {
            var items = new List<ListViewItem>();
            foreach (var game in (IEnumerable<Cheat>) result)
            {
                var primaryListViewItem = new ListViewItem
                {
                    Name = game.Id.ToString(),
                    Text = game.Code
                };
                primaryListViewItem.SubItems.Add(game.Name);
                primaryListViewItem.SubItems.Add(game.Description);
                items.Add(primaryListViewItem);
            }

            Clear();
            AddRange(items.ToArray());
        }

        private void Clear()
        {
            if (listView1.InvokeRequired)
            {
                var clearCallBack = new ClearCallback(Clear);
                Invoke(clearCallBack);
            }
            else
            {
                listView1.Items.Clear();
            }
        }

        private void AddRange(ListViewItem[] items)
        {
            if (listView1.InvokeRequired)
            {
                var addRangeCallback = new AddRangeCallback(AddRange);
                Invoke(addRangeCallback, new object[] { items });
            }
            else
            {
                listView1.Items.AddRange(items);
            }
        }

        private void SetTitle(string title)
        {
            if (titleBarControl1.InvokeRequired)
            {
                var titleCallback = new SetTitleDelegate(SetTitle);
                Invoke(titleCallback, title);
            }
            else
            {
                titleBarControl1.Text = title;
            }
        }

        private delegate void ClearCallback();
        private delegate void AddRangeCallback(ListViewItem[] items);
        private delegate void SetTitleDelegate(string title);

        private async Task<object> DoWork(object argument)
        {
            return await Task.FromResult(_dataAccess
                .RequestAsArray<Cheat>("Game/GetCheats", (IDictionary<string, string>) argument));
        }

        private void TextBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox1.Text.Length > 3)
                GetCheats(2, textBox1.Text);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _requestMessageQueue.Dispose();
        }
    }
}