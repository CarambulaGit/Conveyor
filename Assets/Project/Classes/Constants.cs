namespace Project.Classes {
    public static class Constants {
        public const float EPSILON = 0.001f;

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

        #endregion
    }
}