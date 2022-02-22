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

        public Peca GetPiece(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca GetPiece(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
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
            Peca aux = GetPiece(pos);
            if (aux == null)
                return null;
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;

        }

        

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return GetPiece(pos) != null;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha > Linhas -1 || pos.Coluna < 0 || pos.Coluna > Colunas -1)
                throw new exception.TabuleiroException("Posição inválida!");
        }

    }
}
