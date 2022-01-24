using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Rewards
{
    public class ContainerSlotWeeklyRewardView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundSelect;
        [SerializeField] private Image _iconSingleCurrency;
        [SerializeField] private Image _iconFirstCurrency;
        [SerializeField] private Image _iconSecondCurrency;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private TMP_Text _countSingleReward;
        [SerializeField] private TMP_Text _countFirstReward;
        [SerializeField] private TMP_Text _countSecondReward;

        public void SetData(Reward firstReward, Reward secondReward, bool isSingle, int countWeek, bool isSelect)
        {
            if (isSingle)
            {
                _iconSingleCurrency.sprite = firstReward.IconCurrency;
                _countSingleReward.text = firstReward.CountCurrency.ToString();
                
                _iconFirstCurrency.gameObject.SetActive(false);
                _iconSecondCurrency.gameObject.SetActive(false);
                _countFirstReward.gameObject.SetActive(false);
                _countSecondReward.gameObject.SetActive(false);
                
                _iconSingleCurrency.gameObject.SetActive(true);
                _countSingleReward.gameObject.SetActive(true);
            }
            else
            {
                _iconFirstCurrency.sprite = firstReward.IconCurrency;
                _countFirstReward.text = firstReward.CountCurrency.ToString();
                _iconSecondCurrency.sprite = secondReward.IconCurrency;
                _countSecondReward.text = secondReward.CountCurrency.ToString();
                
                _iconFirstCurrency.gameObject.SetActive(true);
                _iconSecondCurrency.gameObject.SetActive(true);
                _countFirstReward.gameObject.SetActive(true);
                _countSecondReward.gameObject.SetActive(true);
                
                _iconSingleCurrency.gameObject.SetActive(false);
                _countSingleReward.gameObject.SetActive(false);
            }
            
            _textDays.text = $"Week {countWeek}";
            _backgroundSelect.gameObject.SetActive(isSelect);
        }
    }
}