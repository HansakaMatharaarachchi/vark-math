[System.Serializable]
public class LevelProgress

//used when he attempts

{
    public int noOfQuestions = 3;
    public int noOfAttempts;
    public int correctAnswers;
    public int wrongAnswers;


    public void AddCorrectAnswer()
    {
        noOfAttempts ++;
        correctAnswers += 1;
    }
    
    public void AddWrongAnswer()
    {
        noOfAttempts ++;
        wrongAnswers += 1;
    }


    public bool IsLevelPassed()
    {
        return correctAnswers == noOfQuestions;
    }

    public int CalculateLevelProgress()
    {
        if (!IsLevelPassed())
        {
            return correctAnswers / noOfQuestions * 100;
        }
        return 100 - wrongAnswers * 15;
    }

}