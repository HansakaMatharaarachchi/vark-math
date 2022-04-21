[System.Serializable]
public class LevelProgress
{
    public int noOfQuestions;
    public int noOfAttempts;
    public int noOfCorrectAnswers;
    public int noOfWrongAnswers;

    public LevelProgress(int noOfQuestions)
    {
        this.noOfQuestions = noOfQuestions;
    }
    
    public void AddCorrectAnswer()
    {
        noOfAttempts ++;
        noOfCorrectAnswers += 1;
    }
    
    public void AddWrongAnswer()
    {
        noOfAttempts ++;
        noOfWrongAnswers += 1;
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