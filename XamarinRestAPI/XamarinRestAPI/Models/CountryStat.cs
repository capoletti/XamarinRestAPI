using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinRestAPI.Models
{
    public class CountryStat : BaseDataObject
    {
        public CountryStat(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
        public ImageSource Image { get; set; }
    }
}
