namespace _Scripts
{
    [System.Serializable]
    public class Level
    {
        public bool isPassed;
        public int noOfAttempts;
        public LevelProgress lastAttemptProgress;

        public Level()
        {
        }

        public Level(LevelProgress progress)
        {
            lastAttemptProgress = progress;
        }

        public Level(LevelProgress progress, bool isPassed)
        {
            lastAttemptProgress = progress;
            this.isPassed = isPassed;
        }
    }
}