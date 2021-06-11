using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class TargetController : MonoBehaviour {
        private string conveyorTag = "Conveyor";
        private string beltTag = "Belt";
        private Conveyor conveyor;
        private Transform belt;
        private Box _connectedBox;
        private GameManager _gameManager;

        public Target _target { get; private set; }

        public Box ConnectedBox {
            get => _connectedBox;
            set {
                if (ConnectedBox != null) return;
                _connectedBox = value;
            }
        }

        private void Awake() {
            _gameManager = GameObject.FindWithTag(Constants.GAME_MANAGER_TAG).GetComponent<GameManager>();
            conveyor = GameObject.FindWithTag(conveyorTag).GetComponent<Conveyor>();
            belt = GameObject.FindWithTag(beltTag).transform;
        }

        private void FixedUpdate() {
            if (!_gameManager.GameOn) return;
            _target?.Move(Time.fixedDeltaTime);

        }

        public Target CreateTarget() {
            _target = new Target(transform, conveyor, belt, this, RemoveTarget);
            return _target;
        }

        public void RemoveTarget() {
            var connectedBoxController = ConnectedBox.BoxController;
            if (connectedBoxController.StillNotFall) {
                _gameManager.CalculateScore(connectedBoxController);
                connectedBoxController.DestroyBox();
                _gameManager.CreatePair();
            }

            DestroyTarget();
        }

        public void DestroyTarget() {
            Destroy(_target.Transform.gameObject);
            _target = null;
        }
    }
}