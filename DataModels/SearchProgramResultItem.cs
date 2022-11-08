﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class SearchProgramResultItem
    {
        public int id { get; set; }
        public string title { get; set; }

        public string podcaster { get; set; }
        public string cover { get; set; }
        public int updatetime { get; set; }
        public string playcount { get; set; }
        public int program_count { get; set; }
        public string description { get; set; }
    }
}