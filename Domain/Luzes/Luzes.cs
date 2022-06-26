using Domain.Config;
using System.Drawing;

namespace Domain.Luzes
{
    public class Luzes
    {
        Position position;
        Color cor = Color.White;
        double intensidade;
        double decaimento;

        public Luzes(Position position)
        {
            this.position = position;
            this.cor = new Color();
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
