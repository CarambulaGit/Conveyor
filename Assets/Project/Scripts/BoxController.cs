using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BoxController : MonoBehaviour {
        private Conveyor _conveyor;
        private Transform _belt;
        private Target _connectedTarget;
        private GameManager _gameManager;
        private bool _alreadyCalculated;


        public Box Box { get; private set; }

        public Target ConnectedTarget {
            get => _connectedTarget;
            set {
                if (ConnectedTarget != null) return;
                _connectedTarget = value;
            }
        }

        public bool StillNotFall { get; private set; } = true;

        private void Awake() {
            _conveyor = GameObject.FindWithTag(Constants.CONVEYOR_TAG).GetComponent<Conveyor>();
            _belt = GameObject.FindWithTag(Constants.BELT_TAG).transform;
            _gameManager = GameObject.FindWithTag(Constants.GAME_MANAGER_TAG).GetComponent<GameManager>();
        }

        private void FixedUpdate() {
            if (!_gameManager.GameOn) return;
                Box?.CheckDistance();
        }

        public Box CreateBox() {
            var rb = transform.GetComponent<Rigidbody>();
            Box = new Box(transform, _conveyor, _belt, this, rb, DestroyBox);
            return Box;
        }

        public void DestroyBox() {
            Destroy(Box.Transform.gameObject);
            Box = null;
        }

        public void DisableKinematic() {
            Box.Rigidbody.isKinematic = false;
        }

        private void OnCollisionEnter(Collision other) {
            if (_alreadyCalculated || !_gameManager.GameOn) return;

            if (other.gameObject.CompareTag(Constants.CONVEYOR_TAG)) {
                StillNotFall = false;
                _gameManager.CalculateScore(this);
                _alreadyCalculated = true;
                ConnectedTarget.TargetController.RemoveTarget();
                _gameManager.CreatePair();
            }
        }
    }
}