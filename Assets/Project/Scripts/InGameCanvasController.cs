using System;
using System.Globalization;
using Project.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts {
    public class InGameCanvasController : MonoBehaviour {
        [SerializeField] private GameObject beforeGame;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject afterGame;
        [SerializeField] private GameObject pause;
        [SerializeField] private GameObject time;
        [SerializeField] private GameObject amountOfBoxes;
        [SerializeField] private TextMeshProUGUI scoreValue;
        [SerializeField] private TextMeshProUGUI timeValue;
        [SerializeField] private TextMeshProUGUI boxValue;
        [SerializeField] private TextMeshProUGUI scoreAfterGameValue;
        [SerializeField] private TextMeshProUGUI bestScoreValue;
        
        private GameManager _gameManager;

        private void Awake() {
            _gameManager = GameObject.FindWithTag(Constants.GAME_MANAGER_TAG).GetComponent<GameManager>();
            _gameManager.onGameEnd.AddListener(AfterGameInfoUpdate);
            switch (GameMode.CurrentGameMode) {
                case GameMode.Mode.Timer:
                    ActivateCondition(true, false);
                    break;
                case GameMode.Mode.LimitedBoxes:
                    ActivateCondition(false, true);
                    break;
                case GameMode.Mode.FirstLessZero:
                    ActivateCondition(false, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void ActivateCondition(bool _time, bool _amountOfBoxes) {
            time.SetActive(_time);
            amountOfBoxes.SetActive(_amountOfBoxes);
        }

        public void UpdateScore() {
            scoreValue.text = _gameManager.Score.ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateTime() {
            var time = TimeSpan.FromSeconds(_gameManager.CurrentTime);
            var sec = time.Seconds > 9 ? time.Seconds.ToString() : "0" + time.Seconds; 
            timeValue.text = $"{time.Minutes}:{sec}";
        }

        public void UpdateBoxes() {
            boxValue.text = _gameManager.AmountOfBoxes.ToString();
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

        public void Restart() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
        }

        public void GoToMainMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_NAME);
        }

        private void AfterGameInfoUpdate() {
            scoreAfterGameValue.text = _gameManager.Score.ToString();
            bestScoreValue.text = PlayerPrefs.GetInt(_gameManager.BestScoreKey).ToString();
        }
    }
}