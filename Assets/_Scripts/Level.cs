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
            noOfAttempts = 1;
            lastAttemptProgress = progress;
        }

        public Level(LevelProgress progress, bool isPassed)
        {
            noOfAttempts = 1;
            lastAttemptProgress = progress;
            this.isPassed = isPassed;
        }
    }
}