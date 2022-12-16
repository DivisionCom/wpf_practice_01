using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Stroymateriali.ApplicationData
{
    public partial class Materials
    {
        public string CorrectImage
        {
            get
            {
                if (String.IsNullOrEmpty(materials_photo) || String.IsNullOrWhiteSpace(materials_photo))
                {
                    return "/Resources/picture.jpg";
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory + "../../Resources/" + materials_photo;
                }
            }
        }
        public Brush CountZero
        {
            get
            {
                if (materials_count <= 0)
                {
                    return Brushes.Gray;
                }
                else
                {
                    return Brushes.Transparent;
                }
            }
        }
    }
}
