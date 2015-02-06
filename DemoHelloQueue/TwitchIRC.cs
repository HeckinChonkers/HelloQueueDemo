using System;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace DemoHelloQueue {
	
	public class TwitchIRC {
		
		#region Private Variables
		private string ircServer;
		private int ircPort;
		private string ircNick;
		private string ircUser;
		private string ircRealName;
		private string ircChannel;
        private string ircPass;
		private TcpClient ircConnection;
		private NetworkStream ircStream;
		private StreamWriter ircWriter;
		private StreamReader ircReader;
		#endregion

        #region Public Variables
        public delegate void RecievedMessage(object sender, IRCEventArgs e);
        public delegate void lostConnection(object sender, EventArgs e);
        public delegate void gotConnection(object sender, EventArgs e);
        public event RecievedMessage OnGotMessage;
        public event lostConnection ConnectionLost;
	    public bool isConnected = false;
	    public bool shouldRun = false;
        #endregion

        #region Properties

        public string IrcServer {
			get { return this.ircServer; }
			set { this.ircServer = value; }
		} /* IrcServer */

		public int IrcPort {
			get { return this.ircPort; }
			set { this.ircPort = value; }
		} /* IrcPort */
		
		public string IrcNick {
			get { return this.ircNick; }
			set { this.ircNick = value; }
		} /* IrcNick */
		
		public string IrcUser {
			get { return this.ircUser; }
			set { this.ircUser = value; }
		} /* IrcUser */
		
		public string IrcRealName {
			get { return this.ircRealName; }
			set { this.ircRealName = value; }
		} /* IrcRealName */
		
		public string IrcChannel {
			get { return this.ircChannel; }
			set { this.ircChannel = value; }
		} /* IrcChannel */

        public string IrcPass
        {
            get { return this.ircPass; }
            set { this.ircPass = value; }
        }/* IrcPass */
		
		public TcpClient IrcConnection {
			get { return this.ircConnection; }
			set { this.ircConnection = value; }
		} /* IrcConnection */
		
		public NetworkStream IrcStream {
			get { return ircStream; }
			set { ircStream = value; }
		} /* IrcStream */
		
		public StreamWriter IrcWriter {
			get { return ircWriter; }
			set { ircWriter = value; }
		} /* IrcWriter */
		
		public StreamReader IrcReader {
			get { return this.ircReader; }
			set { this.ircReader = value; }
		} /* IrcReader */
		#endregion	
		
		#region Constructor
		public TwitchIRC(string IrcServer, int IrcPort, string IrcUser, string IrcChan, string IrcPass) {
			this.ircNick = IrcUser;
			this.ircRealName = IrcUser;
			this.ircChannel = IrcChan;
            this.ircPort = IrcPort;
            this.ircServer = IrcServer;
            this.ircPass = IrcPass;
		}
		#endregion
		
		#region Public Methods
		public void Connect() {
            try
            {
                shouldRun = true;
                // Connect with the IRC server.
                this.IrcConnection = new TcpClient(this.ircServer, this.ircPort);
                this.IrcStream = this.IrcConnection.GetStream();
                this.IrcReader = new StreamReader(this.IrcStream);
                this.IrcWriter = new StreamWriter(this.IrcStream);
                // Authenticate
                if (!String.IsNullOrEmpty(this.ircPass))
                {
                    this.IrcWriter.WriteLine(String.Format("PASS {0}", this.ircPass));
                    this.IrcWriter.Flush();
                }
                this.IrcWriter.WriteLine(String.Format("NICK {0}", this.ircNick));
                this.IrcWriter.Flush();
                this.IrcWriter.WriteLine(String.Format("JOIN {0}", this.ircChannel));
                this.IrcWriter.Flush();
            }
            catch
            {
                MessageBox.Show(String.Format("Unable to connect to {0} using credentials. Please edit your connection settings.", this.ircChannel));
                return;
            }
			// Listen for commands
            try
            {
                while (true)
                {
                    string ircMessage;
                    while (shouldRun == true)
                    {
                        ircMessage = IrcReader.ReadLine();

                        //Debug
                        Console.Out.WriteLine(ircMessage);

                        string[] messageParts = new string[ircMessage.Split(' ').Length];
                        messageParts = ircMessage.Split(' ');
                        if (messageParts[0].Substring(0, 1) == ":" || messageParts[0] == "PING")
                        {
                            if (messageParts[0] != "PING")
                            messageParts[0] = messageParts[0].Remove(0, 1);

                            if (ircMessage.Contains("Login unsuccessful"))
                                throw new Exception(ircMessage);
                            else if (ircMessage.Contains((":Welcome, GLHF!")))
                            {
                                GotConnection(this, new EventArgs());
                                isConnected = true;
                            }
                            int resultNumber;
                            if (int.TryParse(messageParts[1], out resultNumber))
                            {
                                if (resultNumber <= 553 && resultNumber >= 400)
                                    throw new Exception(ircMessage);
                            }
                            if (messageParts[0] == "PING")
                            {
                                // Server PING, send PONG back
                                this.IrcPing(messageParts);
                            }
                            else
                            {
                                if (ircMessage.Contains("PRIVMSG"))
                                {
                                    string userName = ircMessage.Substring(ircMessage.IndexOf(":") + 1, ircMessage.IndexOf("!") - 1);
                                    string contents = ircMessage.Substring(ircMessage.IndexOf(Globals.IrcChan.ToLower()) + Globals.IrcChan.Length + 2);

                                    if (OnGotMessage != null)
                                    {
                                        IRCEventArgs args = new IRCEventArgs(userName, contents);
                                        OnGotMessage(this, args);
                                    }
                                }
                            }
                        }
                    }

                    isConnected = false;
                    this.IrcWriter.Close();
                    this.IrcReader.Close();
                    this.IrcConnection.Close();
                    
                    //Debug
                    Console.Out.WriteLine("Connection to Twitch is lost");
                    ConnectionLost(this, new EventArgs());
                    return;
                }
         }
         catch(Exception ex)
         {
             isConnected = false;
             this.IrcWriter.Close();
             this.IrcReader.Close();
             this.IrcConnection.Close();
             
             //debug
             Console.Out.WriteLine("Connection to IRC server is lost: " + ex.Message);
             if (!ex.Message.Contains("WSACancelBlockingCall"))
                ConnectionLost(this, new EventArgs());
             return;
         }
        }

	    public void SendMessage(string messageToSend)
	    {
	        if (isConnected)
	        {
                IrcWriter.WriteLine("PRIVMSG " + this.ircChannel + " :" + messageToSend);
                IrcWriter.Flush();
	        }
	    }

        public void RequestStop()
        {
            shouldRun = false;
        }

		#endregion
		
		#region Private Methods		
		#region Ping
		private void IrcPing(string[] IrcCommand) {
			string PingHash = "";
			for (int intI = 1; intI < IrcCommand.Length; intI++) {
				PingHash += IrcCommand[intI] + " ";
			}
			this.IrcWriter.WriteLine("PONG " + PingHash);
			this.IrcWriter.Flush();
		} /* IrcPing */
		#endregion
		#endregion

        public gotConnection GotConnection { get; set; }
    }
}