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

        public (int, int) SetScreen()
        {
            var aspect_ratio = 16.0 / 9.0;
            var _image_width = 400;
            var _image_height = (int)(image_width / aspect_ratio);

            image_width = _image_width;
            image_height = _image_height;
            return (image_width, image_height);
        }

        public abstract Ray GenerateRay(int linha, int coluna);
    }
}
