using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void PrintTabuleiro(tabuleiro.Tabuleiro tabuleiro, bool[,] blnPossiblesPositions = null)
        {
            ConsoleColor backgroundColorOrigin = Console.BackgroundColor;
            ConsoleColor backgroundColorModified = ConsoleColor.DarkGray;
            for (int i = 0; i < tabuleiro.Linhas + 1; i++)
            {

                if (i != tabuleiro.Linhas)
                    Console.Write(tabuleiro.Linhas - i + " ");
                else
                    Console.Write("  ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (i != tabuleiro.Linhas)
                    {
                        if (blnPossiblesPositions != null && blnPossiblesPositions[i, j])
                            Console.BackgroundColor = backgroundColorModified;
                        else
                            Console.BackgroundColor = backgroundColorOrigin;
                        Peca peca = tabuleiro.GetPiece(i, j);

                        if (!(Console.BackgroundColor == backgroundColorModified) &&
                            (i % 2 == 0 && j % 2 == 0) || ((i % 2 == 1 && j % 2 == 1)))
                            Console.BackgroundColor = ConsoleColor.White;

                        ImprimirPeca(peca);
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                    else
                    {
                        Console.Write(Convert.ToChar(j + 'a') + " ");
                    }
                }

                Console.WriteLine("");
                Console.BackgroundColor = backgroundColorOrigin;


            }

        }

        public static void PrintMatch(regras.PartidaDeXadrez partida)
        {
            Tela.PrintTabuleiro(partida.tab);
            Console.WriteLine();
            PrintCapturedPieces(partida);
            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada: " + partida.CorJogadorTurno);
        }

        public static void PrintCapturedPieces(regras.PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças captudaras: ");
            Console.Write("Brancas: ");
            Console.ForegroundColor = ConsoleColor.Red;
            PrintGroupCapturedPieces(partida.PiecesCaptureds(Cor.Branca));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Green;
            PrintGroupCapturedPieces(partida.PiecesCaptureds(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void PrintGroupCapturedPieces(HashSet<Peca> PiecesCaptureds)
        {
            Console.Write("[");
            foreach (Peca x in PiecesCaptureds)
            {
                Console.Write(x+", ");
            }
            Console.Write("]");
        }

        public static void ImprimirPeca(Peca peca)
        {

            if (peca == null)
            {

                Console.Write("  ");

            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                    //Console.Write(peca);
                }
                else if (peca.Cor == Cor.Preta)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez(string strPosicao)
        {
            char coluna = strPosicao[0];
            int linha = int.Parse(strPosicao[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
