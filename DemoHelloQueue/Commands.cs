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
        public Commands()
        {
            InitializeComponent();
            foreach (KeyValuePair<string, string> pair in HelloQueue.commandDict){
                string command = pair.Key;
                string result = pair.Value;
                string[] row = { command, result };
                var lvi = new ListViewItem(row);
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
                    string[] row = { command, result };
                    var lvi = new ListViewItem(row);
                    listView1.Items.Add(lvi);
                }
            }
        }
    }
}
