using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class HighProjectMidProjectId : BaseEntity
    {
        public int HighProjectId { get; set; }
        public int MidProjectId { get; set; }
        public MidProject MidProject { get; set; }
        public HighProject HighProject { get; set; }
    }
}
