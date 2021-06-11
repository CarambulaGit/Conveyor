using System;
using System.Collections.Generic;
using Project.Classes;
using Project.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using MaterialsContent = Project.Classes.ConveyorMaterial.ConveyorMaterialsContent;

namespace Project.Scripts {
    public class MainMenuCanvasController : MonoBehaviour {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject shop;
        [SerializeField] private List<ConveyorMaterialManager> materialsManagers;

        public List<ConveyorMaterial> _conveyorMaterials { get; private set; } = new List<ConveyorMaterial>();

        public int GetMaterialIndex(ConveyorMaterialManager manager) {
            return materialsManagers.FindIndex(elem => elem.Equals(manager));
        } 
        
        private void Awake() {
            foreach (var manager in materialsManagers) {
                _conveyorMaterials.Add(new ConveyorMaterial(manager.GetMaterial()));
            }
        }

        public void StartGame() {
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
        }

        public void ActivateShop() {
            Activate(false, true);
        }

        public void BackToMenu() {
            Activate(true, false);
        }

        private void Activate(bool _menu, bool _shop) {
            menu.SetActive(_menu);
            shop.SetActive(_shop);
        }
    }
}