using System.Collections.Generic;
using xadrez_console.pecas;
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

        public Peca PieceVunerableEnPassant { get; private set; }

        public bool BlnIsCheck { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            CorJogadorTurno = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            PutPieces(Cor.Preta);
            PutPieces(Cor.Branca);
            BlnPartidaTerminada = false;
            BlnIsCheck = false;
            PieceVunerableEnPassant = null;
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

            if (p is Rei)
            {
                bool blnSallRock = destiny.Coluna == origin.Coluna + 2;
                bool blnLargeRock = destiny.Coluna == origin.Coluna - 2;
                if (blnSallRock || blnLargeRock)
                {
                    Posicao positionTower;
                    Posicao positionDestinyTower;
                    if (blnSallRock)
                    {
                        positionTower = new Posicao(origin.Linha, origin.Coluna + 3);
                        positionDestinyTower = new Posicao(origin.Linha, origin.Coluna + 1);
                    }
                    else
                    {
                        positionTower = new Posicao(origin.Linha, origin.Coluna - 4);
                        positionDestinyTower = new Posicao(origin.Linha, origin.Coluna - 1);
                    }
                    Peca pieceTower = tab.RemovePiece(positionTower);
                    pieceTower.AddQtdMoviment();
                    tab.ColocarPeca(pieceTower, positionDestinyTower);
                }

            }

            return pieceCaptured;
        }

        public bool IsInCkeckMate(Cor cor)
        {
            if (!IsInCheck(cor))
                return false;

            foreach (Peca p in GetPiecesInGame(cor))
            {
                bool[,] arrBlnMat = p.GetPossiblesMoviment();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (arrBlnMat[i, j])
                        {
                            Posicao origin = p.Posicao;
                            Posicao destiny = new Posicao(i, j);
                            Peca pieceCaptured = ExecuteMoviment(origin, destiny);
                            bool blnTesteXeque = IsInCheck(cor);
                            UndoMove(origin, destiny, pieceCaptured);
                            if (!blnTesteXeque)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool IsInCheck(Cor cor)
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
            if (pieceCaptured != null)
            {
                tab.ColocarPeca(pieceCaptured, destiny);
                capturadas.Remove(pieceCaptured);
            }
            tab.ColocarPeca(p, origin);

            if (p is Rei)
            {
                bool blnSallRock = destiny.Coluna == origin.Coluna + 2;
                bool blnLargeRock = destiny.Coluna == origin.Coluna - 2;
                if (blnSallRock || blnLargeRock)
                {
                    Posicao positionTower;
                    Posicao positionDestinyTower;
                    if (blnSallRock)
                    {
                        positionTower = new Posicao(origin.Linha, origin.Coluna + 3);
                        positionDestinyTower = new Posicao(origin.Linha, origin.Coluna + 1);
                    }
                    else
                    {
                        positionTower = new Posicao(origin.Linha, origin.Coluna - 4);
                        positionDestinyTower = new Posicao(origin.Linha, origin.Coluna - 1);
                    }
                    Peca pieceTower = tab.RemovePiece(positionTower);
                    pieceTower.DecrementQtdMoviment();
                    tab.ColocarPeca(pieceTower, positionDestinyTower);
                }

            }
        }

        public void MakesMove(Posicao origin, Posicao destiny)
        {
            Peca pieceCaptured = ExecuteMoviment(origin, destiny);

            if (IsInCheck(CorJogadorTurno))
            {
                UndoMove(origin, destiny, pieceCaptured);
                throw new exception.TabuleiroException("Você não pode se colocar em xeque.");
            }

            if (IsInCheck(GetCollorOpponent(CorJogadorTurno)))
                BlnIsCheck = true;
            else
                BlnIsCheck = false;

            if (IsInCkeckMate(GetCollorOpponent(CorJogadorTurno)))
            {
                BlnPartidaTerminada = true;
            }
            else
            {
                Turno++;
                ChangePlayer();
            }

            #region en passant
            Peca piece = tab.GetPiece(destiny);
            if (piece is Peao)
            {
                if (piece.QtdMovimentos == 1 && (destiny.Linha == origin.Linha + (2 * (piece.Cor == Cor.Branca ? 1 : -1))))
                    PieceVunerableEnPassant = piece;
            }
            else
                PieceVunerableEnPassant = null;
            #endregion

        }

        public void PutPieces(Cor cor)
        {
            int intLine = 0;
            if (cor == Cor.Preta)
                intLine = 8;
            else if (cor == Cor.Branca)
                intLine = 1;

            PutNewPiece('a', intLine, new Torre(tab, cor));
            //PutNewPiece('b', intLine, new Cavalo(tab, cor));
            //PutNewPiece('c', intLine, new Bispo(tab, cor));
            //PutNewPiece('d', intLine, new Rainha(tab, cor));
            PutNewPiece('e', intLine, new Rei(tab, cor, this));
            //PutNewPiece('f', intLine, new Bispo(tab, cor));
            //PutNewPiece('g', intLine, new Cavalo(tab, cor));
            PutNewPiece('h', intLine, new Torre(tab, cor));

            if (cor == Cor.Preta)
                intLine--;
            else if (cor == Cor.Branca)
                intLine++;

            char chrValuePreta = 'h';
            while (chrValuePreta > ('a' - 1))
            {
                PutNewPiece(chrValuePreta, intLine, new Peao(tab, cor, this));
                chrValuePreta = (char)(chrValuePreta - 1);
            }

        }

        public void PutNewPiece(Posicao position, Peca newPiece)
        {
            tab.ColocarPeca(newPiece, position);
            pecas.Add(newPiece);
        }
        //public void PutNewPiece(int column, int line, Peca newPiece)
        //{
        //    tab.ColocarPeca(newPiece, new PosicaoXadrez(column, line));
        //    pecas.Add(newPiece);
        //}

        public void PutNewPiece(char column, int line, Peca newPiece)
        {
            tab.ColocarPeca(newPiece, new PosicaoXadrez(column, line).ToPosition());
            pecas.Add(newPiece);
        }
    }
}
