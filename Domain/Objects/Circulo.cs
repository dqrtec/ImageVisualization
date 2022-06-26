using Domain.Config;

namespace Domain.Objects
{
    public class Circulo : Objects
    {
        readonly double r;
        public Circulo(Position position, double _r, Material material) : base(position, material)
        {
            r = _r;
        }

        public override bool Intersept(Ray ray, out InterceptedPoint? interceptedPoint)
        {
            bool isIntercepted = false;
            interceptedPoint = null;

            Position oc = ray.origin - position;
            var a = Position.dot(ray.direction, ray.direction);
            var b = 2.0 * Position.dot(oc, ray.direction);
            var c = Position.dot(oc, oc) - r * r;
            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                isIntercepted = false;
            else if (discriminant > 0)
            {
                double sqrtDelta = Math.Sqrt(discriminant);
                double t0 = (-b - sqrtDelta) / (2 * a);
                double t1 = (-b + sqrtDelta) / (2 * a);
                double menorT = (t0 < t1) ? t0 : t1;

                var pointHited = ray.origin + ray.direction * menorT;
                interceptedPoint = new InterceptedPoint(this, pointHited, GetNormal(pointHited), menorT);
                isIntercepted = true;
            }
            else if (discriminant == 0)
            {
                double dist = -b / (2 * a);

                var pointHited = ray.origin + ray.direction * dist;
                interceptedPoint = new InterceptedPoint(this, pointHited, GetNormal(pointHited), dist);
                isIntercepted = true;
            }

            return isIntercepted;
        }

        private Position? GetNormal(Position pointHited)
        {
            return (pointHited - this.position).Normalize();
        }
    }
}
