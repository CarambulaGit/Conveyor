using UnityEngine;

namespace Project.Classes {
    public class Box {
        public Transform Transform { get; private set; }

        public Box(Transform transform) {
            Transform = transform;
        }
    }
}