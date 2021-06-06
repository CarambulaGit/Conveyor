using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BoxController : MonoBehaviour {
        private readonly string _conveyorTag = "Conveyor";
        private readonly string _beltTag = "Belt";
        private Conveyor _conveyor;
        private Transform _belt;
        private Target _connectedTarget;
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
            _conveyor = GameObject.FindWithTag(_conveyorTag).GetComponent<Conveyor>();
            _belt = GameObject.FindWithTag(_beltTag).transform;
        }

        private void FixedUpdate() {
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
            if (_alreadyCalculated) return;

            if (other.gameObject.CompareTag(_conveyorTag)) {
                StillNotFall = false;
                GameManager.Instance.CalculateScore(this);
                _alreadyCalculated = true;
                ConnectedTarget.TargetController.RemoveTarget();
                GameManager.Instance.CreatePair();
            }
        }

        // todo event of falling 
    }
}