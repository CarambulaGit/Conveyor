using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class Box : GameObj {
        public Box(Transform transform, Conveyor conveyor, Transform belt, NeedToDestroy onNeedToDestroy) : base(
            transform, conveyor, belt, onNeedToDestroy) { }
    }
}