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
        public int level = 1;
        public Level[] levelStats = new Level[5];
        public Inventory inventory = new Inventory();
        public LearningStyle learningStyle;
        public DateTime lastActiveDate;
        public bool isDailyRewardCollected;
        public float lastActiveDatePlaytimeInSeconds;

        public Player()
        {
        }

        public Player(string name, int age)
        {
            Name = name;
            Age = age;
            lastActiveDate = DateTime.Now.Date;
            lastActiveDatePlaytimeInSeconds = 0.0f;
            for (int i = 0; i < levelStats.Length; i++)
            {
                levelStats[i] = new Level();
            }
        }

        public void BuyItem(StoreItemObject storeItemObject)
        {
            if (GoldCoinAmount < storeItemObject.price) return;
            GoldCoinAmount -= storeItemObject.price;
            inventory.AddItem(storeItemObject);
        }

        public void EquipItem(StoreItemObject storeItemObject)
        {
            inventory.SetEquippedItem(storeItemObject);
        }

        public void CollectDailyReward(int amount)
        {
            isDailyRewardCollected = true;
            GoldCoinAmount += amount;
        }
    }
}