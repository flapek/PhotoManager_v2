using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slider
{
    class Pictures : ObservableCollection<Picture>
    {
        public Pictures()
        {
            Add(new Picture
            {
                Name = "1.jpg",
                FullName= new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
            Add(new Picture
            {
                Name = "2.jpg",
                FullName = new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
            Add(new Picture
            {
                Name = "3.jpg",
                FullName = new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
            Add(new Picture
            {
                Name = "4.jpg",
                FullName = new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
            Add(new Picture
            {
                Name = "5.jpg",
                FullName = new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
            Add(new Picture
            {
                Name = "6.jpg",
                FullName = new Uri(@"C:\Users\filap\Desktop\_DSC8277.jpg")
            });
        }
    }
}
