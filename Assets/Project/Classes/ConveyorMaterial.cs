using System;
using System.Collections.Generic;
using Project.ScriptableObjects;
using Project.Utils;
using UnityEngine;

namespace Project.Classes {
    public class ConveyorMaterial {
        // [Flags]
        // public enum ConveyorMaterialsContent {
        //     Material0 = 1,
        //     Material1 = 2,
        //     Material2 = 4,
        //     Material3 = 8,
        //     Material4 = 16,
        //     Material5 = 32,
        //     Material6 = 64,
        //     Material7 = 128,
        // }
        [Flags]
        public enum ConveyorMaterialsContent {
            Material0,
            Material1,
            Material2,
            Material3,
            Material4,
            Material5,
            Material6,
            Material7,
            Material8,
            Material9,
            Material10,
            Material11,
            Material12,
            Material13,
        }

        private static ConveyorMaterialsContent _materialsInventory;

        public static ConveyorMaterialsContent MaterialsInventory {
            get => _materialsInventory;
            set {
                if (_materialsInventory == value) return;
                _materialsInventory = value;
                PlayerPrefs.SetInt(Constants.MATERIALS_KEY, _materialsInventory.TI());
            }
        }

        private static int _currentMaterial;

        public static int CurrentMaterial {
            get => _currentMaterial;
            set {
                if (_currentMaterial == value) return;
                _currentMaterial = value;
                PlayerPrefs.SetInt(Constants.CURRENT_MATERIAL_KEY, _currentMaterial);
            }
        }

        public static List<ConveyorMaterial> Instances { get; private set; } = new List<ConveyorMaterial>();

        public static ConveyorMaterialsContent EnumValueFromIndex(int index) {
            return (ConveyorMaterialsContent) (1 << index);
        }
        
        public MaterialSO MaterialSO { get; private set; }
        public int Index { get; private set; }

        public static ConveyorMaterial FindByIndex(int index) {
            // return Instances.Find(elem => elem.Index == index);
            return Instances[index];
        }

        public ConveyorMaterial(MaterialSO materialSo) {
            MaterialSO = materialSo;
            Index = Instances.Count;
            Instances.Add(this);
        }

        public ConveyorMaterialsContent EnumValue() {
            return EnumValueFromIndex(Index);
        }

        public static void Load() {
            MaterialsInventory = (ConveyorMaterialsContent) PlayerPrefs.GetInt(Constants.MATERIALS_KEY, 1);
            CurrentMaterial = PlayerPrefs.GetInt(Constants.CURRENT_MATERIAL_KEY, 0);
        }
    }
}