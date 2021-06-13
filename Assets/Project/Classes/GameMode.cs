namespace Project.Classes {
    public static class GameMode {
        public enum Mode {
            Timer,
            LimitedBoxes,
            FirstLessZero,
        }

        public static Mode CurrentGameMode { get; set; }
    }
}