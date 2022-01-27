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
            _iconFirstCurrency.gameObject.SetActive(!isSingle);
            _iconSecondCurrency.gameObject.SetActive(!isSingle);
            _countFirstReward.gameObject.SetActive(!isSingle);
            _countSecondReward.gameObject.SetActive(!isSingle);
            
            _iconSingleCurrency.gameObject.SetActive(isSingle);
            _countSingleReward.gameObject.SetActive(isSingle);
            if (isSingle)
            {
                _iconSingleCurrency.sprite = firstReward.IconCurrency;
                _countSingleReward.text = firstReward.CountCurrency.ToString();
            }
            else
            {
                _iconFirstCurrency.sprite = firstReward.IconCurrency;
                _countFirstReward.text = firstReward.CountCurrency.ToString();
                _iconSecondCurrency.sprite = secondReward.IconCurrency;
                _countSecondReward.text = secondReward.CountCurrency.ToString();
            }
            
            _textDays.text = $"Week {countWeek}";
            _backgroundSelect.gameObject.SetActive(isSelect);
        }
    }
}