using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.pecas
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override bool[,] GetPossiblesMoviment()
        {
            bool[,] blnMat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao position = new Posicao(0, 0);
            //acima
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna -2);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Nordeste
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna + 2);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Nordeste
            position.DefineValues(Posicao.Linha -2, Posicao.Coluna + 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Sudeste
            position.DefineValues(Posicao.Linha - 2, Posicao.Coluna - 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Abaixo
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna - 2);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Sudoeste
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna + 2);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Esquerda
            position.DefineValues(Posicao.Linha + 2, Posicao.Coluna - 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Noroeste
            position.DefineValues(Posicao.Linha + 2, Posicao.Coluna + 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;

            return blnMat;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
