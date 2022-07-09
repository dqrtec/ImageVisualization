using Domain.Config;
using System.Drawing;

namespace Domain.Luzes
{
    public class Luzes
    {
        public Position position;
        public Color cor = Color.White;
        public double intensidade;
        public double decaimento;

        public Luzes(Position position)
        {
            this.position = position;
            this.cor = Color.White;
            this.intensidade = 100;
            this.decaimento = 1;
        }

        public double intensidadeAtPoint(Position point)
        {
            double result = intensidade - ((position - point).tamanho() * decaimento);
            return result >= 0 ? result : 0;
        }

    }
}
