using System;
using System.Globalization;
using Project.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts {
    public class InGameCanvasController : MonoBehaviour {
        [SerializeField] private GameObject beforeGame;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject afterGame;
        [SerializeField] private GameObject pause;
        [SerializeField] private TextMeshProUGUI scoreValue;
        [SerializeField] private TextMeshProUGUI timeValue;
        
        private GameManager _gameManager;

        private void Awake() {
            _gameManager = GameObject.FindWithTag(Constants.GAME_MANAGER_TAG).GetComponent<GameManager>();
        }

        public void UpdateScore() {
            scoreValue.text = _gameManager.Score.ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateTime() {
            var time = TimeSpan.FromSeconds(_gameManager.CurrentTime);
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

        public void Pause() {
            Time.timeScale = 0f;
            pause.SetActive(true);
        }

        public void Unpause() {
            Time.timeScale = 1f;
            pause.SetActive(false);
        }
    }
}