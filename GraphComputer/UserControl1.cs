using Domain.Camera;
using Domain.Cenario;
using Domain.Config;
using Domain.Luzes;
using Domain.Objects;

namespace GraphComputer
{
    public partial class UserControl1 : UserControl
    {
        #region Propertis
        Bitmap _canvas;
        Camera _camera;
        Cenario _cenario;

        private static UserControl1? _instance { get; set; }
        public static UserControl1 Instance
        {
            get
            {
                _instance = _instance != null ? _instance : new UserControl1();
                return _instance;
            }
            set { }
        }
        #endregion
        public UserControl1()
        {
            InitializeComponent();
            _canvas = new Bitmap(this.Size.Width, this.Size.Height);


            _camera = new CameraPerspectiva(new Position(0, 0, 0), new Position(0, 0, 0), new Position(0, 1, 0));
            _cenario = new Cenario();
            _cenario.AddObjects(GetObjects());
            _cenario.AddLuz(GetLuz());

            Draw();

            this.MouseWheel += new MouseEventHandler(MoveCam_MouseWheel);
        }

        void ClearScreen()
        {
            Graphics g = Graphics.FromImage(_canvas);
            g.Clear(Color.White);
        }

        public void Draw()
        {
            ClearScreen();

            var aspect_ratio = 16.0 / 9.0;
            var image_width = 400;
            var image_height = (int)(image_width / aspect_ratio);

            _camera.SetScreen(image_width, image_height);

            for (int j = image_height - 1; j >= 0; --j)
            {
                for (int i = 0; i < image_width; ++i)
                {
                    Ray ray = _camera.GenerateRay(i, j);

                    if (_cenario.Intersept(ray, out InterceptedPoint? intersept))
                    {
                        Color cor = getcor(intersept);
                        _canvas.SetPixel(i, j, cor);
                    }
                }
            }

            pictureBox1.Image = _canvas;
        }

        Color getcor(InterceptedPoint interceptedPoint)
        {
            return interceptedPoint.objects.material.color.GetValueOrDefault();
            /*
            if (hit_sphere(new Position(0, 0, -1), 0.5, r))
                return Color.FromArgb(255, 0, 0);

            Position unit_direction = (r.direction);
            double t = 0.5 * (unit_direction.PosY + 1.0);
            Position c = new Position(1.0, 1.0, 1.0) * (1.0 - t) + new Position(0.5, 0.7, 1.0) * t;
            c = c * 255;
            var cor = Color.FromArgb((int)c.PosX, (int)c.PosY, (int)c.PosZ);
            return cor;
            */
        }

        List<Luzes> GetLuz()
        {
            List<Luzes> luzes = new List<Luzes>();

            LuzPontual pontual = new LuzPontual(new Domain.Config.Position(10, 10, 5));

            return luzes;
        }
        List<Objects> GetObjects()
        {
            List<Objects> objects = new List<Objects>();

            objects.Add(new Circulo(new Position(0, 0, -1), 0.5, new Material(Color.FromArgb(255, 0, 0))));

            objects.Add(new Circulo(new Position(0, -7, -1), 5, new Material(Color.FromArgb(0, 255, 0))));

            return objects;
        }

        private void UserControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Position move = new Position(0, 0, 0);
            switch (e.KeyChar.ToString().ToLower())
            {
                case "a":
                    {
                        move.PosX = -0.1;
                        break;
                    }
                case "d":
                    {
                        move.PosX = 0.1;
                        break;
                    }
                case "s":
                    {
                        move.PosY = -0.1;
                        break;
                    }
                case "w":
                    {
                        move.PosY = 0.1;
                        break;
                    }
            }
            _camera.position = _camera.position + move;
            Draw();
        }

        private void MoveCam_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Position move = new Position(0, 0, 0);
            if(e.Delta > 0)
            {
                move.PosZ = -1;// e.Delta;
            }
            else
            {
                move.PosZ = 1;// e.Delta;
            }
            _camera.position = _camera.position + move;
            Draw();
        }
    }
}
