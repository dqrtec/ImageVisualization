using Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Objects
{
    public class Objects
    {
        protected Position position;
        public Material material;
        public Objects(Position pos, Material material)
        {
            position = pos;
            this.material = material;
        }

        public virtual bool Intersept(Ray ray, out InterceptedPoint? interceptedPoint)
        {
            interceptedPoint = null;
            return false;
        }
    }
}
