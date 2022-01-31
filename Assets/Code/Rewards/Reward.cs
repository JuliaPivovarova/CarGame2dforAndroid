using System;
using UnityEngine;

namespace Code.Rewards
{
    [Serializable]
    public class Reward
    {
        public RewardType RewardType;
        public Sprite IconCurrency;
        public int CountCurrency;
    }
}