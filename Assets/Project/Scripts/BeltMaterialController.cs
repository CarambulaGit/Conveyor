using System.Collections.Generic;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class BeltMaterialController : MonoBehaviour {
        [SerializeField] private List<MeshRenderer> beltsParts;

        private void Awake() {
            var materialIndex = PlayerPrefs.GetInt(Constants.CURRENT_MATERIAL_KEY);
            var conveyorMaterial = ConveyorMaterial.FindByIndex(materialIndex);
            var curMaterial = conveyorMaterial.MaterialSO.material;
            foreach (var meshRenderer in beltsParts) {
                meshRenderer.material = curMaterial;
            }
        }
    }
}