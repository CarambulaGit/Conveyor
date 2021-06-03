using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class TargetController : MonoBehaviour {
        [SerializeField] private Conveyor conveyor;
        [SerializeField] private Transform targetPrefab;
        [SerializeField] private Transform belt;

        private Target _target;

        private void Awake() {
            // CreateTarget();
        }

        private void FixedUpdate() {
            if (_target != null) {
                MoveTarget(_target);
            }
        }

        private void MoveTarget(Target target) {
            target.Move(Time.fixedDeltaTime);
        }

        public Target CreateTarget() {
            var target = Instantiate(targetPrefab, new Vector3(0, 1.3f, -4.79f), targetPrefab.rotation); // todo
            _target = new Target(target, conveyor, belt, ClearTargetValue);
            return _target;
        }

        private void ClearTargetValue() {
            _target = null;
        }
    }
}