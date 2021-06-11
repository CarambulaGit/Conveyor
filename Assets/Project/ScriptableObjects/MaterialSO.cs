using UnityEngine;

namespace Project.ScriptableObjects {
    [CreateAssetMenu(fileName = "New conveyor material", menuName = "ConveyorMaterial", order = 0)]
    public class MaterialSO : ScriptableObject {
        public int cost;
        public Material material;
    }
}