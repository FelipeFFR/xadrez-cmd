using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.pecas
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.Posicao = null;
            this.Cor = cor;
            this.Tab = tab;
            this.QtdMovimentos = 0;
        }

        public void AddQtdMoviment()
        {
            QtdMovimentos++;
        }

        public void DecrementQtdMoviment()
        {
            QtdMovimentos--;
        }

        public bool CanMovePiece(Posicao pos)
        {
            try
            {
                if (!Tab.VerifyValidatesPosition(pos))
                    return false;
                Peca p = Tab.GetPiece(pos);
                return p == null || p.Cor != this.Cor;
            }
            catch
            {
                return false;
            }
        }

        public bool CanMoveToDestiny(Posicao pos)
        {
            return GetPossiblesMoviment()[pos.Linha, pos.Coluna];
        }

        public bool ExistsPossiblesMoviments()
        {
            bool[,] mat = GetPossiblesMoviment();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                        return true;
                }
            }
            return false;
        }

        public abstract bool[,] GetPossiblesMoviment();

    }
}
