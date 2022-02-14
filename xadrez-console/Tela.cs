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
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    Peca peca = tabuleiro.ReceberPeca(i, j);
                    if (peca == null)
                        Console.Write("- ");
                    else
                        Console.Write(peca + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
