using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinRestAPI.Models
{
    public class ImageDetail : BaseDataObject
    {
        public string Name { get; set; }

        public ImageSource Image { get; set; }
    }
}
