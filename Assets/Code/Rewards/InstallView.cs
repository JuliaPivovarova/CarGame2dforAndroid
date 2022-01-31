using UnityEngine;

namespace Code.Rewards
{
    public class InstallView : MonoBehaviour
    {
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private WeeklyRewardView _weeklyRewardView;

        private DailyRewardController _dailyRewardController;
        private WeeklyRewardController _weeklyRewardController;

        private void Awake()
        {
            //_dailyRewardController =
            //    new DailyRewardController(_dailyRewardView, _weeklyRewardView.gameObject.transform);
            //_weeklyRewardController = new WeeklyRewardController(_weeklyRewardView);
        }

        private void Start()
        {
            //_dailyRewardController.RefreshDailyView();
            //_weeklyRewardController.RefreshWeeklyView();
        }
    }
}