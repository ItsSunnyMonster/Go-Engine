using System.Collections.Generic;
using SunnyMonster.GoEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyMonster.GoEngine.Rendering
{
    public class PlacementTool : MonoBehaviour
    {
        [SerializeField]
        private BoardDisplay _boardDisplay;
        [SerializeField]
        private RectTransform _displayTransform;
        [SerializeField]
        private Image _displayImage;
        [SerializeField]
        private RectTransform _mousePosition;

        private List<PointCoordinate> _illegalMoves = new();

        private bool _focused = true;

        private void Start()
        {
            _displayTransform.sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
            _boardDisplay.WindowSizeChanged += OnWindowSizeChanged;
            _boardDisplay.Game.BoardChanged += OnBoardChanged;
        }

        private void Update()
        {
            _displayImage.color = _boardDisplay.Game.CurrentPlayer == Player.Black ? new Color(0, 0, 0, 0.5f) : new Color(1, 1, 1, 0.5f);

            _mousePosition.position = Input.mousePosition;
            var distanceBetweenLines = _boardDisplay.DistsanceBetweenLines;
            var mousePosition = new Vector2(_mousePosition.anchoredPosition.x - distanceBetweenLines / 2, _mousePosition.anchoredPosition.y - distanceBetweenLines / 2);

            var cellX = Mathf.RoundToInt(mousePosition.x / distanceBetweenLines);
            var cellY = Mathf.RoundToInt(mousePosition.y / distanceBetweenLines);

            if (_illegalMoves.Contains(new PointCoordinate(cellX, cellY)))
                _displayImage.color = Color.red;

            var x = cellX * distanceBetweenLines + distanceBetweenLines / 2;
            var y = cellY * distanceBetweenLines + distanceBetweenLines / 2;
            LeanTween.cancel(_displayTransform.gameObject);
            LeanTween.move(_displayTransform, new Vector2(x, y), 0.1f).setEaseOutQuad();

            if (cellX < 0 || cellX >= _boardDisplay.BoardSize || cellY < 0 || cellY >= _boardDisplay.BoardSize || _boardDisplay.Game.Board[cellX, cellY] != Point.Empty)
            {
                _displayImage.enabled = false;
                return;
            }
            else
                _displayImage.enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                if (!_focused)
                    _focused = true;
                else
                    _boardDisplay.Game.PlaceStone(cellX, cellY);
            }
        }

        private void OnApplicationFocus(bool focusStatus)
        {
            if (!focusStatus)
                _focused = false;
        }

        private void OnWindowSizeChanged()
        {
            _displayTransform.sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
        }

        private void OnBoardChanged()
        {
            _illegalMoves = _boardDisplay.Game.GetIllegalMoves();
        }
    }
}