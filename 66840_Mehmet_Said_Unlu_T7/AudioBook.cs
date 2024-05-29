using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public class AudioBook : Book
    {
        public string Narrator { get; set; }
        public string Duration { get; set; }
        public override string GetFormattedInfo()
        {
            return $"Title: {Title}, Author: {Author}, Category: {Category}, Type: {Type}, Narrator: {Narrator}, Duration: {Duration}";
        }
    }
}
