﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xadrez_console.tabuleiro
{
    class PosicaoXadrez
    {
        public char Coluna  { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.Coluna = coluna;
            this.Linha = linha;
        }

        public Posicao ToPosicao(int intLinhaTabuleiro)
        {
            return new Posicao(intLinhaTabuleiro - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return ""+Coluna+Linha;
        }
    }
}
