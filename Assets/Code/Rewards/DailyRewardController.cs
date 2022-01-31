using System;
using System.Collections;
using System.Collections.Generic;
using Code.Controllers;
using Code.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Rewards
{
    public class DailyRewardController: BaseRewardController
    {
        private DailyRewardView _dailyRewardView;
        private List<ContainerSlotRewardView> _slots = new List<ContainerSlotRewardView>();
        private bool _isGetReward;
        private ProfilePlayer _profilePlayer;

        public DailyRewardController(Transform placeForUI,  ProfilePlayer profilePlayer, DailyRewardView dailyRewardView, CurrencyView currencyView)
        {
            _profilePlayer = profilePlayer;
            _dailyRewardView = Object.Instantiate(dailyRewardView, placeForUI);
            AddGameObject(_dailyRewardView.gameObject);
            
            var currencyController = new CurrencyController(placeForUI, currencyView);
            AddController(currencyController);
        }

        public void RefreshDailyView()
        {
            RefreshView();
        }
        
        protected override void RefreshView()
        {
            InitSlots();
            _dailyRewardView.StartCoroutine(RewardsUpdater());
            RefreshUI();
            SubscribesButtons();
        }

        protected override void SubscribesButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
            _dailyRewardView.GoToWeeklyRewardsButton.onClick.AddListener(GoToWeeklyRewardsWindow);
            _dailyRewardView.GOBack.onClick.AddListener(GoBack);
        }

        private void GoBack()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private void GoToWeeklyRewardsWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.WeeklyReward;
        }

        protected override void ClaimReward()
        {
            if (!_isGetReward)
                return;
            var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotingActive];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                    break;
            }
            
            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.CurrentSlotingActive =
                (_dailyRewardView.CurrentSlotingActive + 1) % _dailyRewardView.Rewards.Count;
            
            RefreshRewardsState(_dailyRewardView, ref _isGetReward);
        }

        protected override void InitSlots()
        {
            for (int i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                var instanceSlot = Object.Instantiate(_dailyRewardView.ContainerSlotRewardView,
                    _dailyRewardView.MountRootSlotsReward, false);
                _slots.Add(instanceSlot);
            }
        }

        private IEnumerator RewardsUpdater()
        {
            while (true)
            {
                RefreshRewardsState(_dailyRewardView, ref _isGetReward);
                yield return new WaitForSeconds(1);
            }
        }

        protected override void RefreshUI()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;
            if (_isGetReward)
            {
                _dailyRewardView.TimerNewReward.text = "Reward recieved";
                _dailyRewardView.TimerSlider.value = _dailyRewardView.TimeCooldown;
            }
            else
            {
                if (_dailyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward =
                        $"{currentClaimCooldown.Days:D2} : {currentClaimCooldown.Hours:D2} : {currentClaimCooldown.Minutes:D2} : {currentClaimCooldown.Seconds:D2}";
                    _dailyRewardView.TimerNewReward.text = timeGetReward;
                    _dailyRewardView.TimerSlider.value = currentClaimCooldown.Seconds / _dailyRewardView.TimeCooldown;
                }
            }

            for (int i = 0; i < _slots.Count ; i++)
            {
                _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotingActive);
            }
        }

        protected override void OnDispose()
        {
            _dailyRewardView.GetRewardButton.onClick.RemoveAllListeners();
            _dailyRewardView.ResetButton.onClick.RemoveAllListeners();
            _dailyRewardView.GoToWeeklyRewardsButton.onClick.RemoveAllListeners();
            
            base.OnDispose();
        }
    }
}