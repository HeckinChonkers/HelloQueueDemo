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
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
            serverTxtBox.Text = Globals.IrcServer;
            channelTxtBox.Text = Globals.IrcChan;
            userTxtBox.Text = Globals.IrcUser;
            oauthTxtBox.Text = Globals.IrcPass;
            portTxtBox.Text = Globals.IrcPort.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serverTxtBox.Text == "irc.twitch.tv" && !oauthTxtBox.Text.Contains("oauth:"))
            {
                MessageBox.Show("Twitch IRC requires an oauth token as a password. Please enter the oauth token beginning with \"oauth:\" followed by the generated code");
            }

            if (String.IsNullOrEmpty(serverTxtBox.Text))
                MessageBox.Show("Server is required.");
            else if (String.IsNullOrEmpty(userTxtBox.Text))
                MessageBox.Show("User is required.");
            else if (String.IsNullOrEmpty(channelTxtBox.Text))
                MessageBox.Show("Channel is required.");
            else if (String.IsNullOrEmpty(portTxtBox.Text))
                MessageBox.Show("Port is required.");

            try
            {
                Globals.IrcPort = Convert.ToInt32(portTxtBox.Text);
                Globals.IrcServer = serverTxtBox.Text;
                Globals.IrcChan = channelTxtBox.Text;
                Globals.IrcUser = userTxtBox.Text;
                Globals.IrcPass = oauthTxtBox.Text;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                    MessageBox.Show("Port must be a valid port number.");
                else
                    MessageBox.Show("Exception was thrown: " + ex);
            }

            this.Close();

        }
    }
}
