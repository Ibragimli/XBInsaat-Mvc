using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class XBService:BaseEntity
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }
    }
}
