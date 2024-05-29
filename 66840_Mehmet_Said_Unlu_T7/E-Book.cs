using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public class EBook : Book
    {
        public string Format { get; set; }
        public string FileSize { get; set; }
        public override string GetFormattedInfo()
        {
            return $"Title: {Title}, Author: {Author}, Category: {Category}, Type: {Type}, Format: {Format}, File Size: {FileSize}";
        }
    }
}
