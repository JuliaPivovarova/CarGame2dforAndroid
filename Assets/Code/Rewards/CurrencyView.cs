using TMPro;
using UnityEngine;

namespace Code.Rewards
{
    public class CurrencyView: MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        
        public static CurrencyView Instance { get; private set; }

        [SerializeField] private TMP_Text _currentCountWood;
        [SerializeField] private TMP_Text _currentCountDiamond;

        private void Awake()
        {
            Instance = this;
        }

        public void AddWood(int value)
        {
            SaveNewCountInPlayerPrefs(WoodKey, value);
            _currentCountWood.text = PlayerPrefs.GetInt(WoodKey, 0).ToString();
        }
        
        public void AddDiamond(int value)
        {
            SaveNewCountInPlayerPrefs(DiamondKey, value);
            _currentCountDiamond.text = PlayerPrefs.GetInt(DiamondKey, 0).ToString();
        }

        public void ResetCurrency(RewardType rewardType)
        {
            switch (rewardType)
            {
                case RewardType.Wood:
                    SaveNewCountInPlayerPrefs(WoodKey, 0);
                    _currentCountWood.text = PlayerPrefs.GetInt(WoodKey, 0).ToString();
                    break;
                case RewardType.Diamond:
                    SaveNewCountInPlayerPrefs(DiamondKey, 0);
                    _currentCountDiamond.text = PlayerPrefs.GetInt(DiamondKey, 0).ToString();
                    break;
            }
        }

        private void SaveNewCountInPlayerPrefs(string key, int value)
        {
            var currentCount = PlayerPrefs.GetInt(key, 0);
            var newCount =  currentCount + value;
            PlayerPrefs.SetInt(key, newCount);
        }
    }
}