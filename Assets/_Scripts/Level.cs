namespace _Scripts
{
    [System.Serializable]
    public class Level
    {
        public bool IsPassed { get; set; }
        public int NoOfAttempts { get; set; }
        public LevelProgress LastAttemptProgress { get; set; }

        public Level() { }

        public Level(LevelProgress progress)
        {
            LastAttemptProgress = progress;
        }

        public Level(LevelProgress progress, bool isPassed)
        {
            LastAttemptProgress = progress;
            IsPassed = isPassed;
        }
    }
}