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
        public static void PrintTabuleiro(tabuleiro.Tabuleiro tabuleiro)
        {
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
                        Peca peca = tabuleiro.ReceberPeca(i, j);
                        if (peca == null)
                            Console.Write("- ");
                        else
                        {
                            ImprimirPeca(peca);
                            Console.Write(" ");
                        }
                    }
                    else
                    {
                        Console.Write(Convert.ToChar(j + 'a') + " ");
                    }
                }

                Console.WriteLine("");


            }

        }

        public static void ImprimirPeca(Peca peca)
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
        }
    }
}
