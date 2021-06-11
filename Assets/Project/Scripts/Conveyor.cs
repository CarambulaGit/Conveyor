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

        private GameManager _gameManager;

        public Vector3 MoveDirection { get; private set; }
        public float GetSpeed() => speed;

        private void Awake() {
            _gameManager = GameObject.FindWithTag(Constants.GAME_MANAGER_TAG).GetComponent<GameManager>();
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
            if (!_gameManager.GameOn) return;
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