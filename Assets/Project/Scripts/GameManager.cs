﻿using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private Conveyor conveyor;
        [SerializeField] private TargetController targetController;
        [SerializeField] private BoxController boxController;

        public Target ActiveTarget => targetController._target;
        public Box ActiveBox => boxController._box;
        
        private void Awake() {
            if (Instance != null) {
                Debug.Log("There is must be only one GameManager");
                Destroy(this);
                return;
            }

            Instance = this;
        }

        void Update() {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
                CreateTarget();
                CreateBox();
            }
        }

        private void CreateBox() {
            boxController.CreateBox();
        }

        private void CreateTarget() {
            targetController.CreateTarget();
        }
    }
}