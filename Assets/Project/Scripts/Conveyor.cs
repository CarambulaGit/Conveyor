using UnityEngine;

namespace Project.Scripts {
    public class Conveyor : MonoBehaviour {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float speed;

        void FixedUpdate() {
            Move();
        }

        void Move() {
            var oldPos = rigidbody.position;
            var pos = -transform.forward * (speed * Time.fixedDeltaTime);
            rigidbody.position += pos;
            rigidbody.MovePosition(oldPos);
        }
    }
}