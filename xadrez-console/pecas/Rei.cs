using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.jogoXadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor){
        }

        public override bool[,] GetPossiblesMoviment()
        {
            bool[,] blnMat = new bool[Tab.Linhas, Tab.Colunas];
            return blnMat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
