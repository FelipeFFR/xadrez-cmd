using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.pecas
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override bool[,] GetPossiblesMoviment()
        {
            bool[,] blnMat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao position = new Posicao(0, 0);
            //Noroeste
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna -1);
            while (CanMovePiece(position))
            {
                blnMat[position.Linha, position.Coluna] = true;
                Peca piece = Tab.GetPiece(position);
                if (piece != null && piece.Cor != this.Cor)
                    break;
                position.DefineValues(position.Linha - 1, position.Coluna - 1);
            }

            //Nordeste
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (CanMovePiece(position))
            {
                blnMat[position.Linha, position.Coluna] = true;
                Peca piece = Tab.GetPiece(position);
                if (piece != null && piece.Cor != this.Cor)
                    break;
                position.DefineValues(position.Linha - 1, position.Coluna + 1);
            }

            //Sudeste
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna + 1);
            while (CanMovePiece(position))
            {
                blnMat[position.Linha, position.Coluna] = true;
                Peca piece = Tab.GetPiece(position);
                if (piece != null && piece.Cor != this.Cor)
                    break;
                position.DefineValues(position.Linha + 1, position.Coluna + 1);
            }
            //Sudoeste
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna - 1);
            while (CanMovePiece(position))
            {
                blnMat[position.Linha, position.Coluna] = true;
                Peca piece = Tab.GetPiece(position);
                if (piece != null && piece.Cor != this.Cor)
                    break;
                position.DefineValues(position.Linha + 1, position.Coluna - 1);
            }

            return blnMat;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
