using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class LoadManager : MonoBehaviour {
        // private delegate void LoadData();
        // private LoadData loadData;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Load() {
            // PlayerPrefs.DeleteAll(); // todo remove
            Coins.Instance.Load();
            ConveyorMaterial.Load();
        }

        // private void Update() {
        // Debug.Log($"Coins = {Coins.Instance.CoinsValue}");
        // Debug.Log($"Inventory = {(int) ConveyorMaterial.MaterialsInventory}");
        // Debug.Log($"Cur = {(ConveyorMaterial.ConveyorMaterialsContent) ConveyorMaterial.CurrentMaterial}");
        // }
    }
}