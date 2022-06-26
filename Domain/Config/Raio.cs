namespace Domain.Config
{
    public class Ray
    {
        public Position origin;
        public Position direction;

        public Ray(Position origin, Position direction)
        {
            this.origin = origin;
            this.direction = direction.Normalize();
        }

        Position At(double t)
        {
            return this.origin + this.direction * t;
        }
    }
}
