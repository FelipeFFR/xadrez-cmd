using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.pecas
{
    class Rei : Peca
    {
        private regras.PartidaDeXadrez _Partida;
        public Rei(Tabuleiro tab, Cor cor, regras.PartidaDeXadrez partida) : base(tab, cor)
        {
            _Partida = partida;
        }

        public override bool[,] GetPossiblesMoviment()
        {
            bool[,] blnMat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao position = new Posicao(0, 0);
            //acima
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Nordeste
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna + 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Nordeste
            position.DefineValues(Posicao.Linha, Posicao.Coluna + 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Sudeste
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna + 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Abaixo
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Sudoeste
            position.DefineValues(Posicao.Linha + 1, Posicao.Coluna - 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Esquerda
            position.DefineValues(Posicao.Linha, Posicao.Coluna - 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;
            //Noroeste
            position.DefineValues(Posicao.Linha - 1, Posicao.Coluna - 1);
            if ((CanMovePiece(position)))
                blnMat[position.Linha, position.Coluna] = true;

            #region Roque
            if (QtdMovimentos == 0)
            {
                blnMat[Posicao.Linha, Posicao.Coluna + 2] = CanRockMoviment(true);
                blnMat[Posicao.Linha, Posicao.Coluna - 2] = CanRockMoviment(false);
            }
            #endregion

            return blnMat;
        }


        private bool CanRockMoviment(bool blnSmall)
        {


            int intColumn = -4;
            int intMultipleColumn = 1;
            if (blnSmall)
            {
                intColumn = 3;
                intMultipleColumn = -1;
            }


            if (!_Partida.BlnIsCheck)
            {
                Posicao positionTower = new Posicao(Posicao.Linha, Posicao.Coluna + intColumn);
                Peca tower = Tab.GetPiece(positionTower);
                if (tower != null && tower is Torre && tower.Cor == Cor && tower.QtdMovimentos == 0)
                {
                    bool blnPosition1 = Tab.GetPiece(new Posicao(Posicao.Linha, Posicao.Coluna + (1 * intMultipleColumn))) == null;
                    bool blnPosition2 = Tab.GetPiece(new Posicao(Posicao.Linha, Posicao.Coluna + (2 * intMultipleColumn))) == null;


                    if (!blnSmall)
                    {
                        bool blnPosition3 = false;
                        blnPosition3 = Tab.GetPiece(new Posicao(Posicao.Linha, Posicao.Coluna - 3)) == null;

                        if (blnPosition1 && blnPosition2 && blnPosition3)
                            return true;
                    }
                    else
                    {
                        if (blnPosition1 && blnPosition2)
                            return true;
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return "RE";
        }
    }
}
