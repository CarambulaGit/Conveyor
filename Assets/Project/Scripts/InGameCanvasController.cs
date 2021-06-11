using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts {
    public class InGameCanvasController : MonoBehaviour {
        [SerializeField] private GameObject beforeGame;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject afterGame;
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
            Activate(true, false, false);
        }

        public void ActivateInGame() {
            Activate(false, true, false);

        }

        public void ActivateAfterGame() {
            Activate(false, false, true);
        }

        private void Activate(bool _beforeGame, bool _inGame, bool _afterGame) {
            beforeGame.SetActive(_beforeGame);
            inGame.SetActive(_inGame);
            afterGame.SetActive(_afterGame);
        }
    }
}