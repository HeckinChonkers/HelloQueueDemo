using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoHelloQueue
{
    public partial class Commands : Form
    {
        public dbDataSet.changeBot_commandDataTable commandTable = new dbDataSet.changeBot_commandDataTable();

        public Commands()
        {
            InitializeComponent();
            commandTable = HelloQueue.commandsTA.GetData();
            foreach (DataRow row in commandTable)
            {
                if (!HelloQueue.commandDict.ContainsKey(row["trigger"].ToString()))
                    HelloQueue.commandDict.Add(row["trigger"].ToString(), row["result"].ToString());
            }

            Dictionary<string, string> tempDict = new Dictionary<string,string>(HelloQueue.commandDict);

            foreach (KeyValuePair<string, string> pair in tempDict){
                if (!commandTable.Rows.Contains(pair.Key))
                {
                    HelloQueue.commandDict.Remove(pair.Key);
                    HelloQueue.swabbotDB.changeBot_command.Rows.Remove(HelloQueue.swabbotDB.changeBot_command.Rows.Find(pair.Key.ToString()));
                    continue;
                }
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
                    DataRow newRow = HelloQueue.swabbotDB.changeBot_command.NewRow();
                    newRow[0] = command;
                    newRow[1] = result;
                    HelloQueue.swabbotDB.changeBot_command.Rows.Add(newRow);
                    HelloQueue.commandDict.Add(command, result);
                    string[] row = {command, result};
                    var lvi = new ListViewItem(row);
                    lvi.Name = command;
                    listView1.Items.Add(lvi);
                }
                else
                {
                    DataRow foundRow = HelloQueue.swabbotDB.changeBot_command.Rows.Find(command);

                    if (foundRow != null)
                    {
                        foundRow[1] = resultTxt.Text;
                    }

                    HelloQueue.commandDict[command] = resultTxt.Text;
                    int indexOfCommand = listView1.Items.IndexOfKey(command);
                    listView1.Items[indexOfCommand].SubItems[1].Text = resultTxt.Text;
                }
                HelloQueue.UpdateDB();
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
            DataRow newRow = HelloQueue.swabbotDB.changeBot_command.Rows.Find(listView1.Items[indexOfCommand].Name);
            int indexRow = HelloQueue.swabbotDB.changeBot_command.Rows.IndexOf(newRow);
            HelloQueue.swabbotDB.changeBot_command.Rows[indexRow].Delete();
            HelloQueue.UpdateDB();
            listView1.Items[indexOfCommand].Remove();
        }

        private void Commands_FormClosing(object sender, FormClosingEventArgs e)
        {
            HelloQueue.UpdateDB();
        }
    }
}
