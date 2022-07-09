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
        readonly Bitmap _canvas;
        readonly Camera _camera;
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

            (int image_width, int image_height) = _camera.SetScreen();

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
            Color ResultColor = Color.White;
            Color cor = interceptedPoint.objects.material.color.GetValueOrDefault();
            (double r, double g, double b) = (cor.R * interceptedPoint.objects.material.Ka, cor.G * interceptedPoint.objects.material.Ka, cor.B * interceptedPoint.objects.material.Ka);

            foreach (var luz in _cenario._luzes)
            {
                //var trajetoraLuz = interceptedPoint.position - luz.position;
                //Ray ray = new Ray(luz.position, trajetoraLuz);
                //if (!_cenario.HasObjectBetween(ray, interceptedPoint.objects))
                //{

                //diferencial
                var L = (luz.position - interceptedPoint.position).Normalize();
                double fatorDiferencial = Position.dot(interceptedPoint.normal.Normalize(), L.Normalize());
                if (fatorDiferencial < 0)
                    fatorDiferencial = 0;

                //difusa
                r += luz.cor.R * fatorDiferencial * interceptedPoint.objects.material.difuse.PosX * interceptedPoint.objects.material.Kd;
                g += luz.cor.G * fatorDiferencial * interceptedPoint.objects.material.difuse.PosY * interceptedPoint.objects.material.Kd;
                b += luz.cor.B * fatorDiferencial * interceptedPoint.objects.material.difuse.PosZ * interceptedPoint.objects.material.Kd;

                //reflexao
                
                Position reflexao = reflects(L, interceptedPoint.normal);
                double fatorEspecular = Position.dot(interceptedPoint.ray.direction, reflexao);
                if (fatorEspecular < 0)
                    fatorEspecular = 0;

                //especular
                r += luz.cor.R * Math.Pow(fatorEspecular, interceptedPoint.objects.material.k) * interceptedPoint.objects.material.especular.PosX * interceptedPoint.objects.material.Ks;
                g += luz.cor.G * Math.Pow(fatorEspecular, interceptedPoint.objects.material.k) * interceptedPoint.objects.material.especular.PosY * interceptedPoint.objects.material.Ks;
                b += luz.cor.B * Math.Pow(fatorEspecular, interceptedPoint.objects.material.k) * interceptedPoint.objects.material.especular.PosZ * interceptedPoint.objects.material.Ks;
                

                ResultColor = NormalizeColor((int)r, (int)g, (int)b);
                //}
            }
            return ResultColor;
        }

        Color NormalizeColor(double r, double g, double b)
        {
            return Color.FromArgb(
                r > 255 ? 255 : (int)r,
                g > 255 ? 255 : (int)g,
                b > 255 ? 255 : (int)b);
        }

        Position reflects(Position raio, Position norm)
        {
            raio.PosX *= -1.0;
            raio.PosY *= -1.0;
            raio.PosZ *= -1.0;
            Position reflect = raio - (norm * (2.0 * Position.dot(raio, norm)));
            reflect.Normalize();
            return reflect;
        }

        List<Luzes> GetLuz()
        {
            List<Luzes> luzes = new List<Luzes>
            {
                new LuzPontual(new Domain.Config.Position(1, 10, -1))
            };
            return luzes;
        }
        List<Objects> GetObjects()
        {
            List<Objects> objects = new List<Objects>()
            {
                new Circulo(new Position(0, 0, -1), 0.5, new Material(Color.FromArgb(100, 10, 10))),
                //new Circulo(new Position(0, -7, -1), 5, new Material(Color.FromArgb(0, 255, 0)))
            };

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

        private void MoveCam_MouseWheel(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            Position move = new Position(0, 0, 0);
            if (e.Delta > 0)
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
