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
        public List<string> Tags { set; get; }
        public Author Author { set; get; }
        public double Size { set; get; }
        public PhotoDimensions dimensions { set; get; }
        public string TypeOfElement { set; get; }
    }
}
