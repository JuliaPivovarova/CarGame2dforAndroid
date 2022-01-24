using System;
using UnityEngine;

namespace Code.Rewards
{
    [Serializable]
    public class WeeklyReward
    {
        public bool IsSingle;
        public Reward FirstReward;
        public Reward SecondReward;
    }
}