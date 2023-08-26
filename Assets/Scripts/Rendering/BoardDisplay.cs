using System;
using System.Collections;
using SunnyMonster.GoEngine.Core;
using SunnyMonster.GoEngine.Rendering.Utils;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyMonster.GoEngine.Rendering
{
    public class BoardDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject _stonePrefab;
        [SerializeField]
        private Transform _linesParent;
        [SerializeField]
        private Transform _stonesParent;
        [SerializeField]
        [Range(1, 19)]
        private int _boardSize = 19;
        [SerializeField]
        private Color _boardColor = new Color(0.82f, 0.68f, 0.47f);
        [SerializeField]
        private Color _lineColor = Color.black;

        public event Action WindowSizeChanged;

        private Game _game;

        private void Awake()
        {
            _game = new Game(_boardSize);
            _game.BoardChanged += DrawStones;
        }

        private void Start()
        {
            DrawLines();
        }

        // Clears lines and redraws them
        private void DrawLines()
        {
            var distanceBetweenLines = GetComponent<RectTransform>().rect.width / _boardSize;

            // Destroy lines
            foreach (Transform child in _linesParent)
            {
                Destroy(child.gameObject);
            }

            // Set up board color
            GetComponent<Image>().color = _boardColor;

            // Loop through board size
            for (var i = 0; i < _boardSize; i++)
            {
                // Horizontal lines
                // Create game object
                var horizontalLine = new GameObject($"Horizontal Line {i}", typeof(Image));
                horizontalLine.transform.SetParent(_linesParent);
                var rectTransform = horizontalLine.GetComponent<RectTransform>();
                // If first or last line, make it thicker
                float thickness = i == 0 || i == _boardSize - 1 ? 2 : 1;
                rectTransform.sizeDelta = new Vector2(0, thickness);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.pivot = new Vector2(0, 0);
                // For some reason the scale was changing when I modify the canvas scale factor
                rectTransform.localScale = new Vector3(1, 1, 1);
                horizontalLine.GetComponent<Image>().color = _lineColor;
                // Position: 
                // Multiply distance between lines by i to get the distance from the left
                // Add half the distance between lines to center the lines
                // Subtract half the line width to center
                rectTransform.anchoredPosition = new Vector2(0, distanceBetweenLines * i + distanceBetweenLines / 2 - thickness / 2f);
                // Make the line 4 pixels longer to fill up the corners
                rectTransform.SetLeft(distanceBetweenLines / 2 - 1);
                rectTransform.SetRight(distanceBetweenLines / 2 - 1);

                // Vertical lines
                // Same as horizontal lines, but with x and y swapped
                var verticalLine = new GameObject($"Vertical Line {i}", typeof(Image));
                verticalLine.transform.SetParent(_linesParent);
                rectTransform = verticalLine.GetComponent<RectTransform>();
                thickness = i == 0 || i == _boardSize - 1 ? 2 : 1;
                rectTransform.sizeDelta = new Vector2(thickness, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 0);
                rectTransform.localScale = new Vector3(1, 1, 1);
                verticalLine.GetComponent<Image>().color = _lineColor;
                rectTransform.anchoredPosition = new Vector2(distanceBetweenLines * i + distanceBetweenLines / 2 - thickness / 2f, 0);
                rectTransform.SetTop(distanceBetweenLines / 2 - 1);
                rectTransform.SetBottom(distanceBetweenLines / 2 - 1);
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            WindowSizeChanged?.Invoke();
            DrawLines();
            DrawStones();
        }

        private void DrawStones()
        {
            // Destroy stones
            foreach (Transform child in _stonesParent)
            {
                Destroy(child.gameObject);
            }

            // Loop through board
            for (var x = 0; x < _boardSize; x++)
            {
                for (var y = 0; y < _boardSize; y++)
                {
                    // If there is a stone, draw it
                    if (_game != null && _game.Board[x, y] != Point.Empty)
                    {
                        var stone = Instantiate(_stonePrefab);
                        stone.transform.SetParent(_stonesParent);
                        var rectTransform = stone.GetComponent<RectTransform>();
                        rectTransform.sizeDelta = new Vector2(DistsanceBetweenLines, DistsanceBetweenLines);
                        rectTransform.anchorMin = new Vector2(0, 0);
                        rectTransform.anchorMax = new Vector2(0, 0);
                        rectTransform.pivot = new Vector2(0, 0);
                        rectTransform.localScale = new Vector3(1, 1, 1);
                        stone.GetComponent<Image>().color = _game.Board[x, y] == Point.Black ? Color.black : Color.white;
                        rectTransform.anchoredPosition = new Vector2(DistsanceBetweenLines * x, DistsanceBetweenLines * y);
                    }
                }
            }
        }

        public float DistsanceBetweenLines
        {
            get
            {
                return GetComponent<RectTransform>().rect.width / _boardSize;
            }
        }

        public int BoardSize
        {
            get
            {
                return _boardSize;
            }
        }

        public Game Game
        {
            get
            {
                return _game;
            }
        }
    }
}