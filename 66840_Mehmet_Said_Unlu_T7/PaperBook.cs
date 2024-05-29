using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public class PaperBook : Book
    {
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public override string GetFormattedInfo()
        {
            return $"Title: {Title}, Author: {Author}, Category: {Category}, Type: {Type}, ISBN: {ISBN}, Number of Pages: {NumberOfPages}";
        }
    }
}
