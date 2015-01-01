using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace DemoHelloQueue
{
    public partial class HelloQueue : Form
    {
        private Thread TwitchIRCThread;
        TwitchIRC helloQueueConn;
        List<string> userList;
        public delegate void dgAddToList(object sender, IRCEventArgs e);

        public HelloQueue()
        {
            InitializeComponent();
            userList = new List<string>();
        }

        private void sendToQueue(object sender, IRCEventArgs e)
        {
            if (listBox1.InvokeRequired)
            {
                this.BeginInvoke(new dgAddToList(sendToQueue), new object[] { sender, e });
            }
            else {
                if (!String.IsNullOrEmpty(filterTxtBox.Text))
                {
                    if (!userList.Contains(e.Username) && e.Message.Contains(filterTxtBox.Text))
                    {
                        userList.Add(e.Username);
                        listBox1.Items.Add(e.Username + ": " + e.Message);
                        listBox1.Items.Add("-------------------------------------------------------------------");
                        listBox1.Update();
                    }
                }
                else if (!userList.Contains(e.Username))
                {
                    userList.Add(e.Username);
                    listBox1.Items.Add(e.Username + ": " + e.Message);
                    listBox1.Items.Add("-------------------------------------------------------------------");
                    listBox1.Update();
                }
            }
        }

        private void ConnectTwitchMenuItem_Click(object sender, EventArgs e)
        {
            if (TwitchIRCThread == null || !TwitchIRCThread.IsAlive)
            {
                helloQueueConn = new TwitchIRC(Globals.IrcServer, Globals.IrcPort, Globals.IrcUser, Globals.IrcChan, Globals.IrcPass);
                helloQueueConn.OnGotMessage += new TwitchIRC.RecievedMessage(sendToQueue);
                TwitchIRCThread = new Thread(() => helloQueueConn.Connect());
                TwitchIRCThread.Start();
            }

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TwitchIRCThread != null && TwitchIRCThread.IsAlive)
            {
                helloQueueConn.RequestStop();
                TwitchIRCThread.Join();
            }

            Properties.Settings.Default.IrcServer = Globals.IrcServer;
            Properties.Settings.Default.IrcUser = Globals.IrcUser;
            Properties.Settings.Default.IrcChannel= Globals.IrcChan;
            Properties.Settings.Default.IrcPass = Globals.IrcPass;
            Properties.Settings.Default.IrcPort = Globals.IrcPort;
            Properties.Settings.Default.Save();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration configWindow = new Configuration();
            configWindow.Show();
        }

        private void HelloQueue_Load(object sender, EventArgs e)
        {
            Globals.IrcServer = Properties.Settings.Default.IrcServer;
             Globals.IrcUser = Properties.Settings.Default.IrcUser;
             Globals.IrcChan = Properties.Settings.Default.IrcChannel;
             Globals.IrcPass = Properties.Settings.Default.IrcPass;
             Globals.IrcPort = Properties.Settings.Default.IrcPort;
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = (int)e.Graphics.MeasureString(listBox1.Items[e.Index].ToString(), listBox1.Font, listBox1.Width).Height;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            userList.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.Show();
        }
    }
}
