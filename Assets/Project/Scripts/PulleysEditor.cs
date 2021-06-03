using Project.Scripts;
using UnityEditor;
using UnityEngine;

namespace Project.EditorScripts {
    // [ExecuteAlways]
    [ExecuteInEditMode]
    public class PulleysEditor : MonoBehaviour {
        public Transform[] pulleys = new Transform[2];
        public Transform belt;

        private Vector3 _prevBeltScale;

#if UNITY_EDITOR
        public void Update() {
            if (belt.localScale.Equals(_prevBeltScale)) return;
            _prevBeltScale = belt.localScale;
            var posZ = _prevBeltScale.z / 2;
            var localPos0 = pulleys[0].localPosition;
            var localPos1 = pulleys[1].localPosition;
            pulleys[0].localPosition = new Vector3(localPos0.x, localPos0.y, posZ);
            pulleys[1].localPosition = new Vector3(localPos1.x, localPos1.y, -posZ);
        }
#endif
    }
}