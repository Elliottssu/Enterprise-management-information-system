using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewsInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime SubDateTime { get; set; }
    }
}
