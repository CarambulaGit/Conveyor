using Project.Scripts;
using UnityEngine;

namespace Project.Classes {
    public class GameObj {
        public Transform Transform { get; protected set; }
        public Conveyor Conveyor { get; protected set; }
        public Transform Belt { get; protected set; }
        
        public delegate void NeedToDestroy();

        protected event NeedToDestroy OnNeedToDestroyObj;

        public GameObj(Transform transform, Conveyor conveyor, Transform belt, NeedToDestroy onNeedToDestroy) {
            Transform = transform;
            Conveyor = conveyor;
            Belt = belt;
            OnNeedToDestroyObj += onNeedToDestroy;
        }
        
        public void CheckDistance() {
            if (Vector3.Distance(Transform.position, Conveyor.transform.position) > Belt.localScale.z / 2) {
                OnNeedToDestroyObj?.Invoke();
            }
        }
    }
}