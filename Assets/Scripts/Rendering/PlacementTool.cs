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
        private RectTransform _mousePosition;

        private RectTransform _rectTransform;

        private bool _focused = true;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
            _boardDisplay.WindowSizeChanged += OnWindowSizeChanged;
        }

        private void Update()
        {
            GetComponent<Image>().color = _boardDisplay.Game.CurrentPlayer == Player.Black ? new Color(0, 0, 0, 0.5f) : new Color(1, 1, 1, 0.5f);

            _mousePosition.position = Input.mousePosition;
            var distanceBetweenLines = _boardDisplay.DistsanceBetweenLines;
            var mousePosition = new Vector2(_mousePosition.anchoredPosition.x - distanceBetweenLines / 2, _mousePosition.anchoredPosition.y - distanceBetweenLines / 2);

            var cellX = Mathf.RoundToInt(mousePosition.x / distanceBetweenLines);
            var cellY = Mathf.RoundToInt(mousePosition.y / distanceBetweenLines);

            var x = cellX * distanceBetweenLines + distanceBetweenLines / 2;
            var y = cellY * distanceBetweenLines + distanceBetweenLines / 2;
            _rectTransform.anchoredPosition = new Vector2(x, y);

            if (cellX < 0 || cellX >= _boardDisplay.BoardSize || cellY < 0 || cellY >= _boardDisplay.BoardSize)
            {
                GetComponent<Image>().enabled = false;
                return;
            }
            else
                GetComponent<Image>().enabled = true;

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
            GetComponent<RectTransform>().sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
        }
    }
}