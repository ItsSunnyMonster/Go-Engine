using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SunnyMonster.GoEngine.Core.Utils;
using UnityEngine;

namespace SunnyMonster.GoEngine.Core
{
    public class Game
    {
        private Point[,] _board;
        private Player _currentPlayer = Player.Black;

        public event Action BoardChanged;

        public Game(int boardSize)
        {
            _board = new Point[boardSize, boardSize];
        }

        public Game()
        {
            _board = new Point[19, 19];
        }

        public void PlaceStone(int x, int y)
        {
            _board[x, y] = _currentPlayer == Player.Black ? Point.Black : Point.White;

            // Check for captures
            var piecesAround = new List<(int, int)>();

            if (y < _board.GetLength(1) - 1)
                piecesAround.Add((x, y + 1));
            if (y > 0)
                piecesAround.Add((x, y - 1));
            if (x < _board.GetLength(0) - 1)
                piecesAround.Add((x + 1, y));
            if (x > 0)
                piecesAround.Add((x - 1, y));

            foreach (var piece in piecesAround)
            {
                var (seedPieceX, seedPieceY) = piece;

                if (_board[seedPieceX, seedPieceY] != Point.Empty && _board[seedPieceX, seedPieceY] != _board[x, y])
                {
                    var capture = true;
                    var adjacentBlock = Algorithms.FloodFill2D(
                        _board,
                        (p) => p == _board[seedPieceX, seedPieceY],
                        (x, y) =>
                        {
                            // If there is a liberty to the current piece, this group is not captured
                            if ((y < _board.GetLength(1) - 1 && _board[x, y + 1] == Point.Empty) || (y > 0 && _board[x, y - 1] == Point.Empty) || (x < _board.GetLength(0) - 1 && _board[x + 1, y] == Point.Empty) || (x > 0 && _board[x - 1, y] == Point.Empty))
                            {
                                capture = false;
                                return false;
                            }

                            return true;
                        },
                        seedPieceX, seedPieceY);
                    if (capture)
                    {
                        foreach (var item in adjacentBlock)
                        {
                            var (captureX, captureY) = item;
                            _board[captureX, captureY] = Point.Empty;
                        }
                    }
                }
            }

            // Change player
            _currentPlayer = _currentPlayer == Player.Black ? Player.White : Player.Black;

            BoardChanged?.Invoke();
            // TODO: Check for illegal moves and return false
        }

        public void Pass()
        {
            _currentPlayer = _currentPlayer == Player.Black ? Player.White : Player.Black;
        }

        public List<PointCoordinate> GetIllegalMoves()
        {
            var list = new List<PointCoordinate>();

            for (int x = 0; x < _board.GetLength(0); x++)
            {
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    if (_board[x, y] != Point.Empty)
                        list.Add(new PointCoordinate(x, y));
                }
            }

            return list;
        }

        public Point[,] Board
        {
            get
            {
                return _board;
            }
        }
        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
        }
    }

    public enum Point
    {
        Empty,
        Black,
        White
    }

    public enum Player
    {
        Black,
        White
    }

    public struct PointCoordinate
    {
        int x;
        int y;

        public PointCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
