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
        private regras.PartidaDeXadrez _partida;
        public Peao(Tabuleiro tab, Cor cor, regras.PartidaDeXadrez partida) : base(tab, cor)
        {
            _partida = partida;
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
            if (Tab.VerifyValidatesPosition(position) && CanMovePiece(position) && !Tab.ExistePeca(position))
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

            #region En passant
            //En Passant direita
            position.DefineValues(Posicao.Linha, Posicao.Coluna + 1);
            if (CanMovePiece(position) && EnPassant(position))
                blnMat[position.Linha - intValue, position.Coluna] = true;

            //En Passant esquerda
            position.DefineValues(Posicao.Linha, Posicao.Coluna - 1);
            if (CanMovePiece(position) && EnPassant(position))
                blnMat[position.Linha - intValue, position.Coluna] = true;
            #endregion

            return blnMat;
        }

        private bool EnPassant(Posicao position)
        {
            int intLineEnPassant = 3;
            if (Cor == Cor.Preta)
            {
                intLineEnPassant = 4;
            }
            if (position.Linha == intLineEnPassant)
            {
                Peca piece = Tab.GetPiece(position);
                if (piece == _partida.PieceVunerableEnPassant)
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
