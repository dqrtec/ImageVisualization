using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Objects
{
    public class Material
    {
        public System.Drawing.Color? color { get; set; }

        public Material(Color? color)
        {
            this.color = color;
        }
    }
}
