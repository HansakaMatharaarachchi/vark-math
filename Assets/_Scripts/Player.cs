using System;
using System.Collections.Generic;

namespace _Scripts
{
    public enum LearningStyle
    {
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
        public List<Level> levels = new List<Level>();
        public Inventory inventory = new Inventory();
        public LearningStyle learningStyle;
        public DateTime lastActiveDate;
        public bool isDailyRewardCollected;
        public float lastActiveDatePlaytimeInSeconds;

        public Player() {
        }

        public Player(string name, int age)
        {
            Name = name;
            Age = age;
            lastActiveDate = DateTime.Now.Date;
            lastActiveDatePlaytimeInSeconds = 0.0f;
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
