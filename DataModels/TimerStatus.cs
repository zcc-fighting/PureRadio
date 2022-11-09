using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class TimerStatus
    {
        public TimeSpan delay;
        public bool isOn;
        public string closeTime;

        public TimerStatus(TimeSpan timeSpan, bool status, string closetime)
        {
            delay = timeSpan;
            isOn = status;
            closeTime = closetime;
        }
    }
}
