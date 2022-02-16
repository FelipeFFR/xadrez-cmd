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
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(tabuleiro.Linhas - i+ " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
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
                Console.WriteLine("");
                
            }


            for (int j = 0; j < tabuleiro.Colunas; j++)
            {
                Console.Write(Convert.ToChar(j + 'a') + " ");
            }
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca)
                Console.Write(peca);
            else if(peca.Cor == Cor.Preta)
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = consoleColor;
            }
        }
    }
}
