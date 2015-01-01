using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoHelloQueue
{
    public class IRCEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public string Username { get; private set; }

        public IRCEventArgs(string user, string message)
        {
            Username = user;
            Message = message;
        }
    }
}
