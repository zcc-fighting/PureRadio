using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Message
{
    public class RadioDemandMessage
    {
        public int channe_id;
        public bool selected_day_change;
        public int selected_index;
        public List<RadioFullContent> PlayList;
        public RadioDemandMessage(int channe_id, bool selected_day_change, int selected_index, List<RadioFullContent> playList)
        {
            this.channe_id = channe_id;
            this.selected_day_change = selected_day_change;
            this.selected_index = selected_index;
            PlayList = playList;
        }
    }
}
