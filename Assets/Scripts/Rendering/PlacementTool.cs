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

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
            _boardDisplay.BoardSizeChanged += OnBoardSizeChanged;
        }

        private void Update()
        {
            _mousePosition.position = Input.mousePosition;
            var distanceBetweenLines = _boardDisplay.DistsanceBetweenLines;
            var mousePosition = new Vector2(_mousePosition.anchoredPosition.x - distanceBetweenLines / 2, _mousePosition.anchoredPosition.y - distanceBetweenLines / 2);

            var cellX = Mathf.RoundToInt(mousePosition.x / distanceBetweenLines);
            var cellY = Mathf.RoundToInt(mousePosition.y / distanceBetweenLines);

            var x = cellX * distanceBetweenLines + distanceBetweenLines / 2;
            var y = cellY * distanceBetweenLines + distanceBetweenLines / 2;
            _rectTransform.anchoredPosition = new Vector2(x, y);

            if (cellX < 0 || cellX >= _boardDisplay.BoardSize || cellY < 0 || cellY >= _boardDisplay.BoardSize)
                GetComponent<Image>().enabled = false;
            else
                GetComponent<Image>().enabled = true;
        }

        private void OnBoardSizeChanged()
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(_boardDisplay.DistsanceBetweenLines, _boardDisplay.DistsanceBetweenLines);
        }
    }
}