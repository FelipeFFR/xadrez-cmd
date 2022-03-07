using System;
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
                    try
                    {
                        Console.Clear();
                        Tela.PrintMatch(partida);
                        
                        Console.WriteLine();
                        Console.Write("Origem: ");
                        string strInputUser = Tela.ValidateUserInput();
                        Posicao origin = Tela.LerPosicaoXadrez(strInputUser).ToPosition();
                        partida.ValidatePositionOrigen(origin);

                        bool[,] posicoesPossiveis = partida.tab.GetPiece(origin).GetPossiblesMoviment();
                        Console.Clear();
                        //Tela.PrintTabuleiro(partida.tab, posicoesPossiveis);

                        Tela.PrintMatch(partida, posicoesPossiveis);
                        Console.Write("Destino: ");
                        strInputUser = Tela.ValidateUserInput();
                        Posicao destiny = Tela.LerPosicaoXadrez(strInputUser).ToPosition();
                        partida.ValidatePositionDestiny(origin, destiny);
                        partida.MakesMove(origin, destiny);
                    }
                    catch (exception.TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Tela.PrintMatch(partida);

            }
            catch(exception.TabuleiroException te)
            {
                Console.WriteLine(te.Message);
            }
            

            
        }

        
                
    }
}
