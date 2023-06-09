using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class ProjectImage:BaseEntity
    {
        public string Image { get; set; }
        public int ProjectId { get; set; }
        public bool IsPoster { get; set; }
        public Project Project { get; set; }
    }
}
