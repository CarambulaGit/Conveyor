using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class Target {
        public Transform Transform { get; private set; }
        public Conveyor Conveyor { get; private set; }
        public Transform Belt { get; private set; }
        
        public delegate void TargetDestroy();
        
        public event TargetDestroy OnDestroyTarget;

        public Target(Transform transform, Conveyor conveyor, Transform belt, TargetDestroy onTargetDestroy) {
            Transform = transform;
            Conveyor = conveyor;
            Belt = belt;
            OnDestroyTarget += onTargetDestroy;
        }

        public void Move(float fixedDeltaTime) {
            if (Vector3.Distance(Transform.position, Conveyor.transform.position) > Belt.localScale.z / 2) {
                RemoveTarget();
            }
            Transform.position += Conveyor.MoveDirection * (fixedDeltaTime * Conveyor.GetSpeed());
        }

        private void RemoveTarget() {
            // Transform.gameObject.SetActive(false);
            OnDestroyTarget?.Invoke();
            GameManager.Instance.DestroyGO(Transform.gameObject);
        }
    }
}