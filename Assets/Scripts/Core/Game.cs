using System;
using System.Collections;
using System.Collections.Generic;
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
