namespace _Scripts
{
    [System.Serializable]
    public class Level
    {
        public bool isPassed;
        public int noOfAttempts;
        public LevelProgress lastAttempt;

        public Level()
        {
            noOfAttempts = 1;
        }
    }
    [System.Serializable]
    public class LevelProgress
    {
        public int correctAnswersCount;
        public int noOfQuestions = 3;
        public int noOfWrongAnswers;
    
        // public int noOfAttempts;
        // public int correctAnswers;

        //
        // public void AddCorrectAnswer()
        // {
        //     noOfAttempts++;
        //     correctAnswers += 1;
        // }
        //
        // public void AddWrongAnswer()
        // {
        //     noOfAttempts++;
        //     wrongAnswers += 1;
        // }
        //
        //
        // public bool IsLevelPassed()
        // {
        //     return correctAnswers == noOfQuestions;
        // }
        //
        // public int CalculateLevelProgress()
        // {
        //     if (!IsLevelPassed())
        //     {
        //         return correctAnswers / noOfQuestions * 100;
        //     }
        //
        //     return 100 - wrongAnswers * 15;
        // }
    }
}