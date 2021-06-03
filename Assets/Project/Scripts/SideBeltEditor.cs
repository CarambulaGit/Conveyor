using UnityEngine;

namespace Project.Scripts {
    [ExecuteInEditMode]
    public class SideBeltEditor : MonoBehaviour {
        public Transform[] sideBelt = new Transform[2];
        public Transform belt;

        private Vector3 _prevBeltScale;
#if UNITY_EDITOR
        public void Update() {
            if (belt.localScale.Equals(_prevBeltScale)) return;
            sideBelt[0].localScale =
                new Vector3(sideBelt[0].localScale.x, sideBelt[0].localScale.y, 1 / belt.localScale.z);
            sideBelt[1].localScale =
                new Vector3(sideBelt[1].localScale.x, sideBelt[1].localScale.y, 1 / belt.localScale.z);
        }
#endif
    }
}