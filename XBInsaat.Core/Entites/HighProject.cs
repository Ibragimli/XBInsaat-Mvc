using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{
    public class HighProject : BaseEntity
    {
        public string Name { get; set; }
        public string DescribeAz { get; set; }
        public string DescribeEn { get; set; }
        public string DescribeRu { get; set; }
        public string InstagramUrl { get; set; }
        public string ContactInfo { get; set; }
        public ICollection<MidProject> MidProjects { get; set; }
        public ICollection<HighProjectImage> HighProjectImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ProjectImageFile { get; set; }
        [NotMapped]
        public List<int> ProjectImagesIds { get; set; }


    }
}
