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
}
