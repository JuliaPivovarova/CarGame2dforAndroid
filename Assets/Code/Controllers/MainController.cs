using System.Collections.Generic;
using Code.Ability;
using Code.Fight;
using Code.Items;
using Code.Model;
using Code.Rewards;
using UnityEngine;

namespace Code.Controllers
{
    public class MainController: BaseController
    {
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private DailyRewardController _dailyRewardController;
        private WeeklyRewardController _weeklyRewardController;
        private FightWindowController _fightWindowController;
        private StartFightController _startFightController;
        private readonly Transform _placeForUI;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemConfigs;
        private readonly DailyRewardView _dailyRewardView;
        private readonly WeeklyRewardView _weeklyRewardView;
        private readonly CurrencyView _currencyView;
        private readonly FightWindowView _fightWindowView;
        private readonly StartFightView _startFightView;
        private UpgrateItemConfigDataSource _itemConfigDataSource;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;

        public MainController(Transform placeForUI, ProfilePlayer profilePlayer, 
            List<ItemConfig> itemConfigs, UpgrateItemConfigDataSource itemConfigDataSource, 
            List<AbilityItemConfig> abilityItemConfigs, DailyRewardView dailyRewardView, CurrencyView currencyView, 
            FightWindowView fightWindowView, StartFightView startFightView, WeeklyRewardView weeklyRewardView)
        {
            _placeForUI = placeForUI;
            _profilePlayer = profilePlayer;
            _dailyRewardView = dailyRewardView;
            _currencyView = currencyView;
            _fightWindowView = fightWindowView;
            _startFightView = startFightView;
            _weeklyRewardView = weeklyRewardView;
            _itemConfigDataSource = itemConfigDataSource;
            _abilityItemConfigs = abilityItemConfigs;
            _itemConfigs = itemConfigs;
            OnChangeGameState(_profilePlayer.CurrentState.Value);
            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                    
                    _gameController?.Dispose();
                    _dailyRewardController?.Dispose();
                    _weeklyRewardController?.Dispose();
                    break;
                case GameState.Game:
                    _gameController = new GameController(_profilePlayer, _itemConfigs, _itemConfigDataSource, _abilityItemConfigs, _placeForUI);

                    _startFightController = new StartFightController(_placeForUI, _profilePlayer, _startFightView);
                    
                    _mainMenuController?.Dispose();
                    _fightWindowController?.Dispose();
                    break;
                case GameState.DailyReward:
                    _dailyRewardController =
                        new DailyRewardController(_placeForUI, _profilePlayer, _dailyRewardView, _currencyView);
                    _dailyRewardController.RefreshDailyView();
                    
                    _weeklyRewardController?.Dispose();
                    _mainMenuController?.Dispose();
                    break;
                case GameState.WeeklyReward:
                    _weeklyRewardController =
                        new WeeklyRewardController(_placeForUI, _profilePlayer, _weeklyRewardView, _currencyView);
                    _weeklyRewardController.RefreshWeeklyView();
                    
                    _dailyRewardController?.Dispose();
                    break;
                case GameState.Fight:
                    _fightWindowController = new FightWindowController(_placeForUI, _profilePlayer, _fightWindowView);
                    _fightWindowController.RefreshView();
                    
                    _gameController?.Dispose();
                    _startFightController?.Dispose();
                    break;
                default:
                    DisposeAllControllers();
                    break;
            }
        }

        private void DisposeAllControllers()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _fightWindowController?.Dispose();
            _dailyRewardController?.Dispose();
            _startFightController?.Dispose();
        }

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
            base.OnDispose();
        }
    }
}