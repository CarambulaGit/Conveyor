using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class TargetController : MonoBehaviour {
        [SerializeField] private Conveyor conveyor;
        [SerializeField] private Transform targetPrefab;
        [SerializeField] private Transform belt;

        public Target _target { get; private set; }

        private void FixedUpdate() {
            if (_target != null) {
                MoveTarget(_target);
            }
        }
        
        public Target CreateTarget() {
            var target = Instantiate(targetPrefab, new Vector3(0, 1.3f, -4.79f), targetPrefab.rotation); // todo
            _target = new Target(target, conveyor, belt, RemoveTarget);
            return _target;
        }

        private void MoveTarget(Target target) {
            target.Move(Time.fixedDeltaTime);
        }

        private void RemoveTarget() {
            Destroy(_target.Transform.gameObject);
            _target = null;
        }
    }
}