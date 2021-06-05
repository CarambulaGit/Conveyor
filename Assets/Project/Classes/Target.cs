using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class Target : GameObj {
        public TargetController TargetController { get; private set; }

        public Target(Transform transform, Conveyor conveyor, Transform belt, TargetController targetController,
            NeedToDestroy onNeedToDestroy) : base(transform, conveyor, belt, onNeedToDestroy) {
            TargetController = targetController;
        }

        public void Move(float fixedDeltaTime) {
            CheckDistance();
            Transform.position += Conveyor.MoveDirection * (fixedDeltaTime * Conveyor.GetSpeed());
        }
    }
}