using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BoxController : MonoBehaviour {
        [SerializeField] private Transform boxPrefab;
        [SerializeField] private Transform conveyorTransform;

        private Box _box;
        private Vector3 _vectorUp = 3 * Vector3.up; 

        public Box CreateBox() {
            var box = Instantiate(boxPrefab, conveyorTransform.position + _vectorUp, boxPrefab.rotation); // todo
            _box = new Box(box);
            return _box;
        }
    }
}