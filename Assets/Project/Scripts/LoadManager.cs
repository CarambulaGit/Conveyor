using System;
using Project.Classes;
using UnityEngine;

namespace Project.Scripts {
    public class LoadManager : MonoBehaviour {
        // private delegate void LoadData();
        // private LoadData loadData;

        private void Awake() {
            DontDestroyOnLoad(gameObject);
            // PlayerPrefs.DeleteAll(); // todo remove
            Coins.Instance.Load();
            // Coins.Instance.AddCoins(30);
            ConveyorMaterial.Load();
        }

        // private void Update() {
            // Debug.Log($"Coins = {Coins.Instance.CoinsValue}");
            // Debug.Log($"Inventory = {(int) ConveyorMaterial.MaterialsInventory}");
            // Debug.Log($"Cur = {(ConveyorMaterial.ConveyorMaterialsContent) ConveyorMaterial.CurrentMaterial}");
        // }
    }
}