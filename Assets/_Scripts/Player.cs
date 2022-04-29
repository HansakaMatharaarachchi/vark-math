using System;

namespace _Scripts
{
    public enum LearningStyle
    {
        NotSet,
        Visual,
        Auditory,
        Kinesthetic
    }

    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int GoldCoinAmount { get; set; } = 200;
        public int Level { get; set; } = 1;
        public Level[] LevelStats { get; } = new Level[5];
        public Inventory Inventory { get; } = new Inventory();
        public LearningStyle LearningStyle { get; set; }
        public DateTime LastActiveDate { get; set; }
        public bool IsDailyRewardCollected { get; set; }
        public float LastActiveDatePlaytimeInSeconds { get; set; }

        public Player()
        {
            for (int i = 0; i < LevelStats.Length; i++)
            {
                LevelStats[i] = new Level();
            }
        }

        public Player(string name, int age)
        {
            Name = name;
            Age = age;
            LastActiveDate = DateTime.Now.Date;
            LastActiveDatePlaytimeInSeconds = 0.0f;
            for (int i = 0; i < LevelStats.Length; i++)
            {
                LevelStats[i] = new Level();
            }
        }

        public void BuyItem(StoreItemObject storeItemObject)
        {
            if (GoldCoinAmount < storeItemObject.price) return;
            GoldCoinAmount -= storeItemObject.price;
            Inventory.AddItem(storeItemObject);
        }

        public void EquipItem(StoreItemObject storeItemObject)
        {
            Inventory.SetEquippedItem(storeItemObject);
        }

        public void CollectDailyReward(int amount)
        {
            IsDailyRewardCollected = true;
            GoldCoinAmount += amount;
        }
    }
}