﻿using System;
using System.Collections;
using System.Collections.Generic;
using Code.Controllers;
using Code.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Rewards
{
    public class WeeklyRewardController: BaseRewardController
    {
        private WeeklyRewardView _weeklyRewardView;
        private List<ContainerSlotWeeklyRewardView> _slots = new List<ContainerSlotWeeklyRewardView>();
        private bool _isGetReward;
        private ProfilePlayer _profilePlayer;

        public WeeklyRewardController(Transform placeForUI,  ProfilePlayer profilePlayer, WeeklyRewardView weeklyRewardView, CurrencyView currencyView)
        {
            _profilePlayer = profilePlayer;
            _weeklyRewardView = Object.Instantiate(weeklyRewardView, placeForUI);
            AddGameObject(_weeklyRewardView.gameObject);
            
            var currencyController = new CurrencyController(placeForUI, currencyView);
            AddController(currencyController);
        }

        public void RefreshWeeklyView()
        {
            RefreshView();
        }
        
        protected override void RefreshView()
        {
            InitSlots();
            _weeklyRewardView.StartCoroutine(RewardsUpdater());
            RefreshUI();
            SubscribesButtons();
        }
        
        private IEnumerator RewardsUpdater()
        {
            while (true)
            {
                RefreshRewardsState(_weeklyRewardView, ref _isGetReward);
                
                yield return new WaitForSeconds(1);
            }
        }
        
        protected override void SubscribesButtons()
        {
            _weeklyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _weeklyRewardView.ResetButton.onClick.AddListener(ResetTimer);
            _weeklyRewardView.GOBackToDailyRewardButton.onClick.AddListener(GoToDailyRewardsWindow);
            _weeklyRewardView.GoBackToMainMenuButton.onClick.AddListener(GoBack);
        }

        protected override void OnDispose()
        {
            _weeklyRewardView.GetRewardButton.onClick.RemoveAllListeners();
            _weeklyRewardView.ResetButton.onClick.RemoveAllListeners();
            _weeklyRewardView.GOBackToDailyRewardButton.onClick.RemoveAllListeners();
            _weeklyRewardView.GoBackToMainMenuButton.onClick.RemoveAllListeners();
            
            base.OnDispose();
        }

        private void GoToDailyRewardsWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.DailyReward;
        }
        
        private void GoBack()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        protected override void ClaimReward()
        {
            if (!_isGetReward)
                return;
            var reward = _weeklyRewardView.Rewards[_weeklyRewardView.CurrentSlotingActive];

            if (reward.IsSingle)
            {
                AddReward(reward.FirstReward.RewardType, reward.FirstReward.CountCurrency);
            }
            else
            {
                AddReward(reward.FirstReward.RewardType, reward.FirstReward.CountCurrency);
                AddReward(reward.SecondReward.RewardType, reward.SecondReward.CountCurrency);
            }

            _weeklyRewardView.TimeGetReward = DateTime.UtcNow;
            _weeklyRewardView.CurrentSlotingActive =
                (_weeklyRewardView.CurrentSlotingActive + 1) % _weeklyRewardView.Rewards.Count;
            
            RefreshRewardsState(_weeklyRewardView, ref _isGetReward);
        }

        protected override void InitSlots()
        {
            for (int i = 0; i < _weeklyRewardView.Rewards.Count; i++)
            {
                var instanceSlot = Object.Instantiate(_weeklyRewardView.ContainerSlotWeeklyRewardView,
                    _weeklyRewardView.MountRootSlotsReward, false);
                _slots.Add(instanceSlot);
            }
        }
        
        

        protected override void RefreshUI()
        {
            _weeklyRewardView.GetRewardButton.interactable = _isGetReward;
            if (_isGetReward)
            {
                _weeklyRewardView.TimerNewReward.text = "Reward recieved";
                _weeklyRewardView.TimerSlider.value = _weeklyRewardView.TimeCooldown;
            }
            else
            {
                if (_weeklyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime = _weeklyRewardView.TimeGetReward.Value.AddSeconds(_weeklyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward =
                        $"{currentClaimCooldown.Days:D2} : {currentClaimCooldown.Hours:D2} : {currentClaimCooldown.Minutes:D2} : {currentClaimCooldown.Seconds:D2}";
                    _weeklyRewardView.TimerNewReward.text = timeGetReward;
                    _weeklyRewardView.TimerSlider.value = currentClaimCooldown.Seconds / _weeklyRewardView.TimeCooldown;
                }
            }

            for (int i = 0; i < _slots.Count ; i++)
            {
                _slots[i].SetData(_weeklyRewardView.Rewards[i].FirstReward, _weeklyRewardView.Rewards[i].SecondReward,
                    _weeklyRewardView.Rewards[i].IsSingle, i + 1, i == _weeklyRewardView.CurrentSlotingActive);
            }
        }
    }
}