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
                while (!partida.BlnPartidaTerminada)
                {
                    Console.Clear();    
                    Tela.PrintTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origin = Tela.LerPosicaoXadrez(Console.ReadLine()).ToPosition();
                    Console.Write("Destino: ");
                    Posicao destiny = Tela.LerPosicaoXadrez(Console.ReadLine()).ToPosition();

                    partida.ExecuteMoviment(origin, destiny);
                }
            }
            catch(exception.TabuleiroException te)
            {
                Console.WriteLine(te.Message);
            }
            

            
        }
    }
}
