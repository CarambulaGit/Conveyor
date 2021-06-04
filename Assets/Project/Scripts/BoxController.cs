using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BoxController : MonoBehaviour {
        [SerializeField] private Conveyor conveyor;
        [SerializeField] private Transform boxPrefab;
        [SerializeField] private Transform belt;

        private readonly Vector3 _vectorUp = 3 * Vector3.up;

        public Box _box { get; private set; }

        private void FixedUpdate() {
            _box?.CheckDistance();
        }

        public Box CreateBox() {
            var box = Instantiate(boxPrefab, conveyor.transform.position + _vectorUp, boxPrefab.rotation); // todo
            _box = new Box(box, conveyor, belt, RemoveBox);
            return _box;
        }

        private void RemoveBox() {
            Destroy(_box.Transform.gameObject);
            _box = null;
        }

        // todo event of falling 
    }
}