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

        public bool BlnIsXeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            CorJogadorTurno = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPieces();
            BlnPartidaTerminada = false;
            BlnIsXeque = false;

        }

        private Peca ExecuteMoviment(Posicao origin, Posicao destiny)
        {
            Peca p = tab.RemovePiece(origin);
            p.AddQtdMoviment();
            Peca pieceCaptured = tab.RemovePiece(destiny);
            tab.ColocarPeca(p, destiny);
            if (!(pieceCaptured is null))
            {
                capturadas.Add(pieceCaptured);
            }
            return pieceCaptured;
        }

        public bool IsXeque(Cor cor)
        {
            Peca king = King(cor);
            if (king == null)
                throw new exception.TabuleiroException("Não existe o rei da cor " + cor + " no tabuleiro.");

            foreach (Peca x in GetPiecesInGame(GetCollorOpponent(cor)))
            {
                bool[,] mat = x.GetPossiblesMoviment();
                if (mat[king.Posicao.Linha, king.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        private Peca King(Cor cor)
        {
            foreach (Peca x in GetPiecesInGame(cor))
            {
                if (x is Rei)
                    return x;
            }
            return null;
        }

        private Cor GetCollorOpponent(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
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
            else if (piece.Cor != CorJogadorTurno)
            {
                throw new exception.TabuleiroException("A peça de origem escolhida não é sua.");
            }
            else if (!piece.ExistsPossiblesMoviments())
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

        public void UndoMove(Posicao origin, Posicao destiny, Peca pieceCaptured)
        {
            Peca p = tab.RemovePiece(destiny);
            p.DecrementQtdMoviment();
            if(pieceCaptured != null)
            {
                tab.ColocarPeca(pieceCaptured, destiny);
                capturadas.Remove(pieceCaptured);
            }
            tab.ColocarPeca(p, origin);
        }

        public void MakesMove(Posicao origin, Posicao destiny)
        {
            Peca pieceCaptured = ExecuteMoviment(origin, destiny);

            if (IsXeque(CorJogadorTurno))
            {
                UndoMove(origin, destiny, pieceCaptured);
                throw new exception.TabuleiroException("Você não pode se colocar em xeque.");
            }

            if (IsXeque(GetCollorOpponent(CorJogadorTurno)))
                BlnIsXeque = true;
            else
                BlnIsXeque = false;


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
