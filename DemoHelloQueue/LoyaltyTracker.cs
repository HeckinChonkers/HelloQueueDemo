using DemoHelloQueue.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DemoHelloQueue
{
    public class LoyaltyTracker
    {

        #region Private Variables
        #endregion

        #region Public Variables
        public Timer UserCheckTimer;
        #endregion

        #region Properties
        public int EarnFreq { get; set; }
        public int EarnAmount { get; set; }
        public string EarnName { get; set; }
        #endregion

        #region Constructor

        public LoyaltyTracker()
        {
            EarnFreq = (int)Settings.Default["EarnFreq"];
            EarnAmount = (int) Settings.Default["EarnAmount"];
            EarnName = Settings.Default["EarnName"].ToString();
        }

        public LoyaltyTracker(int freq, int amount, string name)
        {
            EarnFreq = freq;
            EarnAmount = amount;
            EarnName = name;
        }
        #endregion

        #region Public Methods
        #endregion

    }
}
