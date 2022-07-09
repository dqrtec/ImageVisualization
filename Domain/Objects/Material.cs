using Domain.Config;
using System.Drawing;

namespace Domain.Objects
{
    public class Material
    {
        public System.Drawing.Color? color { get; set; }
        public double k { get; set; }
        public double Ka { get; set; }//ambiente
        public double Kd { get; set; }//difusa
        public double Ks { get; set; }//especular
        public Position ambiente { get; set; }
        public Position difuse { get; set; }
        public Position especular { get; set; }

        public Material(Color? color)
        {
            this.color = color;
            this.k = 4;
            this.Ka = 1;
            this.Kd = 1;
            this.Ks = 1;

            //ambiente = new Position(.1,.1,.08);
            difuse = new Position(0.9, 0.5, 0.1);
            especular = new Position(1.0, 1.0, 1.0);
        }
    }
}
