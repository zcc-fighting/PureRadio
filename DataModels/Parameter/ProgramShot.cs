using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Parameter
{
    public class ProgramShot
    {
        public int programID;
        public string qingtingID;
        public string access_token;
        public bool isFav;
        public ProgramShot(int programID, string qingtingID, string access_token)
        {
            this.programID = programID;
            this.qingtingID = qingtingID;
            this.access_token = access_token;
        }
        public ProgramShot(int programID)
        {
            this.programID = programID;
            this.qingtingID = string.Empty;
        }
    }
}
