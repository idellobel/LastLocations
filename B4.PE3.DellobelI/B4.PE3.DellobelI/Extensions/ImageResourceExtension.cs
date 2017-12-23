using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.Extensions
{
    [ContentProperty("Source")]
    public class ImageResourceExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            //get the ImageSource based on the given ResourceId 
            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
