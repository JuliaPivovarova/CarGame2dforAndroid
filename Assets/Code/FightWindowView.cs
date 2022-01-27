using Code.Rewards;
using Code.Tweens;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Code
{
    public class FightWindowView: MonoBehaviour
    {
        [SerializeField] private TMP_Text countMoneyText;
        [SerializeField] private TMP_Text countHealthText;
        [SerializeField] private TMP_Text countPowerText;
        [SerializeField] private TMP_Text countCrimeRateText;
        [SerializeField] private TMP_Text countPowerEnemyText;

        [SerializeField] private Button addMoneyButton;
        [SerializeField] private Button minusMoneyButton;
        [SerializeField] private Button addHealthButton;
        [SerializeField] private Button minusHealthButton;
        [SerializeField] private Button addPowerButton;
        [SerializeField] private Button minusPowerButton;
        [SerializeField] private Button addCrimeRateButton;
        [SerializeField] private Button minusCrimeRateButton;

        [SerializeField] private Button goWithoutFightButton;
        [SerializeField] private Button fightButton;
        [SerializeField] private CustomButton goToRewards;
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private int minCountCrimeRate = 0;
        [SerializeField] private int maxCountCrimeRate = 5;
        [SerializeField] private int countCrimeRateWithoutFight = 2;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountPowerPlayer;
        private int _allCountCrimeRatePlayer;

        private Canvas _canvasDailyRewardWindow;

        private Money _money;
        private Health _health;
        private Power _power;
        private CrimeRate _crimeRate;

        private Enemy _enemy;

        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = new Money();
            _money.Attach(_enemy);
            _health = new Health();
            _health.Attach(_enemy);
            _power = new Power();
            _power.Attach(_enemy);
            _crimeRate = new CrimeRate(minCountCrimeRate, maxCountCrimeRate);
            _crimeRate.Attach(_enemy);
            
            addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));
            
            addHealthButton.onClick.AddListener(() => ChangeHealth(true));
            minusHealthButton.onClick.AddListener(() => ChangeHealth(false));
            
            addPowerButton.onClick.AddListener(() => ChangePower(true));
            minusPowerButton.onClick.AddListener(() => ChangePower(false));
            
            addCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(true));
            minusCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(false));
            
            goWithoutFightButton.gameObject.SetActive(true);
            goWithoutFightButton.onClick.AddListener(GoWithoutFight);
            fightButton.onClick.AddListener(Fight);

            _canvasDailyRewardWindow = _dailyRewardView.gameObject.GetComponent<Canvas>();
            goToRewards.onClick.AddListener(GoToDailyRewardsWindow);
        }

        private void GoToDailyRewardsWindow()
        {
            _canvasDailyRewardWindow.sortingOrder = 1;
        }

        private void Fight()
        {
            Debug.Log((float)_allCountPowerPlayer >= _enemy.Power ? "Win" : "Lose");
        }

        private void OnDestroy()
        {
            addMoneyButton.onClick.RemoveAllListeners();
            minusMoneyButton.onClick.RemoveAllListeners();
            
            addHealthButton.onClick.RemoveAllListeners();
            minusHealthButton.onClick.RemoveAllListeners();
            
            addPowerButton.onClick.RemoveAllListeners();
            minusPowerButton.onClick.RemoveAllListeners();
            
            addCrimeRateButton.onClick.RemoveAllListeners();
            minusCrimeRateButton.onClick.RemoveAllListeners();
            
            goWithoutFightButton.onClick.RemoveAllListeners();
            fightButton.onClick.RemoveAllListeners();
            goToRewards.onClick.RemoveAllListeners();
            
            _money.Detach(_enemy);
            _health.Detach(_enemy);
            _power.Detach(_enemy);
            _crimeRate.Detach(_enemy);
        }

        private void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
            {
                _allCountMoneyPlayer++;
            }
            else
            {
                _allCountMoneyPlayer--;
            }

            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        }

        private void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
            {
                _allCountHealthPlayer++;
            }
            else
            {
                _allCountHealthPlayer--;
            }

            ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
        }
        
        private void ChangePower(bool isAddCount)
        {
            if (isAddCount)
            {
                _allCountPowerPlayer++;
            }
            else
            {
                _allCountPowerPlayer--;
            }

            ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
        }
        
        private void ChangeCrimeRate(bool isAddCount)
        {
            if (isAddCount)
            {
                if (_allCountCrimeRatePlayer < maxCountCrimeRate)
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
                if (_allCountCrimeRatePlayer > minCountCrimeRate)
                {
                    _allCountCrimeRatePlayer--;
                }
                else
                {
                    Debug.Log("Crime Rate can't be lower");
                }
            }

            if (_allCountCrimeRatePlayer <= countCrimeRateWithoutFight )
            {
                goWithoutFightButton.gameObject.SetActive(true);
            }
            else
            {
                goWithoutFightButton.gameObject.SetActive(false);
            }

            ChangeDataWindow(_allCountCrimeRatePlayer, DataType.CrimeRate);
        }
        
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Money:
                    countMoneyText.text = $"Player Money: {countChangeData}";
                    _money.CountMoney = countChangeData;
                    break;
                case DataType.Health:
                    countHealthText.text = $"Player Health: {countChangeData}";
                    _health.CountHealth = countChangeData;
                    break;
                case DataType.Power:
                    countPowerText.text = $"Player Power: {countChangeData}";
                    _power.CountPower = countChangeData;
                    break;
                case DataType.CrimeRate:
                    countCrimeRateText.text = $"Player Crime Rate: {countChangeData}";
                    _crimeRate.CountCrimeRate = countChangeData;
                    break;
            }

            countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";
        }

        private void GoWithoutFight()
        {
            Debug.Log("Your Crime Rate is low enough, you go away peacefully");
        }
    }
}