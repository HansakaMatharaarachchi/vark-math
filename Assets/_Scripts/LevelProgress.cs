namespace _Scripts
{
    [System.Serializable]
    public class LevelProgress
    {
        public int NoOfQuestions { get; set; }
        public int NoOfCorrectAnswers { get; set; }
        public int NoOfWrongAnswers { get; set; }

        public LevelProgress(int noOfQuestions)
        {
            NoOfQuestions = noOfQuestions;
        }

        public bool IsLevelPassed()
        {
            return NoOfCorrectAnswers == NoOfQuestions;
        }

        public int CalculateLevelProgress()
        {
            if (!IsLevelPassed())
            {
                return (int) ((float) NoOfCorrectAnswers / NoOfQuestions * 100);
            }

            return 100 - NoOfWrongAnswers * 15;
        }
    }
}