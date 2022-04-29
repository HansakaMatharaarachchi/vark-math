namespace _Scripts
{
    [System.Serializable]
    public class LevelProgress
    {
        public int NoOfQuestions { get; }
        public int NoOfCorrectAnswers { get; set; }
        public int NoOfWrongAnswers { get; set; }

        public LevelProgress(int noOfQuestions)
        {
            this.NoOfQuestions = noOfQuestions;
        }

        public bool IsLevelPassed()
        {
            return NoOfCorrectAnswers == NoOfQuestions;
        }

        public int CalculateLevelProgress()
        {
            if (!IsLevelPassed())
            {
                return NoOfCorrectAnswers / NoOfQuestions * 100;
            }

            return 100 - NoOfWrongAnswers * 15;
        }
    }
}