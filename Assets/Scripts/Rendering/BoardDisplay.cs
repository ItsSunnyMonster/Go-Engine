using System.Collections;
using SunnyMonster.GoEngine.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyMonster.GoEngine.Rendering
{
    public class BoardDisplay : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 19)]
        private int _boardSize = 19;
        [SerializeField]
        private Color _boardColor = new Color(0.82f, 0.68f, 0.47f);
        [SerializeField]
        private Color _lineColor = Color.black;

        private void Start()
        {
            DrawLines();
        }

        // Clears lines and redraws them
        private void DrawLines()
        {
            var distanceBetweenLines = GetComponent<RectTransform>().rect.width / _boardSize;

            // Destroy lines
            foreach (Transform child in transform)
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
                horizontalLine.transform.SetParent(transform);
                var rectTransform = horizontalLine.GetComponent<RectTransform>();
                // If first or last line, make it thicker
                rectTransform.sizeDelta = i == 0 || i == _boardSize - 1 ? new Vector2(0, 4) : new Vector2(0, 2);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.pivot = new Vector2(0, 0);
                horizontalLine.GetComponent<Image>().color = _lineColor;
                // Position: 
                // Multiply distance between lines by i to get the distance from the left
                // Add half the distance between lines to center the lines
                // Subtract 2 to account for the line width
                rectTransform.anchoredPosition = new Vector2(0, distanceBetweenLines * i + distanceBetweenLines / 2 - 2);
                // Make the line 4 pixels longer to fill up the corners
                rectTransform.SetLeft(distanceBetweenLines / 2 - 2);
                rectTransform.SetRight(distanceBetweenLines / 2 - 2);

                // Vertical lines
                // Same as horizontal lines, but with x and y swapped
                var verticalLine = new GameObject($"Vertical Line {i}", typeof(Image));
                verticalLine.transform.SetParent(transform);
                rectTransform = verticalLine.GetComponent<RectTransform>();
                rectTransform.sizeDelta = i == 0 || i == _boardSize - 1 ? new Vector2(4, 0) : new Vector2(2, 0);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 0);
                verticalLine.GetComponent<Image>().color = _lineColor;
                rectTransform.anchoredPosition = new Vector2(distanceBetweenLines * i + distanceBetweenLines / 2 - 2, 0);
                rectTransform.SetTop(distanceBetweenLines / 2 - 2);
                rectTransform.SetBottom(distanceBetweenLines / 2 - 2);
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            DrawLines();
        }
    }
}