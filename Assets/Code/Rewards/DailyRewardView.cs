using System;
using System.Collections.Generic;
using Code.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Rewards
{
    public class DailyRewardView: MonoBehaviour, IBaseRewardView
    {
        private const string CurrentDailySlotinActiveKey = nameof(CurrentDailySlotinActiveKey);
        private const string TimeGetDailyRewardKey = nameof(TimeGetDailyRewardKey);
        
        [Header("Settings Time Get Reward")] [SerializeField]
        private float _timeCooldown = 86400;

        [SerializeField] private float _timeDeadline = 172800;

        [Header("Setting Rewards")] [SerializeField]
        private List<Reward> _rewards;

        [Header("UI Elements")] [SerializeField]
        private TMP_Text _timerNewReward;

        [SerializeField] private Transform _mountRootSlotsReward;
        [SerializeField] private ContainerSlotRewardView _containerSlotRewardView;
        [SerializeField] private CustomButton _getRewardButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _goToWeeklyRewardsButton;
        [SerializeField] private Button _goBack;
        [SerializeField] private Slider _timerSlider;

        public float TimeCooldown => _timeCooldown;
        public float TimeDeadline => _timeDeadline;
        public List<Reward> Rewards => _rewards;
        public TMP_Text TimerNewReward => _timerNewReward;
        public Transform MountRootSlotsReward => _mountRootSlotsReward;
        public ContainerSlotRewardView ContainerSlotRewardView => _containerSlotRewardView;
        public Button GetRewardButton => _getRewardButton;
        public Button ResetButton => _resetButton;
        public Button GoToWeeklyRewardsButton => _goToWeeklyRewardsButton;
        public Button GOBack => _goBack;
        public Slider TimerSlider => _timerSlider;

        public int CurrentSlotingActive
        {
            get => PlayerPrefs.GetInt(CurrentDailySlotinActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentDailySlotinActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                var data = PlayerPrefs.GetString(TimeGetDailyRewardKey, null);

                if (!string.IsNullOrEmpty(data))
                {
                    return DateTime.Parse(data);
                }

                return null;
            }
            set
            {
                if (value != null)
                {
                    PlayerPrefs.SetString(TimeGetDailyRewardKey, value.ToString());
                }
                else
                {
                    PlayerPrefs.DeleteKey(TimeGetDailyRewardKey);
                }
            }
        }
    }
}