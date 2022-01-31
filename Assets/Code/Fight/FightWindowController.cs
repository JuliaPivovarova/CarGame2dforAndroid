using Code.Controllers;
using Code.Model;
using UnityEngine;

namespace Code.Fight
{
    public class FightWindowController: BaseController
    {
        private FightWindowView _fightWindowView;
        private ProfilePlayer _profilePlayer;
        
        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimeRatePlayer;

        private Money _money;
        private Health _health;
        private Power _power;
        private CrimeRate _crimeRate;

        private Enemy _enemy;

        public FightWindowController(Transform placeForUI, ProfilePlayer profilePlayer, FightWindowView fightWindowView)
        {
            _profilePlayer = profilePlayer;
            _fightWindowView = Object.Instantiate(fightWindowView, placeForUI);
            AddGameObject(_fightWindowView.gameObject);
        }

        public void RefreshView()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = new Money();
            _money.Attach(_enemy);
            _health = new Health();
            _health.Attach(_enemy);
            _power = new Power();
            _power.Attach(_enemy);
            _crimeRate = new CrimeRate(_fightWindowView.MinCountCrimeRate, _fightWindowView.MaxCountCrimeRate);
            _crimeRate.Attach(_enemy);
            
            _fightWindowView.AddMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _fightWindowView.MinusMoneyButton.onClick.AddListener(() => ChangeMoney(false));
            
            _fightWindowView.AddHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _fightWindowView.MinusHealthButton.onClick.AddListener(() => ChangeHealth(false));
            
            _fightWindowView.AddPowerButton.onClick.AddListener(() => ChangePower(true));
            _fightWindowView.MinusPowerButton.onClick.AddListener(() => ChangePower(false));
            
            _fightWindowView.AddCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(true));
            _fightWindowView.MinusCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(false));
            
            _fightWindowView.GoWithoutFightButton.gameObject.SetActive(true);
            _fightWindowView.GoWithoutFightButton.onClick.AddListener(GoWithoutFight);
            _fightWindowView.FightButton.onClick.AddListener(Fight);

            _fightWindowView.LeaveFightButton.onClick.AddListener(CloseWindow);
        }

        protected override void OnDispose()
        {
            _fightWindowView.AddMoneyButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusMoneyButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddHealthButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusHealthButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddPowerButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusPowerButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddCrimeRateButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusCrimeRateButton.onClick.RemoveAllListeners();
            
            _fightWindowView.GoWithoutFightButton.onClick.RemoveAllListeners();
            _fightWindowView.FightButton.onClick.RemoveAllListeners();
            _fightWindowView.LeaveFightButton.onClick.RemoveAllListeners();
            
            _money.Detach(_enemy);
            _health.Detach(_enemy);
            _power.Detach(_enemy);
            _crimeRate.Detach(_enemy);
            
            base.OnDispose();
        }
        
        private void CloseWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void Fight()
        {
            Debug.Log((float)_allCountPowerPlayer >= _enemy.Power 
                ? "<color=#07FF00>Win!</color>" 
                : "<color=#FF0000>Lose!</color>");
        }
        
        private void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
                _allCountMoneyPlayer++;
            else
                _allCountMoneyPlayer--;

            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        }

        private void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
                _allCountHealthPlayer++;
            else
                _allCountHealthPlayer--;

            ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
        }
        
        private void ChangePower(bool isAddCount)
        {
            if (isAddCount)
                _allCountPowerPlayer++;
            else
                _allCountPowerPlayer--;

            ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
        }
        
        private void ChangeCrimeRate(bool isAddCount)
        {
            if (isAddCount)
            {
                if (_allCountCrimeRatePlayer < _fightWindowView.MaxCountCrimeRate)
                {
                    _allCountCrimeRatePlayer++;
                }
                else
                {
                    Debug.Log("Crime Rate can't be higher");
                }
            }
            else
            {
                if (_allCountCrimeRatePlayer > _fightWindowView.MinCountCrimeRate)
                {
                    _allCountCrimeRatePlayer--;
                }
                else
                {
                    Debug.Log("Crime Rate can't be lower");
                }
            }

            if (_allCountCrimeRatePlayer <= _fightWindowView.CountCrimeRateWithoutFight )
                _fightWindowView.GoWithoutFightButton.gameObject.SetActive(true);
            else
                _fightWindowView.GoWithoutFightButton.gameObject.SetActive(false);

            ChangeDataWindow(_allCountCrimeRatePlayer, DataType.CrimeRate);
        }
        
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Money:
                    _fightWindowView.CountMoneyText.text = $"Player Money: {countChangeData}";
                    _money.CountMoney = countChangeData;
                    break;
                case DataType.Health:
                    _fightWindowView.CountHealthText.text = $"Player Health: {countChangeData}";
                    _health.CountHealth = countChangeData;
                    break;
                case DataType.Power:
                    _fightWindowView.CountPowerText.text = $"Player Power: {countChangeData}";
                    _power.CountPower = countChangeData;
                    break;
                case DataType.CrimeRate:
                    _fightWindowView.CountCrimeRateText.text = $"Player Crime Rate: {countChangeData}";
                    _crimeRate.CountCrimeRate = countChangeData;
                    break;
            }

            _fightWindowView.CountPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";
        }

        private void GoWithoutFight()
        {
            Debug.Log("Your Crime Rate is low enough, you go away peacefully");
        }
    }
}