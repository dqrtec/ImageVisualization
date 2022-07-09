using Domain.Config;

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
