[System.Serializable]
public class LevelProgress
{
    public int noOfQuestions;
    public int noOfCorrectAnswers;
    public int noOfWrongAnswers;

    public LevelProgress(int noOfQuestions)
    {
        this.noOfQuestions = noOfQuestions;
    }

    public bool IsLevelPassed()
    {
        return noOfCorrectAnswers == noOfQuestions;
    }

    public int CalculateLevelProgress()
    {
        if (!IsLevelPassed())
        {
            return noOfCorrectAnswers / noOfQuestions * 100;
        }

        return 100 - noOfWrongAnswers * 15;
    }
}