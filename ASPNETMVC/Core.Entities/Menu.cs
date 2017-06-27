using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Menu:BaseEntity
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public int ParentID { get; set; }
        public int MenuType { get; set; }
        public bool Status { get; set; }
    }
}
