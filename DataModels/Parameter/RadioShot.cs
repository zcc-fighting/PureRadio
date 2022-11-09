using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Parameter
{
    public class RadioShot
    {
        public int channelID;
        public bool isFav;

        public RadioShot(int channelID)
        {
            this.channelID = channelID;
        }
    }
}
