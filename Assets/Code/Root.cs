using System.Linq;
using Code.Ability;
using Code.Analitics;
using Code.Controllers;
using Code.Fight;
using Code.Items;
using Code.Model;
using Code.Rewards;
using UnityEngine;

namespace Code
{
    public class Root: MonoBehaviour
    {
        [SerializeField] private Transform placeForUI;
        [SerializeField] private float speed = 15f;
        [SerializeField] private UnityAdsTools unityAdsTools;
        [SerializeField] private ItemConfig[] itemConfigs;
        [SerializeField] private UpgrateItemConfigDataSource itemConfigDataSource;
        [SerializeField] private AbilityItemConfig[] _abilityItemConfigs;
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private WeeklyRewardView _weeklyRewardView;
        [SerializeField] private CurrencyView _currencyView;
        [SerializeField] private FightWindowView _fightWindowView;
        [SerializeField] private StartFightView _startFightView;

        private MainController _mainController;

        private void Awake()
        {
            var profilePlayer = new ProfilePlayer(speed, unityAdsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(placeForUI, profilePlayer, itemConfigs.ToList(), 
                itemConfigDataSource, _abilityItemConfigs.ToList(), _dailyRewardView, 
                _currencyView, _fightWindowView, _startFightView, _weeklyRewardView);
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}