using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        [SerializeField] private GameObject iAP;
        [SerializeField] private List<ConveyorMaterialManager> materialsManagers;
        [SerializeField] private List<TextMeshProUGUI> coins;

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
            Activate(false, false, true, false);
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
            Activate(false, true, false, false);
        }

        public void ActivateMenu() {
            Activate(true, false, false, false);
        }

        public void ActivateIAP() {
            Activate(false, false, false, true);
        }

        private void Activate(bool _menu, bool _shop, bool _gameModes, bool _iAP) {
            menu.SetActive(_menu);
            shop.SetActive(_shop);
            gameModes.SetActive(_gameModes);
            iAP.SetActive(_iAP);
        }

        public void ShowLeaderboard() {
            GPSLeaderboard.ShowLeaderboard();
        }

        private void UpdateCoins() {
            foreach (var coin in coins) {
                coin.text = Coins.Instance.CoinsValue.ToString();
            }
        }

        public void ShowAd() {
            Ads.Instance.ShowRewardAd();
        }
    }
}