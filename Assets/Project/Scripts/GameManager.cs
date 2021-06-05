using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        private readonly Vector3 _vectorUp = 3 * Vector3.up;

        [SerializeField] private Conveyor conveyor;
        [SerializeField] private Transform boxPrefab;
        [SerializeField] private Transform targetPrefab;
        [SerializeField] private float scoreForUnitCoef;
        [SerializeField] private float maximumScoreValue;

        private TargetController _targetController;
        private BoxController _boxController;
        private float _score;

        public Target ActiveTarget => _targetController._target;
        public Box ActiveBox => _boxController._box;

        private void Awake() {
            if (Instance != null) {
                Debug.Log("There is must be only one GameManager");
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Update() {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
                CreateTarget();
                CreateBox();
                ConnectBoxAndTarget();
            }

            Debug.Log(_score);
        }

        private void CreateBox() {
            var box = Instantiate(boxPrefab, conveyor.transform.position + _vectorUp, boxPrefab.rotation); // todo
            _boxController = box.GetComponent<BoxController>();
            _boxController.CreateBox();
        }

        private void CreateTarget() {
            var target = Instantiate(targetPrefab, new Vector3(0, 1.3f, -4.79f), targetPrefab.rotation); // todo
            _targetController = target.GetComponent<TargetController>();
            _targetController.CreateTarget();
        }

        private void ConnectBoxAndTarget() {
            _boxController.ConnectedTarget = _targetController._target;
            _targetController.ConnectedBox = _boxController._box;
        }

        private float FindCurrentScore(BoxController boxController) {
            var diffVect = boxController._box.Transform.position - boxController.ConnectedTarget.Transform.position;
            diffVect.y = 0;
            return maximumScoreValue - diffVect.magnitude * scoreForUnitCoef;
        }

        public void CalculateScore(BoxController boxController) {
            _score += FindCurrentScore(boxController);
        }
    }
}