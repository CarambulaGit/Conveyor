using UnityEngine;

namespace Project.Classes {
    public class Coins {
        public static Coins Instance { get; private set; } = new Coins();
        private int _coinsValue;
        public int CoinsValue {
            get => _coinsValue;
            private set {
                if (_coinsValue == value) return;
                _coinsValue = value;
                PlayerPrefs.SetInt(Constants.COINS_KEY, _coinsValue);
            } 
        }

        public void AddCoins(int coins) {
            CoinsValue += coins;
        }

        public void SubtractCoins(int coins) {
            CoinsValue -= coins;
        }

        public bool TryBuyItem(int cost) {
            if (CoinsValue < cost) return false;
            SubtractCoins(cost);
            return true;
        }

        public void Load() {
            CoinsValue = PlayerPrefs.GetInt(Constants.COINS_KEY, 0);
        }
    }
}