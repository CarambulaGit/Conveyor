using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class Conveyor : MonoBehaviour {
        private enum Direction {
            Forward,
            Back
        }
        
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float speed;
        [SerializeField] private Direction dir;
        
        public Vector3 MoveDirection { get; private set; }
        public float GetSpeed() => speed;

        private void Awake() {
            switch (dir) {
                case Direction.Forward:
                    MoveDirection = transform.forward;
                    break;
                case Direction.Back:
                    MoveDirection = -transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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