namespace Project.Classes {
    public static class Constants {
        public const float EPSILON = 0.001f;
        public const float SCORE_TO_COINS_COEF = 0.01f;

        #region SceneNames

        public const string GAME_SCENE_NAME = "GameScene";
        public const string MAIN_MENU_SCENE_NAME = "MainMenu";

        #endregion;

        #region Tags

        public const string GAME_MANAGER_TAG = "GameController";
        public const string CONVEYOR_TAG = "Conveyor";
        public const string BELT_TAG = "Belt";

        #endregion

        #region PlayerPrefsKeys

        public const string COINS_KEY = "coins";
        public const string MATERIALS_KEY = "materials";
        public const string CURRENT_MATERIAL_KEY = "currentMat";
        public const string TIMER_BEST_SCORE_KEY = "timerBestScore";
        public const string LIMITED_BOXES_BEST_SCORE_KEY = "limitedBoxesBestScore";
        public const string FIRST_LESS_ZERO_BEST_SCORE_KEY = "firstLessZeroBestScore";

        #endregion

        #region Ads

        public const string ADS_ANDROID_KEY = "4172047";
        // public const string rewardPlacement = "rewardedVideo";
        public const int rewardValue = 100;
        public const string rewardPlacement = "Rewarded_Android";

        #endregion
    }
}