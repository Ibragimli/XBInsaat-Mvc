using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
   public class MidProject : BaseEntity
    {
        public string Name { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }
        public int HighProjectId { get; set; }
        public HighProject HighProject { get; set; }
        public ICollection<MidProjectImage> MidProjectImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ProjectImageFile { get; set; }
        [NotMapped]
        public List<int> ProjectImagesIds { get; set; }


    }
}
