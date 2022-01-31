using Code.Rewards;
using Code.Tweens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Fight
{
    public class FightWindowView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeRateText;
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;
        [SerializeField] private Button _addCrimeRateButton;
        [SerializeField] private Button _minusCrimeRateButton;

        [SerializeField] private Button _goWithoutFightButton;
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _leaveFightButton;
        [SerializeField] private int _minCountCrimeRate = 0;
        [SerializeField] private int _maxCountCrimeRate = 5;
        [SerializeField] private int _countCrimeRateWithoutFight = 2;

        public TMP_Text CountMoneyText => _countMoneyText;
        public TMP_Text CountHealthText => _countHealthText;
        public TMP_Text CountPowerText => _countPowerText;
        public TMP_Text CountCrimeRateText => _countCrimeRateText;
        public TMP_Text CountPowerEnemyText => _countPowerEnemyText;

        public Button AddMoneyButton => _addMoneyButton;
        public Button MinusMoneyButton => _minusMoneyButton;
        public Button AddHealthButton => _addHealthButton;
        public Button MinusHealthButton => _minusHealthButton;
        public Button AddPowerButton => _addPowerButton;
        public Button MinusPowerButton => _minusPowerButton;
        public Button AddCrimeRateButton => _addCrimeRateButton;
        public Button MinusCrimeRateButton => _minusCrimeRateButton;

        public Button GoWithoutFightButton => _goWithoutFightButton;
        public Button FightButton => _fightButton;
        public Button LeaveFightButton => _leaveFightButton;
        public int MinCountCrimeRate => _minCountCrimeRate;
        public int MaxCountCrimeRate => _maxCountCrimeRate;
        public int CountCrimeRateWithoutFight => _countCrimeRateWithoutFight;
    }
}