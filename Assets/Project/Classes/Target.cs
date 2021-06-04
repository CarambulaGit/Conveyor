using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class Target : GameObj {
        public Target(Transform transform, Conveyor conveyor, Transform belt, NeedToDestroy onNeedToDestroy) : base(
            transform, conveyor, belt, onNeedToDestroy) { }

        public void Move(float fixedDeltaTime) {
            CheckDistance();
            Transform.position += Conveyor.MoveDirection * (fixedDeltaTime * Conveyor.GetSpeed());
        }
    }
}