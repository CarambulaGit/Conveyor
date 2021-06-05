﻿using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class TargetController : MonoBehaviour {
        private string conveyorTag = "Conveyor";
        private string beltTag = "Belt";
        private Conveyor conveyor;
        private Transform belt;

        public Target _target { get; private set; }
        private Box _connectedBox;

        public Box ConnectedBox {
            get => _connectedBox;
            set {
                if (ConnectedBox != null) return;
                _connectedBox = value;
            }
        }

        private void Awake() {
            conveyor = GameObject.FindWithTag(conveyorTag).GetComponent<Conveyor>();
            belt = GameObject.FindWithTag(beltTag).transform;
        }

        private void FixedUpdate() {
            if (_target != null) {
                MoveTarget(_target);
            }
        }

        public Target CreateTarget() {
            _target = new Target(transform, conveyor, belt, this, RemoveTarget);
            return _target;
        }

        private void MoveTarget(Target target) {
            target.Move(Time.fixedDeltaTime);
        }

        public void RemoveTarget() {
            Destroy(_target.Transform.gameObject);
            _target = null;
        }
    }
}