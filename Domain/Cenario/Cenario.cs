using Domain.Config;

namespace Domain.Cenario
{
    public class Cenario
    {
        List<Objects.Objects> _objects = new List<Objects.Objects>();
        List<Luzes.Luzes> _luzes = new List<Luzes.Luzes>();

        public bool Intersept(Ray ray, out InterceptedPoint? interceptPoint)
        {
            bool isIntercepted = false;
            interceptPoint = null;

            foreach (var obj in _objects)
            {
                if (obj.Intersept(ray, out InterceptedPoint? newIntercept))
                {
                    if(interceptPoint == null || newIntercept?.distance.GetValueOrDefault() < interceptPoint.distance.GetValueOrDefault())
                    {
                        interceptPoint = newIntercept;
                        isIntercepted = true;
                    }
                }
            }

            return isIntercepted;
        }

        public void AddObjects(List<Objects.Objects> objects)
        {
            _objects.AddRange(objects);
        }
        public void AddObjects(Objects.Objects objects)
        {
            _objects.Add(objects);
        }
        public void AddLuz(List<Luzes.Luzes> luzes)
        {
            _luzes.AddRange(luzes);
        }
        public void AddLuz(Luzes.Luzes luz)
        {
            _luzes.Add(luz);
        }
    }
}
