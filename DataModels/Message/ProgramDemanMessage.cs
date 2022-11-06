using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Message
{
    public class ProgramDemandMessage
    {
        public int album_id;
        public int content_id;
        public string access_token;
        public string qingting_id;
        public string cover;
        public string title;
        public string album;

        public ProgramDemandMessage(int album_id, int content_id, string access_token, string qingting_id, string cover, string title, string album)
        {
            this.album_id = album_id;
            this.content_id = content_id;
            this.access_token = access_token;
            this.qingting_id = qingting_id;
            this.cover = cover;
            this.title = title;
            this.album = album;
        }
    }
}
