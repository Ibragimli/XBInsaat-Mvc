using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class Career : BaseEntity
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsCV { get; set; }
        public string Message { get; set; }
    }
}
