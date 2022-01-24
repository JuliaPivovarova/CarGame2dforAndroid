using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Rewards
{
    public class WeeklyRewardView : MonoBehaviour, IBaseRewardView
    {
        private const string CurrentWeeklySlotinActiveKey = nameof(CurrentWeeklySlotinActiveKey);
        private const string TimeGetWeeklyRewardKey = nameof(TimeGetWeeklyRewardKey);
        
        [Header("Settings Time Get Reward")] [SerializeField]
        private float _timeCooldown = 604800; // Week in seconds

        [SerializeField] private float _timeDeadline = 1209600; // Two weeks
        
        [Header("Setting Rewards")] [SerializeField]
        private List<WeeklyReward> _rewards;

        [Header("UI Elements")] [SerializeField]
        private TMP_Text _timerNewReward;

        [SerializeField] private Transform _mountRootSlotsReward;
        [SerializeField] private ContainerSlotWeeklyRewardView _containerSlotWeeklyRewardView;
        [SerializeField] private Button _getRewardButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Slider _timerSlider;
        
        public float TimeCooldown => _timeCooldown;
        public float TimeDeadline => _timeDeadline;
        public List<WeeklyReward> Rewards => _rewards;
        public TMP_Text TimerNewReward => _timerNewReward;
        public Transform MountRootSlotsReward => _mountRootSlotsReward;
        public ContainerSlotWeeklyRewardView ContainerSlotWeeklyRewardView => _containerSlotWeeklyRewardView;
        public Button GetRewardButton => _getRewardButton;
        public Button ResetButton => _resetButton;
        public Button GoBackButton => _goBackButton;
        public Slider TimerSlider => _timerSlider;
        
        public int CurrentSlotingActive
        {
            get => PlayerPrefs.GetInt(CurrentWeeklySlotinActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentWeeklySlotinActiveKey, value);
        }
        
        public DateTime? TimeGetReward
        {
            get
            {
                var data = PlayerPrefs.GetString(TimeGetWeeklyRewardKey, null);

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
                    PlayerPrefs.SetString(TimeGetWeeklyRewardKey, value.ToString());
                }
                else
                {
                    PlayerPrefs.DeleteKey(TimeGetWeeklyRewardKey);
                }
            }
        }

        private void OnDestroy()
        {
            _getRewardButton.onClick.RemoveAllListeners();
            _resetButton.onClick.RemoveAllListeners();
            _goBackButton.onClick.RemoveAllListeners();
        }
    }
}