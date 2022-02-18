
using xadrez_console.jogoXadrez;
using xadrez_console.tabuleiro;
namespace xadrez_console.regras
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int _turno;
        private Cor _CorJogadorTurno;
        public bool BlnPartidaTerminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            _turno = 1;
            _CorJogadorTurno = Cor.Branca;
            ColocarPieces();
        }

        public void ExecuteMoviment(Posicao origin, Posicao destiny)
        {
            Peca p = tab.RemovePiece(origin);
            p.AddQtdMoviment();
            Peca pieceCaptured = tab.RemovePiece(destiny);
            tab.ColocarPeca(p, destiny);

        }

        public void ColocarPieces()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 1).ToPosition());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 2).ToPosition());
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('c', 3).ToPosition());

            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 4).ToPosition());
        }
    }
}
