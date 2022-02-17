using System;
using xadrez_console.jogoXadrez;
using xadrez_console.tabuleiro;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //Tabuleiro tabuleiro = new Tabuleiro(8, 8);
                regras.PartidaDeXadrez partida = new regras.PartidaDeXadrez();
                
                Tela.PrintTabuleiro(partida.tab);
            }
            catch(exception.TabuleiroException te)
            {
                Console.WriteLine(te.Message);
            }
            

            
        }
    }
}
