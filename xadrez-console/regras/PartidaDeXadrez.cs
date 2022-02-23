using System.Collections.Generic;
using xadrez_console.jogoXadrez;
using xadrez_console.tabuleiro;

namespace xadrez_console.regras
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor CorJogadorTurno { get; private set; }
        public bool BlnPartidaTerminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            CorJogadorTurno = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPieces();
            BlnPartidaTerminada = false;
            
        }

        private void ExecuteMoviment(Posicao origin, Posicao destiny)
        {
            Peca p = tab.RemovePiece(origin);
            p.AddQtdMoviment();
            Peca pieceCaptured = tab.RemovePiece(destiny);
            tab.ColocarPeca(p, destiny);
            if(!(pieceCaptured is null))
            {
                capturadas.Add(pieceCaptured);
            }

        }

        public HashSet<Peca> PiecesCaptureds(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            aux.UnionWith(capturadas);
            aux.RemoveWhere(a => a.Cor != cor);
            return aux;

        }

        public HashSet<Peca> GetPiecesInGame(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            aux.UnionWith(pecas);
            aux.RemoveWhere(a => a.Cor != cor);
            aux.ExceptWith(PiecesCaptureds(cor));
            return aux;

        }

        public void ValidatePositionOrigen(Posicao pos)
        {
            Peca piece = tab.GetPiece(pos);
            if (piece == null)
            {
                throw new exception.TabuleiroException("Não existe peça na posição de origem escolhida.");
            }
            else if(piece.Cor != CorJogadorTurno)
            {
                throw new exception.TabuleiroException("A peça de origem escolhida não é sua.");
            }
            else if(!piece.ExistsPossiblesMoviments())
                throw new exception.TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida.");
        }

        public void ValidatePositionDestiny(Posicao posOrigen, Posicao posDestiny)
        {
            var piece = tab.GetPiece(posOrigen);
            if (!piece.CanMovePiece(posDestiny))
                throw new exception.TabuleiroException("Posição de destino inválida.");
        }

        public void ChangePlayer()
        {
            CorJogadorTurno = CorJogadorTurno == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void MakesMove(Posicao origin, Posicao destiny)
        {
            ExecuteMoviment(origin, destiny);
            Turno++;
            ChangePlayer();

        }

        public void ColocarPieces()
        {
            PutNewPiece('a', 1, new Torre(tab, Cor.Preta));
            PutNewPiece('h', 1, new Torre(tab, Cor.Preta));
            PutNewPiece('c', 1, new Rei(tab, Cor.Preta));

            PutNewPiece('c', 8, new Rei(tab, Cor.Branca));
            PutNewPiece('a', 8, new Torre(tab, Cor.Branca));
            PutNewPiece('h', 8, new Torre(tab, Cor.Branca));

        }

        public void PutNewPiece(char column, int line, Peca newPiece)
        {
            tab.ColocarPeca(newPiece, new PosicaoXadrez(column, line).ToPosition());
            pecas.Add(newPiece);
        }
    }
}
