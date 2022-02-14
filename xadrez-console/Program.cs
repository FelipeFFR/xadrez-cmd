using System;
using xadrez_console.tabuleiro;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jogo xadrez - Felipe Freitas");
            Posicao p;
            p = new Posicao(3, 4);

            Console.WriteLine("Posição: "+p);
        }
    }
}
