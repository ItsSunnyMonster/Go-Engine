using SunnyMonster.GoEngine.Core;
using TMPro;
using UnityEngine;

namespace SunnyMonster.GoEngine.Rendering
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField]
        private BoardDisplay _boardDisplay;
        [SerializeField]
        private TextMeshProUGUI _gameStatus;

        private void Start()
        {
            _boardDisplay.Game.BoardChanged += UpdateGameStatus;
        }

        public void Pass()
        {
            _boardDisplay.Game.Pass();
            UpdateGameStatus();
        }

        private void UpdateGameStatus()
        {
            _gameStatus.text = _boardDisplay.Game.CurrentPlayer == Player.Black ? "Black To Move" : "White To Move";
        }
    }
}
