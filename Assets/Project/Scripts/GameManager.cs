using System;
using Project.Classes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour {
        private readonly Vector3 _vectorUp = 7 * Vector3.up;

        [SerializeField] private Conveyor conveyor;
        [SerializeField] private Transform belt;
        [SerializeField] private Transform boxPrefab;
        [SerializeField] private Transform targetPrefab;
        [SerializeField] private float scoreForUnitCoef;
        [SerializeField] private float maximumScoreValue;
        [SerializeField] private float startTime;

        private TargetController _targetController;
        private BoxController _boxController;
        private bool _gameOn;
        private int _score;
        private float _currentTime;

        public bool GameOn {
            get => _gameOn;
            private set {
                if (_gameOn == value) return;
                _gameOn = value;
                if (_gameOn) { onStartGame.Invoke(); }
            }
        }

        public UnityEvent onStartGame;
        public UnityEvent onScoreChanged;
        public UnityEvent onTimeChanged;
        public UnityEvent onTimeUp;

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
                if (value > 0) {
                    _currentTime = value;
                }
                else {
                    _currentTime = 0;
                    _gameOn = false;
                    onTimeUp?.Invoke();
                }

                onTimeChanged?.Invoke();
            }
        }


        public Target ActiveTarget => _targetController._target;
        public Box ActiveBox => _boxController.Box;

        private void Awake() {
            CurrentTime = startTime;
        }

        private void Update() {
            if (CurrentTime > 0) {
                HandleTouch();
                UpdateTime();
            }
        }

        private void UpdateTime() {
            CurrentTime -= Time.deltaTime;
        }

        private void HandleTouch() {
            var touchCondition = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                                 !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            var mouseCondition = Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
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
            return (int) (maximumScoreValue - diffVect.magnitude * scoreForUnitCoef);
        }

        public void CalculateScore(BoxController boxController) {
            Score += FindCurrentScore(boxController);
        }
    }
}