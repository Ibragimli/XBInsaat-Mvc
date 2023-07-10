using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class ContactUs : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Message { get; set; }
    }
}
