using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Project.Classes {
    public static class GPSLeaderboard {
        public static bool Activated { get; private set; }

        public static void ActivateGPS() {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(success => {
                if (success) {
                    Activated = true;
                }
            });
        }

        public static void SaveBestTimerResult(int result) {
            SaveBest(result, GPS.leaderboard_timerbest);
        }

        public static void SaveBestLimitedBoxesResult(int result) {
            SaveBest(result, GPS.leaderboard_limitedboxesbest);
        }

        public static void SaveBestFirstLessZeroResult(int result) {
            SaveBest(result, GPS.leaderboard_firstlesszero);
        }

        private static void SaveBest(int result, string board) {
            Social.ReportScore(result, board, success => { });
        }

        public static void ShowLeaderboard() {
            Social.ShowLeaderboardUI();
        }

        public static void ExitFromGPS() {
            PlayGamesPlatform.Instance.SignOut();
        }
    }
}