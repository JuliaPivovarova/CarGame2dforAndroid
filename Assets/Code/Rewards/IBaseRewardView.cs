using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Rewards
{
    public interface IBaseRewardView
    {
        float TimeCooldown { get; }
        float TimeDeadline { get; }
        TMP_Text TimerNewReward { get; }
        Transform MountRootSlotsReward { get; }
        Button GetRewardButton { get; }
        Button ResetButton { get; }
        Slider TimerSlider { get; }
        
        int CurrentSlotingActive { get; set; }
        DateTime? TimeGetReward { get; set; }
    }
}