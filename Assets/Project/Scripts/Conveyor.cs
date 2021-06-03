using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class Conveyor : MonoBehaviour {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float speed;
        public Vector3 MoveDirection { get; private set; }
        public float GetSpeed() => speed;

        private void Awake() {
            MoveDirection = Vector3.forward;
        }

        private void FixedUpdate() {
            Move(rigidbody);
        }

        private void Move(Rigidbody rigidbody) {
            var oldPos = rigidbody.position;
            var pos = -MoveDirection * (speed * Time.fixedDeltaTime);
            rigidbody.position += pos;
            rigidbody.MovePosition(oldPos);
        }
    }
}