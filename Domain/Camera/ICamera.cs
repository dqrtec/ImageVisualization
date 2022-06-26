using Domain.Config;

namespace Domain.Camera
{
    public abstract class Camera
    {
        public Position position;
        Position lookAt;
        Position directionUp;
        Position iCamera = new Position(1, 0, 0);
        Position jCamera = new Position(0, 1, 0);
        Position kCamera = new Position(0, 0, 1);
        protected int image_width;
        protected int image_height;
        public Camera(Position pos, Position lkAt, Position directup)
        {
            position = pos;
            lookAt = lkAt;
            directionUp = directup;
        }

        public void SetScreen(int _image_width , int _image_height)
        {
            image_width = _image_width;
            image_height = _image_height;
        }

        public abstract Ray GenerateRay(int linha, int coluna);

        /*{
            Position ray;

            double xw = (-1.0 / 2.0) + (larguraPixels + (linha * larguraPixels));// NAO SEI AO CERTO
            double yw = (1.0 / 2.0) - (alturaPixels + (coluna * alturaPixels));// NAO SEI AO CERTO

            Position p = position + kCamera * (-1.0) + iCamera * xw + jCamera * yw;// acha o ponto central do pixel
            Position dir = p - position;// cria o raio subtraindo camera do pixel
            dir = dir.Normalize();// cria vetor unitario

            return dir;



            /*
            Position nposition = new Position(0,0,0);

            double xw = (-1.0 / 2.0) + (larguraPixels + (coluna * larguraPixels));// NAO SEI AO CERTO
            double yw = (1.0 / 2.0) - (alturaPixels + (linha * alturaPixels));// NAO SEI AO CERTO

            Position p = nposition + (kCamera * (-1)) + (iCamera * xw) + (jCamera * yw);// acha o ponto central do pixel
            p = p.Normalize();
            nposition = nposition.Normalize();
            Position dir = p - nposition;// cria o raio subtraindo camera do pixel

            ray  = dir.Normalize();// cria vetor unitario
            */
            /*
            ray = (lookAt- position).Normalize() + ray;
            ray = ray.Normalize(); 
            */
            /*
            double xw = (-1.0 / 2.0) + (larguraPixels + (coluna * larguraPixels));
            double yw = (1.0 / 2.0) - (alturaPixels + (linha * alturaPixels));

            Position ray;
            Position p = position + kCamera * (-1.0) + iCamera * xw + jCamera * yw;

            Position raioCentral = (lookAt - position).Normalize();
            Position locationPixel = p.Normalize(); //new Position(linha, coluna, 0).Normalize();

            ray = (raioCentral + locationPixel).Normalize();
            * /
            return ray;
        }*/
    }
}
