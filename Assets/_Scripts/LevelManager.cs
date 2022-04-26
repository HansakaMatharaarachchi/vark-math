using System.Collections.Generic;

namespace _Scripts
{
    public class LevelManager
    {
        public Dictionary<int, Dictionary<LearningStyle, int[]>> levels =
            new Dictionary<int, Dictionary<LearningStyle, int[]>>();

        public LevelManager()
        {
            levels.Add(1, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {3, 4, 5}},
                {LearningStyle.Auditory, new[] {4, 5, 6}},
                {LearningStyle.Kinesthetic, new[] {7, 8, 9}},
            });
            levels.Add(2, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {6, 7, 8}},
                {LearningStyle.Auditory, new[] {7, 8, 9}},
                {LearningStyle.Kinesthetic, new[] {7, 8, 9}},
            });
            levels.Add(3, new Dictionary<LearningStyle, int[]>
            {
                {LearningStyle.Visual, new[] {10, 11, 12}},
                {LearningStyle.Auditory, new[] {7, 8, 9}},
                {LearningStyle.Kinesthetic, new[] {7, 8, 9}},
            });
        }

        public int[] GetQuestionsForALevel(int i, LearningStyle learningStyle)
        {
            return levels[i][learningStyle];
        }
    }
}