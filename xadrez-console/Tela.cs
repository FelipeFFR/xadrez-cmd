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
            ConsoleColor backgroundColorModified = ConsoleColor.Red;
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
                            ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1)))
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        else if (!(Console.BackgroundColor == backgroundColorModified))
                            Console.BackgroundColor = ConsoleColor.DarkGreen;

                        ImprimirPeca(peca);
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                    else
                    {
                        Console.Write(Convert.ToChar(j + 'a') + " ");
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
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
            if (!partida.BlnPartidaTerminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.CorJogadorTurno);
                if (partida.BlnIsCheck)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.CorJogadorTurno);

            }
        }

        public static void PrintCapturedPieces(regras.PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças captudaras: ");
            Console.Write("Brancas: ");
            Console.ForegroundColor = ConsoleColor.White;
            PrintGroupCapturedPieces(partida.PiecesCaptureds(Cor.Branca));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Red;
            PrintGroupCapturedPieces(partida.PiecesCaptureds(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

        public static void PrintGroupCapturedPieces(HashSet<Peca> PiecesCaptureds)
        {
            Console.Write("[");
            foreach (Peca x in PiecesCaptureds)
            {
                Console.Write(x + ", ");
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
                string strPrintPiece = peca.ToString();
                if (peca.Cor == Cor.Branca)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(strPrintPiece);
                    Console.ForegroundColor = consoleColor;
                    //Console.Write(peca);
                }
                else if (peca.Cor == Cor.Preta)
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(strPrintPiece);
                    Console.ForegroundColor = consoleColor;
                }
                if (strPrintPiece.Length == 1)
                    Console.Write(" ");
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez(string strPosicao)
        {
            if (!String.IsNullOrEmpty(strPosicao))
            {
                char coluna = strPosicao[0];
                int linha = int.Parse(strPosicao[1] + "");
                return new PosicaoXadrez(coluna, linha);
            }
            else
            {
                throw new exception.TabuleiroException("Digite uma posição válida.");
            }
        }


        public static string ValidateUserInput()
        {
            string strInput = "";
            strInput = Console.ReadLine();
            while (!Tabuleiro.PositionBoardIsValid(strInput))
            {
                Console.WriteLine("Digite uma posição válida!");
                strInput = Console.ReadLine();
            }
            return strInput;
        }

    }
}
