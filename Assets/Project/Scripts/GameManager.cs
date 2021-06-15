using System;
using Project.Classes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour {
        private readonly Vector3 _vectorUp = 7 * Vector3.up;

        [SerializeField] private Conveyor conveyor;
        [SerializeField] private InGameCanvasController canvasController;
        [SerializeField] private Transform belt;
        [SerializeField] private Transform boxPrefab;
        [SerializeField] private Transform targetPrefab;
        [SerializeField] private float scoreForUnitCoef;
        [SerializeField] private float maximumScoreValue;
        [SerializeField] private float startTime;
        [SerializeField] private int amountOfBoxes;

        private TargetController _targetController;
        private BoxController _boxController;
        private GameMode.Mode _gameMode;
        private bool _gameOn;
        private int _score;
        private float _currentTime;
        private int _amountOfBoxes;
        private bool _greaterThanZero = true;
        
        public string BestScoreKey { get; private set; }

        public bool GameOn {
            get => _gameOn;
            private set {
                if (_gameOn == value) return;
                _gameOn = value;
                if (_gameOn) {
                    onStartGame.Invoke();
                }
            }
        }

        public int Score {
            get => _score;
            private set {
                if (_score == value) return;
                _score = value;
                onScoreChanged?.Invoke();
            }
        }

        public float CurrentTime {
            get => _currentTime;
            private set {
                if (Math.Abs(_currentTime - value) < Constants.EPSILON) return;
                _currentTime = value < 0 ? 0 : value;
                onConditionValueChanged?.Invoke();
            }
        }

        public int AmountOfBoxes {
            get => _amountOfBoxes;
            private set {
                if (_amountOfBoxes == value) return;
                _amountOfBoxes = value;
                onConditionValueChanged?.Invoke();
            }
        }
        
        public UnityEvent onStartGame;
        public UnityEvent onScoreChanged;
        public UnityEvent onGameEnd;
        private UnityEvent onConditionValueChanged = new UnityEvent();

        private delegate bool FinishCondition();

        private FinishCondition CheckForFinish;

        private Action<int> SaveBestToLeaderboard;

        public Target ActiveTarget => _targetController._target;
        public Box ActiveBox => _boxController.Box;

        private void Awake() {
            _gameMode = GameMode.CurrentGameMode;
            switch (_gameMode) {
                case GameMode.Mode.Timer:
                    CurrentTime = startTime;
                    CheckForFinish = Timer;
                    onConditionValueChanged.AddListener(canvasController.UpdateTime);
                    BestScoreKey = Constants.TIMER_BEST_SCORE_KEY;
                    SaveBestToLeaderboard = GPSLeaderboard.SaveBestTimerResult;
                    break;
                case GameMode.Mode.LimitedBoxes:
                    AmountOfBoxes = amountOfBoxes;
                    CheckForFinish = LimitedBoxes;
                    onConditionValueChanged.AddListener(canvasController.UpdateBoxes);
                    BestScoreKey = Constants.LIMITED_BOXES_BEST_SCORE_KEY;
                    SaveBestToLeaderboard = GPSLeaderboard.SaveBestLimitedBoxesResult;
                    break;
                case GameMode.Mode.FirstLessZero:
                    CheckForFinish = FirstLessZero;
                    BestScoreKey = Constants.FIRST_LESS_ZERO_BEST_SCORE_KEY;
                    SaveBestToLeaderboard = GPSLeaderboard.SaveBestFirstLessZeroResult;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void Update() {
            HandleTouch();
            if (!GameOn) return;
            if (!CheckForFinish.Invoke()) {
                OnGameEnd();
            }
        }

        private void FinishGame() {
            CompareScores();
            CalculateCoinsAward();
        }

        private void CalculateCoinsAward() {
             var toAdd = (int) (Score * Constants.SCORE_TO_COINS_COEF);
            if (toAdd > 0) { 
                Coins.Instance.AddCoins(toAdd);
            }
        }

        private void CompareScores() {
            var best = PlayerPrefs.GetInt(BestScoreKey);
            if (Score > best) {
                PlayerPrefs.SetInt(BestScoreKey, Score);
                if (GPSLeaderboard.Activated) {
                   SaveBestToLeaderboard?.Invoke(Score); 
                }
            }
        }

        private bool Timer() {
            CurrentTime -= Time.deltaTime;
            return CurrentTime > 0;
        }

        private bool LimitedBoxes() {
            return AmountOfBoxes > 0;
        }

        private bool FirstLessZero() {
            return _greaterThanZero;
        }

        private void OnGameEnd() {
            _gameOn = false;
            FinishGame();
            onGameEnd?.Invoke();
        }

        public void DecreaseAmountOfBoxes() => AmountOfBoxes--;

        private void HandleTouch() {
            var touchCondition = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                                 (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || !_gameOn);
            var mouseCondition = Input.GetMouseButtonDown(0) &&
                                 (!EventSystem.current.IsPointerOverGameObject() || !_gameOn);
            if (touchCondition || mouseCondition) {
                if (!GameOn) {
                    CreatePair();
                    GameOn = true;
                    return;
                }

                _boxController.DisableKinematic();
            }
        }

        public void CreatePair() {
            CreateTarget();
            CreateBox();
            ConnectBoxAndTarget();
        }

        private void CreateBox() {
            var box = Instantiate(boxPrefab, conveyor.transform.position + _vectorUp, boxPrefab.rotation); // todo
            _boxController = box.GetComponent<BoxController>();
            _boxController.CreateBox();
        }

        private void CreateTarget() {
            var targetPos = conveyor.transform.position - conveyor.MoveDirection * belt.localScale.z / 2.1f +
                            Vector3.up * conveyor.transform.localScale.y / 1.8f; // todo
            var target = Instantiate(targetPrefab, targetPos, targetPrefab.rotation);
            _targetController = target.GetComponent<TargetController>();
            _targetController.CreateTarget();
        }

        private void ConnectBoxAndTarget() {
            _boxController.ConnectedTarget = _targetController._target;
            _targetController.ConnectedBox = _boxController.Box;
        }

        private int FindCurrentScore(BoxController boxController) {
            var diffVect = boxController.Box.Transform.position - boxController.ConnectedTarget.Transform.position;
            diffVect.y = 0;
            var result = (int) (maximumScoreValue - diffVect.magnitude * scoreForUnitCoef);
            if (result < 0) {
                _greaterThanZero = false;
            }
            return result;
        }

        public void CalculateScore(BoxController boxController) {
            Score += FindCurrentScore(boxController);
        }
    }
}