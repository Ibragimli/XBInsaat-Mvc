using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class Logger:BaseEntity
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string? Product { get; set; }
    }
}
