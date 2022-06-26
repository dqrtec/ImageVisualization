using Domain.Config;

namespace Domain.Camera
{
    public class CameraPerspectiva : Camera
    {
        public CameraPerspectiva(Position position, Position lkat, Position up) : base(position, lkat, up)
        {

        }

        public override Ray GenerateRay(int linha, int coluna)
        {
            var aspect_ratio = 16.0 / 9.0;
            // Camera
            double viewport_height = 2.0;
            double viewport_width = aspect_ratio * viewport_height;
            double focal_length = 1.0;

            Position origin = position;//new Position(0, 0, 0);
            Position horizontal = new Position(viewport_width, 0, 0);
            Position vertical = new Position(0, viewport_height, 0);
            Position lower_left_corner = origin - horizontal / 2 - vertical / 2 - new Position(0, 0, focal_length);

            double u = linha / (double)(image_width - 1);
            double v = coluna / (double)(image_height - 1);
            Ray r = new Ray(origin, lower_left_corner + horizontal * u + vertical * v - origin);
            return r;
            /*
            Position nposition = new Position(0, 0, 0);

            double xw = (-1.0 / 2.0) + (larguraPixels + (coluna * larguraPixels));// NAO SEI AO CERTO
            double yw = (1.0 / 2.0) - (alturaPixels + (linha * alturaPixels));// NAO SEI AO CERTO

            Position p = nposition + (kCamera * (-1)) + (iCamera * xw) + (jCamera * yw);// acha o ponto central do pixel
            p = p.Normalize();
            nposition = nposition.Normalize();
            Position dir = p - nposition;// cria o raio subtraindo camera do pixel

            ray = dir.Normalize();// cria vetor unitario
            ray = (lookAt - position).Normalize() + ray;
            ray = ray.Normalize();

            double xw = (-1.0 / 2.0) + (larguraPixels + (coluna * larguraPixels));
            double yw = (1.0 / 2.0) - (alturaPixels + (linha * alturaPixels));

            Position ray;
            Position p = position + kCamera * (-1.0) + iCamera * xw + jCamera * yw;

            Position raioCentral = (lookAt - position).Normalize();
            Position locationPixel = p.Normalize(); //new Position(linha, coluna, 0).Normalize();

            ray = (raioCentral + locationPixel).Normalize();
            return ray;
            */
        }
    }
}
