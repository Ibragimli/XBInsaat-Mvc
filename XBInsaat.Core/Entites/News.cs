using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBInsaat.Core.Entites
{

    public class News : BaseEntity
    {
        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string TitleRu { get; set; }
        public string TextAz { get; set; }
        public string TextEn { get; set; }
        public string TextRu { get; set; }
        public string InstagramUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public ICollection<NewsImage> NewsImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ProjectImageFile { get; set; }
        [NotMapped]
        public List<int> ProjectImagesIds { get; set; }
    }
}
