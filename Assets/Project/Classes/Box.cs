using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class Box : GameObj {
        public BoxController BoxController { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public Box(Transform transform, Conveyor conveyor, Transform belt, BoxController boxController,
            Rigidbody rigidbody, NeedToDestroy onNeedToDestroy) : base(transform, conveyor, belt, onNeedToDestroy) {
            BoxController = boxController;
            Rigidbody = rigidbody;
        }
    }
}