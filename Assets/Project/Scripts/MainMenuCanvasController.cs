using System;
using System.Collections.Generic;
using Project.Classes;
using Project.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using MaterialsContent = Project.Classes.ConveyorMaterial.ConveyorMaterialsContent;

namespace Project.Scripts {
    public class MainMenuCanvasController : MonoBehaviour {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject shop;
        [SerializeField] private GameObject gameModes;
        [SerializeField] private List<ConveyorMaterialManager> materialsManagers;
        [SerializeField] private TextMeshProUGUI coins;

        public List<ConveyorMaterial> _conveyorMaterials { get; private set; } = new List<ConveyorMaterial>();

        public int GetMaterialIndex(ConveyorMaterialManager manager) {
            return materialsManagers.FindIndex(elem => elem.Equals(manager));
        } 
        
        private void Awake() {
            foreach (var manager in materialsManagers) {
                _conveyorMaterials.Add(new ConveyorMaterial(manager.GetMaterial()));
            }

            Coins.Instance.OnCoinsValueChanged += UpdateCoins;
        }

        private void Start() {
            UpdateCoins();
        }

        public void Play() {
            Activate(false, false, true);
        }

        public void StartTimerMode() {
            GameMode.CurrentGameMode = GameMode.Mode.Timer;
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
        }

        public void StartLimitedBoxesMode() {
            GameMode.CurrentGameMode = GameMode.Mode.LimitedBoxes;
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
        }

        public void StartFirstLessZeroMode() {
            GameMode.CurrentGameMode = GameMode.Mode.FirstLessZero;
            SceneManager.LoadScene(Constants.GAME_SCENE_NAME);
        }

        public void ActivateShop() {
            Activate(false, true, false);
        }

        public void BackToMenu() {
            Activate(true, false, false);
        }

        private void Activate(bool _menu, bool _shop, bool _gameModes) {
            menu.SetActive(_menu);
            shop.SetActive(_shop);
            gameModes.SetActive(_gameModes);
        }

        private void UpdateCoins() {
            coins.text = Coins.Instance.CoinsValue.ToString();
        }
    }
}