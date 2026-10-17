using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace sem_1_lab_05_save_manager
{
    public class SaveEntry
    {
        public string name { get; set; }
        public string file { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
