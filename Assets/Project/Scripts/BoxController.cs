using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BoxController : MonoBehaviour {
        private string conveyorTag = "Conveyor";
        private string beltTag = "Belt";
        private Conveyor conveyor;
        private Transform belt;

        public Box _box { get; private set; }
        private Target _connectedTarget;

        public Target ConnectedTarget {
            get => _connectedTarget;
            set {
                if (ConnectedTarget != null) return;
                _connectedTarget = value;
            }
        }


        private void Awake() {
            conveyor = GameObject.FindWithTag(conveyorTag).GetComponent<Conveyor>();
            belt = GameObject.FindWithTag(beltTag).transform;
        }

        private void FixedUpdate() {
            _box?.CheckDistance();
        }

        public Box CreateBox() {
            _box = new Box(transform, conveyor, belt, this, RemoveBox);
            return _box;
        }

        private void RemoveBox() {
            Destroy(_box.Transform.gameObject);
            _box = null;
        }

        // todo event of falling 
    }
}