namespace Domain.Config
{
    public class Position
    {
        public double PosX;
        public double PosY;
        public double PosZ;
        public double PosW;

        public Position(double posX, double posY, double posZ, double w = 0)
        {
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
            PosW = w;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.PosX + b.PosX, a.PosY + b.PosY, a.PosZ + b.PosZ);
        }
        public static Position operator -(Position a, Position b)
        {
            return new Position(a.PosX - b.PosX, a.PosY - b.PosY, a.PosZ - b.PosZ);
        }
        public static Position operator /(Position a, double b)
        {
            return new Position(a.PosX / b, a.PosY / b, a.PosZ / b);
        }
        public static Position operator *(Position a, Position b)
        {
            return new Position(a.PosX * b.PosX, a.PosY * b.PosY, a.PosZ * b.PosZ);
        }
        public static Position operator *(double b, Position a)
        {
            return new Position(a.PosX * b, a.PosY * b, a.PosZ * b);
        }
        public static Position operator *(Position a,double b)
        {
            return b * a;
        }
        public static double dot(Position a, Position b)
        {
            return a.PosX * b.PosX + a.PosY * b.PosY + a.PosZ * b.PosZ;
        }

        public static Position cross(Position u, Position v)
        {
            return new Position(u.PosY * v.PosZ - u.PosZ * v.PosY,
                            u.PosZ * v.PosX - u.PosX * v.PosZ,
                            u.PosX * v.PosY - u.PosY * v.PosX);
        }
        public Position Normalize()
        {
            return this / tamanho();
        }

        public double tamanho()
        {
            double tamanho = Math.Sqrt(Math.Pow(PosX, 2) + Math.Pow(PosY, 2) + Math.Pow(PosZ, 2));
            return tamanho;
        }
    }
}
