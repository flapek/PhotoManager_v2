using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoManager_v2.Class.Photo
{
    class Photo
    {
        public string Name { set; get; }
        public DateTime DateOfExecutin { get; set; }
        public PhotoDimensions Dimensions { set; get; }
        public double Size { set; get; }
        public List<Author> Author { set; get; }
        public List<string> Tags { set; get; }
        public string PathFolder { set; get; }
        public string Comment { set; get; }
        public double ApertureUnit { set; get; }
        public double ExposureTime { set; get; }
        public double FocalLenght { set; get; }
    }
}
