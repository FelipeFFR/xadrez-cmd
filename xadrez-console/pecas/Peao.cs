using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.pecas
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override bool[,] GetPossiblesMoviment()
        {
            bool[,] blnMat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao position = new Posicao(0, 0);
            int intValue = 0;
            if (Cor == Cor.Preta)
                intValue = -1;
            else if (Cor == Cor.Branca)
                intValue = 1;

            //acima
            position.DefineValues(Posicao.Linha - intValue, Posicao.Coluna);
            if (CanMovePiece(position) && !Tab.ExistePeca(position))
                blnMat[position.Linha, position.Coluna] = true;

            //Primeiro movimento
            if (QtdMovimentos == 0)
            {
                position.DefineValues(Posicao.Linha - intValue * 2, Posicao.Coluna);
                if (CanMovePiece(position) && !Tab.ExistePeca(position))
                    blnMat[position.Linha, position.Coluna] = true;
            }

            //Verifica se pode capturar esquerda
            position.DefineValues(Posicao.Linha - intValue, Posicao.Coluna - 1);
            if (CanMovePiece(position) && Tab.ExistePeca(position))
                blnMat[position.Linha, position.Coluna] = true;

            //Verifica se pode capturar direita
            position.DefineValues(Posicao.Linha - intValue, Posicao.Coluna + 1);
            if (CanMovePiece(position) && Tab.ExistePeca(position))
                blnMat[position.Linha, position.Coluna] = true;


            return blnMat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
