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
                        ImprimirPeca(peca);

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

        public static void ImprimirPeca(Peca peca)
        {

            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                    Console.Write(peca);
                else if (peca.Cor == Cor.Preta)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
