using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts {
    public class CanvasController : MonoBehaviour {
        [SerializeField] private GameObject BeforeGame;
        [SerializeField] private GameObject InGame;
        [SerializeField] private GameObject AfterGame;
        [SerializeField] private TextMeshProUGUI scoreValue;
        [SerializeField] private TextMeshProUGUI timeValue;

        public void UpdateScore() {
            scoreValue.text = GameManager.Instance.Score.ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateTime() {
            var time = TimeSpan.FromSeconds(GameManager.Instance.CurrentTime);
            timeValue.text = $"{time.Minutes}:{time.Seconds}";
        }

        public void ActivateBeforeGame() {
            ActivateMenu(true, false, false);
        }

        public void ActivateInGame() {
            ActivateMenu(false, true, false);

        }

        public void ActivateAfterGame() {
            ActivateMenu(false, false, true);
        }

        private void ActivateMenu(bool beforeGame, bool inGame, bool afterGame) {
            BeforeGame.SetActive(beforeGame);
            InGame.SetActive(inGame);
            AfterGame.SetActive(afterGame);
        }
    }
}