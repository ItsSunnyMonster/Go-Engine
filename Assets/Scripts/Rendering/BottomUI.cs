using SunnyMonster.GoEngine.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SunnyMonster.GoEngine.Rendering
{
    public class BottomUI : MonoBehaviour
    {
        [FormerlySerializedAs("_boardDisplay")] [SerializeField]
        private BoardDisplay boardDisplay;
        [FormerlySerializedAs("_currentPlayer")] [SerializeField]
        private Image currentPlayer;

        private void Start()
        {
            boardDisplay.Game.BoardChanged += UpdateGameStatus;
        }

        public void Pass()
        {
            boardDisplay.Game.Pass();
            UpdateGameStatus();
        }

        private void UpdateGameStatus()
        {
            currentPlayer.color = boardDisplay.Game.CurrentPlayer == Player.Black ? Color.black : Color.white;
        }
    }
}
