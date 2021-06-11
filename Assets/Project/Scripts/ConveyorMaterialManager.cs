using Project.Classes;
using Project.ScriptableObjects;
using Project.Utils;
using TMPro;
using UnityEngine;
using MaterialsContent = Project.Classes.ConveyorMaterial.ConveyorMaterialsContent;

namespace Project.Scripts {
    public class ConveyorMaterialManager : MonoBehaviour {
        private static ConveyorMaterialManager curMatManager;

        [SerializeField] private MainMenuCanvasController menuCanvasController;
        [SerializeField] private MaterialSO materialSO;
        [SerializeField] private TextMeshProUGUI text;

        private ConveyorMaterial material;
        private bool _isPurchased;
        private string _purchasedText = "Take";
        private string _buyText = "Buy\n";
        private string _currentText = "Taken";

        private delegate void OnClick();

        private OnClick _onClick;

        public MaterialSO GetMaterial() => materialSO;
        public int Index => material.Index;

        private void Start() {
            material = ConveyorMaterial.FindByIndex(menuCanvasController.GetMaterialIndex(this));
            _isPurchased = ConveyorMaterial.MaterialsInventory.Get(material.EnumValue());

            UpdateUI();
        }

        public void OnClickMaterial() {
            _onClick?.Invoke();
        }

        private void UpdateUI() {
            if (!_isPurchased) {
                OnDefaultUI();
            }
            else {
                if (Index != ConveyorMaterial.CurrentMaterial) {
                    OnBuyUI();
                }
                else {
                    OnTakeUI();
                    curMatManager = this;
                }
            }
        }

        private void OnDefaultUI() {
            text.text = _buyText + $"{materialSO.cost} $";
            _onClick = Buy;
        }

        private void OnBuyUI() {
            text.text = _purchasedText;
            _onClick = Take;
        }

        private void OnTakeUI() {
            text.text = _currentText;
            _onClick = () => { };
        }

        private void Buy() {
            if (!Coins.Instance.TryBuyItem(materialSO.cost)) {
                return;
            }

            var conveyorMaterialsContent = ConveyorMaterial.MaterialsInventory;
            conveyorMaterialsContent.On(material.EnumValue());
            ConveyorMaterial.MaterialsInventory = conveyorMaterialsContent;
            OnBuyUI();
        }

        private void Take() {
            ConveyorMaterial.CurrentMaterial = material.Index;
            OnTakeUI();
            curMatManager.OnBuyUI();
            curMatManager = this;
        }
    }
}