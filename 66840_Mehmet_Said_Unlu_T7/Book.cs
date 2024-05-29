using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public abstract class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }

        public abstract string GetFormattedInfo(); 
    }
}
