using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_console.tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas;


        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca ReceberPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
                throw new exception.TabuleiroException("Já existe uma peça nessa posição!");
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RemovePiece(Posicao pos)
        {
            Peca aux = GetPeca(pos);
            if (aux == null)
                return null;
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;

        }

        public Peca GetPeca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return GetPeca(pos) != null;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha > Linhas || pos.Coluna < 0 || pos.Coluna > Colunas)
                throw new exception.TabuleiroException("Posição inválida!");
        }

    }
}
