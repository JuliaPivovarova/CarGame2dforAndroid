using System;
using Code.Controllers;
using UnityEngine;

namespace Code.Rewards
{
    public abstract class BaseRewardController: BaseController
    {
        protected abstract void RefreshView();

        protected abstract void SubscribesButtons();

        protected abstract void ClaimReward();

        protected abstract void InitSlots();
        
        private protected void RefreshRewardsState(IBaseRewardView baseRewardView, ref bool isGetReward)
        {
            isGetReward = true;

            if (baseRewardView.TimeGetReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - baseRewardView.TimeGetReward.Value;
                if (timeSpan.Seconds > baseRewardView.TimeDeadline)
                {
                    baseRewardView.TimeGetReward = null;
                    baseRewardView.CurrentSlotingActive = 0;
                }
                else if(timeSpan.Seconds < baseRewardView.TimeCooldown)
                {
                    isGetReward = false;
                }
            }

            RefreshUI();
        }
        
        private protected void AddReward(RewardType rewardType, int countCurrency)
        {
            switch (rewardType)
            {
                case RewardType.Wood:
                    CurrencyView.Instance.AddWood(countCurrency);
                    break;
                case RewardType.Diamond:
                    CurrencyView.Instance.AddDiamond(countCurrency);
                    break;
            }
        }

        private protected void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
            CurrencyView.Instance.ResetCurrency(RewardType.Wood);
            CurrencyView.Instance.ResetCurrency(RewardType.Diamond);
        }

        protected abstract void RefreshUI();
    }
}