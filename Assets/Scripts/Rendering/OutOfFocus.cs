using UnityEngine;

namespace SunnyMonster.GoEngine.Rendering
{
    public class OutOfFocus : MonoBehaviour
    {
        [SerializeField]
        private GameObject _outOfFocusPanel;
        [SerializeField]
        private CanvasGroup _outOfFocusCanvasGroup;
        [SerializeField]
        private float _animationTime = 0.3f;

        private void OnApplicationFocus(bool focusStatus)
        {
            if (focusStatus)
            {
                LeanTween.cancel(_outOfFocusPanel);
                _outOfFocusCanvasGroup.alpha = 0;
                _outOfFocusPanel.SetActive(false);
            }
            else
            {
                _outOfFocusPanel.SetActive(true);
                LeanTween.alphaCanvas(_outOfFocusCanvasGroup, 1f, _animationTime).setEaseOutQuad();
            }
        }
    }
}