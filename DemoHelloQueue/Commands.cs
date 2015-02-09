using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoHelloQueue
{
    public partial class Commands : Form
    {
        public Commands()
        {
            InitializeComponent();
            foreach (KeyValuePair<string, string> pair in HelloQueue.commandDict){
                string command = pair.Key;
                string result = pair.Value;
                string[] row = { command, result };
                var lvi = new ListViewItem(row);
                lvi.Name = command;
                listView1.Items.Add(lvi);
            }
        }

        private void addCmdBtn_Click(object sender, EventArgs e)
        {
            if (!commandTxt.Text.Contains("!") && !String.IsNullOrEmpty(commandTxt.Text) && !String.IsNullOrEmpty(resultTxt.Text))
            {
                string command = "!" + commandTxt.Text;
                string result = resultTxt.Text;
                if (!HelloQueue.commandDict.ContainsKey(command))
                {
                    HelloQueue.commandDict.Add(command, result);
                    string[] row = {command, result};
                    var lvi = new ListViewItem(row);
                    lvi.Name = command;
                    listView1.Items.Add(lvi);
                }
                else
                {
                    HelloQueue.commandDict[command] = resultTxt.Text;
                    int indexOfCommand = listView1.Items.IndexOfKey(command);
                    listView1.Items[indexOfCommand].SubItems[1].Text = resultTxt.Text;
                }
                commandTxt.Clear();
                resultTxt.Clear();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedIndexCollection selectedIndicies = listView1.SelectedIndices;
            if (selectedIndicies.Count > 0)
            {
                commandTxt.Text = listView1.Items[selectedIndicies[0]].Text.Replace("!","");
                resultTxt.Text = listView1.Items[selectedIndicies[0]].SubItems[1].Text;
            }
            else
            {
                commandTxt.Text = "";
                resultTxt.Text = "";
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    rightClickContext.Show((Cursor.Position));
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int indexOfCommand = listView1.FocusedItem.Index;
            HelloQueue.commandDict.Remove(listView1.Items[indexOfCommand].Name);
            listView1.Items[indexOfCommand].Remove();
        }
    }
}
