using System.Collections.Generic;

namespace _Scripts
{
    public class LevelManager
    {
        private readonly Dictionary<int, Dictionary<LearningStyle, int[]>> levels = new Dictionary<int, Dictionary<LearningStyle, int[]>>();

        public LevelManager()
        {
            levels.Add(1, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {12, 13, 14}},
                {LearningStyle.Auditory, new[] {27, 28, 29}},
                {LearningStyle.Kinesthetic, new[] {18, 19, 20}},
            });
            levels.Add(2, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {21, 22, 23}},
                {LearningStyle.Auditory, new[] {30, 31, 32}},
                {LearningStyle.Kinesthetic, new[] {7, 8, 9}},
            });
            levels.Add(3, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {21, 22, 23}},
                {LearningStyle.Auditory, new[] {15, 16, 17}},
                {LearningStyle.Kinesthetic, new[] {7, 8, 9}},
            });
        }

        public int[] GetQuestionsForALevel(int i, LearningStyle learningStyle)
        {
            return levels[i][learningStyle];
        }
    }
}