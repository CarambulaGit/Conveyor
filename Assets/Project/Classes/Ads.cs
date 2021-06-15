using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Advertisements;
using Task = System.Threading.Tasks.Task;

namespace Project.Classes {
    public class Ads : IUnityAdsListener {
        public static Ads Instance { get; } = new Ads();
        public event Action<int> OnNeedToReward;

        private Ads() {
            Init(Constants.ADS_ANDROID_KEY);
            OnNeedToReward += Coins.Instance.AddCoins;
        }

        private void Init(string key) {
            Advertisement.AddListener(this);
            Advertisement.Initialize(key);
            Debug.Log(Advertisement.isInitialized);
        }

        public async void ShowRewardAd() {
            while (!Advertisement.IsReady(Constants.rewardPlacement)) {
                await Task.Yield();
            }
            Advertisement.Show(Constants.rewardPlacement);

        }

        public void OnUnityAdsReady(string placementId) { }

        public void OnUnityAdsDidError(string message) { }

        public void OnUnityAdsDidStart(string placementId) { }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            switch (placementId) {
                case Constants.rewardPlacement:
                    if (showResult == ShowResult.Finished) {
                        OnNeedToReward?.Invoke(Constants.rewardValue);
                    }

                    break;
                default:
                    break;
            }
        }
    }
}