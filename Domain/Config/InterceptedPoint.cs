using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Config
{
    public class InterceptedPoint
    {
        public Domain.Objects.Objects? objects;
        public Position? position;
        public Position? normal;
        public double? distance;
        
        public InterceptedPoint(Domain.Objects.Objects? objects, Position? position, Position? normal, double? distance)
        {
            this.objects = objects;
            this.position = position;
            this.normal = normal;
            this.distance = distance;
        }
    }
}
