namespace Domain.Config
{
    public class InterceptedPoint
    {
        public Domain.Objects.Objects? objects;
        public Position? position;
        public Position? normal;
        public double? distance;
        public Ray ray;
        public InterceptedPoint(Domain.Objects.Objects? objects, Position? position, Position? normal, double? distance, Ray ray)
        {
            this.objects = objects;
            this.position = position;
            this.normal = normal;
            this.distance = distance;
            this.ray = ray;
        }
    }
}
