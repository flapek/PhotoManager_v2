using System;
using System.Collections.Generic;

namespace PhotoManager_v2.Class.Photo
{
    class Picture
    {
        public string Name { get; set; }
        public DateTime DateOfExecutin { get; set; }
        public PhotoDimensions Dimensions { get; set; }
        public double Size { get; set; }
        public List<Author> Author { get; set; }
        public List<string> Tags { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public double ApertureUnit { get; set; }
        public double ExposureTime { get; set; }
        public double FocalLenght { get; set; }
    }
}
